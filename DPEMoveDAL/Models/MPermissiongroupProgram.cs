using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MPermissiongroupProgram
    {
        public int PermissiongroupProgramId { get; set; }
        public int ProgramId { get; set; }
        public int MPermissionGroupId { get; set; }
        public string Canview { get; set; }
        public string Canedit { get; set; }
        public string Canexcel { get; set; }
        public string Canpdf { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateBy { get; set; }

        public MPermissionGroup MPermissionGroup { get; set; }
        public MProgram Program { get; set; }
    }
}
