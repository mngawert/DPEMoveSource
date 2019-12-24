using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MAddressType
    {
        public int AddressTypeId { get; set; }
        public string AddressTypeCode { get; set; }
        public string AddressType { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
