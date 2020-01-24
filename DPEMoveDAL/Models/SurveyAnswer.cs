using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class SurveyAnswer
    {
        public int SurveyAnswerId { get; set; }
        public int? QuestionId { get; set; }
        public string AnswerValue { get; set; }
        public string AnswerText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public SurveyQuestion Question { get; set; }
    }
}
