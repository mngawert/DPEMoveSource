using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public interface IVoteService
    {
        void AddOrEditVote(VoteRequest model);
        IEnumerable<VoteDbQuery> GetVote(VoteRequest2 model);
        IEnumerable<VoteSummaryDbQuery> GetVoteSummary(VoteSummaryRequest model);
    }
}
