using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Vip.DFe.Writer;

namespace Vip.DFe.Extensions
{
    internal static class XDocumentExtensions
    {
        public static string AsString(this XContainer document, bool identado = false, bool showDeclaration = true)
        {
            return document.AsString(identado, showDeclaration, Encoding.UTF8);
        }

        public static string AsString(this XContainer document, bool identado, bool showDeclaration, Encoding encode)
        {
            var settings = new XmlWriterSettings
            {
                Indent = identado,
                Encoding = encode,
                OmitXmlDeclaration = !showDeclaration,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };

            using (var xmlString = new VipStringWriter(encode))
            using (var xmlTextWriter = XmlWriter.Create(xmlString, settings))
            {
                document.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return xmlString.ToString();
            }
        }

        public static TType GetValue<TType>(this XElement element, IFormatProvider format = null)
        {
            if (element == null) return default;

            TType ret;
            try
            {
                if (format == null) format = CultureInfo.InvariantCulture;

                ret = (TType) Convert.ChangeType(element.Value, typeof(TType), format);
            }
            catch (System.Exception)
            {
                ret = default;
            }

            return ret;
        }

        public static void RemoveEmptyNs(this XContainer doc)
        {
            foreach (var node in doc.Descendants())
            {
                if (!node.Name.NamespaceName.IsNullOrEmpty()) continue;

                node.Attributes("xmlns").Remove();
                if (node.Parent != null) node.Name = node.Parent.Name.Namespace + node.Name.LocalName;
            }
        }

        public static void AddChild(this XContainer parent, params XElement[] childrens)
        {
            if (childrens == null || parent == null) return;
            if (childrens.Length < 1) return;

            parent.Add(childrens);
        }

        public static void AddAttribute(this XContainer parent, params XAttribute[] attributes)
        {
            if (attributes == null || parent == null) return;
            if (attributes.Length < 1) return;

            parent.Add(attributes);
        }

        public static XElement[] ElementsAnyNs(this XContainer source, string name)
        {
            return source.Elements().Where(e => e.Name.LocalName == name).ToArray();
        }

        public static XElement ElementAnyNs(this XContainer source, string name)
        {
            return source.Elements().SingleOrDefault(e => e.Name.LocalName == name);
        }

        public static XmlDocument ToXmlDocument(this XDocument document)
        {
            using (var reader = document.CreateReader())
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(reader);
                return xmlDocument;
            }
        }
    }
}