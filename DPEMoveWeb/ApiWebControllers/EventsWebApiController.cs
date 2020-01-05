using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveWeb.ApiWebControllers
{
    [Route("WebApi/Events/[action]")]
    [ApiController]
    public class EventsWebApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public EventsWebApiController(AppDbContext context, IMapper mapper, IEventService eventService)
        {
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
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


    }
}