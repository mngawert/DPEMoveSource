using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using BOLibs.Common.BOObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BOLibs.Common.BOController
{
    public class BOController : Controller
    {
        private string SESSION_DPE_TOKEN = "SESSION_DPE_TOKEN";

        public BOController() : base()
        {

        }

        protected string CallDPEDataAPI(string strURL)
        {
            return CallDPEDataAPI(strURL, new PostData());
        }

            protected string CallDPEDataAPI(string strURL, PostData postData)
        {
            if (HttpContext.Session.GetString(SESSION_DPE_TOKEN) == null)
            {
                InitDPEToken();
            }

            return CallDPEExecute(strURL, postData);
        }

        private void InitDPEToken()
        {
            string strURLToke = "http://data.dpe.go.th/api/tokens/keys/tokens";

            PostData postData = new PostData(true);
            postData.Add("username", "dpeusers");
            postData.Add("password", "users_api@dpe.go.th");

            string jsonResult = this.CallDPEExecute(strURLToke, postData);
            string strToken = GetValueFromJSON("data", jsonResult);
            HttpContext.Session.SetString(SESSION_DPE_TOKEN, strToken);
        }

        private string CallDPEExecute(string strURL, PostData p_data)
        {
            string jsonReturn = "";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(strURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent contentPost = null;

                if (p_data.IsTokenRequest())
                {
                    contentPost = p_data.getDataToPostToken();

                }
                else
                {
                    contentPost = p_data.getDataToPostMultipartForm(HttpContext.Session.GetString(SESSION_DPE_TOKEN));

                    string strAccessToken = HttpContext.Session.GetString(SESSION_DPE_TOKEN).ToString();
                    client.DefaultRequestHeaders.Add("Token", strAccessToken);

                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strAccessToken);
                }

                var responseTask = client.PostAsync(strURL, contentPost);
                responseTask.Wait();

                var result = responseTask.Result;
                

                if (result.IsSuccessStatusCode)
                {
                    var dataReturn = result.Content.ReadAsStringAsync();
                    jsonReturn = dataReturn.Result;
                }
                else
                {
                    var dataReturn = result.Content.ReadAsStringAsync();
                    jsonReturn = dataReturn.Result;

                }
            }

            return jsonReturn;
        }

        protected string GetValueFromJSON(string p_strFieldName, string p_strJSON)
        {
            JObject jObject = JObject.Parse(p_strJSON);
            string strValue = (string)jObject.SelectToken(p_strFieldName);

            return strValue;
        }
    }
}
