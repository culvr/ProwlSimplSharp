using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp.Net.Https;
using Nivloc.Web; // System.Web

namespace ProwlSimplSharp
{
    public class ProwlRequest : HttpsClientRequest
    {
        private const string ProwlApiUrl = "api.prowlapp.com/publicapi/";
        private const string ProwlHttpProtocol = "https";
                
        public ProwlRequest(string method, Dictionary<string, string> kwargs) : base()
        {
            if (method == "add")
            {
                this.RequestType = RequestType.Post;
            }

            Url.Hostname = ProwlApiUrl;
            Url.Protocol = ProwlHttpProtocol;
            Url.Path = method;
            Url.Params = EncodeParams(kwargs);
        }


        private string EncodeParams(Dictionary<string, string> paramDict)
        {
            List<string> args = new List<string>();

            foreach (var kvp in paramDict)
            {
                args.Add(String.Format("{0}={1}", HttpUtility.UrlEncode(kvp.Key), HttpUtility.UrlEncode(kvp.Value)));
            }
            
            return String.Join("&", args.ToArray());
        }
    }
}