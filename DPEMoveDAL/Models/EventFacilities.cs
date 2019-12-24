using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventFacilities
    {
        public int EventFacilitiesId { get; set; }
        public int MEventFacilitiesTopicId { get; set; }
        public int EventId { get; set; }
        public string EventFacilitiesName { get; set; }
        public int? FacilitiesAmount { get; set; }
        public string FacilitiesUnit { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Event Event { get; set; }
        public MEventFacilitiesTopic MEventFacilitiesTopic { get; set; }
    }
}
