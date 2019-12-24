using System;
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

namespace DPEMoveWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly SmtpClient smtpClient;

        public AccountController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, SmtpClient smtpClient)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.smtpClient = smtpClient;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register_OLD(User user)
        {
            string email = user.Email;
            string password = user.Password;

            var userIdentity = new ApplicationUser { UserName = email, Email = email };
            var result = await userManager.CreateAsync(userIdentity, password);

            if (result == IdentityResult.Success)
            {
                return await Login(user);
            }
            else return BadRequest();
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
                    Status = "1"
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                            new { userId = user.Id, token = token }, Request.Scheme);

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
                    //new Claim("test1", "test1xx")
                };

                //claims.Add(new Claim(ClaimTypes.Role, "HOME_ABOUT"));

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
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);
                    
                    passwordResetLink = passwordResetLink.Replace("/api/Account/ResetPassword", "/Account/ResetPassword");
                    passwordResetLink = passwordResetLink.Replace("44375", "44348");

                    logger.Log(LogLevel.Warning, passwordResetLink);
                    smtpClient.Send (new MailMessage(from: configuration["Email:Smtp:From"], to: model.Email, subject: "DPEMove Reset Password", body: passwordResetLink));

                    return Ok(passwordResetLink);
                }

                return BadRequest();
            }

            return BadRequest();
        }

        // Fake Action
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return Ok();
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
        [Authorize(Roles= "HOME_VIEW")]
        public string Test_HOME_VIEW()
        {
            return "Test2_HOME_VIEW";
        }

    }
}