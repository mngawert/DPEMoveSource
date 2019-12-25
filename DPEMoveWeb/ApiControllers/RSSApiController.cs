using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace DPEMoveWeb.ApiControllers
{
    [Route("api/RSS/[action]")]
    [ApiController]
    public class RSSApiController : ControllerBase
    {

        public string GetNews()
        {
            var client = new RestClient("http://www.dpe.go.th/rss/news/?id=601000000001");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Referer", "http://www.dpe.go.th/rss/news/?id=601000000001");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Postman-Token", "0d17c722-76fe-4d99-96b8-948f54dde6e3,ffcdef6c-1697-4f7b-9243-7c69742e13cb");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            IRestResponse response = client.Execute(request);

            return response.Content;
        }





    }
}