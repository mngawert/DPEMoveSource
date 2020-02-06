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
    public class VW_RPT_SURVEY_15_1_A_Request
    {
        public DateTime CreatedDateFrom { get; set; }
        public DateTime CreatedDateTo { get; set; }
    }
    public class VW_RPT_SURVEY_15_1_A_DbQuery
    {
        [Column("SPORT_ID")]
        public int SportId { get; set; }
        [Column("SPORT_NAME")]
        public string SportName { get; set; }
        [Column("SUM_ATTR")]
        public int SumAttr { get; set; }
    }

}
