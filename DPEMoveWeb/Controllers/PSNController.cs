﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveWeb.Controllers
{
    public class PSNController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}