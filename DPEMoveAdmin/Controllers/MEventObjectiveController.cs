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
    public class MEventObjectiveController : Controller
    {
        private readonly AppDbContext _context;

        public MEventObjectiveController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.MEventObjective);
        }
        
        [HttpPost]
        public IActionResult Index(MEventObjective model)
        {
            var q = _context.MEventObjective as IQueryable<MEventObjective>;

            if (!string.IsNullOrEmpty(model.EventObjectiveCode))
            {
                q = q.Where(a => a.EventObjectiveCode.Contains(model.EventObjectiveCode));
            }
            if (!string.IsNullOrEmpty(model.EventObjectiveName))
            {
                q = q.Where(a => a.EventObjectiveName.Contains(model.EventObjectiveName));
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

        [HttpPost]
        public IActionResult Create(MEventObjective model)
        {
            var q = new MEventObjective
            {
                EventObjectiveCode = model.EventObjectiveCode,
                EventObjectiveName = model.EventObjectiveName,
                Status = model.Status,
                CreatedDate = DateTime.Now,
                CreatedBy = 0
            };

            _context.Entry(q).State = EntityState.Added;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var q = _context.MEventObjective.Where(a => a.MEventObjectiveId == id).FirstOrDefault();

            return View(q);
        }

        [HttpPost]
        public IActionResult Edit(MEventObjective model)
        {
            var q = _context.MEventObjective.Where(a => a.MEventObjectiveId == model.MEventObjectiveId).FirstOrDefault();
            if (q != null)
            {
                q.EventObjectiveCode = model.EventObjectiveCode;
                q.EventObjectiveName = model.EventObjectiveName;
                q.Status = model.Status;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();            
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(MEventObjective model)
        {
            var q = _context.MEventObjective.Where(a => a.MEventObjectiveId == model.MEventObjectiveId).FirstOrDefault();
            if (q != null)
            {
                _context.Entry(q).State = EntityState.Deleted;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}