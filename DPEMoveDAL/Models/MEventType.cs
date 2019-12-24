using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MEventType
    {
        public MEventType()
        {
            Event = new HashSet<Event>();
        }

        public int EventTypeId { get; set; }
        public string EventTypeCode { get; set; }
        public string EventTypeName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<Event> Event { get; set; }
    }
}
