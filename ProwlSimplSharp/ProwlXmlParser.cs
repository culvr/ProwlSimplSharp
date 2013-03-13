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
                    DateTime resetDate = DateTime.Parse(el.Attribute("resetdate").Value);

                    return new ProwlSuccess { Code = code, Remaining = remaining, ResetDateTime = resetDate };
                }
                else
                {
                    return new ProwlError(el.Value) { Code = code };
                }

            }
            catch(Exception)
            {
                return new ProwlError(String.Format("Invalid Data: {0}", data));
            }
        }
    }
}