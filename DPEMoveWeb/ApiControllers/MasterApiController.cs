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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/master/[action]")]
    [ApiController]
    public class MasterApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MasterApiController(AppDbContext context)
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

        [HttpGet]
        [Authorize]
        public IEnumerable<CpeConfig> GetConfig()
        {
            var q = _context.CpeConfig.ToList();

            return q;
        }
        [HttpGet]
        [Authorize]
        public CpeConfig GetTermAndCondition()
        {
            var q = _context.CpeConfig.Where(a => a.Name == "Terms and Condition").FirstOrDefault();

            return q;
        }



    }
}