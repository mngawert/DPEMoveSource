using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Province
    {
        public Province()
        {
            Amphur = new HashSet<Amphur>();
        }

        public int ProvinceId { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }

        public ICollection<Amphur> Amphur { get; set; }
    }
}
