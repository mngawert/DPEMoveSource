using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPEMoveWeb.Controllers
{
    public class SurveyController : Controller
    {
        [Authorize(Roles = "SURVEY_VIEW")]
        public IActionResult Index()
        {
            return View();
        }
    }
}