using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Employeeshierarchy
    {
        public decimal Employeeid { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public decimal? Reportingmanager { get; set; }
        public string Photopath { get; set; }
    }
}
