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
        private User GetCurrentUser()
        {
            string userName = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;
            return _context.User.Where(a => a.UserName == userName).FirstOrDefault();
        }

        public CommentService(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Comment AddComment(CommentViewModel model)
        {
            var q = _mapper.Map<Comment>(model);

            var user = GetCurrentUser();

            q.CommentCode = "CM_001";
            q.UserCode = user?.UserName ?? "N/A";
            q.CreatedBy = user?.UserId ?? 0;
            q.CreatedDate = DateTime.Now;

            _context.Add(q).State = EntityState.Added;
            _context.SaveChanges();

            return q;
        }


        public IEnumerable<CommentViewModel> GetComment(CommentViewModelReq model)
        {
            //var q = _context.Comment
            //    //.Select(a => _mapper.Map<CommentViewModel>(a))
            //    .Select(a => new CommentViewModel
            //    { 
            //        CommentCode = a.CommentCode,
            //        Comment1 = a.Comment1,
            //        UserCode = a.UserCode,
            //        EventCode = a.EventCode
            //    });
            //    ;

            string sql = "select a.* ,b.USER_NAME " +
                "from \"COMMENT\" a, \"USER\" b where a.CREATED_BY = b.USER_ID";

            var q = _context.CommentDbQuery.FromSql(sql);

            if (model.CommentOf == "1")
            {
                q = q.Where(a => a.EventCode == model.EventOrStadiumCode);
            }
            else if (model.CommentOf == "2")
            {
                q = q.Where(a => a.EventCode == model.EventOrStadiumCode);
            }

            /* Order by*/
            q = q.OrderBy(a => a.CreatedDate);

            var qq = PaginatedList<CommentDbQuery>.Create(q, model.LimitStart ?? 1, model.LimitSize ?? 10000);

            var q3 = _mapper.Map<List<CommentViewModel>>(qq);
            //var q3 = qq.Select(a => new CommentViewModel 
            //{
            //    CommentId = a.CommentId,
            //    CommentCode = a.CommentCode,
            //    Comment1 = a.Comment1,
            //    CreatedBy = a.CreatedBy,
            //    CreatedDate = a.CreatedDate,
            //    EventCode = a.EventCode,
            //    //StadiumCode = "",
            //    //Status = 1,
            //    UserCode = a.UserCode,
            //    UserName = a.UserName            
            //});

            return q3;
        }



    }
}
