using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MEventObjective
    {
        public MEventObjective()
        {
            EventObjective = new HashSet<EventObjective>();
        }

        public int EventObjectiveId { get; set; }
        public string EventObjectiveCode { get; set; }
        public string EventObjectiveName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<EventObjective> EventObjective { get; set; }
    }
}
