using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class DepartmentPerson
    {
        public int DepartmentPersonId { get; set; }
        public string DepartmentPersonCode { get; set; }
        public int DepartmentId { get; set; }
        public string TitleCode { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? ParentPersonId { get; set; }
        public string PositionName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Department Department { get; set; }
    }
}
