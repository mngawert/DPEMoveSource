using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DPEMoveDAL.ViewModels;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;

namespace DPEMoveWeb.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Steps/[action]")]
    [ApiController]
    public class StepsApiController : ControllerBase
    {
        private readonly IStepService _stepService;

        public StepsApiController(IStepService StepService)
        {
            _stepService = StepService;
        }

        public IActionResult AddStep([FromBody] Step model)
        {
            try
            {
                var q = _stepService.AddStep(model);

                Response.Headers["ResponseCode"] = "2000";
                Response.Headers["ResponseDescription"] = "Ok";

                return Ok(q);
            }
            catch (Exception e)
            {
                Response.Headers["ResponseCode"] = "4000";
                Response.Headers["ResponseDescription"] = e.Message;
                return BadRequest(e.ToString());
            }
        }

    }
}