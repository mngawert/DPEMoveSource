using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MVoteType
    {
        public int VoteTypeId { get; set; }
        public string VoteTypeCode { get; set; }
        public string VoteType { get; set; }
        public string VoteOf { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
