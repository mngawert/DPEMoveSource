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

            string sql = @"SELECT   event_id, event_code, event_name, a.status, a.created_date,
                                 a.created_by, b.account_type, b.account_type_name, b.GROUP_ID,
                                 b.group_name, c.province_code, c.amphur_code, c.tambon_code
                            FROM event a, vw_user b, address c
                           WHERE a.created_by = b.app_user_id AND a.address_id = c.address_id
                            ORDER BY 1
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
            if (!string.IsNullOrEmpty(model.ProvinceCode))
            {
                q = q.Where(a => a.ProvinceCode == model.ProvinceCode).ToList();
            }
            if (!string.IsNullOrEmpty(model.AmphurCode))
            {
                q = q.Where(a => a.AmphurCode == model.AmphurCode).ToList();
            }
            if (!string.IsNullOrEmpty(model.TambonCode))
            {
                q = q.Where(a => a.TambonCode == model.TambonCode).ToList();
            }

            q = q.OrderByDescending(a => a.EventId).ToList();

            var qq = PaginatedList<VW_EVENTS>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

        [HttpPost]
        public IActionResult DeleteEvent(EventViewModel3 model)
        {
            var q = _context.Event.Where(a => a.EventId == model.EventId).FirstOrDefault();

            var addressId = q.AddressId;

            _context.EventActivityType.RemoveRange(_context.EventActivityType.Where(a => a.EventId == q.EventId).ToList());
            _context.EventFacilities.RemoveRange(_context.EventFacilities.Where(a => a.EventId == q.EventId));
            _context.EventFee.RemoveRange(_context.EventFee.Where(a => a.EventId == q.EventId));
            _context.EventGoal.RemoveRange(_context.EventGoal.Where(a => a.EventId == q.EventId));
            _context.EventJoinPersonType.RemoveRange(_context.EventJoinPersonType.Where(a => a.EventId == q.EventId));
            _context.EventLevel.RemoveRange(_context.EventLevel.Where(a => a.EventId == q.EventId).ToList());
            _context.EventNearby.RemoveRange(_context.EventNearby.Where(a => a.EventId == q.EventId));
            _context.EventObjective.RemoveRange(_context.EventObjective.Where(a => a.EventId == q.EventId));
            _context.EventObjectivePerson.RemoveRange(_context.EventObjectivePerson.Where(a => a.EventId == q.EventId).ToList());
            _context.EventParticipant.RemoveRange(_context.EventParticipant.Where(a => a.EventId == q.EventId));
            _context.EventSport.RemoveRange(_context.EventSport.Where(a => a.EventId == q.EventId));

            _context.UploadedFile.RemoveRange(_context.UploadedFile.Where(a => a.EventUploadedFile.Any(b => b.EventId == q.EventId)));
            _context.EventUploadedFile.RemoveRange(_context.EventUploadedFile.Where(a => a.EventId == q.EventId));

            _context.Comment.RemoveRange(_context.Comment.Where(a => a.EventCode == q.EventCode));
            _context.Vote.RemoveRange(_context.Vote.Where(a => a.EventCode == q.EventCode));

            _context.Event.Remove(q);
            _context.Address.Remove(_context.Address.Where(a => a.AddressId == addressId).FirstOrDefault());


            _context.SaveChanges();

            return Ok();
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