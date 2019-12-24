using System;
using System.Collections.Generic;

namespace DPEMoveDAL.ViewModels
{
    public class VoteSummaryViewModel
    {
        public string VoteOf { get; set; }
        public string EventOrStadiumCode { get; set; }
        public int VoteValue { get; set; }
        public ICollection<VoteSummaryData> VoteSummaryData { get; set; }
    }

    public class VoteSummaryData
    {
        public int VoteValue { get; set; }
        public int VoteCount { get; set; }
    }

}
