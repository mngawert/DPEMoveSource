using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DPEMoveWebApi.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var qq = PaginatedList<Event>.Create(_context.Event, 1, 10);

            return View(qq);
        }
        
        [HttpPost]
        public IActionResult Index(EventViewModel3 model)
        {
            var q = _context.Event as IQueryable<Event>;

            if (!string.IsNullOrEmpty(model.EventCode))
            {
                q = q.Where(a => a.EventCode.Contains(model.EventCode));
            }
            if (!string.IsNullOrEmpty(model.EventName))
            {
                q = q.Where(a => a.EventName.Contains(model.EventName));
            }
            if (model.Status != null)
            {
                q = q.Where(a => a.Status == model.Status);
            }

            var qq = PaginatedList<Event>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10);

            return View(qq);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var q = _context.Event.Where(a => a.EventId == id).FirstOrDefault();

            return View(q);
        }

        [HttpPost]
        public IActionResult Edit(Event model)
        {
            var q = _context.Event.Where(a => a.EventId == model.EventId).FirstOrDefault();
            if (q != null)
            {
                //q.EventCode = model.EventCode;
                //q.EventName = model.EventName;
                q.Status = model.Status;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();            
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Event model)
        {
            var q = _context.Event.Where(a => a.EventId == model.EventId).FirstOrDefault();
            if (q != null)
            {
                _context.Entry(q).State = EntityState.Deleted;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}