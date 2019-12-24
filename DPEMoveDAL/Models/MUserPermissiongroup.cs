using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MUserPermissiongroup
    {
        public int UserPermissiongroupId { get; set; }
        public int UserId { get; set; }
        public int MPermissionGroupId { get; set; }
        public string Comment { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateBy { get; set; }

        public MPermissionGroup MPermissionGroup { get; set; }
        public User User { get; set; }
    }
}
