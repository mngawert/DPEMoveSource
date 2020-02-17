using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveWeb.Controllers
{
    public class PSNController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PSNController(UserManager<ApplicationUser> userManager)
        {
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
                    return user.AppUserId;
                }
            }
            return -1;
        }

        private async Task<IList<string>> GetLoginRoles()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    return userRoles;
                }
            }
            return new List<string>();
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

        //[Authorize(Roles = "PSN_VIEW")]
        public async Task<IActionResult> Index()
        {
            ViewBag.AppUserId = await GetLoginAppUserId();

            return View();
        }

        //[Authorize(Roles = "PSN_CREATE")]
        public IActionResult Create()
        {
            return View();
        }

        //[Authorize(Roles = "PSN_EDIT")]
        public IActionResult Edit(int id)
        {
            ViewBag.routeId = id;
            ViewBag.appIdcardNo = GetLoginIdcardNo();

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.routeId = id;
            ViewBag.appIdcardNo = await GetLoginIdcardNo();

            ViewBag.Mode = "View";
            var roles = await GetLoginRoles();
            if (roles.Any(a => a == "PSN_EDIT"))
            {
                ViewBag.Mode = "Edit";
            }

            return View();
        }


    }
}