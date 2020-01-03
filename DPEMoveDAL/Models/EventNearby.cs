using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventNearby
    {
        public int EventNearbyId { get; set; }
        public int EventId { get; set; }
        public string NearbyName { get; set; }
        public string Distance { get; set; }
        public string DistanceUnit { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Event Event { get; set; }
    }
}
