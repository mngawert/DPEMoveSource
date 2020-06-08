using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;
using DPEMoveDAL.ViewModels;
using DPEMoveWeb.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly ICommentService _commentService;
        private readonly ILogger<EventsWebApiController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventsWebApiController(AppDbContext context, IMapper mapper, IEventService eventService, ICommentService commentService, ILogger<EventsWebApiController> logger, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
            _commentService = commentService;
            _logger = logger;
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

        private async Task<string> GetLoginName()
        {
            string value = "";
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    _logger.LogDebug("user.Name={0}", user.Name);

                    value = user.Name;
                    if (string.IsNullOrEmpty(value))
                    {
                        value = user.UserName;
                    }
                }
            }
            return value;
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
        public IActionResult GetEvent([FromBody] EventRequestViewModel model)
        {
            var q = _eventService.GetEvent2(model);

            return Ok(q);
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

        [HttpGet("{id}")]
        public IActionResult GetCommentsByEventId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = _context.Event.Where(a => a.EventId == id).FirstOrDefault();

            if (@event == null)
            {
                return NotFound();
            }

            var input = new CommentViewModel2
            {
                CommentOf = "1",
                EventOrStadiumCode = @event.EventCode                
            };

            var q = _commentService.GetComment(input);

            var comments = _mapper.Map<List<CommentViewModel3>>(q);
            foreach (var x in comments)
            {
                x.CreatedDateTH = x.CreatedDate.ToString();
            }

            return Ok(comments);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            var input = new CommentViewModel
            {
                CommentOf = model.CommentOf,
                EventOrStadiumCode = model.EventOrStadiumCode,
                Comment1 = model.Comment1,
                UserCode = await GetLoginName(),
                CreatedBy = await GetLoginAppUserId(),
                CreatedDate = DateTime.Now
            };

            var q = _commentService.AddComment(input);

            if (q.CommentId == 0)
                return BadRequest();

            return Ok();
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
            string SessionName = "Session_EventFacilities_" + model.EventId + "_" + model.MEventFacilitiesTopicId;

            if (HttpContext.Session.Get<List<EventFacilities>>(SessionName) == null)
            {
                var data = _context.EventFacilities.Where(a => a.EventId == model.EventId && a.MEventFacilitiesTopicId == model.MEventFacilitiesTopicId).ToList();
                HttpContext.Session.Set<List<EventFacilities>>(SessionName, data);
            }
            
            var q = HttpContext.Session.Get<List<EventFacilities>>(SessionName);
            
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
                Status = 1,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now
            };

            data.Add(q);

            string sessionName = "Session_EventFacilities_" + model.EventId + "_" + model.MEventFacilitiesTopicId;
            HttpContext.Session.Set<List<EventFacilities>>(sessionName, data);

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

            string sessionName = "Session_EventFacilities_" + model.EventId + "_" + model.MEventFacilitiesTopicId;
            HttpContext.Session.Set<List<EventFacilities>>(sessionName, data);

            return Ok();
        }

        //[Authorize]
        public IActionResult GetMFee()
        {
            var q = _context.MFee.ToList();

            return Ok(q);
        }

        [HttpPost]
        //[Authorize]
        public List<EventFee> GetEventFee(EventFee model)
        {
            var q = _context.EventFee.Where(a => a.EventId == model.EventId).ToList();

            return q;
        }

        [HttpPost]
        [Authorize]
        public List<EventFee> GetEventFeeFromSession(EventFee model)
        {
            string SessionName = "Session_EventFee_" + model.EventId + "_" + model.FeeId;

            if (HttpContext.Session.Get<List<EventFee>>(SessionName) == null)
            {
                var data = _context.EventFee.Where(a => a.EventId == model.EventId && a.FeeId == model.FeeId).ToList();
                HttpContext.Session.Set<List<EventFee>>(SessionName, data);
            }

            var q = HttpContext.Session.Get<List<EventFee>>(SessionName);

            return q;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddEventFeeToSession(EventFee model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = GetEventFeeFromSession(model);

            var q = new EventFee
            {
                EventFeeId = new Random().Next(-100000, -1),
                EventId = model.EventId,
                FeeId = model.FeeId,
                EventFeeName = model.EventFeeName,
                EventFeeAmount = model.EventFeeAmount,
                EventFeeUnit = model.EventFeeUnit,
                Status = 1,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now
            };

            data.Add(q);

            string sessionName = "Session_EventFee_" + model.EventId + "_" + model.FeeId;
            HttpContext.Session.Set<List<EventFee>>(sessionName, data);

            return Ok();
        }


        [HttpPost]
        [Authorize]
        public IActionResult DeleteEventFeeFromSession(EventFee model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = GetEventFeeFromSession(model);

            var q = data.Where(a => a.EventFeeId == model.EventFeeId).FirstOrDefault();

            data.Remove(q);

            string sessionName = "Session_EventFee_" + model.EventId + "_" + model.FeeId;
            HttpContext.Session.Set<List<EventFee>>(sessionName, data);

            return Ok();
        }

        //[Authorize]
        public IActionResult GetMParticipant()
        {
            var q = _context.MParticipant.ToList();

            return Ok(q);
        }

        [HttpPost]
        //[Authorize]
        public List<EventParticipant> GetEventParticipant(EventParticipant model)
        {
            var q = _context.EventParticipant.Where(a => a.EventId == model.EventId).ToList();

            return q;
        }

        [HttpPost]
        [Authorize]
        public List<EventParticipant> GetEventParticipantFromSession(EventParticipant model)
        {
            string SessionName = "Session_EventParticipant_" + model.EventId + "_" + model.ParticipantId;

            if (HttpContext.Session.Get<List<EventParticipant>>(SessionName) == null)
            {
                var data = _context.EventParticipant.Where(a => a.EventId == model.EventId && a.ParticipantId == model.ParticipantId).ToList();
                HttpContext.Session.Set<List<EventParticipant>>(SessionName, data);
            }

            var q = HttpContext.Session.Get<List<EventParticipant>>(SessionName);

            return q;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddEventParticipantToSession(EventParticipant model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = GetEventParticipantFromSession(model);

            var q = new EventParticipant
            {
                EventParticipantId = new Random().Next(-100000, -1),
                EventId = model.EventId,
                ParticipantId = model.ParticipantId,
                EventParticipantName = model.EventParticipantName,
                EventParticipantAmount = model.EventParticipantAmount,
                EventParticipantUnit = model.EventParticipantUnit,
                EventParticipantDuration = model.EventParticipantDuration,
                Status = 1,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now
            };

            data.Add(q);

            string sessionName = "Session_EventParticipant_" + model.EventId + "_" + model.ParticipantId;
            HttpContext.Session.Set<List<EventParticipant>>(sessionName, data);

            return Ok();
        }


        [HttpPost]
        [Authorize]
        public IActionResult DeleteEventParticipantFromSession(EventParticipant model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = GetEventParticipantFromSession(model);

            var q = data.Where(a => a.EventParticipantId == model.EventParticipantId).FirstOrDefault();

            data.Remove(q);

            string sessionName = "Session_EventParticipant_" + model.EventId + "_" + model.ParticipantId;
            HttpContext.Session.Set<List<EventParticipant>>(sessionName, data);

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public List<EventNearby> GetEventNearbyFromSession(EventNearby model)
        {
            string SessionName = "Session_EventNearby_" + model.EventId;

            if (HttpContext.Session.Get<List<EventNearby>>(SessionName) == null)
            {
                var data = _context.EventNearby.Where(a => a.EventId == model.EventId).ToList();
                HttpContext.Session.Set<List<EventNearby>>(SessionName, data);
            }

            var q = HttpContext.Session.Get<List<EventNearby>>(SessionName);

            return q;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddEventNearbyToSession(EventNearby model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = GetEventNearbyFromSession(model);

            var q = new EventNearby
            {
                EventNearbyId = new Random().Next(-100000, -1),
                EventId = model.EventId,
                NearbyName = model.NearbyName,
                Distance = model.Distance,
                DistanceUnit = model.DistanceUnit,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now
            };

            data.Add(q);

            string sessionName = "Session_EventNearby_" + model.EventId;
            HttpContext.Session.Set<List<EventNearby>>(sessionName, data);

            return Ok();
        }


        [HttpPost]
        [Authorize]
        public IActionResult DeleteEventNearbyFromSession(EventNearby model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = GetEventNearbyFromSession(model);

            var q = data.Where(a => a.EventNearbyId == model.EventNearbyId).FirstOrDefault();

            data.Remove(q);

            string sessionName = "Session_EventNearby_" + model.EventId;
            HttpContext.Session.Set<List<EventNearby>>(sessionName, data);

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