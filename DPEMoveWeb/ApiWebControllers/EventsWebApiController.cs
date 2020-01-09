﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;
using DPEMoveDAL.ViewModels;
using DPEMoveWeb.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DPEMoveWeb.ApiWebControllers
{
    [Route("WebApi/Events/[action]")]
    [ApiController]
    public class EventsWebApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;
        private readonly ILogger<EventsWebApiController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EventsWebApiController(AppDbContext context, IMapper mapper, IEventService eventService, ILogger<EventsWebApiController> logger, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        //[Authorize]
        public IEnumerable<EventViewModel> GetAllEvent()
        {
            var q = _eventService.GetEvent(new EventViewModel());

            return q;
        }

        [HttpPost]
        //[Authorize]
        public IEnumerable<EventDbQuery> GetEvent([FromBody] EventRequestViewModel model)
        {
            var q = _eventService.GetEvent2(model);

            return q;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetEventDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _eventService.GetEventDetails(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        //[Authorize]
        public IActionResult GetMEventFacilitiesTopic()
        {
            var q = _context.MEventFacilitiesTopic.ToList();

            return Ok(q);
        }

        [HttpPost]
        [Authorize]
        public List<EventFacilities> GetEventFacilitiesFromSession(EventFacilities model)
        {
            if (HttpContext.Session.Get<List<EventFacilities>>("Session_EventFacilities_" + model.EventId + "_" + model.MEventFacilitiesTopicId) == null)
            {
                var data = _context.EventFacilities.Where(a => a.EventId == model.EventId && a.MEventFacilitiesTopicId == model.MEventFacilitiesTopicId).ToList();
                HttpContext.Session.Set<List<EventFacilities>>("Session_EventFacilities_" + model.EventId + "_" + model.MEventFacilitiesTopicId, data);
            }
            
            var q = HttpContext.Session.Get<List<EventFacilities>>("Session_EventFacilities_" + model.EventId + "_" + model.MEventFacilitiesTopicId);
            
            return q;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddEventFacilitiesToSession(EventFacilities model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = GetEventFacilitiesFromSession(model);

            var q = new EventFacilities
            {
                EventFacilitiesId = new Random().Next(-100000, -1),
                EventId = model.EventId,
                MEventFacilitiesTopicId = model.MEventFacilitiesTopicId,
                EventFacilitiesName = model.EventFacilitiesName,
                FacilitiesAmount = model.FacilitiesAmount,
                FacilitiesUnit = model.FacilitiesUnit,
                Status = model.Status,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now
            };

            data.Add(q);

            HttpContext.Session.Set<List<EventFacilities>>("Session_EventFacilities_" + model.EventId + "_" + model.MEventFacilitiesTopicId, data);

            return Ok();
        }


        [HttpPost]
        [Authorize]
        public IActionResult DeleteEventFacilitiesFromSession(EventFacilities model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = GetEventFacilitiesFromSession(model);

            var q = data.Where(a => a.EventFacilitiesId == model.EventFacilitiesId).FirstOrDefault();

            data.Remove(q);

            HttpContext.Session.Set<List<EventFacilities>>("Session_EventFacilities_" + model.EventId + "_" + model.MEventFacilitiesTopicId, data);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public List<UploadedFile> GetUploadedFileSession(UploadedFileViewModel model)
        {
            _logger.LogDebug("model.FileName={0}", model.EventId);

            string sessionName = "Session_UploadedFileId_" + model.EventId;
            if (HttpContext.Session.Get<List<UploadedFile>>(sessionName) == null)
            {
                var data = _context.EventUploadedFile.Where(a => a.EventId == model.EventId).Select(b => b.UploadedFile).ToList();
                HttpContext.Session.Set<List<UploadedFile>>(sessionName, data);
            }

            var q = HttpContext.Session.Get<List<UploadedFile>>(sessionName);

            return q;
        }

        [HttpPost]
        [Authorize]
        public List<UploadedFile> GetUploadedFile(UploadedFileViewModel model)
        {
            _logger.LogDebug("model.FileName={0}", model.EventId);
            
            return _context.EventUploadedFile.Where(a => a.EventId == model.EventId).Select(b => b.UploadedFile).ToList();
            ;
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddUploadedFileToDatabase(UploadedFileViewModel model)
        {
            _logger.LogDebug("model.FileName={0}", model.FileName);

            var basePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var q = new UploadedFile
            {
                UploadedFileCode = "n/a",
                FileType = Path.GetExtension(model.FileName),
                FileUrl = basePath + "/Uploads/" + model.FileName,
                FileName = model.FileName,
                CreatedBy = 0,
                CreatedDate = DateTime.Now,                
            };

            _context.Entry(q).State = EntityState.Added;
            _context.SaveChanges();

            var eup = new EventUploadedFile
            {
                EventId = model.EventId,
                UploadedFileId = q.UploadedFileId,
                EventUploadedFileCode = "n/a",
                Order = _context.EventUploadedFile.Where(a => a.EventId == model.EventId).Count()+1,
                CreatedBy = 0,
                CreatedDate = DateTime.Now,
                Status = 1
            };

            _context.Entry(eup).State = EntityState.Added;
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteUploadedFile(UploadedFileViewModel model)
        {
            _logger.LogDebug("model.EventId={0}, model.UploadedFileId={1}", model.EventId, model.UploadedFileId);

            var dd_1 = _context.EventUploadedFile.Where(a => a.UploadedFileId == model.UploadedFileId);
            foreach (var x in dd_1)
            {
                _context.Entry(x).State = EntityState.Deleted;
                _context.SaveChanges();
            }

            var dd_2 = _context.UploadedFile.Where(a => a.UploadedFileId == model.UploadedFileId);
            foreach (var x in dd_2)
            {
                _context.Entry(x).State = EntityState.Deleted;
                _context.SaveChanges();
            }

            return Ok();
        }

    }
}