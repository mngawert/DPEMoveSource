using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventObjective
    {
        public int EventObjectiveId { get; set; }
        public int EventId { get; set; }
        public int MEventObjectiveId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string EventObjectiveEtc { get; set; }

        public Event Event { get; set; }
        public MEventObjective MEventObjective { get; set; }
    }
}
