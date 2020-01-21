using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveWeb.ApiWebControllers
{
    [Route("WebApi/Votes/[action]")]
    [ApiController]
    public class VotesWebApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IVoteService _voteService;
        public VotesWebApiController(AppDbContext context, IVoteService voteService)
        {
            _context = context;
            _voteService = voteService;
        }

        public IActionResult GetVoteType(VoteRequest3 model)
        {
            try
            {
                var q = _voteService.GetVoteType(model);

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
        public IActionResult AddOrEditVote([FromBody] VoteRequest model)
        {
            try
            {
                _voteService.AddOrEditVote(model);

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

        public IActionResult GetVote(VoteRequest2 model)
        {
            try
            {
                var q = _voteService.GetVote(model);

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

        public IActionResult GetVoteAvg(VoteRequest2 model)
        {
            try
            {
                var q = _voteService.GetVoteAvg(model);

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

        public IActionResult GetVoteTotalAvg(VoteSummaryRequest model)
        {
            try
            {
                var q = _voteService.GetVoteTotalAvg(model);

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

        public IActionResult GetVoteTotalAvgDetails(VoteSummaryRequest model)
        {
            try
            {
                var q = _voteService.GetVoteTotalAvgDetails(model);

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