using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DPEMoveWeb.Controllers
{
    public class StadiumController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StadiumController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public StadiumController(AppDbContext context, ILogger<StadiumController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
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
        private async Task<int> GetLoginAppUserGroupId()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    _logger.LogDebug("user.GroupId={0}", user.GroupId);

                    return (int)(user.GroupId??-1);
                }
            }
            return -1;
        }

        private async Task<string> GetLoginIdcardNo()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    return user.IdcardNo;
                }
            }
            return null;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.AppUserGroupId = await GetLoginAppUserGroupId();
            ViewBag.appIdcardNo = await GetLoginIdcardNo();

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.routeId = id;
            ViewBag.AppUserId = await GetLoginAppUserId();
            //ViewBag.MVoteType = _context.MVoteType.Where(a => a.VoteOf == "2" && a.Status == 1).ToList();

            return View();
        }

    }
}