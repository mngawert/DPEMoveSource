using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventFee
    {
        public int EventFeeId { get; set; }
        public int EventId { get; set; }
        public int FeeId { get; set; }
        public string EventFeeName { get; set; }
        public int? EventFeeAmount { get; set; }
        public string EventFeeUnit { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Event Event { get; set; }
        public MFee Fee { get; set; }
    }
}
