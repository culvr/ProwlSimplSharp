using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ProwlSimplSharp
{
    public class ProwlError : ProwlResponse
    {
        private string _message;

        public string Message
        {
            get { return _message; }
        }

        public ProwlError(string errorMsg)
        {
            _message = errorMsg;
        }
    }
}