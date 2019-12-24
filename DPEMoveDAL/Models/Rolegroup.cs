using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Rolegroup
    {
        public Rolegroup()
        {
            Rolegrouphasrole = new HashSet<Rolegrouphasrole>();
        }

        public string RoleGroupId { get; set; }
        public string RoleGroupName { get; set; }

        public ICollection<Rolegrouphasrole> Rolegrouphasrole { get; set; }
    }
}
