using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp.Net.Http;
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
            return ProwlDispatch(request);
        }
        

        private int ProwlDispatch(ProwlRequest request)
        {
            try
            {
                 HttpsClientResponse response = Dispatch(request);
                 return response.Code;
            }
            catch (HttpsException)
            {
                return 0;
            }
        }


        private bool IsValidKeyFormat(string key)
        {
            return !String.IsNullOrEmpty(key) && key.Length.Equals(40); // Order important here
        }


        public int AddApiKey(string apiKey)                                                                                                            
        {
            if (IsValidKeyFormat(apiKey))
            {
                lock (_apiKeys)
                {
                    _apiKeys.Add(apiKey);
                }
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}