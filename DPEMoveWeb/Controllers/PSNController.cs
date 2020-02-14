using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveWeb.Controllers
{
    public class PSNController : Controller
    {

        [Authorize(Roles = "PSN_VIEW")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "PSN_CREATE")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "PSN_EDIT")]
        public IActionResult Edit(int id)
        {
            return View();
        }

    }
}