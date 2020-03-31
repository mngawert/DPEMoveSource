using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Step
    {
        public int StepId { get; set; }
        public int StepValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
