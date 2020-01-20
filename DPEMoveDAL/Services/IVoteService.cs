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
        VoteDbQuery GetVote(VoteRequest2 model);
        IEnumerable<VoteSummaryDbQuery> GetVoteSummary(VoteSummaryRequest model);
        VoteAvgDbQuery GetVoteAvg(VoteRequest2 model);
        IEnumerable<VoteSummaryAvgDbQuery> GetVoteSummaryAvg(VoteSummaryRequest model);
        IEnumerable<MVoteType> GetVoteType(VoteRequest3 model);

    }
}
