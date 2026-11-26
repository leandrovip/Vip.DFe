using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.NotaFiscal.Detalhe;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs;
using Vip.DFe.NFe.NotaFiscal.Identificacao;
using Vip.DFe.NFe.NotaFiscal.Total;
using Xunit;

namespace Vip.DFe.Tests.Serializer
{
    public class NFeOpcionaisOmitidosTests
    {
        [Theory]
        [InlineData("IPI")]
        [InlineData("II")]
        [InlineData("PISST")]
        [InlineData("COFINSST")]
        [InlineData("ICMSUFDest")]
        [InlineData("IS")]
        [InlineData("indDoacao")]
        [InlineData("gIBSCBSMono")]
        [InlineData("gTransfCred")]
        [InlineData("gAjusteCompet")]
        [InlineData("gEstornoCred")]
        [InlineData("tpRegimeIBSCBS")]
        [InlineData("vUnComLiq")]
        [InlineData("vProdLiq")]
        [InlineData("cAtivSN")]
        [InlineData("gTribSN")]
        [InlineData("vProdLiqTot")]
        [InlineData("gCredPresOper")]
        [InlineData("gCredPresIBSZFM")]
        [InlineData("gTribRegular")]
        [InlineData("gTribCompraGov")]
        [InlineData("gMono")]
        [InlineData("ISSQNtot")]
        [InlineData("retTrib")]
        [InlineData("ISTot")]
        [InlineData("vNFTot")]
        [InlineData("cMunFGIBS")]
        [InlineData("dhSaiEnt")]
        [InlineData("dPrevEntrega")]
        [InlineData("tpNFDebito")]
        [InlineData("tpNFCredito")]
        [InlineData("indIntermed")]
        [InlineData("cIndOp")]
        [InlineData("dhCont")]
        [InlineData("xJust")]
        [InlineData("gCompraGov")]
        [InlineData("gPagAntecipado")]
        [InlineData("NFref")]
        [InlineData("DFeReferenciado")]
        [InlineData("impostoDevol")]
        [InlineData("obsItem")]
        [InlineData("vItem")]
        [InlineData("infNFeSupl")]
        [InlineData("Signature")]
        public void NFe_Serializar_ComGruposOpcionaisPadrao_NaoDeveEmitirTag(string tag)
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, tag), $"Tag <{tag}> nao deveria ser serializada.");
        }

        [Fact]
        public void NFe_Serializar_ComIbsCbsNulo_NaoDeveEmitirGrupoIbsCbsDoItem()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs = null;

            var xml = nfe.GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "IBSCBS"));
        }

        [Fact]
        public void NFe_Serializar_ComIbsCbsVazio_NaoDeveEmitirGrupoIbsCbsDoItem()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs = new IbsCbs();

            var xml = nfe.GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "IBSCBS"));
        }

        [Fact]
        public void NFe_Serializar_ComGrupoIbsCbsSemBase_NaoDeveEmitirGIbsCbs()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs.GrupoIbsCbs = new GrupoIbsCbs();

            var xml = nfe.GetXml();

            Assert.True(SerializerTestHelper.ContemTag(xml, "IBSCBS"));
            Assert.False(SerializerTestHelper.ContemTag(xml, "gIBSCBS"));
        }

        [Fact]
        public void NFe_Serializar_ComIbsCbsTotNulo_NaoDeveEmitirIbsCbsTot()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Total.IbsCbsTot = null;

            var xml = nfe.GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "IBSCBSTot"));
        }

        [Fact]
        public void NFe_Serializar_ComIbsCbsTotSemBase_NaoDeveEmitirIbsCbsTot()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Total.IbsCbsTot = new NFeIbsCbsTot();

            var xml = nfe.GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "IBSCBSTot"));
        }

        [Fact]
        public void NFe_Serializar_ComIsTotNulo_NaoDeveEmitirIsTot()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Total.IsTot = null;

            Assert.False(SerializerTestHelper.ContemTag(nfe.GetXml(), "ISTot"));
        }

        [Fact]
        public void NFe_Serializar_ComGCompraGovNulo_NaoDeveEmitirGCompraGov()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.GCompraGov = null;

            Assert.False(SerializerTestHelper.ContemTag(nfe.GetXml(), "gCompraGov"));
        }

        [Fact]
        public void NFe_Serializar_ComGCompraGovSemTipoOperacao_NaoDeveEmitirGCompraGov()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.GCompraGov = new NFeGCompraGov {TpEnteGov = NFeTipoCompraGov.Uniao, PRedutor = 1m};

            Assert.False(SerializerTestHelper.ContemTag(nfe.GetXml(), "gCompraGov"));
        }

        [Fact]
        public void NFe_Serializar_ComGPagAntecipadoNulo_NaoDeveEmitirGPagAntecipado()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.GPagAntecipado = null;

            Assert.False(SerializerTestHelper.ContemTag(nfe.GetXml(), "gPagAntecipado"));
        }

        [Fact]
        public void NFe_Serializar_ComDFeReferenciadoNulo_NaoDeveEmitirDFeReferenciado()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].DFeReferenciado = null;

            Assert.False(SerializerTestHelper.ContemTag(nfe.GetXml(), "DFeReferenciado"));
        }

        [Fact]
        public void NFe_Serializar_ComDFeReferenciadoSemChave_NaoDeveEmitirDFeReferenciado()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].DFeReferenciado = new NFeDFeReferenciado {NItem = 1};

            Assert.False(SerializerTestHelper.ContemTag(nfe.GetXml(), "DFeReferenciado"));
        }

        [Fact]
        public void NFe_Serializar_ComNFrefSomenteRefNFe_NaoDeveEmitirOutrosTiposDeReferencia()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.NFref.Add(new NFeNfRef {RefNFe = "35160810873538000245550010000000111504749126"});

            var xml = nfe.GetXml();

            Assert.True(SerializerTestHelper.ContemTag(xml, "refNFe"));
            foreach (var tag in new[] {"refNFeSig", "refNF", "refNFP", "refCTe", "refECF"})
                Assert.False(SerializerTestHelper.ContemTag(xml, tag), $"Tag <{tag}> nao deveria ser serializada.");
        }

        [Theory]
        [InlineData("Ipi")]
        [InlineData("II")]
        [InlineData("PisSt")]
        [InlineData("CofinsSt")]
        [InlineData("IcmsUfDest")]
        [InlineData("IS")]
        public void NFe_Serializar_ComGrupoOpcionalDoImpostoNulo_NaoDeveLancarNemEmitirTag(string propriedade)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            var imposto = nfe.InfNFe.Detalhe[0].Imposto;
            typeof(NFeDetImposto).GetProperty(propriedade).SetValue(imposto, null);

            var xml = nfe.GetXml();

            Assert.NotEmpty(xml);
        }
    }
}