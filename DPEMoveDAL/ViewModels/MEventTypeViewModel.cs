using System;
using System.Collections.Generic;

namespace DPEMoveDAL.ViewModels
{
    public partial class MEventTypeViewModel
    {
        public int EventTypeId { get; set; }
        public string EventTypeCode { get; set; }
        public string EventTypeName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public MStatusViewModel StatusNavigation { get; set; }
        public ICollection<EventViewModel> Event { get; set; }
    }
}
