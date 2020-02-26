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
    [Authorize(Roles = "ADMIN_VIEW")]
    public class ConfigController : Controller
    {
        private readonly AppDbContext _context;

        public ConfigController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.CpeConfig);
        }
        
        [HttpPost]
        public IActionResult Index(CpeConfig model)
        {
            var q = _context.CpeConfig as IQueryable<CpeConfig>;

            if (!string.IsNullOrEmpty(model.Name))
            {
                q = q.Where(a => a.Name.Contains(model.Name));
            }
            if (!string.IsNullOrEmpty(model.Value))
            {
                q = q.Where(a => a.Value != null && a.Value.Contains(model.Value));
            }
            if (model.Status != null)
            {
                q = q.Where(a => a.Status == model.Status);
            }

            return View(q);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        public IActionResult Edit(int id)
        {
            var q = _context.CpeConfig.Where(a => a.ConfigId == id).FirstOrDefault();

            return View(q);
        }

        [HttpPost]
        public IActionResult EditConfig(CpeConfig model)
        {
            var q = _context.CpeConfig.Where(a => a.ConfigId == model.ConfigId).FirstOrDefault();
            if (q != null)
            {
                q.Value = model.Value;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();            
            }

            return RedirectToAction("Index");
        }

    }
}