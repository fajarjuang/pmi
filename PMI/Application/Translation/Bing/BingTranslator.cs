using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;
using PMI.Application.Translation.Bing.Authentication;

namespace PMI.Application.Translation.Bing
{
    public static class BingTranslator
    {
        private static readonly string translateUrl = "http://api.microsofttranslator.com/V2/Http.svc/Translate?text={0}&from={1}&to={2}&contentType={3}";
        private static readonly string clientId = "13511";
        private static readonly string clientSecret = "ujuZ/1sgQaHxIfRiJ+S0FChsAzo1E2vf21EqqaiC6Dw=";

        public static string Translate(string text, string from, string to)
        {
            string url = BuildTranslateUrl(text, from, to);
            AdmAccessToken admToken;
            string headerValue;
            AdmAuthentication admAuth = new AdmAuthentication(clientId, clientSecret);
            admToken = admAuth.GetAccessToken();
            headerValue = "Bearer " + admToken.access_token;
            string translation;

            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            hwr.Headers.Add("Authorization", headerValue);

            WebResponse wr = null;
            try
            {
                wr = hwr.GetResponse();
                using (Stream stream = wr.GetResponseStream())
                {
                    DataContractSerializer dcs = new DataContractSerializer(Type.GetType("System.String"));
                    translation = (string)dcs.ReadObject(stream);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                wr.Close();
            }
            return translation;
        }

        private static string BuildTranslateUrl(string text, string from, string to)
        {
            return String.Format(translateUrl, HttpUtility.UrlEncode(text), from, to, "text/html");
        }
    }
}