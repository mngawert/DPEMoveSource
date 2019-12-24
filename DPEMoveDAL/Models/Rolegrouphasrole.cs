using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Rolegrouphasrole
    {
        public string RoleGroupId { get; set; }
        public string RoleId { get; set; }

        public Rolegroup RoleGroup { get; set; }
    }
}
