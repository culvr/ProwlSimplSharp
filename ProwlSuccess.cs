using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ProwlSimplSharp
{
    public class ProwlSuccess : ProwlResponse
    {
        public int Remaining
        {
            get;
            set;
        }

        public DateTime ResetDateTime
        {
            get;
            set;
        }
    }
}