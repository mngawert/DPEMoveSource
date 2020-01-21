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
        IEnumerable<MVoteType> GetVoteType(VoteRequest3 model);
        void AddOrEditVote(VoteRequest model);
        VoteDbQuery GetVote(VoteRequest2 model);
        VoteAvgDbQuery GetVoteAvg(VoteRequest2 model);
        VoteTotalAvgDbQuery GetVoteTotalAvg(VoteSummaryRequest model);
        IEnumerable<VoteTotalAvgDetailsDbQuery> GetVoteTotalAvgDetails(VoteSummaryRequest model);

    }
}
