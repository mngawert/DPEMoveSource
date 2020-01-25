using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class SurveyQuestion
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int? SurveyId { get; set; }
        public string Remarks { get; set; }
        public string SectionText { get; set; }

        public Survey Survey { get; set; }
    }
}
