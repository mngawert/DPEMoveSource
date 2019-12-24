using DPEMoveDAL.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.Models
{
    public partial class AppDbContext : DbContext
    {
        public DbQuery<CommentDbQuery> CommentDbQuery { get; set; }
        public DbQuery<RoleDbQuery> RoleDbQuery { get; set; }

    }
}
