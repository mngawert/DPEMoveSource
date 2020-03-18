using DPEMoveDAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Models
{
    public partial class AppDbContext : DbContext
    {
        public DbQuery<CommentDbQuery> CommentDbQuery { get; set; }
        public DbQuery<RoleDbQuery> RoleDbQuery { get; set; }
        public DbQuery<VoteDbQuery> VoteDbQuery { get; set; }
        public DbQuery<VoteAvgDbQuery> VoteAvgDbQuery { get; set; }
        public DbQuery<VoteTotalAvgDbQuery> VoteTotalAvgDbQuery { get; set; }
        public DbQuery<VoteTotalAvgDetailsDbQuery> VoteTotalAvgDetailsDbQuery { get; set; }
        public DbQuery<ReportEvent1DbQuery> ReportEvent1DbQuery { get; set; }
        public DbQuery<ReportEvent2DbQuery> ReportEvent2DbQuery { get; set; }
        public DbQuery<ReportEvent3DbQuery> ReportEvent3DbQuery { get; set; }
        public DbQuery<ReportEvent4DbQuery> ReportEvent4DbQuery { get; set; }
        public DbQuery<ReportEvent5DbQuery> ReportEvent5DbQuery { get; set; }
        public DbQuery<ReportEvent6DbQuery> ReportEvent6DbQuery { get; set; }
        public DbQuery<ReportEvent7DbQuery> ReportEvent7DbQuery { get; set; }
        public DbQuery<ReportEvent8DbQuery> ReportEvent8DbQuery { get; set; }
        public DbQuery<VW_RPT_SURVEY_15_1_A_DbQuery> VW_RPT_SURVEY_15_1_A_DbQuery { get; set; }
        public DbQuery<VwSurveyAnswerDbQuery> VwSurveyAnswerDbQuery { get; set; }
        public DbQuery<VwSurveyAnswerDetailsDbQuery> VwSurveyAnswerDetailsDbQuery { get; set; }
        public DbQuery<VW_USER> VW_USER { get; set; }
    }
}
