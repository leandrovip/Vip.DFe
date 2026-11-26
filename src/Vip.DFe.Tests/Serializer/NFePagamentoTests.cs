using System;
using System.IO;
using Vip.DFe.Enum;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;
using Vip.DFe.NFe.NotaFiscal.Pagamento;
using Vip.DFe.Shared.Enum;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.Serializer
{
    /// <summary>
    ///     NT 2023.004 v1.20: grupo YA (dPag, CNPJPag/UFPag, CNPJReceb, idTermPag, cAut de 128 posições, novos meios de
    ///     pagamento) e indDeduzDeson (N28b).
    /// </summary>
    public class NFePagamentoTests
    {
        private static readonly string[] CaminhoDetPag = {"infNFe", "pag", "detPag"};

        private static NFeDoc NFeComPagamento(Action<NFeDetPag> configurar)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            configurar(nfe.InfNFe.Pagamento.DetPag[0]);
            return nfe;
        }

        private static string[] FilhosDetPag(string xml) => SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, CaminhoDetPag));

        private static string[] FilhosCard(string xml) => SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, "infNFe", "pag", "detPag", "card"));

        private static NFeDoc NFeCompleta()
        {
            return NFeComPagamento(d =>
            {
                d.TPag = MeioPagamento.PagamentoInstantaneoAutomatico;
                d.DPag = new DateTime(2026, 3, 10);
                d.CNPJPag = "11222333000181";
                d.UFPag = "SP";
                d.Card.TpIntegra = NFeTipoIntegracaoPagamento.PagamentoNaoIntegradoPOS;
                d.Card.Cnpj = "11222333000181";
                d.Card.CAut = "AUT123";
                d.Card.CNPJReceb = "11222333000181";
                d.Card.IdTermPag = "TERM-0001";
            });
        }

        #region dPag / CNPJPag / UFPag

        [Fact]
        public void DetPag_SemCamposNovos_NaoDeveEmitirTags()
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            Assert.Equal(new[] {"tPag", "vPag"}, FilhosDetPag(xml));
            Assert.False(SerializerTestHelper.ContemTag(xml, "dPag"));
            Assert.False(SerializerTestHelper.ContemTag(xml, "CNPJPag"));
            Assert.False(SerializerTestHelper.ContemTag(xml, "UFPag"));
        }

        [Fact]
        public void DetPag_DPag_DeveSerializarAposVPagNoFormatoData()
        {
            var xml = NFeComPagamento(d => d.DPag = new DateTime(2026, 3, 10)).GetXml();

            Assert.Equal(new[] {"tPag", "vPag", "dPag"}, FilhosDetPag(xml));
            Assert.Equal("2026-03-10", SerializerTestHelper.Elemento(xml, "infNFe", "pag", "detPag", "dPag").Value);
        }

        [Fact]
        public void DetPag_CnpjPagEUfPag_DevemSerializarEntreDPagECard()
        {
            var xml = NFeComPagamento(d =>
            {
                d.DPag = new DateTime(2026, 3, 10);
                d.CNPJPag = "11222333000181";
                d.UFPag = "SP";
                d.Card.TpIntegra = NFeTipoIntegracaoPagamento.PagamentoIntegradoTEF;
                d.Card.Cnpj = "11222333000181";
            }).GetXml();

            Assert.Equal(new[] {"tPag", "vPag", "dPag", "CNPJPag", "UFPag", "card"}, FilhosDetPag(xml));
            Assert.Equal("11222333000181", SerializerTestHelper.Elemento(xml, "infNFe", "pag", "detPag", "CNPJPag").Value);
            Assert.Equal("SP", SerializerTestHelper.Elemento(xml, "infNFe", "pag", "detPag", "UFPag").Value);
        }

        [Fact]
        public void DetPag_CnpjPagSemUfPag_NaoDeveEmitirNenhumDosDois()
        {
            var xml = NFeComPagamento(d => d.CNPJPag = "11222333000181").GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "CNPJPag"));
            Assert.False(SerializerTestHelper.ContemTag(xml, "UFPag"));
        }

        [Fact]
        public void DetPag_UfPagSemCnpjPag_NaoDeveEmitirNenhumDosDois()
        {
            var xml = NFeComPagamento(d => d.UFPag = "SP").GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "CNPJPag"));
            Assert.False(SerializerTestHelper.ContemTag(xml, "UFPag"));
        }

        #endregion

        #region card

        [Fact]
        public void Card_SemPreenchimento_NaoDeveEmitirGrupo()
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            Assert.False(SerializerTestHelper.ContemTag(xml, "card"));
        }

        [Fact]
        public void Card_SoComCnpjReceb_DeveSerEmitido()
        {
            var xml = NFeComPagamento(d =>
            {
                d.Card.TpIntegra = NFeTipoIntegracaoPagamento.PagamentoIntegradoTEF;
                d.Card.CNPJReceb = "11222333000181";
            }).GetXml();

            Assert.Equal(new[] {"tpIntegra", "CNPJReceb"}, FilhosCard(xml));
        }

        [Fact]
        public void Card_SoComIdTermPag_DeveSerEmitido()
        {
            var xml = NFeComPagamento(d =>
            {
                d.Card.TpIntegra = NFeTipoIntegracaoPagamento.PagamentoIntegradoTEF;
                d.Card.IdTermPag = "TERM-0001";
            }).GetXml();

            Assert.Equal(new[] {"tpIntegra", "idTermPag"}, FilhosCard(xml));
        }

        [Fact]
        public void Card_Completo_DeveSerializarNaOrdemDoXsd()
        {
            var xml = NFeCompleta().GetXml();

            Assert.Equal(new[] {"tpIntegra", "CNPJ", "cAut", "CNPJReceb", "idTermPag"}, FilhosCard(xml));
        }

        [Fact]
        public void Card_CAutComBandeira_DeveEmitirBandeiraAntesDeCAut()
        {
            var xml = NFeComPagamento(d =>
            {
                d.Card.TpIntegra = NFeTipoIntegracaoPagamento.PagamentoIntegradoTEF;
                d.Card.Bandeira = NFeBandeiraCartao.Visa;
                d.Card.CAut = "123";
            }).GetXml();

            Assert.Equal(new[] {"tpIntegra", "tBand", "cAut"}, FilhosCard(xml));
        }

        [Fact]
        public void Card_CAutCom128Caracteres_DeveSerAceitoEValidoNoXsd()
        {
            var cAut = new string('A', 128);
            var nfe = NFeComPagamento(d =>
            {
                d.Card.TpIntegra = NFeTipoIntegracaoPagamento.PagamentoIntegradoTEF;
                d.Card.CAut = cAut;
            });

            Assert.Equal(cAut, SerializerTestHelper.Elemento(nfe.GetXml(), "infNFe", "pag", "detPag", "card", "cAut").Value);
            AssertValido(nfe);
        }

        #endregion

        #region tPag

        [Theory]
        [InlineData(MeioPagamento.DuplicataMercantil, "14")]
        [InlineData(MeioPagamento.PagamentoInstantaneoEstatico, "20")]
        [InlineData(MeioPagamento.CreditoEmLoja, "21")]
        [InlineData(MeioPagamento.PagamentoEletronicoNaoInformado, "22")]
        [InlineData(MeioPagamento.PagamentoInstantaneoAutomatico, "23")]
        [InlineData(MeioPagamento.TefBookTransfer, "24")]
        [InlineData(MeioPagamento.PagamentoPosterior, "91")]
        [InlineData(MeioPagamento.Dinheiro, "01")]
        public void TPag_NovosCodigos_DevemSerializarComDoisDigitos(MeioPagamento meio, string esperado)
        {
            var xml = NFeComPagamento(d => d.TPag = meio).GetXml();

            Assert.Equal(esperado, SerializerTestHelper.Elemento(xml, "infNFe", "pag", "detPag", "tPag").Value);
        }

        [Fact]
        public void TPag_NovoCodigo_DeveSerDesserializado()
        {
            var nfe = NFeDoc.Load(NFeComPagamento(d => d.TPag = MeioPagamento.PagamentoPosterior).GetXml());

            Assert.Equal(MeioPagamento.PagamentoPosterior, nfe.InfNFe.Pagamento.DetPag[0].TPag);
        }

        #endregion

        #region Desserialização e XSD

        [Fact]
        public void DetPag_CamposNovos_DevemSerDesserializados()
        {
            var nfe = NFeDoc.Load(NFeCompleta().GetXml());

            var det = nfe.InfNFe.Pagamento.DetPag[0];
            Assert.Equal(new DateTime(2026, 3, 10), det.DPag);
            Assert.Equal("11222333000181", det.CNPJPag);
            Assert.Equal("SP", det.UFPag);
            Assert.Equal("11222333000181", det.Card.CNPJReceb);
            Assert.Equal("TERM-0001", det.Card.IdTermPag);
            Assert.Equal("AUT123", det.Card.CAut);
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

        private static void AssertValido(NFeDoc nfe)
        {
            var erros = ValidarAssinada(nfe);

            Assert.True(erros.Length == 0, string.Join(Environment.NewLine, erros));
        }

        [Fact]
        public void NFe_ComTodosOsCamposNovosDoPagamento_DeveSerValidaContraXsd() => AssertValido(NFeCompleta());

        [Fact]
        public void NFe_ComIndDeduzDeson_DeveSerValidaContraXsd()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.Imposto = new Icms
            {
                Tipo = new Icms40
                {
                    Origem = OrigemMercadoria.Nacional, Cst = "40", VIcmsDeson = 10m, MotDesIcms = NFeMotivoDesoneracao.Suframa,
                    IndDeduzDeson = NFeIndDeduzDeson.Deduz
                }
            };

            AssertValido(nfe);
        }

        #endregion

        #region indDeduzDeson

        private static string XmlComIcms(INFeIcms tipo)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.Imposto = new Icms {Tipo = tipo};
            return nfe.GetXml();
        }

        private static string[] FilhosIcms(string xml, string grupo) =>
            SerializerTestHelper.NomesFilhos(SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", "ICMS", grupo));

        private static void AssertIndDeduzAposMotivo(string[] filhos)
        {
            var i = Array.IndexOf(filhos, "motDesICMS");
            Assert.True(i >= 0);
            Assert.Equal("indDeduzDeson", filhos[i + 1]);
        }

        [Fact]
        public void Icms20_IndDeduzDeson_DeveSerEmitidoAposMotDesIcms()
        {
            var xml = XmlComIcms(new Icms20 {VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Suframa, IndDeduzDeson = NFeIndDeduzDeson.Deduz});

            AssertIndDeduzAposMotivo(FilhosIcms(xml, "ICMS20"));
            Assert.Equal("1", SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", "ICMS", "ICMS20", "indDeduzDeson").Value);
        }

        [Fact]
        public void Icms30_IndDeduzDeson_DeveSerEmitidoAposMotDesIcms()
        {
            var xml = XmlComIcms(new Icms30 {VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Suframa, IndDeduzDeson = NFeIndDeduzDeson.NaoDeduz});

            AssertIndDeduzAposMotivo(FilhosIcms(xml, "ICMS30"));
            Assert.Equal("0", SerializerTestHelper.Elemento(xml, "infNFe", "det", "imposto", "ICMS", "ICMS30", "indDeduzDeson").Value);
        }

        [Fact]
        public void Icms40_IndDeduzDeson_DeveSerEmitidoAposMotDesIcms()
        {
            var xml = XmlComIcms(new Icms40 {Cst = "40", VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Suframa, IndDeduzDeson = NFeIndDeduzDeson.Deduz});

            AssertIndDeduzAposMotivo(FilhosIcms(xml, "ICMS40"));
        }

        [Fact]
        public void Icms70_IndDeduzDeson_DeveSerEmitidoAposMotDesIcms()
        {
            var xml = XmlComIcms(new Icms70 {VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Suframa, IndDeduzDeson = NFeIndDeduzDeson.Deduz});

            AssertIndDeduzAposMotivo(FilhosIcms(xml, "ICMS70"));
        }

        [Fact]
        public void Icms90_IndDeduzDeson_DeveSerEmitidoAposMotDesIcms()
        {
            var xml = XmlComIcms(new Icms90 {VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Suframa, IndDeduzDeson = NFeIndDeduzDeson.Deduz});

            AssertIndDeduzAposMotivo(FilhosIcms(xml, "ICMS90"));
        }

        [Fact]
        public void IndDeduzDeson_SemValorDeDesoneracao_NaoDeveSerEmitido()
        {
            var tipos = new INFeIcms[]
            {
                new Icms20 {IndDeduzDeson = NFeIndDeduzDeson.Deduz}, new Icms30 {IndDeduzDeson = NFeIndDeduzDeson.Deduz},
                new Icms40 {Cst = "40", IndDeduzDeson = NFeIndDeduzDeson.Deduz}, new Icms70 {IndDeduzDeson = NFeIndDeduzDeson.Deduz},
                new Icms90 {IndDeduzDeson = NFeIndDeduzDeson.Deduz}
            };

            foreach (var tipo in tipos)
            {
                var xml = XmlComIcms(tipo);

                Assert.False(SerializerTestHelper.ContemTag(xml, "indDeduzDeson"), tipo.GetType().Name);
                Assert.False(SerializerTestHelper.ContemTag(xml, "motDesICMS"), tipo.GetType().Name);
            }
        }

        [Fact]
        public void IndDeduzDeson_ComDesoneracaoSemIndicador_NaoDeveSerEmitido()
        {
            var tipos = new INFeIcms[]
            {
                new Icms20 {VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Taxi}, new Icms30 {VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Taxi},
                new Icms40 {Cst = "40", VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Taxi},
                new Icms70 {VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Taxi}, new Icms90 {VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Taxi}
            };

            foreach (var tipo in tipos)
                Assert.False(SerializerTestHelper.ContemTag(XmlComIcms(tipo), "indDeduzDeson"), tipo.GetType().Name);
        }

        [Fact]
        public void IndDeduzDeson_DeveSerDesserializado()
        {
            var xml = XmlComIcms(new Icms40 {Cst = "40", VIcmsDeson = 5m, MotDesIcms = NFeMotivoDesoneracao.Suframa, IndDeduzDeson = NFeIndDeduzDeson.Deduz});

            var icms = Assert.IsType<Icms40>(((Icms) NFeDoc.Load(xml).InfNFe.Detalhe[0].Imposto.Imposto).Tipo);

            Assert.Equal(NFeIndDeduzDeson.Deduz, icms.IndDeduzDeson);
        }

        #endregion
    }
}
