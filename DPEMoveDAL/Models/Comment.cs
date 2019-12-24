using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string CommentCode { get; set; }
        public string Comment1 { get; set; }
        public string UserCode { get; set; }
        public string EventCode { get; set; }
        public string StadiumCode { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
