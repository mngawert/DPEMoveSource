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
        IEnumerable<CommentViewModel> GetComment(CommentViewModel2 model);
        CommentDbQuery GetCommentDetails(int id);
        void AddComment(CommentViewModel model);
        void EditComment(CommentViewModel model);
        void DeleteComment(CommentViewModel model);
    }
}
