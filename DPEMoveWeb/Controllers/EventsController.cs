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

namespace DPEMoveWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EventsController> _logger;
        private readonly IEventService _eventService;

        public EventsController(AppDbContext context, ILogger<EventsController> logger, IEventService eventService)
        {
            _context = context;
            _logger = logger;
            _eventService = eventService;
        }



        // GET: Events
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Event
                .Include(@a => @a.Address)
                .Include(@a => @a.EventLevel)
                .Include(@a => @a.EventType)
                .Include(a => a.EventUploadedFile)
                    .ThenInclude(b => b.UploadedFile)
                ;

            return View(await appDbContext.ToListAsync());
        }

        // GET: Events/Details/5
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

            ViewBag.MEventFacilitiesTopic = _context.MEventFacilitiesTopic.ToList();
            ViewBag.MEventLevel = _context.MEventLevel.ToList();
            ViewBag.Address = _context.Event.Where(a => a.EventId == id).FirstOrDefault()?.Address;

            return View(eventVM);
        }

        [HttpPost]
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


            _eventService.UpdateEvent(model);

            // EventFacilities
            var eventFacilities = HttpContext.Session.Get<List<EventFacilities>>("Session_EventFacilities_" + model.EventId);
            if (eventFacilities != null)
            {
                var ddEventFacilities = _context.EventFacilities.Where(a => a.EventId == model.EventId);
                foreach (var k in ddEventFacilities)
                {
                    _context.Entry(k).State = EntityState.Deleted;
                    _context.SaveChanges();
                }

                foreach (var k in eventFacilities)
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

            _context.Entry(addressFromDB).State = addressFromDB.AddressId == 0 ? EntityState.Added : EntityState.Modified;
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
