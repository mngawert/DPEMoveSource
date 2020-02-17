using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DPEMoveWeb.ApiControllers
{
    [Route("WebApi/PSN/[action]")]
    [ApiController]
    public class PSNWebApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly ILogger<PSNWebApiController> _logger;
        private readonly SmtpClient _smtpClient;

        public PSNWebApiController(IConfiguration configuration, AppDbContext context, ILogger<PSNWebApiController> logger, SmtpClient smtpClient)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _smtpClient = smtpClient;
        }

        [HttpPost]
        //[Authorize]
        public IActionResult SendEmail(EmailViewModel model)
        {
            _logger.LogInformation("PSN Sending Mail {0} {1} {2}", model.To, model.Subject, model.Body );
            _smtpClient.Send(new MailMessage(from: _configuration["Email:Smtp:From"], to: model.To, subject: model.Subject, body: model.Body));

            return Ok();
        }

        [HttpPost]
        public IActionResult Test()
        {
            return Ok("hi");
        }

    }
}