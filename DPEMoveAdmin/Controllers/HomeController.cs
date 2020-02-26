using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveAdmin.Controllers
{
    [Authorize(Roles ="ADMIN_VIEW") ]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}