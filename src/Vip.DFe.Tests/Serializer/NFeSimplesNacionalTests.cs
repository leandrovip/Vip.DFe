using System;
using System.IO;
using Vip.DFe.Enum;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs;
using Vip.DFe.NFe.NotaFiscal.Total;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.Serializer
{
    /// <summary>
    ///     NT 2026.005 (IBS/CBS no Simples Nacional) e NT 2026.008 (valor líquido; ICMS previsto no pagamento
    ///     antecipado).
    /// </summary>
    public class NFeSimplesNacionalTests
    {
        private static NFeDoc NFeCompleta()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Emitente.TpRegimeIbsCbs = TipoRegimeIbsCbs.SimplesNacionalComRegimeRegular;

            var prod = nfe.InfNFe.Detalhe[0].Produto;
            prod.VUnComLiq = 90m;
            prod.VProdLiq = 90m;
            prod.CAtivSN = CodigoAtividadeSimplesNacional.RevendaMercadoriasAnexoI;

            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs.TributacaoSimplesNacional = new TributacaoSimplesNacional
            {
                IbsSN = new IbsSimplesNacional {PIbsSN = 0.1m, VIbsSN = 0.09m},
                CbsSN = new CbsSimplesNacional {PCbsSN = 0.9m, VCbsSN = 0.81m}
            };

            nfe.InfNFe.Total.VProdLiqTot = 90m;
            nfe.InfNFe.Total.IbsCbsTot.TribSN = new NFeTribSnTot {VIbsSN = 0.09m, VCbsSN = 0.81m};
            return nfe;
        }

        private static NFeDoc NFeComIcmsPrevisto()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.Imposto = new Icms {Tipo = new IcmsPrevistoPagtoAntecip {VIcmsPrevisto = 18m}};
            return nfe;
        }

        private static string[] Filhos(string xml, params string[] caminho) => SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, caminho));

        [Fact]
        public void Emit_TpRegimeIbsCbs_DeveSerializarAposCrt()
        {
            var xml = NFeCompleta().GetXml();

            var filhos = Filhos(xml, "infNFe", "emit");
            Assert.Equal("tpRegimeIBSCBS", filhos[^1]);
            Assert.Equal("CRT", filhos[^2]);
            Assert.Equal("1", SerializerTestHelper.Elemento(xml, "infNFe", "emit", "tpRegimeIBSCBS").Value);
        }

        [Fact]
        public void Prod_ValorLiquido_DeveSerializarAposVProdEAntesDeCEANTrib()
        {
            var xml = NFeCompleta().GetXml();

            var filhos = Filhos(xml, "infNFe", "det", "prod");
            var i = Array.IndexOf(filhos, "vProd");
            Assert.Equal(new[] {"vProd", "vUnComLiq", "vProdLiq", "cEANTrib"}, filhos[i..(i + 4)]);
            Assert.Equal("90.0000000000", SerializerTestHelper.Elemento(xml, "infNFe", "det", "prod", "vUnComLiq").Value);
            Assert.Equal("90.00", SerializerTestHelper.Elemento(xml, "infNFe", "det", "prod", "vProdLiq").Value);
        }

        [Fact]
        public void Prod_CAtivSN_DeveSerializarAposIndTotEAntesDeOpcionaisSeguintes()
        {
            var xml = NFeCompleta().GetXml();

            var filhos = Filhos(xml, "infNFe", "det", "prod");
            Assert.Equal("indTot", filhos[^2]);
            Assert.Equal("cAtivSN", filhos[^1]);
            Assert.Equal("01", SerializerTestHelper.Elemento(xml, "infNFe", "det", "prod", "cAtivSN").Value);
        }

        [Fact]
        public void Prod_VUnComLiqSemVProdLiq_NaoDeveEmitirNenhum()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Produto.VUnComLiq = 90m;

            var xml = nfe.GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "vUnComLiq"));
            Assert.False(SerializerTestHelper.ContemTag(xml, "vProdLiq"));
        }

        [Fact]
        public void Prod_VProdLiqSemVUnComLiq_NaoDeveEmitirNenhum()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Produto.VProdLiq = 90m;

            var xml = nfe.GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "vUnComLiq"));
            Assert.False(SerializerTestHelper.ContemTag(xml, "vProdLiq"));
        }

        [Fact]
        public void ItemIbsCbs_GTribSN_DeveSerializarEntreGIbsCbsEGEstornoCred()
        {
            var nfe = NFeCompleta();
            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs.EstornoCredito = new EstornoCredito {VIbsEstCred = 1m, VCbsEstCred = 1m};

            var xml = nfe.GetXml();

            var filhos = Filhos(xml, "infNFe", "det", "imposto", "IBSCBS");
            Assert.Equal(new[] {"CST", "cClassTrib", "gIBSCBS", "gTribSN", "gEstornoCred"}, filhos);
            Assert.Equal(new[] {"gIBSSN", "gCBSSN"}, Filhos(xml, "infNFe", "det", "imposto", "IBSCBS", "gTribSN"));
            Assert.Equal(new[] {"pIBSSN", "vIBSSN"}, Filhos(xml, "infNFe", "det", "imposto", "IBSCBS", "gTribSN", "gIBSSN"));
            Assert.Equal(new[] {"pCBSSN", "vCBSSN"}, Filhos(xml, "infNFe", "det", "imposto", "IBSCBS", "gTribSN", "gCBSSN"));
            Assert.Equal("0.1000", SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", "IBSCBS", "gTribSN", "gIBSSN", "pIBSSN").Value);
            Assert.Equal("0.81", SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", "IBSCBS", "gTribSN", "gCBSSN", "vCBSSN").Value);
        }

        [Fact]
        public void ItemIbsCbs_GTribSNSemGIbsSN_DeveOmitirGIbsSN()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs.TributacaoSimplesNacional = new TributacaoSimplesNacional
            {
                CbsSN = new CbsSimplesNacional {PCbsSN = 0.9m, VCbsSN = 0.9m}
            };

            var xml = nfe.GetXml();

            Assert.Equal(new[] {"gCBSSN"}, Filhos(xml, "infNFe", "det", "imposto", "IBSCBS", "gTribSN"));
        }

        [Fact]
        public void TotalIbsCbs_GTribSN_DeveSerializarComoUltimoFilho()
        {
            var nfe = NFeCompleta();
            nfe.InfNFe.Total.IbsCbsTot.EstornoCredito = new NFeEstornoCreditoTot {VIbsEstCred = 1m, VCbsEstCred = 1m};

            var xml = nfe.GetXml();

            var filhos = Filhos(xml, "infNFe", "total", "IBSCBSTot");
            Assert.Equal("gEstornoCred", filhos[^2]);
            Assert.Equal("gTribSN", filhos[^1]);
            Assert.Equal(new[] {"vIBSSN", "vCBSSN"}, Filhos(xml, "infNFe", "total", "IBSCBSTot", "gTribSN"));
        }

        [Fact]
        public void Total_VProdLiqTot_DeveSerPrimeiroFilhoDeTotal()
        {
            var xml = NFeCompleta().GetXml();

            var filhos = Filhos(xml, "infNFe", "total");
            Assert.Equal("vProdLiqTot", filhos[0]);
            Assert.Equal("ICMSTot", filhos[1]);
            Assert.Equal("90.00", SerializerTestHelper.Elemento(xml, "infNFe", "total", "vProdLiqTot").Value);
        }

        [Fact]
        public void Total_VProdLiqTotZeroInformado_DeveSerEmitido()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Total.VProdLiqTot = 0m;

            var xml = nfe.GetXml();

            Assert.Equal("0.00", SerializerTestHelper.Elemento(xml, "infNFe", "total", "vProdLiqTot").Value);
        }

        [Fact]
        public void Icms_PrevistoPagtoAntecip_DeveSerializarGrupoComVIcmsPrevisto()
        {
            var xml = NFeComIcmsPrevisto().GetXml();

            Assert.Equal(new[] {"ICMSPrevistoPagtoAntecip"}, Filhos(xml, "infNFe", "det", "imposto", "ICMS"));
            Assert.Equal(new[] {"vICMSPrevisto"}, Filhos(xml, "infNFe", "det", "imposto", "ICMS", "ICMSPrevistoPagtoAntecip"));
            Assert.Equal("18.00", SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", "ICMS", "ICMSPrevistoPagtoAntecip", "vICMSPrevisto").Value);
        }

        [Theory]
        [InlineData("tpRegimeIBSCBS")]
        [InlineData("vUnComLiq")]
        [InlineData("vProdLiq")]
        [InlineData("cAtivSN")]
        [InlineData("gTribSN")]
        [InlineData("gIBSSN")]
        [InlineData("gCBSSN")]
        [InlineData("vProdLiqTot")]
        [InlineData("ICMSPrevistoPagtoAntecip")]
        public void NFe_SemCamposNovos_NaoDeveEmitirTag(string tag)
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, tag), $"Tag <{tag}> nao deveria ser serializada.");
        }

        [Fact]
        public void NFe_CamposNovos_DeveDesserializarValores()
        {
            var xml = NFeCompleta().GetXml();

            var nfe = NFeDoc.Load(xml);

            Assert.Equal(TipoRegimeIbsCbs.SimplesNacionalComRegimeRegular, nfe.InfNFe.Emitente.TpRegimeIbsCbs);
            var prod = nfe.InfNFe.Detalhe[0].Produto;
            Assert.Equal(90m, prod.VUnComLiq);
            Assert.Equal(90m, prod.VProdLiq);
            Assert.Equal(CodigoAtividadeSimplesNacional.RevendaMercadoriasAnexoI, prod.CAtivSN);
            var sn = nfe.InfNFe.Detalhe[0].Imposto.IbsCbs.TributacaoSimplesNacional;
            Assert.Equal(0.1m, sn.IbsSN.PIbsSN);
            Assert.Equal(0.09m, sn.IbsSN.VIbsSN);
            Assert.Equal(0.9m, sn.CbsSN.PCbsSN);
            Assert.Equal(0.81m, sn.CbsSN.VCbsSN);
            Assert.Equal(90m, nfe.InfNFe.Total.VProdLiqTot);
            Assert.Equal(0.09m, nfe.InfNFe.Total.IbsCbsTot.TribSN.VIbsSN);
            Assert.Equal(0.81m, nfe.InfNFe.Total.IbsCbsTot.TribSN.VCbsSN);
        }

        [Fact]
        public void NFe_IcmsPrevisto_DeveDesserializarValor()
        {
            var nfe = NFeDoc.Load(NFeComIcmsPrevisto().GetXml());

            var icms = Assert.IsType<IcmsPrevistoPagtoAntecip>(((Icms) nfe.InfNFe.Detalhe[0].Imposto.Imposto).Tipo);
            Assert.Equal(18m, icms.VIcmsPrevisto);
        }

        private static string[] ValidarAssinada(NFeDoc nfe)
        {
#pragma warning disable CS0618
            using var cert = NFeValidacaoXsdTests.CriarCertificadoAutoassinado();
#pragma warning restore CS0618
            nfe.Assinar(cert, SaveOptions.DisableFormatting);
            SchemaHelper.ValidarXml(nfe.GetXml(), Path.Combine(SerializerTestHelper.CaminhoBase, "schemas", "nfe_v4.00.xsd"), out var erros, out _);
            return erros;
        }

        [Fact]
        public void NFe_ComTodosOsCamposNovos_DeveSerValidaContraXsdPl010h()
        {
            var erros = ValidarAssinada(NFeCompleta());

            Assert.True(erros.Length == 0, string.Join(Environment.NewLine, erros));
        }

        [Fact]
        public void NFe_ComIcmsPrevisto_DeveSerValidaContraXsdPl010h()
        {
            var erros = ValidarAssinada(NFeComIcmsPrevisto());

            Assert.True(erros.Length == 0, string.Join(Environment.NewLine, erros));
        }

        [Theory]
        [InlineData("nfe-modelo55.xml")]
        [InlineData("nfce-modelo65.xml")]
        [InlineData("nfe-modelo55-monofasia.xml")]
        public void XmlGolden_DeveSerValidoContraXsdPl010hSomenteComFaltaDeSignature(string arquivo)
        {
            var xml = File.ReadAllText(Path.Combine(SerializerTestHelper.CaminhoBase, "Fixtures", "golden", arquivo));

            SchemaHelper.ValidarXml(xml, Path.Combine(SerializerTestHelper.CaminhoBase, "schemas", "nfe_v4.00.xsd"), out var erros, out _);

            Assert.All(erros, e => Assert.Contains("Signature", e));
        }
    }
}