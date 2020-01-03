using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DPEMoveDAL.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentCode { get; set; }
        public string Comment1 { get; set; }
        public string UserCode { get; set; }
        public string CommentOf { get; set; }
        public string EventOrStadiumCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }

    public class CommentViewModel2
    {
        public string CommentOf { get; set; }
        public string EventOrStadiumCode { get; set; }
        public int? LimitStart { get; set; }
        public int? LimitSize { get; set; }
        public string OrderBy { get; set; }
        public string OrderDesc { get; set; }
    }

    public class CommentDbQuery
    {
        [Column("COMMENT_ID")]
        public int CommentId { get; set; }
        [Column("COMMENT_CODE")]
        public string CommentCode { get; set; }
        [Column("COMMENT")]
        public string Comment1 { get; set; }
        [Column("USER_CODE")]
        public string UserCode { get; set; }
        [Column("COMMENT_OF")]
        public string CommentOf { get; set; }
        [Column("EVENT_OR_STADIUM_CODE")]
        public string EventOrStadiumCode { get; set; }
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
        [Column("CREATED_BY")]
        public int CreatedBy { get; set; }
        [Column("UPDATED_DATE")]
        public DateTime? UpdatedDate { get; set; }
        [Column("UPDATED_BY")]
        public int? UpdatedBy { get; set; }
    }
}
