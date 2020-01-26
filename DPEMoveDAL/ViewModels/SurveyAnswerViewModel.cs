using System;
using System.Collections.Generic;
using System.Text;

namespace DPEMoveDAL.ViewModels
{

    public partial class SurveyAnswerViewModel
    {
        public SurveyAnswerDetailsViewModel[] SurveyAnswer { get; set; }
    }


    public partial class SurveyAnswerDetailsViewModel
    {
        public int SurveyAnswerId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerValue { get; set; }
        public string AnswerText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
