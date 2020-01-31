using AutoMapper;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SurveyService> _logger;
        public SurveyService(AppDbContext context, ILogger<SurveyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<VwSurveyAnswerDbQuery> GetVwSurveyAnswer(VwSurveyAnswerDbQuery model)
        {
            string sql = "select * from VW_SURVEY_ANSWER";

            var q = _context.VwSurveyAnswerDbQuery.FromSql(sql);

            if (!string.IsNullOrEmpty(model.CreatedByEmail))
            {
                q = q.Where(a => a.CreatedByEmail == model.CreatedByEmail);
            }


            return q.ToList();
        }

        public IEnumerable<VwSurveyAnswerDetailsDbQuery> GetVwSurveyAnswerDetails(VwSurveyAnswerDetailsDbQuery model)
        {
            string sql = "select * from VW_SURVEY_ANSWER_DETAILS";

            var q = _context.VwSurveyAnswerDetailsDbQuery.FromSql(sql);

            q = q.Where(a => a.SurveyAnswerId == model.SurveyAnswerId);

            return q.ToList();
        }
    }
}
