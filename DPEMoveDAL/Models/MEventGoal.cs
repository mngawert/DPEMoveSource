using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MEventGoal
    {
        public MEventGoal()
        {
            EventGoal = new HashSet<EventGoal>();
        }

        public int EventGoalId { get; set; }
        public string EventGoalCode { get; set; }
        public string EventGoalName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<EventGoal> EventGoal { get; set; }
    }
}
