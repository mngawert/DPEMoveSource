using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveWeb.ApiControllers
{
    [Route("WebApi/Master/[action]")]
    [ApiController]
    public class MasterWebApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MasterWebApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<MIdcardType> GetIDCardType()
        {
            var q = _context.MIdcardType.ToList();

            return q;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<MAccountType> GetAccountType()
        {
            var q = _context.MAccountType.ToList();

            return q;
        }

        [HttpPost]
        //[Authorize]
        public IEnumerable<CpeConfig> GetConfig(CpeConfig model)
        {
            var q = _context.CpeConfig.ToList();

            if (!string.IsNullOrEmpty(model.Name))
            {
                q = q.Where(a => a.Name == model.Name).ToList();
            }

            return q;
        }

        [HttpGet]
        [Authorize]
        public CpeConfig GetTermAndCondition()
        {
            var q = _context.CpeConfig.Where(a => a.Name == "Terms and Condition").FirstOrDefault();

            return q;
        }

        [HttpPost]
        [Authorize]
        public IEnumerable<MSport> GetMSport()
        {
            var q = _context.MSport.ToList();

            return q;
        }

    }
}