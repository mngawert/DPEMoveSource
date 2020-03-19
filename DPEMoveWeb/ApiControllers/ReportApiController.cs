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
        public IEnumerable<VW_RPT_SURVEY_15_1_A_DbQuery> GetReportSurvey151A(VW_RPT_SURVEY_15_1_A_Request model)
        {
            string sql = @"
                SELECT   sport_name, SUM (sum_attr) AS sum_attr
                    FROM vw_rpt_survey_15_1_a
                   WHERE created_date BETWEEN {0} AND {1}
                GROUP BY sport_name
                 ORDER BY 1,2
                ";

            var q = _context.VW_RPT_SURVEY_15_1_A_DbQuery.FromSql(sql, model.CreatedDateFrom, model.CreatedDateTo);

            return q.ToList();
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent1DbQuery> GetReportEvent1(ReportEvent1Request model)
        {
            string sql = @"
                select prov_code, prov_namt, sum(no_of_events) as no_of_events
                from VW_RPT_EVENT_1
                where event_start between {0} and {1}
                group by prov_code, prov_namt
                order by 3 desc
                ";

            var q = _context.ReportEvent1DbQuery.FromSql(sql, model.EventDateFrom, model.EventDateTo);

            return q.ToList();
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent2DbQuery> GetReportEvent2(ReportEvent2Request model)
        {
            string sql = @"
                select * 
                from VW_RPT_EVENT_2 
                where ID_CARD = {0}
                ";

            var q = _context.ReportEvent2DbQuery.FromSql(sql, model.IdCard);

            return q.ToList();
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent3DbQuery> GetReportEvent3(ReportEvent4Request model)
        {
            string sql = @"
                select *
                from VW_RPT_EVENT_3
                where event_start_date between {0} and {1}
                order by EVENT_START_DATE
                ";

            var q = _context.ReportEvent3DbQuery.FromSql(sql, model.EventDateFrom, model.EventDateTo);

            return q.ToList();
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent4DbQuery> GetReportEvent4(ReportEvent4Request model)
        {
            string sql = @"
                select SECTION_CAT_ID, count(1) as NO_OF_EVENTS 
                from VW_RPT_EVENT_3
                where event_start_date between {0} and {1}
                group by SECTION_CAT_ID
                ";

            var q = _context.ReportEvent4DbQuery.FromSql(sql, model.EventDateFrom, model.EventDateTo);

            return q.ToList();
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent5DbQuery> GetReportEvent5(ReportEvent5Request model)
        {
            string sql = @"
                select SECTION_CAT_ID, count(1) as NO_OF_EVENTS 
                from VW_RPT_EVENT
                where EVENT_START_TIMESTAMP between {0} and {1}
                and (PROVINCE_CODE = {2} or {2} is null)
                group by SECTION_CAT_ID
                ";

            var q = _context.ReportEvent5DbQuery.FromSql(sql, model.EventDateFrom, model.EventDateTo, model.ProvinceCode);

            return q.ToList();
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent6DbQuery> GetReportEvent6(ReportEvent6Request model)
        {
            string sql = @"
                select a.EVENT_LEVEL_ID, b.EVENT_LEVEL_NAME, count(1) as NO_OF_EVENTS 
                from VW_RPT_EVENT a, M_EVENT_LEVEL b
                where EVENT_START_TIMESTAMP between {0} and {1}
                and (PROVINCE_CODE = {2} or {2} is null)
                and a.EVENT_LEVEL_ID = b.EVENT_LEVEL_ID
                group by a.EVENT_LEVEL_ID, b.EVENT_LEVEL_NAME
                ORDER BY 1
                ";

            var q = _context.ReportEvent6DbQuery.FromSql(sql, model.EventDateFrom, model.EventDateTo, model.ProvinceCode);

            return q.ToList();
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent7DbQuery> GetReportEvent7(ReportEvent7Request model)
        {
            string sql = @"
                select c.M_EVENT_OBJECTIVE_ID, c.EVENT_OBJECTIVE_NAME, count(1) as NO_OF_EVENTS 
                from VW_RPT_EVENT a, EVENT_OBJECTIVE b, M_EVENT_OBJECTIVE c
                where EVENT_START_TIMESTAMP between {0} and {1}
                and (PROVINCE_CODE = {2} or {2} is null)
                and a.EVENT_ID = b.EVENT_ID
                and b.M_EVENT_OBJECTIVE_ID = c.M_EVENT_OBJECTIVE_ID
                group by c.M_EVENT_OBJECTIVE_ID, c.EVENT_OBJECTIVE_NAME
                ORDER BY 1
                ";

            var q = _context.ReportEvent7DbQuery.FromSql(sql, model.EventDateFrom, model.EventDateTo, model.ProvinceCode);

            return q.ToList();
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<ReportEvent8DbQuery> GetReportEvent8(ReportEvent8Request model)
        {
            string sql = @"
                select b.ACT_TYPE_ID, count(1) as NO_OF_EVENTS 
                from VW_RPT_EVENT a, EVENT_ACTIVITY_TYPE b
                where 1=1
                --and EVENT_START_TIMESTAMP between {0} and {1}
                and (PROVINCE_CODE = {2} or {2} is null)
                and a.EVENT_ID = b.EVENT_ID
                and (a.SECTION_CAT_ID = {3} or {3} is null)
                group by b.ACT_TYPE_ID
                ORDER BY 2 desc
                ";

            var q = _context.ReportEvent8DbQuery.FromSql(sql, model.EventDateFrom, model.EventDateTo, model.ProvinceCode, model.SectionCatId);

            return q.ToList();
        }


    }
}