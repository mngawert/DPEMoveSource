using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MObjectivePerson
    {
        public int ObjectivePersonId { get; set; }
        public string ObjectivePersonCode { get; set; }
        public string ObjectivePersonName { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
