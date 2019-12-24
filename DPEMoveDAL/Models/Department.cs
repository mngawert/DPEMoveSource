using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Department
    {
        public Department()
        {
            DepartmentPerson = new HashSet<DepartmentPerson>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string Department1 { get; set; }
        public string DepartmentType { get; set; }
        public int? AddressId { get; set; }
        public string Vistion { get; set; }
        public string Mission { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Address Address { get; set; }
        public ICollection<DepartmentPerson> DepartmentPerson { get; set; }
    }
}
