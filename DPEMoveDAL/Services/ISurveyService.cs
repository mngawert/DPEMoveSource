using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public interface ISurveyService
    {
        IEnumerable<VwSurveyAnswerDbQuery> GetVwSurveyAnswer(VwSurveyAnswerDbQuery model);
        IEnumerable<VwSurveyAnswerDetailsDbQuery> GetVwSurveyAnswerDetails(VwSurveyAnswerDetailsDbQuery model);
    }
}
