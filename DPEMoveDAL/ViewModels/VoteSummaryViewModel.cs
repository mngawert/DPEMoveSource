using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DPEMoveDAL.ViewModels
{
    //public class VoteSummaryViewModel
    //{
    //    public string VoteOf { get; set; }
    //    public string EventOrStadiumCode { get; set; }
    //    public int VoteValue { get; set; }
    //    public ICollection<VoteSummaryData> VoteSummaryData { get; set; }
    //}
    //public class VoteSummaryData
    //{
    //    public int VoteValue { get; set; }
    //    public int VoteCount { get; set; }
    //}



    public class VoteRequest
    {
        public string VoteOf { get; set; }
        public string EventOrStadiumCode { get; set; }
        public int VoteTypeId { get; set; }
        public int VoteValue { get; set; }
        public int CreatedBy { get; set; }
    }

    public class VoteRequest2
    {
        public string VoteOf { get; set; }
        public string EventOrStadiumCode { get; set; }
        public int CreatedBy { get; set; }
    }

    public class VoteDbQuery
    {
        [Column("VOTE_OF")]
        public string VoteOf { get; set; }
        [Column("EVENT_OR_STADIUM_CODE")]
        public string EventOrStadiumCode { get; set; }
        [Column("VOTE_TYPE_ID")]
        public int VoteTypeId { get; set; }
        [Column("VOTE_TYPE")]
        public string VoteType { get; set; }
        [Column("VOTE_VALUE")]
        public int VoteValue { get; set; }
        [Column("CREATED_BY")]
        public int CreatedBy { get; set; }
    }

    public class VoteSummaryRequest
    {
        public string VoteOf { get; set; }
        public string EventOrStadiumCode { get; set; }
    }

    public class VoteSummaryDbQuery
    {
        [Column("VOTE_OF")]
        public string VoteOf { get; set; }
        [Column("EVENT_OR_STADIUM_CODE")]
        public string EventOrStadiumCode { get; set; }
        [Column("VOTE_TYPE_ID")]
        public int VoteTypeId { get; set; }
        [Column("VOTE_TYPE")]
        public string VoteType { get; set; }
        [Column("VOTE_VALUE")]
        public int VoteValue { get; set; }
        [Column("VOTE_COUNT")]
        public int VoteCount { get; set; }
    }

    public class VoteAvgDbQuery
    {
        [Column("VOTE_OF")]
        public string VoteOf { get; set; }
        [Column("EVENT_OR_STADIUM_CODE")]
        public string VoteType { get; set; }
        [Column("VOTE_AVG")]
        public double? VoteAvg { get; set; }
    }

    public class VoteSummaryAvgDbQuery
    {
        [Column("VOTE_OF")]
        public string VoteOf { get; set; }
        [Column("EVENT_OR_STADIUM_CODE")]
        public string EventOrStadiumCode { get; set; }
        [Column("VOTE_TYPE_ID")]
        public int VoteTypeId { get; set; }
        [Column("VOTE_TYPE")]
        public string VoteType { get; set; }
        [Column("VOTE_AVG")]
        public double? VoteAvg { get; set; }
    }


}
