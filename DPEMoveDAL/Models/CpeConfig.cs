﻿using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class CpeConfig
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int? ConfigId { get; set; }
        public int? Status { get; set; }
    }
}
