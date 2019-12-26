using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Amphur
    {
        public Amphur()
        {
            Tambon = new HashSet<Tambon>();
        }

        public int AmphurId { get; set; }
        public string AmphurCode { get; set; }
        public string AmphurName { get; set; }
        public int ProvinceId { get; set; }

        public Province Province { get; set; }
        public ICollection<Tambon> Tambon { get; set; }
    }
}
