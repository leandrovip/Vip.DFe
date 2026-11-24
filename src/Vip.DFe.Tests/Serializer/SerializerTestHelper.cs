using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.Serializer
{
    internal static class SerializerTestHelper
    {
        public static readonly XNamespace Nfe = "http://www.portalfiscal.inf.br/nfe";

        public static string CaminhoBase => AppContext.BaseDirectory;

        public static string LerFixture(string nome) =>
            File.ReadAllText(Path.Combine(CaminhoBase, "Fixtures", nome));

        public static NFeDoc CarregarNFeModelo()
            => NFeDoc.Load(LerFixture("nfe-modelo55-ibscbs.xml"));

        public static XDocument ParaXDocument(string xml) => XDocument.Parse(xml, LoadOptions.None);

        /// <summary>Normaliza removendo declaracao, comentarios e whitespace entre elementos.</summary>
        public static string Normalizar(string xml)
        {
            var doc = XDocument.Parse(xml, LoadOptions.None);
            return doc.Root.ToString(SaveOptions.DisableFormatting);
        }

        public static XElement Elemento(string xml, params string[] caminho)
        {
            XElement atual = XDocument.Parse(xml).Root;
            foreach (var nome in caminho)
                atual = atual.Elements().FirstOrDefault(e => e.Name.LocalName == nome);
            return atual;
        }

        public static string[] NomesFilhos(XElement el) => el.Elements().Select(e => e.Name.LocalName).ToArray();

        public static bool ContemTag(string xml, string nomeLocal) =>
            XDocument.Parse(xml).Descendants().Any(e => e.Name.LocalName == nomeLocal);
    }
}
