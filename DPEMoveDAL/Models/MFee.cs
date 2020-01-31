using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MFee
    {
        public MFee()
        {
            EventFee = new HashSet<EventFee>();
        }

        public int FeeId { get; set; }
        public string FeeCode { get; set; }
        public string FeeName { get; set; }
        public int? Status { get; set; }

        [JsonIgnore]
        public ICollection<EventFee> EventFee { get; set; }
    }
}
