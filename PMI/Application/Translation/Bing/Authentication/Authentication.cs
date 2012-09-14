using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace PMI.Application.Translation.Bing.Authentication
{    public class AdmAuthentication
    {
        public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private string clientId;
        private string cientSecret;
        private string request;

        public AdmAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.cientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
        }

        public AdmAccessToken GetAccessToken()
        {
            return HttpPost(DatamarketAccessUri, this.request);
        }

        private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;
            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                //Get deserialized object from JSON stream
                AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }
    }


    /*        [DataContract]
        public class AccessToken
        {
            [DataMember]
            public string acess_token { get; set; }
            [DataMember]
            public string token_type { get; set; }
            [DataMember]
            public string expires_in { get; set; }
            [DataMember]
            public string scope { get; set; }
        }

        public class Authentication
        {
            public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";

            private string clientId;
            private string clientSecret;
            private string request;

            public Authentication(string clientId, string clientSecret)
            {
                this.clientId = clientId;
                this.clientSecret = clientSecret;

                this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope={2}", 
                    HttpUtility.UrlEncode(clientId), 
                    HttpUtility.UrlEncode(clientSecret),
                    HttpUtility.UrlEncode("http://api.microsofttranslator.com"));
            }

            public AccessToken GetAccessToken()
            {
                return HttpPost(DatamarketAccessUri, this.request);
            }

            private AccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
            {
                /*
                WebRequest wr = WebRequest.Create(DataMarketAccessUri);
                wr.ContentType = "application/x-www-form-urlencoded";
                wr.Method = "POST";
                byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
                wr.ContentLength = bytes.Length;

                using (Stream outputStream = wr.GetRequestStream())
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }

                using (WebResponse webResponse = wr.GetResponse())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AccessToken));
                    var a = webResponse.GetResponseStream().ToString();
                    AccessToken token = (AccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                    var b = (AccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                    var c = serializer.ReadObject(webResponse.GetResponseStream());
                    return token;
                }
         


                //Prepare OAuth request 
                WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
                byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
                webRequest.ContentLength = bytes.Length;
                using (Stream outputStream = webRequest.GetRequestStream())
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AccessToken));
                    //Get deserialized object from JSON stream
                    AccessToken token = (AccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                    return token;
                }
        
            }   
        }   */
}