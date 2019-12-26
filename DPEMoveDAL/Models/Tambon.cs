using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Tambon
    {
        public int TambonId { get; set; }
        public string TambonCode { get; set; }
        public string TambonName { get; set; }
        public int AmphurId { get; set; }

        public Amphur Amphur { get; set; }
    }
}
