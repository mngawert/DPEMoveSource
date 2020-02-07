using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BOLibs.Common.BOController;
using BOLibs.Common.BOObject;

namespace DPEMoveWeb.Controllers
{
    public class HomeController : BOController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetStadiumData()
        {
            PostData postData = new PostData();
            postData.Add("STADIUM_NAME", "โรงเรียนบ้านหอระฆัง");


            return Content(base.CallDPEDataAPI("http://data.dpe.go.th/api/stadium/address/getStadium", postData));
        }
    }
}