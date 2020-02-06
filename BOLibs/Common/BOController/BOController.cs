using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BOLibs.Common.BOController
{
    public class BOController : Controller
    {
        private string SESSION_DPE_TOKEN = "SESSION_DPE_TOKEN";

        public BOController() : base()
        {

        }

        protected string CallDPEDataAPI(string strURL, string strFormData)
        {
            if (HttpContext.Session.GetString(SESSION_DPE_TOKEN) == null)
            {

            }

            return "";
        }

        protected string GetDPEToken()
        {
            using (HttpClient client = new HttpClient())
            {
            }

            return "";
        }
    }
}
