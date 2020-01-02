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
            var qdb = _context.Vote.Where(a => a.CreatedBy == model.CreatedBy && a.VoteTypeId == model.VoteTypeId);

            if (model.VoteOf == "1") {
                qdb.Where(a => a.EventCode == model.EventOrStadiumCode);
            }
            else {
                qdb.Where(a => a.StadiumCode == model.EventOrStadiumCode);
            }

            var q = qdb.FirstOrDefault();
            if (q == null)
            {
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
                q.VoteValue = model.VoteValue;
                q.UpdatedDate = DateTime.Now;
                q.UpdatedBy = model.CreatedBy;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public IEnumerable<VoteDbQuery> GetVote(VoteRequest2 model)
        {

            string sql = "select * from VW_VOTE where VOTE_OF = {0} and EVENT_OR_STADIUM_CODE = {1} and CREATED_BY = {2}";

            var q = _context.VoteDbQuery.FromSql(sql, model.VoteOf, model.EventOrStadiumCode, model.CreatedBy);

            return q.ToList();
        }

        public IEnumerable<VoteSummaryDbQuery> GetVoteSummary(VoteSummaryRequest model)
        {

            string sql = "select * from VW_VOTE_SUMMARY where VOTE_OF = {0} and EVENT_OR_STADIUM_CODE = {1}";

            var q = _context.VoteSummaryDbQuery.FromSql(sql, model.VoteOf, model.EventOrStadiumCode);

            return q.ToList();
        }
    }
}
