using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DPEMoveDAL.ViewModels;
using DPEMoveDAL.Models;

namespace DPEMoveWeb.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/votes/[action]")]
    [ApiController]
    public class VotesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VotesApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Votes
        [HttpGet]
        public IEnumerable<Vote> GetVote()
        {
            return _context.Vote;
        }

        // GET: api/Votes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vote = await _context.Vote.FindAsync(id);

            if (vote == null)
            {
                return NotFound();
            }

            return Ok(vote);
        }


        [HttpGet("{eventOrStadiumCode}")]
        public IActionResult GetVoteSummary([FromRoute] string eventOrStadiumCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vote = _context.Vote
                .Where(a => a.EventCode == eventOrStadiumCode || a.StadiumCode == eventOrStadiumCode)
                .GroupBy(a => new { a.VoteValue })
                .Select(a => new VoteSummaryData { VoteValue = a.Key.VoteValue, VoteCount = a.Count() })
                .OrderBy(a => a.VoteValue);
            ;

            if (vote == null)
            {
                return NotFound();
            }

            return Ok(vote);
        }


        [HttpPost]
        public IActionResult AddVote([FromBody] VoteSummaryViewModel m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var q = new Vote
            {
                VoteValue = m.VoteValue,
                VoteMaxValue = 5,
                VoteTypeId = 6,
                CreatedDate = DateTime.Now,
                CreatedBy = 0
            };

            if (m.VoteOf == "1")
            {
                q.EventCode = m.EventOrStadiumCode;
            }
            else
            {
                q.StadiumCode = m.EventOrStadiumCode;
            }

            _context.Add(q).State = EntityState.Added;
            _context.SaveChanges();

            return GetVoteSummary(m);

            //return Ok(q);
        }


        [HttpPost]
        public IActionResult GetVoteSummary([FromBody] VoteSummaryViewModel m)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var q = _context.Vote
                .Where(a => m.EventOrStadiumCode == (m.VoteOf == "1" ? a.EventCode : a.StadiumCode))
                .GroupBy(a => new { a.VoteValue })
                .Select(a => new VoteSummaryData { VoteValue = a.Key.VoteValue, VoteCount = a.Count() })
                .OrderBy(a => a.VoteValue)
                .ToList();
            ;

            if (q == null)
            {
                return NotFound();
            }

            m.VoteSummaryData = q;

            return Ok(m);
        }

        // PUT: api/Votes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVote([FromRoute] int id, [FromBody] Vote vote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vote.VoteId)
            {
                return BadRequest();
            }

            _context.Entry(vote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Votes
        [HttpPost]
        public async Task<IActionResult> PostVote([FromBody] Vote vote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vote.Add(vote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVote", new { id = vote.VoteId }, vote);
        }

        // DELETE: api/Votes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vote = await _context.Vote.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }

            _context.Vote.Remove(vote);
            await _context.SaveChangesAsync();

            return Ok(vote);
        }

        private bool VoteExists(int id)
        {
            return _context.Vote.Any(e => e.VoteId == id);
        }
    }
}