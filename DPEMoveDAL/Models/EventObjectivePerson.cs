using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventObjectivePerson
    {
        public int EventObjectivePersonId { get; set; }
        public int EventId { get; set; }
        public int ObjectivePersonId { get; set; }
        public string ObjectivePersonEtc { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
