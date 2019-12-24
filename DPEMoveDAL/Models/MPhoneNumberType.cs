using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MPhoneNumberType
    {
        public int PhoneNumberTypeId { get; set; }
        public string PhoneNumberTypeCode { get; set; }
        public string PhoneNumberType { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
