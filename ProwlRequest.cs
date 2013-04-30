using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp.Net.Http;
using Crestron.SimplSharp.Net.Https;
using Nivloc.Web; // System.Web

namespace ProwlSimplSharp
{
    public class ProwlRequest : HttpsClientRequest
    {
        private const string ProwlApiUrl = "https://api.prowlapp.com/publicapi/";
                
        public ProwlRequest(string method, Dictionary<string, string> kwargs) : base()
        {
            string url = string.Format("{0}{1}{2}", ProwlApiUrl, method, EncodeParams(kwargs));
            Url.Parse(url);

            Encoding = Encoding.UTF8;
            RequestType = Crestron.SimplSharp.Net.Https.RequestType.Post;
            FinalizeHeader();
        }


        private string EncodeParams(Dictionary<string, string> paramDict)
        {
            // return early if given no params
            if (paramDict.Count == 0)
            {
                return "";
            }

            List<string> args = new List<string>();

            foreach (var kvp in paramDict)
            {
                args.Add(String.Format("{0}={1}", HttpUtility.UrlEncode(kvp.Key), HttpUtility.UrlEncode(kvp.Value)));
            }

            string result = string.Join("&", args.ToArray());
            return string.Format("?{0}", result);
        }
    }
}