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
    public class MObjectivePersonController : Controller
    {
        private readonly AppDbContext _context;

        public MObjectivePersonController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.MObjectivePerson);
        }
        
        [HttpPost]
        public IActionResult Index(MObjectivePerson model)
        {
            var q = _context.MObjectivePerson as IQueryable<MObjectivePerson>;

            if (!string.IsNullOrEmpty(model.ObjectivePersonCode))
            {
                q = q.Where(a => a.ObjectivePersonCode.Contains(model.ObjectivePersonCode));
            }
            if (!string.IsNullOrEmpty(model.ObjectivePersonName))
            {
                q = q.Where(a => a.ObjectivePersonName.Contains(model.ObjectivePersonName));
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
        public IActionResult Create(MObjectivePerson model)
        {
            var q = new MObjectivePerson
            {
                ObjectivePersonCode = model.ObjectivePersonCode,
                ObjectivePersonName = model.ObjectivePersonName,
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
            var q = _context.MObjectivePerson.Where(a => a.ObjectivePersonId == id).FirstOrDefault();

            return View(q);
        }

        [HttpPost]
        public IActionResult Edit(MObjectivePerson model)
        {
            var q = _context.MObjectivePerson.Where(a => a.ObjectivePersonId == model.ObjectivePersonId).FirstOrDefault();
            if (q != null)
            {
                q.ObjectivePersonCode = model.ObjectivePersonCode;
                q.ObjectivePersonName = model.ObjectivePersonName;
                q.Status = model.Status;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();            
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(MObjectivePerson model)
        {
            var q = _context.MObjectivePerson.Where(a => a.ObjectivePersonId == model.ObjectivePersonId).FirstOrDefault();
            if (q != null)
            {
                _context.Entry(q).State = EntityState.Deleted;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}