using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class OtpLog
    {
        public int OtpLogId { get; set; }
        public string OtpLogCode { get; set; }
        public string Otp { get; set; }
        public string Reference { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
