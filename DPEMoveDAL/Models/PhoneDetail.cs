using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class PhoneDetail
    {
        public int PhoneDetailId { get; set; }
        public string PhoneDetail1 { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
