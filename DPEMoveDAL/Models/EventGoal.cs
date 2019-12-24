using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventGoal
    {
        public int EventGoalId { get; set; }
        public int EventId { get; set; }
        public int MEventGoalId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Event Event { get; set; }
        public MEventGoal MEventGoal { get; set; }
    }
}
