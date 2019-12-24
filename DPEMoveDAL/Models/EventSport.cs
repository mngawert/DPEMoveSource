using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventSport
    {
        public int EventSportId { get; set; }
        public string EventSportCode { get; set; }
        public int EventId { get; set; }
        public int SportId { get; set; }
        public string SportEtc { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Event Event { get; set; }
        public MSport Sport { get; set; }
    }
}
