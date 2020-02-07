using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BOLibs.Common.BOObject
{
    public class PostData
    {
        private Dictionary<string, string> m_internalData;
        private bool m_bolPostForToken = false;
        public bool PostDataOnly = true;
        private Dictionary<string, Dictionary<string, ByteArrayContent>> m_filePostData;

        public const string KEY_GRANT_TYPE = "grant_type";
        public const string KEY_USERNAME = "username";
        public const string KEY_PASSWORD = "password";

        public const string DATA_USER_ANONYMOUS = "TSTAnonymous";
        public const string DATA_GRANT_TYPE_PASSWORD = "password";

        public PostData()
        {
            this.m_internalData = new Dictionary<string, string>();
            this.m_filePostData = new Dictionary<string, Dictionary<string, ByteArrayContent>>();
        }

        public PostData(bool p_bolPostToken)
        {
            this.m_internalData = new Dictionary<string, string>();
            this.m_bolPostForToken = p_bolPostToken;
        }

        public bool IsTokenRequest()
        {
            return this.m_bolPostForToken;
        }

        public void Add(string p_strKey, string p_strValue)
        {
            this.m_internalData.Add(p_strKey, p_strValue);
        }

        public void AddFileData(string p_strKey, string p_strFileName, ByteArrayContent p_byte)
        {
            var dic = new Dictionary<string, ByteArrayContent>();
            dic.Add(p_strFileName, p_byte);

            this.m_filePostData.Add(p_strKey, dic);
        }

        public MultipartFormDataContent getDataToPostMultipartForm(string strToken)
        {
            if(!PostDataOnly)
            {
                /*HttpRequest httpRequest = HttpContext.Current;
                NameValueCollection formData = httpRequest.Form;

                foreach (string key in formData.AllKeys)
                {
                    if (key != null)
                    {
                        m_internalData.Add(key, formData[key]);
                    }
                }*/
            }

            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpContent DictionaryItems = new FormUrlEncodedContent(m_internalData);

            form.Add(new StringContent(strToken), "Token");

            foreach(string key in m_internalData.Keys)
            {
                form.Add(new StringContent(m_internalData[key]), key);
            }

            return form;
        }

        public HttpContent getDataToPostToken()
        {
            HttpContent DictionaryItems = new FormUrlEncodedContent(m_internalData);

            return DictionaryItems;
        }

        public HttpContent getDateToPost()
        {
            if(this.m_bolPostForToken)
            {
                return this.getDataToPostToken();

            } else
            {
                return null;
            }
        }
    }
}
