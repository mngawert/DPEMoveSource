using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MAccountType
    {
        public string AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public int? DefaultGroupId { get; set; }
        public string RequireProfileBoo { get; set; }
    }
}
