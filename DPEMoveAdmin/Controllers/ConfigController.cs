using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DPEMoveDAL.Models;
using DPEMoveDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DPEMoveWebApi.Controllers
{
    [Authorize]
    public class ConfigController : Controller
    {
        private readonly AppDbContext context;

        public ConfigController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {            
            return View(context.CpeConfig);
        }

        public IActionResult Create()
        {
            return View();
        }       

    }
}