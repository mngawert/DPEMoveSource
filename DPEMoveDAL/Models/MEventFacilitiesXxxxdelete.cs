using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MEventFacilitiesXxxxdelete
    {
        public int EventFacilitiesId { get; set; }
        public string EventFacilitiesCode { get; set; }
        public int EventFacilitiesTopicId { get; set; }
        public string EventFacilitiesName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
