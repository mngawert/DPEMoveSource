using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventJoinPersonType
    {
        public int EventJoinPersonTypeId { get; set; }
        public string EventJoinPersonTypeCode { get; set; }
        public int EventId { get; set; }
        public int JoinPersonTypeId { get; set; }
        public int? JoinPersonTypeCount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Event Event { get; set; }
        public MJoinPersonType JoinPersonType { get; set; }
    }
}
