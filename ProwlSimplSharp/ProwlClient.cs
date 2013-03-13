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
        private IProwlParser _parser;

        public ProwlClient() : base()
        {
            _apiKeys = new List<string>();
            _parser = new ProwlXmlParser();
        }


        public int Send(string app, ushort priority, string url, string subject, string message)
        {
            string apiKeys = String.Join(",", _apiKeys.ToArray());
 
            var parameters = new Dictionary<string, string>()
               {{ "apikey", apiKeys },
                { "application", app },
                { "priority", priority.ToString() },
                { "url", url },
                { "event", subject },
                { "description", message }};

            var request = new ProwlRequest("add", parameters);
            var response = ProwlDispatch(request);

            return response.Code;
        }


        private IProwlResponse ProwlDispatch(ProwlRequest request)
        {
            var httpsResponse = TryDispatch(request);
            var prowlResponse = _parser.Parse(httpsResponse.ContentString);
            return prowlResponse;    
        }


        public void AddApiKey(string apiKey)
        {
            _apiKeys.Add(apiKey);
        }
    }
}