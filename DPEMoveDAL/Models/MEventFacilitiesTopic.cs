using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MEventFacilitiesTopic
    {
        public MEventFacilitiesTopic()
        {
            EventFacilities = new HashSet<EventFacilities>();
        }

        public int EventFacilitiesTopicId { get; set; }
        public string EventFacilitiesTopicCode { get; set; }
        public string EventFacilitiesTopicName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        [JsonIgnore]
        public ICollection<EventFacilities> EventFacilities { get; set; }
    }
}
