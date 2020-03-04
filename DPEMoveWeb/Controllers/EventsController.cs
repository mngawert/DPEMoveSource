using System;
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
            ViewBag.AppUserId = await GetLoginAppUserId();
            return View();
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

            _eventService.AddViewCount(eventVM.EventCode);

            ViewBag.AppUserId = await GetLoginAppUserId();
            ViewBag.Address = _context.Event.Where(a => a.EventId == id).FirstOrDefault()?.Address;

            ViewBag.CommentCount = _context.Comment.Where(a => a.EventCode == eventVM.EventCode).Count();

            ViewBag.VoteAvg = 0;
            var votes = _context.Vote.Where(a => a.EventCode == eventVM.EventCode).ToList();
            if (votes.Count != 0)
            {
                ViewBag.VoteAvg = Math.Truncate(votes.Average(b => b.VoteValue)*10)/10;
            }

            ViewBag.MVoteType = _context.MVoteType.Where(a => a.VoteOf == "1" && a.Status == 1).ToList();

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
                //return NotFound();
                
                ViewBag.ErrorTitle = "Not Found";
                //ViewBag.ErrorMessage = "There is no this event in the system!";
                return View("Error");                
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
                        //ViewBag.ErrorMessage = "This event does not create by you!";
                        return View("Error");
                    }
                }
            }

            ViewBag.MEventFacilitiesTopic = _context.MEventFacilitiesTopic.Where(a => a.Status == 1).ToList();
            ViewBag.MFee = _context.MFee.Where(a => a.Status == 1).ToList();
            ViewBag.MParticipant = _context.MParticipant.Where(a => a.Status == 1).ToList();
            ViewBag.MEventLevel = _context.MEventLevel.Where(a => a.Status == 1).ToList();
            ViewBag.Address = _context.Event.Where(a => a.EventId == id).FirstOrDefault()?.Address;
            ViewBag.MSport = _context.MSport.Where(a => a.Status == 1).ToList();
            ViewBag.MObjectivePerson = _context.MObjectivePerson.Where(a => a.Status == 1).OrderBy(a => a.ObjectivePersonId).ToList();
            ViewBag.MEventObjective = _context.MEventObjective.Where(a => a.Status == 1).OrderBy(a => a.MEventObjectiveId).ToList();
            ViewBag.EventActivityType = _context.EventActivityType.Where(a => a.EventId == id).ToList();

            return View(eventVM);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromForm] EventViewModel2 model)
        {
            int appUserId = await GetLoginAppUserId();
            if (ModelState.IsValid)
            {
                // Creating default address.
                var addr = new Address
                {
                    CreatedBy = appUserId,
                    CreatedDate = DateTime.Now
                };
                _context.Entry(addr).State = EntityState.Added;
                _context.SaveChanges();

                var q = new Event
                {
                    EventName = model.EventName,
                    EventCode = "EVT" + DateTime.Now.ToString("yyyyMMdd") + (_context.Event.Max(a => a.EventId) + 1).ToString().PadLeft(4, '0'),
                    EventDescription = "...", //model.EventDescription,
                    EventStartTimestamp = model.EventStartTimestamp,
                    EventFinishTimestamp = model.EventFinishTimestamp,
                    ReadCount = 0,
                    EventLevelId = 1,
                    IsFree = "1",
                    Status = -1,
                    CreatedBy = appUserId, //model.CreatedBy,
                    CreatedDate = DateTime.Now,
                    AddressId = addr.AddressId
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

                /* EVENT_Fee */
                foreach (var topic in _context.MFee)
                {
                    string sessionName = "Session_EventFee_" + model.EventId + "_" + topic.FeeId;
                    var eventFeeSession = HttpContext.Session.Get<List<EventFee>>(sessionName);
                    if (eventFeeSession != null)
                    {
                        var existingEventFee = _context.EventFee.Where(a => a.EventId == model.EventId && a.FeeId == topic.FeeId);
                        foreach (var k in existingEventFee)
                        {
                            _context.Entry(k).State = EntityState.Deleted;
                            _context.SaveChanges();
                        }

                        foreach (var k in eventFeeSession)
                        {
                            var qq = new EventFee
                            {
                                EventId = k.EventId,
                                FeeId = k.FeeId,
                                EventFeeName = k.EventFeeName,
                                EventFeeAmount = k.EventFeeAmount,
                                EventFeeUnit = k.EventFeeUnit,
                                Status = k.Status,
                                CreatedBy = k.CreatedBy,
                                CreatedDate = k.CreatedDate
                            };

                            _context.Entry(qq).State = EntityState.Added;
                        }
                        _context.SaveChanges();

                        // Clear session after Saved.
                        HttpContext.Session.Set<List<EventFee>>(sessionName, null);
                    }
                }

                /* EVENT_PARTICIPANT */
                foreach (var topic in _context.MParticipant)
                {
                    string sessionName = "Session_EventParticipant_" + model.EventId + "_" + topic.ParticipantId;
                    var eventParticipantSession = HttpContext.Session.Get<List<EventParticipant>>(sessionName);
                    if (eventParticipantSession != null)
                    {
                        var existingParticipantFee = _context.EventParticipant.Where(a => a.EventId == model.EventId && a.ParticipantId == topic.ParticipantId);
                        foreach (var k in existingParticipantFee)
                        {
                            _context.Entry(k).State = EntityState.Deleted;
                            _context.SaveChanges();
                        }

                        foreach (var k in eventParticipantSession)
                        {
                            var qq = new EventParticipant
                            {
                                EventId = k.EventId,
                                ParticipantId = k.ParticipantId,
                                EventParticipantName = k.EventParticipantName,
                                EventParticipantAmount = k.EventParticipantAmount,
                                EventParticipantUnit = k.EventParticipantUnit,
                                Status = k.Status,
                                CreatedBy = k.CreatedBy,
                                CreatedDate = k.CreatedDate
                            };

                            _context.Entry(qq).State = EntityState.Added;
                        }
                        _context.SaveChanges();

                        // Clear session after Saved.
                        HttpContext.Session.Set<List<EventParticipant>>(sessionName, null);
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

                /* EVENT_ACTIVITYTYPE */
                foreach (var x in _context.EventActivityType.Where(a => a.EventId == model.EventId))
                {
                    _context.Remove(x).State = EntityState.Deleted;
                }
                _context.SaveChanges();

                foreach (var id in model.ActTypeIds ?? new int[] { })
                {
                    var obj = new EventActivityType
                    {
                        EventId = model.EventId,
                        ActTypeId = id,
                        ActTypeEtc = null
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
