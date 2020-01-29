using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DPEMoveWebApi.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly AppDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper _mapper;

        public GroupController(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {            
            return View(context.MGroup);
        }

        [HttpPost]
        public IActionResult Index(GroupReqViewModel model)
        {
            var q = context.MGroup as IQueryable<MGroup>;

            if (model.GroupName != null)
                q = q.Where(a => a.GroupName.Contains(model.GroupName));
            if (model.Description != null)
                q = q.Where(a => a.Description.Contains(model.Description));
            if (model.Status != null)
                q = q.Where(a => a.Status == model.Status);

            return View(q);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required] string groupName, string description, int status)
        {
            if (ModelState.IsValid)
            {
                var q = new MGroup
                { 
                    GroupName = groupName,
                    Description = description,
                    Status = status
                };

                await context.AddRangeAsync(q);
                await context.SaveChangesAsync();

                return Redirect("Index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var rolegroup = await context.MGroup.Where(a => a.GroupId == id).FirstOrDefaultAsync();

            var members = new List<RoleViewModel>();

            if (rolegroup != null)
            {
                var listGroupRole = context.MGroupRole.Where(a => a.GroupId == id).ToList();

                var roleFromDb = context.RoleDbQuery.FromSql("select * from VW_ROLE");

                foreach (var role in roleFromDb)
                {
                    var r = new RoleViewModel
                    {
                        Name = role.Name,
                        Description = role.Description,
                        RoleIdView = role.RoleIdView,
                        RoleIdAdd = role.RoleIdAdd,
                        RoleIdEdit = role.RoleIdEdit,
                        RoleIdDelete = role.RoleIdDelete,
                        RoleIdPrint = role.RoleIdPrint
                    };

                    r.SelectedView = listGroupRole.Any(a => a.RoleId == role.RoleIdView);
                    r.SelectedAdd = listGroupRole.Any(a => a.RoleId == role.RoleIdAdd);
                    r.SelectedEdit = listGroupRole.Any(a => a.RoleId == role.RoleIdEdit);
                    r.SelectedDelete = listGroupRole.Any(a => a.RoleId == role.RoleIdDelete);
                    r.SelectedPrint = listGroupRole.Any(a => a.RoleId == role.RoleIdPrint);

                    members.Add(r);
                }
            }

            var q = new GroupViewModel
            {
                Group = rolegroup,
                Members = members
            };

            return View(q);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GroupEditViewModel model)
        {

            var group = context.MGroup.Find(model.GroupId);
            group.GroupName = model.GroupName;
            group.Description = model.Description;
            group.Status = model.Status;
            context.SaveChanges();

            foreach (var role in context.MGroupRole.Where(a => a.GroupId == model.GroupId))
            {
                context.Remove(role);
                context.SaveChanges();
            }

            foreach (var roleId in model.IdsToAdd ?? new string[] { })
            {
                var q = new MGroupRole { GroupId = model.GroupId, RoleId = roleId };
                context.MGroupRole.Add(q);
                context.SaveChanges();
            }

            foreach (var roleId in model.IdsToDelete ?? new string[] { })
            {
                var q = context.MGroupRole.Where(a => a.GroupId == model.GroupId && a.RoleId == roleId ).FirstOrDefault();
                if (q != null)
                {
                    context.Remove(q);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUsers(int id)
        {
            var group = await context.MGroup.Where(a => a.GroupId == id).FirstOrDefaultAsync();

            var members = new List<UserViewModel>();

            if (group != null)
            {
                foreach (var user in userManager.Users)
                {
                    var u = new UserViewModel 
                    { 
                        Id = user.Id,
                        UserName = user.UserName
                    };

                    u.Selected = user.GroupId == id;

                    members.Add(u);
                }
            }

            var q = new GroupUserViewModel
            {
                Group = group,
                Members = members
            };

            return View(q);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsers(GroupUserEditViewModel model)
        {
            foreach (var id in model.IdsToAdd ?? new string[] { })
            {
                var user = await userManager.FindByIdAsync(id);
                user.GroupId = model.GroupId;

                await userManager.UpdateAsync(user);
            }

            foreach (var id in model.IdsToDelete ?? new string[] { })
            {
                var user = await userManager.FindByIdAsync(id);
                user.GroupId = null;

                await userManager.UpdateAsync(user);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Required] int id)
        {
            if (ModelState.IsValid)
            {
                var q = await context.MGroup.Where(a => a.GroupId == id).FirstOrDefaultAsync();

                if (q != null)
                {
                    context.MGroup.Remove(q);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View();
        }


    }
}