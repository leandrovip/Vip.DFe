using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;
using SaveOptions = Vip.DFe.Enum.SaveOptions;

namespace Vip.DFe.Tests.Serializer
{
    /// <summary>
    ///     PISOutr/COFINSOutr: CST → choice obrigatório (vBC + pPIS/pCOFINS) ou (qBCProd + vAliqProd) → vPIS/vCOFINS.
    ///     Regressão VipERP #908: com alíquota zero (CST 49) o XML saía sem vBC/pPIS/pCOFINS/vCOFINS e era rejeitado.
    /// </summary>
    public class NFePisCofinsOutrTests
    {
        private static NFeDoc NFeCom(PisOutr pis, CofinsOutr cofins)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            var imposto = nfe.InfNFe.Detalhe.First().Imposto;
            imposto.Pis.Imposto = pis;
            imposto.Cofins.Imposto = cofins;
            return nfe;
        }

        private static XElement Grupo(string xml, string pai, string grupo) =>
            SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", pai, grupo);

        private static string[] Filhos(string xml, string pai, string grupo) =>
            SerializerTestHelper.NomesFilhos(Grupo(xml, pai, grupo));

        private static string Valor(string xml, string pai, string grupo, string tag) =>
            Grupo(xml, pai, grupo).Element(SerializerTestHelper.Nfe + tag)?.Value;

        [Fact]
        public void PisCofinsOutr_Cst49AliquotaZero_DeveGerarParPorAliquotaZerado()
        {
            var xml = NFeCom(new PisOutr {Cst = "49"}, new CofinsOutr {Cst = "49"}).GetXml();

            Assert.Equal(new[] {"CST", "vBC", "pPIS", "vPIS"}, Filhos(xml, "PIS", "PISOutr"));
            Assert.Equal("0.00", Valor(xml, "PIS", "PISOutr", "vBC"));
            Assert.Equal("0.0000", Valor(xml, "PIS", "PISOutr", "pPIS"));
            Assert.Equal("0.00", Valor(xml, "PIS", "PISOutr", "vPIS"));

            Assert.Equal(new[] {"CST", "vBC", "pCOFINS", "vCOFINS"}, Filhos(xml, "COFINS", "COFINSOutr"));
            Assert.Equal("0.00", Valor(xml, "COFINS", "COFINSOutr", "vBC"));
            Assert.Equal("0.0000", Valor(xml, "COFINS", "COFINSOutr", "pCOFINS"));
            Assert.Equal("0.00", Valor(xml, "COFINS", "COFINSOutr", "vCOFINS"));
        }

        [Fact]
        public void PisCofinsOutr_Cst99ComAliquota_DeveGerarParPorAliquotaComValores()
        {
            var xml = NFeCom(
                new PisOutr {Cst = "99", VBc = 100m, PPis = 1.65m, VPis = 1.65m},
                new CofinsOutr {Cst = "99", VBc = 100m, PCofins = 7.6m, VCofins = 7.6m}).GetXml();

            Assert.Equal(new[] {"CST", "vBC", "pPIS", "vPIS"}, Filhos(xml, "PIS", "PISOutr"));
            Assert.Equal("100.00", Valor(xml, "PIS", "PISOutr", "vBC"));
            Assert.Equal("1.6500", Valor(xml, "PIS", "PISOutr", "pPIS"));
            Assert.Equal("1.65", Valor(xml, "PIS", "PISOutr", "vPIS"));

            Assert.Equal(new[] {"CST", "vBC", "pCOFINS", "vCOFINS"}, Filhos(xml, "COFINS", "COFINSOutr"));
            Assert.Equal("7.6000", Valor(xml, "COFINS", "COFINSOutr", "pCOFINS"));
            Assert.Equal("7.60", Valor(xml, "COFINS", "COFINSOutr", "vCOFINS"));
        }

        [Fact]
        public void PisCofinsOutr_Cst99PorQuantidade_DeveGerarParPorQuantidadeSemVBc()
        {
            var xml = NFeCom(
                new PisOutr {Cst = "99", QBcProd = 10m, VAliqProd = 0.5m, VPis = 5m},
                new CofinsOutr {Cst = "99", QBcProd = 10m, VAliqProd = 2.3m, VCofins = 23m}).GetXml();

            Assert.Equal(new[] {"CST", "qBCProd", "vAliqProd", "vPIS"}, Filhos(xml, "PIS", "PISOutr"));
            Assert.Equal("10.0000", Valor(xml, "PIS", "PISOutr", "qBCProd"));
            Assert.Equal("0.5000", Valor(xml, "PIS", "PISOutr", "vAliqProd"));

            Assert.Equal(new[] {"CST", "qBCProd", "vAliqProd", "vCOFINS"}, Filhos(xml, "COFINS", "COFINSOutr"));
            Assert.Equal("23.00", Valor(xml, "COFINS", "COFINSOutr", "vCOFINS"));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void PisCofinsOutr_Desserializar_DeveManterParInformado(bool porQuantidade)
        {
            var original = porQuantidade
                ? NFeCom(new PisOutr {Cst = "99", QBcProd = 10m, VAliqProd = 0.5m, VPis = 5m},
                    new CofinsOutr {Cst = "99", QBcProd = 10m, VAliqProd = 2.3m, VCofins = 23m})
                : NFeCom(new PisOutr {Cst = "49"}, new CofinsOutr {Cst = "49"});
            var xml = original.GetXml();

            var recarregada = NFeDoc.Load(xml);

            var imposto = recarregada.InfNFe.Detalhe.First().Imposto;
            Assert.IsType<PisOutr>(imposto.Pis.Imposto);
            Assert.IsType<CofinsOutr>(imposto.Cofins.Imposto);
            Assert.Equal(SerializerTestHelper.Normalizar(xml), SerializerTestHelper.Normalizar(recarregada.GetXml()));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void PisCofinsOutr_NFeAssinada_DeveSerValidaContraXsd(bool porQuantidade)
        {
            var nfe = porQuantidade
                ? NFeCom(new PisOutr {Cst = "99", QBcProd = 10m, VAliqProd = 0.5m, VPis = 5m},
                    new CofinsOutr {Cst = "99", QBcProd = 10m, VAliqProd = 2.3m, VCofins = 23m})
                : NFeCom(new PisOutr {Cst = "49"}, new CofinsOutr {Cst = "49"});
            using var cert = NFeValidacaoXsdTests.CriarCertificadoAutoassinado();
            nfe.Assinar(cert, SaveOptions.DisableFormatting);

            SchemaHelper.ValidarXml(nfe.GetXml(), Path.Combine(SerializerTestHelper.CaminhoBase, "schemas", "nfe_v4.00.xsd"), out var erros, out _);

            Assert.True(erros.Length == 0, string.Join(Environment.NewLine, erros));
        }
    }
}
