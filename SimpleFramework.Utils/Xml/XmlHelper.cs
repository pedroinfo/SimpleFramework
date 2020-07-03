using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SimpleFramework.Utils.Xml
{
    public static class XmlHelper
    {
        public static string SerializeToXml<T>(T t)
        {
            using (var stringWriter = new StringWriter(new StringBuilder()))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, t);
                return stringWriter.ToString();
            }
        }

        public static T DeserializeXml<T>(string xml)
        {
            using (var stringReader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

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
