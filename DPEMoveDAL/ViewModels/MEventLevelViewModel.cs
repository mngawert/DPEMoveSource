using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DPEMoveDAL.ViewModels
{
    public partial class MEventLevelViewModel
    {
        public int EventLevelId { get; set; }
        public string EventLevelCode { get; set; }
        public string EventLevelName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public MStatusViewModel StatusNavigation { get; set; }

        [JsonIgnore]
        public ICollection<EventViewModel> Event { get; set; }


        /* Custom Attributes*/
        public int LimitStart { get; set; }

    }
}
