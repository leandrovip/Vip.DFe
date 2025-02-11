using System.IO;
using System.Xml;

namespace Vip.DFe.Demo.Extensions
{
    public static class StringExtensions
    {
        public static string FormatarXml(this string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                using (var sw = new StringWriter())
                using (var xtw = new XmlTextWriter(sw) {Formatting = Formatting.Indented})
                {
                    doc.WriteTo(xtw);
                    return sw.ToString();
                }
            }
            catch
            {
                return "Erro ao formatar XML!";
            }
        }
    }
}