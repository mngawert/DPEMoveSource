using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;
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
        private readonly ISurveyService _surveyService;

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

        public SurveyController(AppDbContext context, ILogger<SurveyController> logger, UserManager<ApplicationUser> userManager, ISurveyService surveyService)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _surveyService = surveyService;
        }

        [Authorize(Roles = "SURVEY_VIEW")]
        public IActionResult Index()
        {
            return View("Create");
        }

        [Authorize(Roles = "SURVEY_VIEW")]
        public IActionResult List()
        {
            return View();
        }

        [Authorize(Roles = "SURVEY_VIEW")]
        public IActionResult Details(int id)
        {

            return View();
        }


    }
}