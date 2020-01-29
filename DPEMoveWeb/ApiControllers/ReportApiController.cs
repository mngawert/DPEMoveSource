using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DPEMoveWeb.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/report/[action]")]
    [ApiController]
    public class ReportApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ReportApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent1DbQuery> GetReportEvent1()
        {

            string sql = "select * from VW_RPT_EVENT_1";

            var q = _context.ReportEvent1DbQuery.FromSql(sql);

            return q.ToList();
        }

    }
}