using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class TmpSurveyDetail
    {
        public TmpSurveyDetail()
        {
            TmpSurveyDetail151 = new HashSet<TmpSurveyDetail151>();
        }

        public int SurveyDetailId { get; set; }
        public string SurveyDetailCode { get; set; }
        public int SurveyHeaderId { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }
        public string Ans5 { get; set; }
        public string Ans6 { get; set; }
        public string Ans7 { get; set; }
        public string Ans8 { get; set; }
        public string Ans6Etc { get; set; }
        public string Ans7Etc { get; set; }
        public string Ans8Etc { get; set; }
        public string Ans9 { get; set; }
        public string Ans10 { get; set; }
        public string Ans11 { get; set; }
        public string Ans9Etc { get; set; }
        public string Ans10Etc { get; set; }
        public string Ans11Etc { get; set; }
        public decimal? Ans12High { get; set; }
        public decimal? Ans12Weight { get; set; }
        public string Ans13 { get; set; }
        public string Ans14 { get; set; }
        public string Ans152 { get; set; }
        public string Ans152Etc { get; set; }
        public string Ans153 { get; set; }
        public string Ans154 { get; set; }
        public string Ans155 { get; set; }
        public string Ans154Etc { get; set; }
        public string Ans155Etc { get; set; }
        public string Ans16 { get; set; }
        public string Ans16Etc { get; set; }
        public string Ans17 { get; set; }
        public string Ans17Etc { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public TmpSurveyHeader SurveyHeader { get; set; }
        public ICollection<TmpSurveyDetail151> TmpSurveyDetail151 { get; set; }
    }
}
