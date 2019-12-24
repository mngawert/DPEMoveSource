using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MEventLevel
    {
        public MEventLevel()
        {
            Event = new HashSet<Event>();
            EventLevel = new HashSet<EventLevel>();
        }

        public int EventLevelId { get; set; }
        public string EventLevelCode { get; set; }
        public string EventLevelName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<Event> Event { get; set; }
        public ICollection<EventLevel> EventLevel { get; set; }
    }
}
