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
    public class MVoteTypeController : Controller
    {
        private readonly AppDbContext _context;

        public MVoteTypeController(AppDbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View(_context.MVoteType);
        //}

        public IActionResult Index(MVoteTypeViewModel model)
        {
            var q = _context.MVoteType as IQueryable<MVoteType>;

            if (!string.IsNullOrEmpty(model.VoteTypeCode))
            {
                q = q.Where(a => a.VoteTypeCode.Contains(model.VoteTypeCode));
            }
            if (!string.IsNullOrEmpty(model.VoteType))
            {
                q = q.Where(a => a.VoteType.Contains(model.VoteType));
            }
            if (!string.IsNullOrEmpty(model.VoteOf))
            {
                q = q.Where(a => a.VoteOf.Contains(model.VoteOf));
            }
            if (model.Status != null)
            {
                q = q.Where(a => a.Status == model.Status);
            }
            q = q.OrderBy(a => a.VoteOf).ThenBy(b => b.VoteTypeId);

            return View(q);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MVoteType model)
        {
            var q = new MVoteType
            {
                VoteTypeCode = model.VoteTypeCode,
                VoteType = model.VoteType,
                VoteOf = model.VoteOf,
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
            var q = _context.MVoteType.Where(a => a.VoteTypeId == id).FirstOrDefault();

            return View(q);
        }

        [HttpPost]
        public IActionResult Edit(MVoteType model)
        {
            var q = _context.MVoteType.Where(a => a.VoteTypeId == model.VoteTypeId).FirstOrDefault();
            if (q != null)
            {
                q.VoteTypeCode = model.VoteTypeCode;
                q.VoteType = model.VoteType;
                q.VoteOf = model.VoteOf;
                q.Status = model.Status;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();            
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(MVoteType model)
        {
            var q = _context.MVoteType.Where(a => a.VoteTypeId == model.VoteTypeId).FirstOrDefault();
            if (q != null)
            {
                _context.Entry(q).State = EntityState.Deleted;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}