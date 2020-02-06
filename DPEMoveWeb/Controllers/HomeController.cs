using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BOLibs.Common.BOController;

namespace DPEMoveWeb.Controllers
{
    public class HomeController : BOController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}