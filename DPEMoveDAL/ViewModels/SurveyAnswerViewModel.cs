using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DPEMoveDAL.ViewModels
{

    public partial class SurveyAnswerViewModel
    {
        public int? SurveyId { get; set; }
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

    public class VwSurveyAnswerDbQuery
    {
        [Column("SURVEY_ID")]
        public int SurveyId { get; set; }
        [Column("SURVEY_DESCRIPTION")]
        public string SurveyDescription { get; set; }
        [Column("SURVEY_ANSWER_ID")]
        public int SurveyAnswerId { get; set; }
        [Column("CREATED_BY_EMAIL")]
        public string CreatedByEmail { get; set; }
    }

    public class VwSurveyAnswerDetailsDbQuery
    {
        [Column("SURVEY_ID")]
        public int SurveyId { get; set; }
        [Column("SURVEY_DESCRIPTION")]
        public string SurveyDescription { get; set; }
        [Column("SURVEY_ANSWER_ID")]
        public int SurveyAnswerId { get; set; }
        [Column("CREATED_BY_EMAIL")]
        public string CreatedByEmail { get; set; }
        [Column("QUESTION_ID")]
        public int QuestionId { get; set; }
        [Column("QUESTION_TEXT")]
        public string QuestionText { get; set; }
        [Column("ANSWER_VALUE")]
        public string AnswerValue { get; set; }
        [Column("ANSWER_TEXT")]
        public string AnswerText { get; set; }
    }


}
