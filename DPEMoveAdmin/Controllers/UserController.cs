using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DPEMoveAdmin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index(int? pageNumber, string email, string name)
        {

            string sql = @"SELECT * from VW_USER order by 1";

            var q = _context.VW_USER.FromSql(sql).ToList();

            if (email != null)
            {
                q = q.Where(a => a.EMAIL != null && a.EMAIL.Contains(email)).ToList();
            }
            if (name != null)
            {
                q = q.Where(a => a.NAME != null && a.NAME.Contains(name)).ToList();
            }

            int pageSize = 10;
            var qq = PaginatedList<VW_USER>.Create(q, pageNumber ?? 1, pageSize);

            return View(qq);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.routeId = id;

            string sql = @"SELECT * from VW_USER where APP_USER_ID = {0}";

            var q = _context.VW_USER.FromSql(sql, id).FirstOrDefault();

            return View(q);
        }
        public IActionResult EditUser(VW_USER model)
        {
            var q = _context.AspNetUsers.Where(a => a.AppUserId == model.APP_USER_ID).FirstOrDefault();
            if (q != null)
            {
                q.Name = model.NAME;
                q.GroupId = model.GROUP_ID;
                q.IdcardNo = model.ID_CARD_NO;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(VW_USER model)
        {
            var q = _context.AspNetUsers.Where(a => a.AppUserId == model.APP_USER_ID).FirstOrDefault();
            if (q != null)
            {
                _context.Entry(q).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}