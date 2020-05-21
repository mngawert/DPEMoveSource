using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
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

        [HttpPost]
        public IActionResult GetEvents(EventViewModel3 model)
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
            q = q.OrderByDescending(a => a.EventId);

            var qq = PaginatedList<Event>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

    }
}