using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MVoteText
    {
        public decimal VoteFrom { get; set; }
        public decimal? VoteTo { get; set; }
        public string VoteText { get; set; }
        public string RatingColor { get; set; }
    }
}
