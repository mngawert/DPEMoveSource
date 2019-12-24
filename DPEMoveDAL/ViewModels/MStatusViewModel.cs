using System;
using System.Collections.Generic;

namespace DPEMoveDAL.ViewModels
{
    public class MStatusViewModel
    {
        public int StatusId { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
