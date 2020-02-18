using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventActivityType
    {
        public int EventActivityTypeId { get; set; }
        public int? EventId { get; set; }
        public int? ActTypeId { get; set; }
        public string ActTypeEtc { get; set; }
    }
}
