using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class DepartmentEvent
    {
        public int DepartmentEventId { get; set; }
        public string DepartmentEventCode { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentEventType { get; set; }
        public int? DepartmentEventSubtypeId { get; set; }
        public string EventName { get; set; }
        public string EventShortDescription { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventStartTimestamp { get; set; }
        public DateTime? EventFinishTimestamp { get; set; }
        public int? AddressId { get; set; }
        public int? ParentDepartmentEventId { get; set; }
        public decimal? Budget { get; set; }
        public decimal? BudgetUsed { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
