using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MUserType
    {
        public MUserType()
        {
            User = new HashSet<User>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeCode { get; set; }
        public string UserTypeName { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<User> User { get; set; }
    }
}
