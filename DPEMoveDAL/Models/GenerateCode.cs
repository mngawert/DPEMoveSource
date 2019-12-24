using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class GenerateCode
    {
        public int GenerateId { get; set; }
        public string GenerateKey { get; set; }
        public long? GenerateValue { get; set; }
        public string Years { get; set; }
        public string IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
