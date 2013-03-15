using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Crestron.SimplSharp.CrestronXmlLinq;
using Crestron.SimplSharp.CrestronXml;

namespace ProwlSimplSharp
{
    public class ProwlXmlParser : IProwlParser
    {
        public IProwlResponse Parse(string data)
        {
            XDocument doc;
            XElement el;

            try
            {
                doc = XDocument.Parse(data);
                el = doc.Root.Elements().First();
                int code = int.Parse(el.Attribute("code").Value);

                if (code.Equals(200))
                {
                    int remaining = int.Parse(el.Attribute("remaining").Value);
                    return new ProwlSuccess { Code = code, Remaining = remaining };
                }
                else
                {
                    return new ProwlError(el.Value) { Code = code };
                }

            }
            catch (Exception e)
            {
                Crestron.SimplSharp.ErrorLog.Error("ProwlXmlParser.Parse", e.ToString());
                return new ProwlError(String.Format("Invalid Data: {0}", data)) { Code = -999 };
            }
        }
    }
}