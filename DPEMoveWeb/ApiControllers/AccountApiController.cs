﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DPEMoveWeb.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/account/[action]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AccountApiController> logger;
        private readonly SmtpClient smtpClient;

        public AccountApiController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountApiController> logger, SmtpClient smtpClient)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.smtpClient = smtpClient;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    IdcardType = model.IdcardType,
                    IdcardNo = model.IdcardNo,
                    AccountType = model.AccountType,
                    GroupId = model.GroupId,
                    Status = "1",
                    PrefixId = model.PrefixId,
                    Surname = model.Surname,
                    TelNo = model.TelNo
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                            new { userId = user.Id, token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, confirmationLink);
                    smtpClient.Send(new MailMessage(from: configuration["Email:Smtp:From"], to: model.Email, subject: "DPEMove Confirm User", body: confirmationLink));

                    return Ok(confirmationLink);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User user)
        {
            return await GetToken(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> FacebookLogin(User model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    AccountType = "1",
                    Status = "1",
                    GroupId = 1,
                    FacebookId = model.FacebookId
                };

                await userManager.CreateAsync(user);
            }

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JwtIssuer"],
                audience: configuration["JwtAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken(User user)
        {
            string email = user.Email;
            string password = user.Password;

            // get the user to verifty
            var userToVerify = await userManager.FindByEmailAsync(email);

            if (userToVerify == null) return BadRequest();

            // check the credentials
            if (await userManager.CheckPasswordAsync(userToVerify, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userToVerify.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var userRoles = await userManager.GetRolesAsync(userToVerify);
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JwtIssuer"],
                    audience: configuration["JwtAudience"],
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetReadOnlyToken()
        {
            var user = new User
            {
                Email = configuration["ReadOnlyToken:Email"],
                Password = configuration["ReadOnlyToken:Password"]
            };

            return await GetToken(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token }, Request.Scheme);

                    //passwordResetLink = passwordResetLink.Replace("/api/Account/ResetPassword", "/Account/ResetPassword");
                    //passwordResetLink = passwordResetLink.Replace("44375", "44348");

                    logger.Log(LogLevel.Warning, passwordResetLink);
                    smtpClient.Send(new MailMessage(from: configuration["Email:Smtp:From"], to: model.Email, subject: "DPEMove Reset Password", body: passwordResetLink));

                    return Ok(passwordResetLink);
                }

                return BadRequest();
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetProfile(UserViewModel2 model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null) 
                return BadRequest();

            var q = new UserViewModel2
            {
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                IdcardType = user.IdcardType,
                IdcardNo = user.IdcardNo,
                AccountType = user.AccountType,
                GroupId = user.GroupId,
                AppUserId = user.AppUserId,
                Status = user.Status,
                FacebookId = user.FacebookId,
                BirthDate = user.BirthDate,
                Height = user.Height,
                Weight = user.Weight,
                PrefixId = user.PrefixId,
                Surname = user.Surname,
                TelNo = user.TelNo
            };

            return Ok(q);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UserViewModel2 model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return BadRequest();

            user.UserName = model.UserName;
            user.Name = model.Name;
            user.IdcardType = model.IdcardType;
            user.IdcardNo = model.IdcardNo;
            user.AccountType = model.AccountType;
            user.GroupId = model.GroupId;
            user.Status = model.Status;
            user.FacebookId = model.FacebookId;
            user.BirthDate = model.BirthDate;
            user.Height = model.Height;
            user.Weight = model.Weight;
            user.PrefixId = model.PrefixId;
            user.Surname = model.Surname;
            user.TelNo = model.TelNo;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok();

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        //[HttpGet("claims")]
        public object Claims()
        {
            return User.Claims.Select(c =>
            new
            {
                c.Type,
                c.Value
            });
        }

        [HttpGet]
        public IEnumerable<string> Test()
        {
            smtpClient.Send(new MailMessage(from: "dpemove@bbss.co.th", to: "montrin@gmail.com", subject: "DPEMove Reset Password", body: "passwordResetLink"));

            return new string[] { "Data_1", "Data_2" };
        }

        [HttpPost]
        [Authorize(Roles = "HOME_VIEW")]
        public string Test_HOME_VIEW()
        {
            return "Test2_HOME_VIEW";
        }


        [HttpPost]
        [AllowAnonymous]
        public string GetDPEToken()
        {
            var client = new RestClient("https://data.dpe.go.th/api/tokens/keys/tokens");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            
            request.AddParameter("username", configuration["DPEToken:Username"]);
            request.AddParameter("password", configuration["DPEToken:Password"]);
            IRestResponse response = client.Execute(request);

            return response.Content;

            //return GetValueFromJSON("data", response.Content);
        }


        protected string GetValueFromJSON(string p_strFieldName, string p_strJSON)
        {
            JObject jObject = JObject.Parse(p_strJSON);
            string strValue = (string)jObject.SelectToken(p_strFieldName);

            return strValue;
        }

    }
}