using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveAdmin.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required] string roleName)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(roleName);
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Redirect("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View();
        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

    }
}