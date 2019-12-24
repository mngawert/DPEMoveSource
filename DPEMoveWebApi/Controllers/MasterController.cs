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

namespace DPEMoveWebApi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MasterController(AppDbContext context)
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


    }
}