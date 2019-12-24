using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MProgram
    {
        public MProgram()
        {
            MPermissiongroupProgram = new HashSet<MPermissiongroupProgram>();
        }

        public int ProgramId { get; set; }
        public string Description { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }

        public ICollection<MPermissiongroupProgram> MPermissiongroupProgram { get; set; }
    }
}
