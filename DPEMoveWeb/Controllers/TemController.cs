using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveWeb.Controllers
{
    public class TermController : Controller
    {
        private readonly AppDbContext _context;

        public TermController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var q = _context.CpeConfig.Where(a => a.Name == "Terms and Condition").FirstOrDefault();

            return View(q);
        }
    }
}