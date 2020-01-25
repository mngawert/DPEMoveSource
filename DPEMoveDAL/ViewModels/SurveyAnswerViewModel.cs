using System;
using System.Collections.Generic;
using System.Text;

namespace DPEMoveDAL.ViewModels
{

    public partial class SurveyAnswers
    {
        public SurveyAnswerViewModel[] SurveyAnswer { get; set; }
    }


    public partial class SurveyAnswerViewModel
    {
        public int SurveyAnswerId { get; set; }
        public int? QuestionId { get; set; }
        public string AnswerValue { get; set; }
        public string AnswerValue_1 { get; set; }
        public string AnswerValue_2 { get; set; }
        public string AnswerValue_3 { get; set; }
        public string AnswerValue_4 { get; set; }
        public string AnswerValue_5 { get; set; }
        public string AnswerText_1 { get; set; }
        public string AnswerText_2 { get; set; }
        public string AnswerText_3 { get; set; }
        public string AnswerText_4 { get; set; }
        public string AnswerText_5 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
