using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;
using System;
using System.Linq;
using Vip.DFe.Tests.Serializer;
using Xunit;

namespace Vip.DFe.Tests.Serializer
{
    public class NFeRoundTripTests
    {
        [Fact]
        public void NFe_Carregar_FixtureModelo55_DeveMapearValoresChave()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();

            Assert.Equal("NFe35160810873538000245550010000000111504749126", nfe.InfNFe.Id);
            Assert.Equal(11, nfe.InfNFe.Ide.NumeroNFe);
            Assert.Equal(100m, nfe.InfNFe.Detalhe[0].Produto.VProd);

            var ibsCbs = nfe.InfNFe.Detalhe[0].Imposto.IbsCbs;
            Assert.Equal("000", ibsCbs.Cst);
            Assert.Equal("000001", ibsCbs.CClassTrib);
            Assert.Equal(0.10m, ibsCbs.GrupoIbsCbs.VIbs);
            Assert.Equal(0.90m, ibsCbs.GrupoIbsCbs.Cbs.VCbs);

            var tot = nfe.InfNFe.Total;
            Assert.Equal(100m, tot.IcmsTot.VNf);
            Assert.Equal(100m, tot.IbsCbsTot.VBcIbsCbs);
            Assert.Equal(0.10m, tot.IbsCbsTot.Ibs.VIbs);
            Assert.Equal(0.90m, tot.IbsCbsTot.Cbs.VCbs);
        }

        [Fact]
        public void NFe_RoundTrip_SerializarDesserializarSerializar_DeveGerarXmlIdentico()
        {
            var original = SerializerTestHelper.CarregarNFeModelo();
            var xml1 = original.GetXml();

            var recarregada = NFeDoc.Load(xml1);
            var xml2 = recarregada.GetXml();

            Assert.Equal(SerializerTestHelper.Normalizar(xml1), SerializerTestHelper.Normalizar(xml2));
        }

        [Fact]
        public void NFe_Serializar_FixtureModelo_DeveReproduzirFixtureOriginal()
        {
            var fixture = SerializerTestHelper.LerFixture("nfe-modelo55-ibscbs.xml");
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            Assert.Equal(SerializerTestHelper.Normalizar(fixture), SerializerTestHelper.Normalizar(xml));
        }

        [Fact]
        public void NFe_RoundTrip_DeveManterValoresChaveNoXml()
        {
            var xml = NFeDoc.Load(SerializerTestHelper.LerFixture("nfe-modelo55-ibscbs.xml")).GetXml();
            var n = SerializerTestHelper.Nfe;
            var doc = SerializerTestHelper.ParaXDocument(xml);

            Assert.Equal("NFe35160810873538000245550010000000111504749126", (string) doc.Root.Element(n + "infNFe").Attribute("Id"));
            Assert.Equal("11", (string) doc.Descendants(n + "nNF").Single());

            var det = doc.Descendants(n + "det").Single();
            Assert.Equal("100.00", (string) det.Element(n + "prod").Element(n + "vProd"));
            Assert.Equal("00", (string) det.Descendants(n + "ICMS00").Single().Element(n + "CST"));

            var ibsCbs = det.Element(n + "imposto").Element(n + "IBSCBS");
            Assert.Equal("000", (string) ibsCbs.Element(n + "CST"));
            Assert.Equal("000001", (string) ibsCbs.Element(n + "cClassTrib"));

            var tot = doc.Descendants(n + "IBSCBSTot").Single();
            Assert.Equal("0.10", (string) tot.Element(n + "gIBS").Element(n + "vIBS"));
            Assert.Equal("0.90", (string) tot.Element(n + "gCBS").Element(n + "vCBS"));
        }

        [Fact]
        public void NFe_GetXml_DevePreencherPropriedadeXml()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();

            var xml = nfe.GetXml();

            Assert.False(string.IsNullOrEmpty(nfe.Xml));
            Assert.Equal(xml, nfe.Xml);
        }
    }
}
