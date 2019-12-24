using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class District
    {
        public int DistrictId { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public int ProvinceId { get; set; }

        public Province Province { get; set; }
    }
}
