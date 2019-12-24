using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventLevel
    {
        public int EventLevelId { get; set; }
        public int EventId { get; set; }
        public int MEventLevelId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Event Event { get; set; }
        public MEventLevel MEventLevel { get; set; }
    }
}
