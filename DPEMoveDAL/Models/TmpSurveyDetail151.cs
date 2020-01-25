using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class TmpSurveyDetail151
    {
        public int SurveyDetail151Id { get; set; }
        public string SurveyDetail151Code { get; set; }
        public int SurveyDetailId { get; set; }
        public string Ans151 { get; set; }
        public string Ans151Detail { get; set; }
        public string Ans151Heavy { get; set; }
        public decimal? Ans151Min { get; set; }
        public int? Ans151Day { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public TmpSurveyDetail SurveyDetail { get; set; }
    }
}
