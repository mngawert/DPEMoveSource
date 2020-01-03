using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MProvince
    {
        public MProvince()
        {
            Address = new HashSet<Address>();
        }

        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }

        public ICollection<Address> Address { get; set; }
    }
}
