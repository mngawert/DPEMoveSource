using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Services
{
    public interface ICommentService
    {
        IEnumerable<CommentViewModel> GetComment(CommentViewModelReq model);
        Comment AddComment(CommentViewModel model);
    }
}
