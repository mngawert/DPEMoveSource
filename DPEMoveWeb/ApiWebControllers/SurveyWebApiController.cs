using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DPEMoveWeb.ApiWebControllers
{
    [Route("WebApi/Survey/[action]")]
    [ApiController]
    public class SurveyWebApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SurveyWebApiController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISurveyService _surveyService;
        public SurveyWebApiController(AppDbContext context, ILogger<SurveyWebApiController> logger, UserManager<ApplicationUser> userManager, ISurveyService surveyService)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _surveyService = surveyService;
        }

        private async Task<int> GetLoginAppUserId()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    _logger.LogDebug("user.AppUserId={0}", user.AppUserId);

                    return user.AppUserId;
                }
            }
            return -1;
        }

        [Authorize(Roles = "SURVEY_VIEW")]
        [HttpPost]
        public async Task<IActionResult> Create(SurveyAnswerViewModel model)
        {
            int appUserId = await GetLoginAppUserId();
            if (appUserId != -1)
            {
                var q = new SurveyAnswer
                {
                    SurveyId = model.SurveyId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = appUserId
                };

                _context.Entry(q).State = EntityState.Added;

                foreach (var m in model.SurveyAnswer)
                {
                    var q_details = new SurveyAnswerDetails
                    {
                        SurveyAnswerId = q.SurveyAnswerId,
                        QuestionId = m.QuestionId,
                        AnswerValue = m.AnswerValue,
                        AnswerText = m.AnswerText,
                        CreatedDate = DateTime.Now,
                        CreatedBy = appUserId,
                    };
                    _context.Entry(q_details).State = EntityState.Added;
                }
                _context.SaveChanges();
            }

            return Ok();
        }


        //[Authorize(Roles = "SURVEY_VIEW")]
        //[HttpPost]
        public IActionResult GetSurveyAnswer(VwSurveyAnswerDbQuery model)
        {
            var q = _surveyService.GetVwSurveyAnswer(model);

            return Ok(q);
        }

        //[Authorize(Roles = "SURVEY_VIEW")]
        //[HttpPost]
        public IActionResult GetSurveyAnswerDetails(VwSurveyAnswerDetailsDbQuery model)
        {
            var q = _surveyService.GetVwSurveyAnswerDetails(model);

            return Ok(q);
        }


    }
}