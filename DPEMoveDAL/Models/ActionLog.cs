using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class ActionLog
    {
        public int ActionlogId { get; set; }
        public string Ip { get; set; }
        public string MacAddress { get; set; }
        public string Username { get; set; }
        public string LogHeader { get; set; }
        public string LogMessage { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
