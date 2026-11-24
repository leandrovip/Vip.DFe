using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;
using Vip.DFe.Enum;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.NotaFiscal.Total;
using Xunit;

namespace Vip.DFe.Tests.Serializer
{
    public class NFeValidacaoXsdTests
    {
        private static string CaminhoSchema => Path.Combine(SerializerTestHelper.CaminhoBase, "schemas", "nfe_v4.00.xsd");

        /// <summary>Certificado autoassinado gerado em memoria (RSA 2048 / SHA-256), sem acesso ao repositorio de certificados.</summary>
        private static X509Certificate2 CriarCertificadoAutoassinado()
        {
            using var rsa = RSA.Create(2048);
            var req = new CertificateRequest("CN=Vip.DFe Teste", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            using var cert = req.CreateSelfSigned(new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2035, 1, 1, 0, 0, 0, TimeSpan.Zero));
            return new X509Certificate2(cert.Export(X509ContentType.Pfx, "teste"), "teste", X509KeyStorageFlags.Exportable);
        }

        private static string[] Validar(string xml)
        {
            SchemaHelper.ValidarXml(xml, CaminhoSchema, out var erros, out _);
            return erros;
        }

        [Fact]
        public void NFe_Assinar_ComCertificadoAutoassinado_DeveGerarAssinaturaValida()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            using var cert = CriarCertificadoAutoassinado();

            nfe.Assinar(cert, SaveOptions.DisableFormatting);

            Assert.True(nfe.Assinado);
            Assert.True(nfe.ValidarAssinatura());
            Assert.True(SerializerTestHelper.ContemTag(nfe.GetXml(), "Signature"));
        }

        [Fact]
        public void NFe_Assinada_DeveSerValidaContraXsdNfe400()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            using var cert = CriarCertificadoAutoassinado();
            nfe.Assinar(cert, SaveOptions.DisableFormatting);

            var erros = Validar(nfe.GetXml());

            Assert.True(erros.Length == 0, string.Join(Environment.NewLine, erros));
        }

        [Fact]
        public void NFe_AssinadaComIssqnTotEDCompet_DeveSerValidaContraXsdNfe400()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Total.IssqnTot = new NFeIssqnTot {VServ = 10m, VBc = 10m, VIss = 0.5m, DCompet = new DateTime(2026, 3, 15)};
            using var cert = CriarCertificadoAutoassinado();
            nfe.Assinar(cert, SaveOptions.DisableFormatting);

            var erros = Validar(nfe.GetXml());

            Assert.True(erros.Length == 0, string.Join(Environment.NewLine, erros));
        }

        /// <summary>
        ///     Regressão: após Assinar + ValidarAssinatura(gerarXml: true) (fluxo do NFeService), nfe.Xml passa a conter
        ///     a declaração XML; o lote enviNFe deve ser montado com GetXml(opções sem declaração) e ser válido no XSD.
        /// </summary>
        [Fact]
        public void EnviNFe_AposAssinarEValidarAssinatura_DeveSerValidoContraXsdSemDeclaracaoInterna()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            var opcoes = SaveOptions.DisableFormatting | SaveOptions.OmitDeclaration;
            using var cert = CriarCertificadoAutoassinado();
            nfe.Assinar(cert, opcoes);
            Assert.True(nfe.ValidarAssinatura(true));

            var nfeXml = nfe.GetXml(opcoes);
            var lote = "<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\"><idLote>1</idLote><indSinc>1</indSinc>" + nfeXml + "</enviNFe>";

            Assert.DoesNotContain("<?xml", nfeXml);
            SchemaHelper.ValidarXml(lote, Path.Combine(SerializerTestHelper.CaminhoBase, "schemas", "enviNFe_v4.00.xsd"), out var erros, out _);
            Assert.True(erros.Length == 0, string.Join(Environment.NewLine, erros));
        }

        [Fact]
        public void NFe_SemAssinatura_DeveFalharNoXsdSomentePorCausaDaSignature()
        {
            var xml = SerializerTestHelper.CarregarNFeModelo().GetXml();

            var erros = Validar(xml);

            Assert.NotEmpty(erros);
            Assert.All(erros, e => Assert.Contains("Signature", e));
        }
    }
}
