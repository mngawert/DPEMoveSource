using AutoMapper;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public class VoteService : IVoteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<VoteService> _logger;

        public VoteService(AppDbContext context, IMapper mapper, ILogger<VoteService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public void AddOrEditVote(VoteRequest model)
        {
            _logger.LogDebug("model.VoteOf, model.EventOrStadiumCode, model.VoteTypeId, model.VoteValue => {0}, {1}, {2}, {3}", model.VoteOf, model.EventOrStadiumCode, model.VoteTypeId, model.VoteValue);

            var qdb = _context.Vote.Where(a => a.CreatedBy == model.CreatedBy && a.VoteTypeId == model.VoteTypeId);

            if (model.VoteOf == "1") {
                qdb = qdb.Where(a => a.EventCode == model.EventOrStadiumCode);
            }
            else {
                qdb = qdb.Where(a => a.StadiumCode == model.EventOrStadiumCode);
            }

            var q = qdb.FirstOrDefault();
            if (q == null)
            {
                _logger.LogDebug("adding vote");

                q = new Vote
                {
                    VoteValue = model.VoteValue,
                    VoteMaxValue = 5,
                    VoteTypeId = model.VoteTypeId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = model.CreatedBy,
                    Status = 1
                };

                if (model.VoteOf == "1") {
                    q.EventCode = model.EventOrStadiumCode;
                }
                else {
                    q.StadiumCode = model.EventOrStadiumCode;
                }

                _context.Add(q).State = EntityState.Added;
                _context.SaveChanges();
            }
            else
            {
                _logger.LogDebug("updating vote");

                q.VoteValue = model.VoteValue;
                q.UpdatedDate = DateTime.Now;
                q.UpdatedBy = model.CreatedBy;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public VoteDbQuery GetVote(VoteRequest2 model)
        {
            string sql = "select * from VW_VOTE where VOTE_OF = {0} and EVENT_OR_STADIUM_CODE = {1} and VOTE_TYPE_ID = {2} and CREATED_BY = {3}";

            var q = _context.VoteDbQuery.FromSql(sql, model.VoteOf, model.EventOrStadiumCode, model.VoteTypeId, model.CreatedBy).FirstOrDefault();

            return q;
        }

        public IEnumerable<VoteSummaryDbQuery> GetVoteSummary(VoteSummaryRequest model)
        {

            string sql = "select * from VW_VOTE_SUMMARY where VOTE_OF = {0} and EVENT_OR_STADIUM_CODE = {1}";

            var q = _context.VoteSummaryDbQuery.FromSql(sql, model.VoteOf, model.EventOrStadiumCode);

            return q.ToList();
        }

        public VoteAvgDbQuery GetVoteAvg(VoteRequest2 model)
        {

            string sql = "select * from VW_VOTE_AVG where VOTE_OF = {0} and EVENT_OR_STADIUM_CODE = {1}";

            var q = _context.VoteAvgDbQuery.FromSql(sql, model.VoteOf, model.EventOrStadiumCode).FirstOrDefault();

            if (q == null)
            {
                return new VoteAvgDbQuery
                {
                    VoteOf = model.VoteOf,
                    EventOrStadiumCode = model.EventOrStadiumCode,
                    VoteAvg = null
                };
            }

            return q;
        }

        public IEnumerable<VoteSummaryAvgDbQuery> GetVoteSummaryAvg(VoteSummaryRequest model)
        {

            string sql = "select * from VW_VOTE_SUMMARY_AVG where VOTE_OF = {0} and EVENT_OR_STADIUM_CODE = {1}";

            var q = _context.VoteSummaryAvgDbQuery.FromSql(sql, model.VoteOf, model.EventOrStadiumCode);

            return q.ToList();
        }

        public IEnumerable<MVoteType> GetVoteType(VoteRequest3 model)
        {
            IQueryable<MVoteType> q = _context.MVoteType;

            if (!string.IsNullOrEmpty(model.VoteOf))
            {
                q = q.Where(a => a.VoteOf == model.VoteOf);
            }

            return q;            
        }

    }
}
