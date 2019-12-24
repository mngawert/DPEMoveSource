using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Vote
    {
        public int VoteId { get; set; }
        public int VoteValue { get; set; }
        public int VoteMaxValue { get; set; }
        public int VoteTypeId { get; set; }
        public string EventCode { get; set; }
        public string StadiumCode { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
