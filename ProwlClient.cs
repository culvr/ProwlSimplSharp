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
        private object ApiKeysLock = new object();

        public ProwlClient() : base()
        {
            _apiKeys = new List<string>();
        }


        private string ApiKeys
        {
            get
            {
                lock (ApiKeysLock)
                {
                    return string.Join(",", _apiKeys.ToArray());
                }
            }
        }


        public int Send(string app, short priority, string url, string subject, string message)
        {
            string keys = this.ApiKeys;
            
            if (String.IsNullOrEmpty(keys))
            {
                return -1;
            }

            var parameters = new Dictionary<string, string>()
               {{ "apikey", keys },
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
                lock (ApiKeysLock)
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

        public void RemoveApiKey(string key)
        {
            lock (ApiKeysLock)
            {
                _apiKeys.Remove(key);
            }
        }
    }
}