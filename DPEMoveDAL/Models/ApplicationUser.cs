using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string IdcardType { get; set; }
        public string IdcardNo { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public int? GroupId { get; set; }
    }
}
