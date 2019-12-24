using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class PrivateMessage
    {
        public int PrivateMessageId { get; set; }
        public string PrivateMessageCode { get; set; }
        public string UserCode { get; set; }
        public string Ip { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
