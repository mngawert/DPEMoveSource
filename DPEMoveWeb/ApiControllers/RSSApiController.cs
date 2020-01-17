using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string GetProvince()
        {
            var client = new RestClient("http://data.dpe.go.th/api/information/location/getProvince");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Token", "eyJ0eXAiOiJqd3QiLCJhbGciOiJIUzI1NiJ9.eyJzZXNzaW9uX2lkIjoicnU0aGNiNjd2a2U5aDUzcG9iMHNmdGtzMW00dWdvdGciLCJjcmVhdGVkX2F0IjoiMjAyMC0wMS0xNiAxNDoxNDozMyIsImV4cGlyZWQiOiIyMDIwLTAxLTE3IDE0OjE0OjMzIn0.7h4_V2e0NZo7OhZJnBDk9LFk81Nbp8KQ80McLOXYKaQ");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);

            //Encoding encoding = Encoding.UTF8;
            //var result = encoding.GetString(response.RawBytes);

            return response.Content;
        }

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