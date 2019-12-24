using DPEMoveDAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.ViewModels
{
    public class GroupViewModel
    {
        public MGroup Group { get; set; }

        public IEnumerable<RoleViewModel> Members { get; set; }
    }
}
