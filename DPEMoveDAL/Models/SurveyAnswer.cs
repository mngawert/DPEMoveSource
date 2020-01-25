using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class SurveyAnswer
    {
        public SurveyAnswer()
        {
            SurveyAnswerDetails = new HashSet<SurveyAnswerDetails>();
        }

        public int SurveyAnswerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public ICollection<SurveyAnswerDetails> SurveyAnswerDetails { get; set; }
    }
}
