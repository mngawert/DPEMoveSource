using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MPermissionGroup
    {
        public MPermissionGroup()
        {
            MPermissiongroupProgram = new HashSet<MPermissiongroupProgram>();
            MUserPermissiongroup = new HashSet<MUserPermissiongroup>();
        }

        public int PermissionGroupId { get; set; }
        public string Description { get; set; }
        public string MPermissionGroupName { get; set; }
        public string MPermissionGroupCode { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateBy { get; set; }

        public ICollection<MPermissiongroupProgram> MPermissiongroupProgram { get; set; }
        public ICollection<MUserPermissiongroup> MUserPermissiongroup { get; set; }
    }
}
