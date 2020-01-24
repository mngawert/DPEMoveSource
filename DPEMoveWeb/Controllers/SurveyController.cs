using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DPEMoveWeb.Controllers
{
    public class SurveyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SurveyController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

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

        public SurveyController(AppDbContext context, ILogger<SurveyController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize(Roles = "SURVEY_VIEW")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SURVEY_VIEW")]
        [HttpPost]
        public async Task<IActionResult> Create(SurveyAnswerViewModel model)
        {
            int appUserId = await GetLoginAppUserId();
            if (appUserId != -1)
            {
                var q_1 = new SurveyAnswer
                {
                    QuestionId = 1,
                    AnswerValue = model.AnswerValue_1,
                    CreatedDate = DateTime.Now,
                    CreatedBy = appUserId,
                };
                var q_2 = new SurveyAnswer
                {
                    QuestionId = 2,
                    AnswerValue = model.AnswerValue_2,
                    CreatedDate = DateTime.Now,
                    CreatedBy = appUserId,
                };

                _context.Entry(q_1).State = EntityState.Added;
                _context.Entry(q_2).State = EntityState.Added;
                _context.SaveChanges();
            }

            ViewBag.OKMessage = "OK";

            return View("CreateOK");
        }

    }
}