using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using DPEMoveDAL.Context;
using DPEMoveDAL.ViewModels;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;

namespace DPEMoveWeb.ApiControllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme + "," + JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/comments/[action]")]
    [ApiController]
    public class CommentsApiController : ControllerBase
    {
        private readonly ILogger<CommentsApiController> _logger;
        private readonly AppDbContext _context;
        private readonly ICommentService _commentService;

        public CommentsApiController(ILogger<CommentsApiController> logger, AppDbContext context, ICommentService commentService)
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
                
                _commentService.AddComment(model);

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


        //[HttpGet]
        //[Authorize]
        //public IEnumerable<CommentViewModel> GetComment()
        //{
        //    var model = new CommentViewModel2
        //    {
        //        CommentOf = "1",
        //        EventOrStadiumCode = "EVT201911260001",
        //        LimitStart = 1,
        //        LimitSize = 1000
        //    };

        //    var q = _commentService.GetComment(model);

        //    return q;
        //}

        //[HttpGet]
        //[Authorize]
        //public IEnumerable<CommentViewModel> GetComment_GET()
        //{
        //    var model = new CommentViewModel2
        //    {
        //        CommentOf = "1",
        //        EventOrStadiumCode = "EVT201911260001",
        //        LimitStart = 1,
        //        LimitSize = 1000
        //    };

        //    var q = _commentService.GetComment(model);

        //    return q;
        //}


        //[HttpPost]
        //[Authorize]
        //public IEnumerable<CommentViewModel> GetComment2([FromBody] CommentViewModel2 model)
        //{
        //    //var model = new CommentViewModelReq
        //    //{
        //    //    CommentOf = "1",
        //    //    EventOrStadiumCode = "EVT201911260001",
        //    //    LimitStart = 1,
        //    //    LimitSize = 1000
        //    //};

        //    var q = _commentService.GetComment(model);

        //    return q;
        //}


        //// GET: api/Comments/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetComment([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var comment = await _context.Comment.FindAsync(id);

        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(comment);
        //}

        //// PUT: api/Comments/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutComment([FromRoute] int id, [FromBody] Comment comment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != comment.CommentId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(comment).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CommentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Comments
        //[HttpPost]
        //public async Task<IActionResult> PostComment([FromBody] Comment comment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Comment.Add(comment);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        //}

        //// DELETE: api/Comments/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteComment([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var comment = await _context.Comment.FindAsync(id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Comment.Remove(comment);
        //    await _context.SaveChangesAsync();

        //    return Ok(comment);
        //}

        //private bool CommentExists(int id)
        //{
        //    return _context.Comment.Any(e => e.CommentId == id);
        //}
    }
}