using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DPEMoveDAL.Context;
using DPEMoveDAL.Helper;
using DPEMoveDAL.Models;
using DPEMoveDAL.Services;
using DPEMoveWeb.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DPEMoveWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<ISurveyService, SurveyService>();

            services.AddAutoMapper(typeof(MapperProfile));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<SmtpClient>((serviceProvider) =>
            {
                var smtpClient = new SmtpClient
                {
                    Host = Configuration["Email:Smtp:Host"],
                    Port = int.Parse(Configuration["Email:Smtp:Port"]),
                    Credentials = new NetworkCredential
                    {
                        UserName = Configuration["Email:Smtp:Username"],
                        Password = Configuration["Email:Smtp:Password"],
                    }
                };

                return smtpClient;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(a => a.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<AppDbContext>(a => 
            {
                a.UseOracle(Configuration["ConnectionStrings:DPEMoveDatabase"], b => b.UseOracleSQLCompatibility("11"));
                a.EnableSensitiveDataLogging(true);                
            });
            services.AddDbContext<AppIdentityDbContext>(options => options.UseOracle(Configuration["ConnectionStrings:DPEMoveDatabase"], b => b.UseOracleSQLCompatibility("11")));

            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireDigit = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequiredLength = 3;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services
            .AddAuthentication()
            .AddCookie(cfg => cfg.SlidingExpiration = true)
            .AddJwtBearer(xx =>
            {
                xx.RequireHttpsMetadata = false;
                xx.SaveToken = true;
                xx.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JwtIssuer"],
                    ValidAudience = Configuration["JwtAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            })
            .AddFacebook(options =>
            {
                options.AppId = Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            })
            ;

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            //app.UseMiddleware(typeof(VisitorCounterMiddleware));

            var supportedCultures = new[]
            {
                new CultureInfo("th-TH"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("th-TH"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
