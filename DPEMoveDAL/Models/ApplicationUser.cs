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
        public decimal? GroupId { get; set; }
        public int AppUserId { get; set; }
        public string FacebookId { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
    }
}
