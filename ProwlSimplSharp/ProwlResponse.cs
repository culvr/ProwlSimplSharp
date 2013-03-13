using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProwlSimplSharp
{
    abstract public class ProwlResponse : IProwlResponse
    {
        public int Code
        {
            get;
            set;
        }

        public bool IsSuccessful
        {
            get { return Code == 200; }
        }
    }
}