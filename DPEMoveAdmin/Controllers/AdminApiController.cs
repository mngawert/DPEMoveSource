using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveAdmin.Controllers
{
    [Route("api/Admin/[action]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminApiController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult GetMGroup()
        {
            var q = _context.MGroup.ToList();

            return Ok(q);
        }
    }
}