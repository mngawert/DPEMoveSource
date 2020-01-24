using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class SurveyQuestion
    {
        public SurveyQuestion()
        {
            SurveyAnswer = new HashSet<SurveyAnswer>();
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int? SurveyId { get; set; }
        public string Remarks { get; set; }
        public string SectionText { get; set; }

        public Survey Survey { get; set; }
        public ICollection<SurveyAnswer> SurveyAnswer { get; set; }
    }
}
