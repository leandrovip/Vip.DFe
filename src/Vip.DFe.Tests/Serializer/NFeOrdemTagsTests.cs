using System;
using System.Linq;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;
using Vip.DFe.NFe.NotaFiscal.Total;
using Vip.DFe.Shared.Enum;
using Xunit;

namespace Vip.DFe.Tests.Serializer
{
    public class NFeOrdemTagsTests
    {
        // Sequencias conforme schemas/leiauteNFe_v4.00.xsd (total: linhas ~5333-5631; imposto: ~2106-5190)
        private static readonly string[] OrdemTotal = {"ICMSTot", "ISSQNtot", "retTrib", "ISTot", "IBSCBSTot", "vNFTot"};

        private static readonly string[] OrdemImposto = {"vTotTrib", "ICMS", "IPI", "II", "ISSQN", "PIS", "PISST", "COFINS", "COFINSST", "ICMSUFDest", "IS", "IBSCBS"};

        private static readonly string[] OrdemIssqnTot = {"vServ", "vBC", "vISS", "vPIS", "vCOFINS", "dCompet", "vDeducao", "vOutro", "vDescIncond", "vDescCond", "vISSRet", "cRegTrib"};

        private static readonly string[] OrdemIbsCbsTot = {"vBCIBSCBS", "gIBS", "gCBS", "gMono", "gEstornoCred"};

        private static readonly string[] OrdemIbsCbsItem = {"CST", "cClassTrib", "indDoacao", "gIBSCBS", "gIBSCBSMono", "gTransfCred", "gAjusteCompet", "gEstornoCred", "gCredPresOper", "gCredPresIBSZFM"};

        private static readonly string[] OrdemGIbsCbs = {"vBC", "gIBSUF", "gIBSMun", "vIBS", "gCBS", "gTribRegular", "gTribCompraGov"};

        private static NFeDoc CarregarComTotaisCompletos()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            var total = nfe.InfNFe.Total;
            total.IssqnTot = new NFeIssqnTot {VServ = 10m, VBc = 10m, VIss = 0.5m, DCompet = new DateTime(2026, 3, 15)};
            total.RetTrib = new NFeRetTrib {VRetPis = 1m};
            total.IsTot = new NFeIsTot {VIS = 2m};
            total.VNfTot = 102.1m;
            return nfe;
        }

        private static void AssertOrdemRelativa(string[] esperada, string[] atual)
        {
            var filtradaEsperada = esperada.Where(atual.Contains).ToArray();
            Assert.Equal(filtradaEsperada, atual);
        }

        [Fact]
        public void NFe_Serializar_Total_DeveSeguirSequenciaDoXsd()
        {
            var xml = CarregarComTotaisCompletos().GetXml();

            var filhos = SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, "infNFe", "total"));

            Assert.Equal(OrdemTotal, filhos);
        }

        [Fact]
        public void NFe_Serializar_TotalSemOpcionais_DeveManterSomenteIcmsTotEIbsCbsTot()
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            var filhos = SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, "infNFe", "total"));

            Assert.Equal(new[] {"ICMSTot", "IBSCBSTot"}, filhos);
        }

        [Fact]
        public void NFe_Serializar_DetImposto_DeveSeguirSequenciaDoXsd()
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            var filhos = SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto"));

            Assert.Equal(new[] {"vTotTrib", "ICMS", "PIS", "COFINS", "IBSCBS"}, filhos);
            AssertOrdemRelativa(OrdemImposto, filhos);
        }

        [Fact]
        public void NFe_Serializar_DetImpostoIbsCbs_DeveSeguirSequenciaDoXsd()
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();
            var ibsCbs = SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", "IBSCBS");

            AssertOrdemRelativa(OrdemIbsCbsItem, SerializerTestHelper.NomesFilhos(ibsCbs));
            AssertOrdemRelativa(OrdemGIbsCbs, SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", "IBSCBS", "gIBSCBS")));
        }

        [Fact]
        public void NFe_Serializar_TotalIbsCbsTot_DeveSeguirSequenciaDoXsd()
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            var filhos = SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, "infNFe", "total", "IBSCBSTot"));

            Assert.Equal(new[] {"vBCIBSCBS", "gIBS", "gCBS"}, filhos);
            AssertOrdemRelativa(OrdemIbsCbsTot, filhos);
        }

        [Fact]
        public void NFe_Serializar_IssqnTot_DeveSeguirSequenciaDoXsd()
        {
            var nfe = CarregarComTotaisCompletos();
            nfe.InfNFe.Total.IssqnTot.VDeducao = 1m;
            nfe.InfNFe.Total.IssqnTot.CRegTrib = RegimeTributario.SimplesNacional;

            var filhos = SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(nfe.GetXml(), "infNFe", "total", "ISSQNtot"));

            Assert.Contains("dCompet", filhos);
            AssertOrdemRelativa(OrdemIssqnTot, filhos);
        }
    }
}
