using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SimpleFramework.Utils.Xml
{
    public static class XmlHelper
    {
        public static string ConvertListToXml(IEnumerable<string> list)
        {
            return new XElement("list", list.Where(c => c != null).Select(c => new XElement("item", c))).ToString();
        }

        public static string ConvertListToXml(IEnumerable<int> list)
        {
            return new XElement("list", list.Select(c => new XElement("item", c))).ToString();
        }
    }
}
