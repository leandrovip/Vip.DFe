using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Helpers;
using Vip.DFe.NFe;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;
using Vip.DFe.Tests.Serializer;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.NFe
{
    public class QrCodeTests
    {
        private const string Chave = "35160810873538000245550010000000111504749126";
        private const string ChaveOff = "35160810873538000245550010000000119504749126";
        private const string UrlBase = "https://www.homologacao.nfce.fazenda.sp.gov.br/NFCeConsultaPublica/Paginas/ConsultaQRCode.aspx?p=";

        // Trechos do pattern de qrCode do XSD (leiauteNFe_v4.00.xsd) para a versão 3 off-line.
        private static readonly Regex PatternOfflineV3 = new Regex(
            @"^https?://[^|]+\?p=[0-9]{6}[0-9A-Z]{12}[0-9]{16}9[0-9]{9}\|3\|[1-2]\|(0[1-9]|[12][0-9]|3[01])\|[0-9]+\.[0-9]{2}\|(1|2|3)?\|(([0-9A-Z]{12}[0-9]{2})|([0-9]{11}))?\|[a-zA-Z0-9+/]+[=]{0,2}$");

        private static NFeConfig Config(NFeVersaoQrCode versao = NFeVersaoQrCode.V2)
        {
            var cfg = new NFeService().Configuracoes;
            cfg.Modelo = NFeModelo.NFCe;
            cfg.NFCeVersaoQrCode = versao;
            if (versao == NFeVersaoQrCode.V2)
            {
                cfg.NFCeIdToken = "1";
                cfg.NFCeCSC = "ABCDEF0123456789";
            }

            return cfg;
        }

        private static NFeDoc CriarNFCe(bool offline = false)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.Modelo = NFeModelo.NFCe;
            if (offline) nfe.InfNFe.Ide.TipoEmissao = TipoEmissao.OffLine;
            return nfe;
        }

        /// <summary>Ajusta o tpEmis da chave do fixture para 9 (contingência off-line), como exige o pattern do XSD.</summary>
        private static void ChaveOffline(NFeDoc nfe) => nfe.InfNFe.Id = "NFe" + ChaveOff;

        private static void DefinirDest(NFeDoc nfe, string cnpj, string cpf, string estrangeiro)
        {
            var d = nfe.InfNFe.Destinatario;
            d.CNPJ = cnpj;
            d.CPF = cpf;
            d.IdEstrangeiro = estrangeiro;
        }

        [Fact]
        [Obsolete("Obsolete")]
        public void QrCodeV2_ConfiguracaoPadrao_DeveSerIdenticoAoComportamentoAnterior()
        {
            Assert.Equal(NFeVersaoQrCode.V2, new NFeService().Configuracoes.NFCeVersaoQrCode);

            var nfe = CriarNFCe();
            nfe.GerarQrCode(Config());
            Assert.Equal(UrlBase + Chave + "|2|2|1|00c92d503d321f02e7b45c260268542551015273", nfe.InfNFeSupl.QrCode);

            var off = CriarNFCe(true);
            using var cert = NFeValidacaoXsdTests.CriarCertificadoAutoassinado();
            off.Assinar(cert, SaveOptions.DisableFormatting);
            off.GerarQrCode(Config());
            Assert.Equal(UrlBase + Chave + "|2|2|01|100.00|302b56534277656c694a4f587554454261502b46676874437068493d|1|65f2cc07e528570b7cd19cf6a8f5571b42d22bad", off.InfNFeSupl.QrCode);

            // A sobrecarga com certificado não altera a versão 2.
            off.GerarQrCode(Config(), cert);
            Assert.EndsWith("|65f2cc07e528570b7cd19cf6a8f5571b42d22bad", off.InfNFeSupl.QrCode);
        }

        [Fact]
        public void QrCodeV3_Online_DeveGerarSomenteChaveVersaoEAmbienteSemCsc()
        {
            var nfe = CriarNFCe();

            nfe.GerarQrCode(Config(NFeVersaoQrCode.V3));

            Assert.Equal(UrlBase + Chave + "|3|2", nfe.InfNFeSupl.QrCode);
            Assert.False(string.IsNullOrEmpty(nfe.InfNFeSupl.UrlChave));
        }

        [Fact]
        public void QrCodeV3_OfflineSemCertificado_DeveLancarVipException()
        {
            var nfe = CriarNFCe(true);

            Assert.Throws<VipException>(() => nfe.GerarQrCode(Config(NFeVersaoQrCode.V3)));
        }

        [Theory]
        [InlineData("27076697000130", null, null, "1", "27076697000130")]
        [InlineData("12ABC34500DE35", null, null, "1", "12ABC34500DE35")]
        [InlineData(null, "12345678909", null, "2", "12345678909")]
        [InlineData(null, null, "PASS12345", "3", "")]
        [InlineData(null, null, null, "", "")]
        [InlineData("27.076.697/0001-30", null, null, "1", "27076697000130")]
        [InlineData("12.abc.345/00de-35", null, null, "1", "12ABC34500DE35")]
        [InlineData(null, "123.456.789-09", null, "2", "12345678909")]
        [Obsolete("Obsolete")]
        public void QrCodeV3_Offline_DeveGerarOitoParametrosComAssinaturaValida(string cnpj, string cpf, string estrangeiro, string tpDest, string idDest)
        {
            var nfe = CriarNFCe(true);
            ChaveOffline(nfe);
            DefinirDest(nfe, cnpj, cpf, estrangeiro);
            using var cert = NFeValidacaoXsdTests.CriarCertificadoAutoassinado();
            nfe.Assinar(cert, SaveOptions.DisableFormatting);

            nfe.GerarQrCode(Config(NFeVersaoQrCode.V3), cert);

            var qr = nfe.InfNFeSupl.QrCode;
            Assert.StartsWith(UrlBase, qr);
            var partes = qr.Substring(UrlBase.Length).Split('|');
            Assert.Equal(8, partes.Length);
            Assert.Equal(ChaveOff, partes[0]);
            Assert.Equal("3", partes[1]);
            Assert.Equal("2", partes[2]);
            Assert.Equal("01", partes[3]);
            Assert.Equal("100.00", partes[4]);
            Assert.Equal(tpDest, partes[5]);
            Assert.Equal(idDest, partes[6]);

            var dados = Encoding.UTF8.GetBytes(string.Join("|", partes.Take(7)));
            using var rsa = cert.GetRSAPublicKey();
            Assert.True(rsa.VerifyData(dados, Convert.FromBase64String(partes[7]), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1));
            Assert.Matches(PatternOfflineV3, qr);
        }

        [Fact]
        [Obsolete("Obsolete")]
        public void QrCodeV3_OfflineEstrangeiroSemNome_NaoDeveInformarDestinatario()
        {
            // Sem CNPJ/CPF/Nome o grupo dest não é serializado; o QR Code também não identifica o destinatário (NT 2025.001, ZX02).
            var nfe = CriarNFCe(true);
            ChaveOffline(nfe);
            DefinirDest(nfe, null, null, "PASS12345");
            nfe.InfNFe.Destinatario.Nome = null;
            using var cert = NFeValidacaoXsdTests.CriarCertificadoAutoassinado();
            nfe.Assinar(cert, SaveOptions.DisableFormatting);

            nfe.GerarQrCode(Config(NFeVersaoQrCode.V3), cert);

            var partes = nfe.InfNFeSupl.QrCode.Substring(UrlBase.Length).Split('|');
            Assert.Equal(string.Empty, partes[5]);
            Assert.Equal(string.Empty, partes[6]);
        }

        [Fact]
        [Obsolete("Obsolete")]
        public void QrCodeV3_OfflineAssinada_NaoDeveGerarErroDeSchemaNoQrCode()
        {
            var nfe = CriarNFCe(true);
            ChaveOffline(nfe);
            using var cert = NFeValidacaoXsdTests.CriarCertificadoAutoassinado();
            nfe.Assinar(cert, SaveOptions.DisableFormatting);
            nfe.GerarQrCode(Config(NFeVersaoQrCode.V3), cert);

            SchemaHelper.ValidarXml(nfe.GetXml(), Path.Combine(SerializerTestHelper.CaminhoBase, "schemas", "nfe_v4.00.xsd"), out var erros, out _);

            Assert.DoesNotContain(erros, e => e.Contains("qrCode"));
        }
    }
}
