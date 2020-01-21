using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DPEMoveWeb.ApiWebControllers
{
    [Route("WebApi/Comments/[action]")]
    [ApiController]
    public class CommentsWebApiController : ControllerBase
    {
        private readonly ILogger<CommentsWebApiController> _logger;
        private readonly AppDbContext _context;
        private readonly ICommentService _commentService;

        public CommentsWebApiController(ILogger<CommentsWebApiController> logger, AppDbContext context, ICommentService commentService)
        {
            _logger = logger;
            _context = context;
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddComment([FromBody] CommentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var q = _commentService.AddComment(model);

                Response.Headers["ResponseCode"] = "2000";
                Response.Headers["ResponseDescription"] = "Ok";

                return Ok(q);
            }
            catch (Exception e)
            {
                Response.Headers["ResponseCode"] = "4000";
                Response.Headers["ResponseDescription"] = e.Message;
                return BadRequest(e.ToString());
            }
        }


        [HttpPost]
        [Authorize]
        public IActionResult EditComment([FromBody] CommentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _commentService.EditComment(model);

                Response.Headers["ResponseCode"] = "2000";
                Response.Headers["ResponseDescription"] = "Ok";

                return Ok();
            }
            catch (Exception e)
            {
                Response.Headers["ResponseCode"] = "4000";
                Response.Headers["ResponseDescription"] = e.Message;
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteComment([FromBody] CommentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _commentService.DeleteComment(model);

                Response.Headers["ResponseCode"] = "2000";
                Response.Headers["ResponseDescription"] = "Ok";

                return Ok();
            }
            catch (Exception e)
            {
                Response.Headers["ResponseCode"] = "4000";
                Response.Headers["ResponseDescription"] = e.Message;
                return BadRequest(e.ToString());
            }
        }


        [HttpPost]
        [Authorize]
        public IActionResult GetComment([FromBody] CommentViewModel2 model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var q = _commentService.GetComment(model);

                Response.Headers["ResponseCode"] = "2000";
                Response.Headers["ResponseDescription"] = "Ok";

                return Ok(q);
            }
            catch (Exception e)
            {
                Response.Headers["ResponseCode"] = "4000";
                Response.Headers["ResponseDescription"] = e.Message;
                return BadRequest(e.ToString());
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetCommentDetails([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var q = _commentService.GetCommentDetails(id);

                Response.Headers["ResponseCode"] = "2000";
                Response.Headers["ResponseDescription"] = "Ok";

                return Ok(q);
            }
            catch (Exception e)
            {
                Response.Headers["ResponseCode"] = "4000";
                Response.Headers["ResponseDescription"] = e.Message;
                return BadRequest(e.ToString());
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetCommentCount([FromBody] CommentViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var q = _commentService.GetCommentCount (model);

                Response.Headers["ResponseCode"] = "2000";
                Response.Headers["ResponseDescription"] = "Ok";

                return Ok(q);
            }
            catch (Exception e)
            {
                Response.Headers["ResponseCode"] = "4000";
                Response.Headers["ResponseDescription"] = e.Message;
                return BadRequest(e.ToString());
            }
        }

    }
}