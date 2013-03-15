using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp.Net.Https;

namespace ProwlSimplSharp
{
    public class ProwlClient : HttpsClient
    { 
        private List<string> _apiKeys;

        public ProwlClient() : base()
        {
            _apiKeys = new List<string>();
        }


        public int Send(string app, ushort priority, string url, string subject, string message)
        {
            if (_apiKeys.Count.Equals(0))
            {
                Crestron.SimplSharp.ErrorLog.Error("No API Keys have been addded. Notification not sent.");
                return -1;
            }

            string apiKeys = String.Join(",", _apiKeys.ToArray());

            var parameters = new Dictionary<string, string>()
               {{ "apikey", apiKeys },
                { "application", app },
                { "priority", priority.ToString() },
                { "url", url },
                { "event", subject },
                { "description", message }};

            var request = new ProwlRequest("add", parameters);
            return ProwlDispatch(request).Code;
        }


        private IProwlResponse ProwlDispatch(ProwlRequest request)
        {
            var response = TryDispatch(request);

            if (response.Code.Equals(200))
            {
                return new ProwlSuccess() { Code = 200 };
            }
            else
            {
                return new ProwlError(response.ContentString) { Code = response.Code };
            }
        }

        
        private bool IsValidKeyFormat(string key)
        {
            return !String.IsNullOrEmpty(key) && key.Length.Equals(40); // Order important here
        }


        public bool AddApiKey(string apiKey)                                                                                                            
        {
            if (IsValidKeyFormat(apiKey))
            {
                _apiKeys.Add(apiKey);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}