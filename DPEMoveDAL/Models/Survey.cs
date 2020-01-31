using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Survey
    {
        public Survey()
        {
            SurveyAnswer = new HashSet<SurveyAnswer>();
            SurveyQuestion = new HashSet<SurveyQuestion>();
        }

        public int SurveyId { get; set; }
        public string SurveyDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }

        public ICollection<SurveyAnswer> SurveyAnswer { get; set; }
        public ICollection<SurveyQuestion> SurveyQuestion { get; set; }
    }
}
