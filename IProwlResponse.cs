using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ProwlSimplSharp
{
    public interface IProwlResponse
    {
        int Code { get; set; }
        bool IsSuccessful { get; }
    }
}