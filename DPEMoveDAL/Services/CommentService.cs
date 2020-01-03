using AutoMapper;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DPEMoveDAL.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //private User GetCurrentUser()
        //{
        //    string userName = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;
        //    return _context.User.Where(a => a.UserName == userName).FirstOrDefault();
        //}

        public CommentService(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public void AddComment(CommentViewModel model)
        {
            var q = new Comment
            {
                Comment1 = model.Comment1,
                CommentCode = "N/A",
                UserCode = model.UserCode,
                Status = 1,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
            };

            if (model.CommentOf == "1") 
                q.EventCode = model.EventOrStadiumCode;
            else
                q.StadiumCode = model.EventOrStadiumCode;

            _context.Add(q).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void EditComment(CommentViewModel model)
        {
            var q = _context.Comment.Where(a => a.CommentId == model.CommentId).FirstOrDefault();

            if (q != null)
            {
                q.Comment1 = model.Comment1;
                q.CommentCode = "N/A";
                q.UserCode = model.UserCode;
                q.Status = 1;
                q.CreatedBy = model.CreatedBy;
                q.UpdatedBy = model.CreatedBy;
                q.UpdatedDate = DateTime.Now;

                if (model.CommentOf == "1")
                    q.EventCode = model.EventOrStadiumCode;
                else
                    q.StadiumCode = model.EventOrStadiumCode;

                _context.Entry(q).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteComment(CommentViewModel model)
        {
            var q = _context.Comment.Where(a => a.CommentId == model.CommentId).FirstOrDefault();

            if (q != null)
            {
                _context.Remove(q).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        public IEnumerable<CommentViewModel> GetComment(CommentViewModel2 model)
        {
            string sql = "select * from VW_COMMENT";

            var q = _context.CommentDbQuery.FromSql(sql);

            /* Order by*/
            q = q.OrderBy(a => a.CreatedDate);

            var qq = PaginatedList<CommentDbQuery>.Create(q, model.LimitStart ?? 1, model.LimitSize ?? 10000);

            var q3 = _mapper.Map<List<CommentViewModel>>(qq);

            return q3;
        }

        public CommentDbQuery GetCommentDetails(int id)
        {
            string sql = "select * from VW_COMMENT where COMMENT_ID = {0}";

            var q = _context.CommentDbQuery.FromSql(sql, id).FirstOrDefault();

            return q;
        }
    }
}
