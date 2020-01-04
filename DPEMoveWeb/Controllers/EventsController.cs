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
            if (id == null)
            {
                return NotFound();
            }

            var eventVM = await _eventService.GetEventDetails2(id.Value);
            
            if (eventVM == null)
            {
                return NotFound();
            }

            return View(eventVM);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId");
            ViewData["EventLevelId"] = new SelectList(_context.MEventLevel, "EventLevelId", "EventLevelCode");
            ViewData["EventTypeId"] = new SelectList(_context.MEventType, "EventTypeId", "EventTypeCode");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventCode,EventName,EventShortDescription,EventDescription,EventStartTimestamp,EventFinishTimestamp,AddressId,StadiumCode,PublishUrl,ResponsiblePerson,ResponsiblePersonCode,ReadCount,EventLevelId,EventLevelEtc,EventObjectivePersonId,Status,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,Budget,Budgetused,EventTypeId,ProjectSelect,ProjectCode,ResponsiblePersonType,ContactPersonName,ContactPersonEmail,ContactPersonMobile,ContactPersonFax,ContactPersonLineid")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", @event.AddressId);
            ViewData["EventLevelId"] = new SelectList(_context.MEventLevel, "EventLevelId", "EventLevelCode", @event.EventLevelId);
            ViewData["EventTypeId"] = new SelectList(_context.MEventType, "EventTypeId", "EventTypeCode", @event.EventTypeId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", @event.AddressId);
            ViewData["EventLevelId"] = new SelectList(_context.MEventLevel, "EventLevelId", "EventLevelCode", @event.EventLevelId);
            ViewData["EventTypeId"] = new SelectList(_context.MEventType, "EventTypeId", "EventTypeCode", @event.EventTypeId);
            return View(@event);
        }

        [HttpPost]
        public IActionResult EditEvent([FromForm] EventViewModel2 model)
        {
            _logger.LogDebug("model.EventId={0}", model.EventId);
            _logger.LogDebug("model.EventName={0}", model.EventName);
            //_logger.LogDebug("model.MEventObjective.Length={0}", model.MEventObjective.Length);

            //foreach (var x in model.MEventObjective)
            //{
            //    _logger.LogDebug("x.MEventObjectiveId={0}", x.MEventObjectiveId);
            //    _logger.LogDebug("x.EventObjectiveName={0}", x.EventObjectiveName);
            //}

            _logger.LogDebug("model.MEventObjectiveIds.Length={0}", model.MEventObjectiveIds.Length);
            foreach (var x in model.MEventObjectiveIds)
            {
                _logger.LogDebug("MEventObjectiveId={0}", x);
            }

            _eventService.UpdateEvent(model);

            return RedirectToAction("Index");
        }


        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventCode,EventName,EventShortDescription,EventDescription,EventStartTimestamp,EventFinishTimestamp,AddressId,StadiumCode,PublishUrl,ResponsiblePerson,ResponsiblePersonCode,ReadCount,EventLevelId,EventLevelEtc,EventObjectivePersonId,Status,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,Budget,Budgetused,EventTypeId,ProjectSelect,ProjectCode,ResponsiblePersonType,ContactPersonName,ContactPersonEmail,ContactPersonMobile,ContactPersonFax,ContactPersonLineid")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "AddressId", @event.AddressId);
            ViewData["EventLevelId"] = new SelectList(_context.MEventLevel, "EventLevelId", "EventLevelCode", @event.EventLevelId);
            ViewData["EventTypeId"] = new SelectList(_context.MEventType, "EventTypeId", "EventTypeCode", @event.EventTypeId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(@a => @a.Address)
                .Include(@a => @a.EventLevel)
                .Include(@a => @a.EventType)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }
    }
}
