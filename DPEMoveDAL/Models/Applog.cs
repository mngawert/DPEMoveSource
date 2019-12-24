using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Applog
    {
        public decimal Id { get; set; }
        public string RequestIp { get; set; }
        public string Logged { get; set; }
        public string Loglevel { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
