using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DPEMoveDAL.ViewModels
{
    public class ReportEvent1Request
    {
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
    }

    public class ReportEvent1DbQuery
    {
        [Column("PROV_CODE")]
        public string ProvCode { get; set; }
        [Column("PROV_NAMT")]
        public string ProvNamt { get; set; }
        [Column("NO_OF_EVENTS")]
        public int NoOfEvents { get; set; }
    }

    public class ReportEvent2Request
    {
        public string IdCard { get; set; }
    }
    public class ReportEvent2DbQuery
    {
        [Column("EVENT_ID")]
        public int EventId { get; set; }
        [Column("EVENT_CODE")]
        public string EventCode { get; set; }
        [Column("EVENT_NAME")]
        public string EventName { get; set; }
        [Column("EVENT_START_DATE")]
        public DateTime EventStartDate { get; set; }
        [Column("PARTICIPANT_COUNT")]
        public int? ParticipantCount { get; set; }
        [Column("CREATED_BY")]
        public int CreatedBy { get; set; }
        [Column("ID_CARD")]
        public string IdCard { get; set; }
    }


    public class ReportEvent3Request
    {
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
    }
    public class ReportEvent3DbQuery
    {
        [Column("EVENT_ID")]
        public int EventId { get; set; }
        [Column("EVENT_CODE")]
        public string EventCode { get; set; }
        [Column("EVENT_NAME")]
        public string EventName { get; set; }
        [Column("EVENT_START_DATE")]
        public DateTime EventStartDate { get; set; }
        [Column("PARTICIPANT_COUNT")]
        public int? ParticipantCount { get; set; }
        [Column("CREATED_BY")]
        public int CreatedBy { get; set; }
        [Column("ID_CARD")]
        public string IdCard { get; set; }
        [Column("SECTION_CAT_ID")]
        public int? SectionCatId { get; set; }
        //[Column("SECTION_CAT_NAME")]
        //public string SectionCatName { get; set; }
    }


    public class ReportEvent4Request
    {
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
    }
    public class ReportEvent4DbQuery
    {
        [Column("SECTION_CAT_ID")]
        public int? SectionCatId { get; set; }
        //[Column("SECTION_CAT_NAME")]
        //public string SectionCatName { get; set; }
        [Column("NO_OF_EVENTS")]
        public int NoOfEvents { get; set; }
    }

    public class ReportEvent5Request
    {
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public string ProvinceCode { get; set; }
    }
    public class ReportEvent5DbQuery
    {
        [Column("SECTION_CAT_ID")]
        public int? SectionCatId { get; set; }
        [Column("NO_OF_EVENTS")]
        public int NoOfEvents { get; set; }
    }

    public class ReportEvent6Request
    {
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public string ProvinceCode { get; set; }
    }
    public class ReportEvent6DbQuery
    {
        [Column("EVENT_LEVEL_ID")]
        public int? EventLevelId { get; set; }
        [Column("EVENT_LEVEL_NAME")]
        public string EventLevelName { get; set; }
        [Column("NO_OF_EVENTS")]
        public int NoOfEvents { get; set; }
    }


    public class ReportEvent7Request
    {
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public string ProvinceCode { get; set; }
    }
    public class ReportEvent7DbQuery
    {
        [Column("M_EVENT_OBJECTIVE_ID")]
        public int? MEventObjectiveId { get; set; }
        [Column("EVENT_OBJECTIVE_NAME")]
        public string EventObjectiveName { get; set; }
        [Column("NO_OF_EVENTS")]
        public int NoOfEvents { get; set; }
    }


    public class ReportEvent8Request
    {
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public string ProvinceCode { get; set; }
        public string SectionCatId { get; set; }
    }
    public class ReportEvent8DbQuery
    {
        [Column("ACT_TYPE_ID")]
        public int? ActTypeId { get; set; }
        [Column("NO_OF_EVENTS")]
        public int NoOfEvents { get; set; }
    }

    public class VW_RPT_SURVEY_15_1_A_Request
    {
        public DateTime CreatedDateFrom { get; set; }
        public DateTime CreatedDateTo { get; set; }
    }
    public class VW_RPT_SURVEY_15_1_A_DbQuery
    {
        [Column("SPORT_NAME")]
        public string SportName { get; set; }
        [Column("SUM_ATTR")]
        public int SumAttr { get; set; }
    }

    public class VW_USER
    {
        public int APP_USER_ID { get; set; }
        public string EMAIL { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public int GROUP_ID { get; set; }
        public string NAME { get; set; }
        public string ID_CARD_NO { get; set; }
        public DateTime? BIRTH_DATE { get; set; }
        public decimal? HEIGHT { get; set; }
        public decimal? WEIGH { get; set; }
        public string ACCOUNT_TYPE_NAME { get; set; }
        public string GROUP_NAME { get; set; }
    }

    public class VW_EVENTS
    {
        [Column("EVENT_ID")]
        public int EventId { get; set; }
        [Column("EVENT_CODE")]
        public string EventCode { get; set; }
        [Column("EVENT_NAME")]
        public string EventName { get; set; }
        [Column("STATUS")]
        public int? Status { get; set; }
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
        [Column("CREATED_BY")]
        public int CreatedBy { get; set; }
        [Column("ACCOUNT_TYPE")]
        public string AccountType { get; set; }
        [Column("ACCOUNT_TYPE_NAME")]
        public string AccountTypeName { get; set; }
        [Column("GROUP_ID")]
        public decimal GroupId { get; set; }
        [Column("GROUP_NAME")]
        public string GroupName { get; set; }
        [Column("PROVINCE_CODE")]
        public string ProvinceCode { get; set; }
        [Column("AMPHUR_CODE")]
        public string AmphurCode { get; set; }
        [Column("TAMBON_CODE")]
        public string TambonCode { get; set; }
    }

}


