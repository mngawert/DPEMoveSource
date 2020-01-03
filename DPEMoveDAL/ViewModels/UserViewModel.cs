using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string IdcardType { get; set; }
        public string IdcardNo { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public int? GroupId { get; set; }
        public bool Selected { get; set; }
    }

    public class UserViewModel2
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string IdcardType { get; set; }
        public string IdcardNo { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public int? GroupId { get; set; }
        public int AppUserId { get; set; }
    }

}
