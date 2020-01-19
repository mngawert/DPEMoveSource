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
    [Route("WebApi/Stadium/[action]")]
    [ApiController]
    public class StadiumWebApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;
        private readonly ICommentService _commentService;
        private readonly ILogger<StadiumWebApiController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public StadiumWebApiController(AppDbContext context, IMapper mapper, IEventService eventService, ICommentService commentService, ILogger<StadiumWebApiController> logger, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
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

        [HttpGet("{id}")]
        public IActionResult GetCommentsByStadiumId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var input = new CommentViewModel2
            {
                CommentOf = "2",
                EventOrStadiumCode = id.ToString()
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


    }
}