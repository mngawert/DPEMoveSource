﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DPEMoveDAL.Models;
using Microsoft.AspNetCore.Authorization;
using DPEMoveDAL.ViewModels;
using Microsoft.Extensions.Logging;
using DPEMoveDAL.Services;
using DPEMoveWeb.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DPEMoveWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EventsController> _logger;
        private readonly IEventService _eventService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        public EventsController(AppDbContext context, ILogger<EventsController> logger, IEventService eventService, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _eventService = eventService;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        private async Task<int> GetLoginAppUserId()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    _logger.LogDebug("user.AppUserId={0}", user.AppUserId);

                    return user.AppUserId;
                }
            }
            return -1;
        }


        // GET: Events
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Event.Where(a => a.Status == 1)
                .Include(@a => @a.Address)
                .Include(@a => @a.EventLevel)
                .Include(@a => @a.EventType)
                .Include(a => a.EventUploadedFile)
                    .ThenInclude(b => b.UploadedFile)
                ;

            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.routeId = id;

            if (id == null)
            {
                return NotFound();
            }

            var eventVM = await _eventService.GetEventDetails2(id.Value);

            if (eventVM == null)
            {
                return NotFound();
            }

            ViewBag.AppUserId = await GetLoginAppUserId();
            ViewBag.Address = _context.Event.Where(a => a.EventId == id).FirstOrDefault()?.Address;

            ViewBag.CommentCount = _context.Comment.Where(a => a.EventCode == eventVM.EventCode).Count();

            ViewBag.VoteAvg = 0;
            var votes = _context.Vote.Where(a => a.EventCode == eventVM.EventCode).ToList();
            if (votes.Count != 0)
            {
                ViewBag.VoteAvg = votes.Average(b => b.VoteValue);
            }

            //ViewBag.MEventFacilitiesTopic = _context.MEventFacilitiesTopic.ToList();
            //ViewBag.MEventLevel = _context.MEventLevel.ToList();
            //ViewBag.MSport = _context.MSport.ToList();
            //ViewBag.MObjectivePerson = _context.MObjectivePerson.ToList();

            return View(eventVM);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.routeId = id;

            if (id == null)
            {
                return NotFound();
            }

            var eventVM = await _eventService.GetEventDetails2(id.Value);
            
            if (eventVM == null)
            {
                return NotFound();
            }

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    _logger.LogDebug("user.AppUserId={0}", user.AppUserId);

                    if (user.AppUserId != eventVM.CreatedBy)
                    {
                        ViewBag.ErrorTitle = "Permission Denied";
                        ViewBag.Message = "This event does not create by you!";
                        return View("Error");
                    }
                }
            }

            ViewBag.MEventFacilitiesTopic = _context.MEventFacilitiesTopic.ToList();
            ViewBag.MEventLevel = _context.MEventLevel.ToList();
            ViewBag.Address = _context.Event.Where(a => a.EventId == id).FirstOrDefault()?.Address;
            ViewBag.MSport = _context.MSport.ToList();
            ViewBag.MObjectivePerson = _context.MObjectivePerson.ToList();

            return View(eventVM);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromForm] EventViewModel2 model)
        {
            int appUserId = await GetLoginAppUserId();
            if (ModelState.IsValid)
            {
                var q = new Event
                {
                    EventName = model.EventName,
                    EventCode = "EVT" + DateTime.Now.ToString("yyyyMMdd") + (_context.Event.Max(a => a.EventId) + 1).ToString().PadLeft(4, '0'),
                    EventDescription = "...", //model.EventDescription,
                    EventStartTimestamp = model.EventStartTimestamp,
                    EventFinishTimestamp = model.EventFinishTimestamp,
                    ReadCount = 0,
                    EventLevelId = 1,
                    Status = 1,
                    CreatedBy = appUserId, //model.CreatedBy,
                    CreatedDate = DateTime.Now,
                };

                _context.Entry(q).State = EntityState.Added;
                _context.SaveChanges();

                return RedirectToAction("Edit", new { id = q.EventId });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditEvent([FromForm] EventViewModel2 model)
        {
            _logger.LogDebug("model.EventId={0}", model.EventId);
            _logger.LogDebug("model.EventName={0}", model.EventName);
            _logger.LogDebug("model.EventLevelId={0}", model.EventLevelId);
            _logger.LogDebug("model.MEventObjectiveIds.Length={0}", model.MEventObjectiveIds?.Length);
            foreach (var x in model.MEventObjectiveIds ?? new int[] { })
            {
                _logger.LogDebug("MEventObjectiveId={0}", x);
            }

            var q_ev = _context.Event.Where(a => a.EventId == model.EventId).FirstOrDefault();
            if (q_ev != null)
            {
                _eventService.UpdateEvent(model);

                /* EVENT_FACILITIES */
                foreach (var topic in _context.MEventFacilitiesTopic)
                {
                    string sessionName = "Session_EventFacilities_" + model.EventId + "_" + topic.EventFacilitiesTopicId;
                    var eventFacilitiesSession = HttpContext.Session.Get<List<EventFacilities>>(sessionName);
                    if (eventFacilitiesSession != null)
                    {
                        var existingEventFacilities = _context.EventFacilities.Where(a => a.EventId == model.EventId && a.MEventFacilitiesTopicId == topic.EventFacilitiesTopicId);
                        foreach (var k in existingEventFacilities)
                        {
                            _context.Entry(k).State = EntityState.Deleted;
                            _context.SaveChanges();
                        }

                        foreach (var k in eventFacilitiesSession)
                        {
                            var qq = new EventFacilities
                            {
                                EventId = k.EventId,
                                MEventFacilitiesTopicId = k.MEventFacilitiesTopicId,
                                EventFacilitiesName = k.EventFacilitiesName,
                                FacilitiesAmount = k.FacilitiesAmount,
                                FacilitiesUnit = k.FacilitiesUnit,
                                Status = k.Status,
                                CreatedBy = k.CreatedBy,
                                CreatedDate = k.CreatedDate
                            };

                            _context.Entry(qq).State = EntityState.Added;
                        }
                        _context.SaveChanges();

                        // Clear session after Saved.
                        HttpContext.Session.Set<List<EventFacilities>>(sessionName, null);
                    }
                }

                /* EVENT_NEARBY */
                foreach (var topic in _context.EventNearby)
                {
                    string sessionName = "Session_EventNearby_" + model.EventId;
                    var eventNearbySession = HttpContext.Session.Get<List<EventNearby>>(sessionName);
                    if (eventNearbySession != null)
                    {
                        var existingEventNearby = _context.EventNearby.Where(a => a.EventId == model.EventId);
                        foreach (var k in existingEventNearby)
                        {
                            _context.Entry(k).State = EntityState.Deleted;
                            _context.SaveChanges();
                        }

                        foreach (var k in eventNearbySession)
                        {
                            var qq = new EventNearby
                            {
                                EventId = k.EventId,
                                NearbyName = k.NearbyName,
                                Distance = k.Distance,
                                DistanceUnit = k.DistanceUnit,
                                CreatedBy = k.CreatedBy,
                                CreatedDate = k.CreatedDate
                            };

                            _context.Entry(qq).State = EntityState.Added;
                        }
                        _context.SaveChanges();

                        // Clear session after Saved.
                        HttpContext.Session.Set<List<EventNearby>>(sessionName, null);
                    }
                }



                /* ADDRESS */
                _logger.LogDebug("model.EventId={0}", model.EventId);

                var addressFromDB = _context.Event.Include(b => b.Address).Where(a => a.EventId == model.EventId).FirstOrDefault()?.Address;
                _logger.LogDebug("addressFromDB={0}", addressFromDB);
                if (addressFromDB == null)
                {
                    addressFromDB = new Address();
                }
                _logger.LogDebug("addressFromDB.AddressId={0}", addressFromDB.AddressId);

                addressFromDB.BuildingName = model.BuildingName;
                addressFromDB.No = model.No;
                addressFromDB.Moo = model.Moo;
                addressFromDB.Soi = model.Soi;
                addressFromDB.Road = model.Road;
                addressFromDB.ProvinceCode = model.ProvinceCode;
                addressFromDB.AmphurCode = model.AmphurCode;
                addressFromDB.TambonCode = model.TambonCode;
                addressFromDB.Postcode = model.Postcode;
                addressFromDB.Latitude = model.Latitude;
                addressFromDB.Longitude = model.Longitude;

                _context.Entry(addressFromDB).State = addressFromDB.AddressId == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                _logger.LogDebug("addressFromDB.AddressId={0}", addressFromDB.AddressId);
                q_ev.AddressId = addressFromDB.AddressId;
                _context.SaveChanges();

                /* EVENT_SPORT*/
                var esDb = _context.EventSport.Where(a => a.EventId == model.EventId);
                foreach (var x in esDb)
                {
                    _context.Remove(x).State = EntityState.Deleted;
                }
                _context.SaveChanges();

                foreach (var id in model.SportIds ?? new int[] { })
                {
                    var obj = new EventSport
                    {
                        EventId = model.EventId,
                        SportId = id,
                        SportEtc = model.SportEtc,
                        Status = 1,
                        CreatedBy = 0,
                        CreatedDate = DateTime.Now
                    };

                    _context.Entry(obj).State = EntityState.Added;
                }
                _context.SaveChanges();


                /* EVENT_OBJECTIVE_PERSON */
                foreach (var x in _context.EventObjectivePerson.Where(a => a.EventId == model.EventId))
                {
                    _context.Remove(x).State = EntityState.Deleted;
                }
                _context.SaveChanges();

                foreach (var id in model.ObjectivePersonIds ?? new int[] { })
                {
                    var obj = new EventObjectivePerson
                    {
                        EventId = model.EventId,
                        ObjectivePersonId = id,
                        ObjectivePersonEtc = model.ObjectivePersonEtc,
                        Status = 1,
                        CreatedBy = 0,
                        CreatedDate = DateTime.Now
                    };

                    _context.Entry(obj).State = EntityState.Added;
                }
                _context.SaveChanges();

            }

            return RedirectToAction("Details", new { id = model.EventId});
        }


        [HttpPost]
        public IActionResult DeleteEvent([FromForm] EventViewModel2 model)
        {
            var q_ev = _context.Event.Where(a => a.EventId == model.EventId).FirstOrDefault();
            if (q_ev != null)
            {
                q_ev.Status = 2;

                _context.Entry(q_ev).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(IFormFile file)
        {
            if (file?.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString().Substring(0, 4) + "_" + System.IO.Path.GetFileName(file.FileName);

                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
                var filePath = Path.Combine(uploads, fileName);

                using (var localFile = new FileStream(filePath, FileMode.Create))
                {
                    using (var uploadedFile = file.OpenReadStream())
                    {
                        uploadedFile.CopyTo(localFile);
                    }
                }
                return Json(fileName);
            }
            return BadRequest();
        }

        [HttpPost]
        public ActionResult AddUploadedFileToDatabase(UploadedFileViewModel model)
        {
            _logger.LogDebug("model.FileName={0}", model.FileName);

            var q = new UploadedFile
            {
                UploadedFileCode = "n/a",
                FileType = "n/1", //Path.GetExtension(model.FileName),
                FileUrl =  "n/q", //Path.Combine(_hostingEnvironment.WebRootPath, model.FileName),
                FileName = model.FileName,
                CreatedBy = 0,
                CreatedDate = DateTime.Now,
            };

            _context.Entry(q).State = EntityState.Added;
            _context.SaveChanges();

            return Ok();
        }

    }
}
