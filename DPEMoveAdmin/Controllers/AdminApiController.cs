using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            //var q = _context.Event as IQueryable<Event>;

            string sql = @"SELECT EVENT_ID, EVENT_CODE, EVENT_NAME, STATUS, CREATED_DATE, CREATED_BY, B.ACCOUNT_TYPE, B.ACCOUNT_TYPE_NAME, b.GROUP_ID, b.GROUP_NAME
                            FROM event a, VW_USER b
                              where a.created_by = b.app_user_id
                            order by 1
                            ";

            var q = _context.VW_EVENTS.FromSql(sql).ToList();

            if (!string.IsNullOrEmpty(model.EventCode))
            {
                q = q.Where(a => a.EventCode.Contains(model.EventCode)).ToList();
            }
            if (!string.IsNullOrEmpty(model.EventName))
            {
                q = q.Where(a => a.EventName.Contains(model.EventName)).ToList();
            }
            if (model.Status != null)
            {
                q = q.Where(a => a.Status == model.Status).ToList();
            }
            if (model.GroupId != null)
            {
                q = q.Where(a => a.GroupId == model.GroupId).ToList();
            }
            if (!string.IsNullOrEmpty(model.AccountType))
            {
                q = q.Where(a => a.AccountType == model.AccountType).ToList();
            }

            q = q.OrderByDescending(a => a.EventId).ToList();

            var qq = PaginatedList<VW_EVENTS>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

        [HttpPost]
        public IActionResult GetUsers(UserViewModel3 model)
        {

            string sql = @"SELECT * from VW_USER order by 1";

            var q = _context.VW_USER.FromSql(sql).ToList();

            if (model.Email != null)
            {
                q = q.Where(a => a.EMAIL != null && a.EMAIL.Contains(model.Email)).ToList();
            }
            if (model.Name != null)
            {
                q = q.Where(a => a.NAME != null && a.NAME.Contains(model.Name)).ToList();
            }
            if (!string.IsNullOrEmpty(model.AccountType))
            {
                q = q.Where(a => a.ACCOUNT_TYPE == model.AccountType).ToList();
            }
            if (model.GroupId != null)
            {
                q = q.Where(a => a.GROUP_ID == model.GroupId).ToList();
            }

            var qq = PaginatedList<VW_USER>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

    }
}