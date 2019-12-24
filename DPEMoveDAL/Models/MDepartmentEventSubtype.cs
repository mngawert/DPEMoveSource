using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MDepartmentEventSubtype
    {
        public int DepartmentEventSubtypeId { get; set; }
        public string DepartmentEventSubtypeCode { get; set; }
        public string DepartmentEventSubtypeName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
