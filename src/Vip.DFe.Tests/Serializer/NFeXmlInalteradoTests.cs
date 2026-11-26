using System.IO;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.Serializer
{
    /// <summary>
    ///     Garante que o XML de quem NAO preenche os campos novos (NT 2026.005 / NT 2026.008) permanece
    ///     identico, byte a byte, ao gerado antes da inclusao desses campos (arquivos em Fixtures/golden).
    ///     Quebras de linha sao normalizadas apenas para independer do SO (Environment.NewLine).
    /// </summary>
    public class NFeXmlInalteradoTests
    {
        private static NFeDoc CriarNFe55() => SerializerTestHelper.CarregarNFeModelo();

        private static NFeDoc CriarNFCe65()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.Modelo = NFeModelo.NFCe;
            return nfe;
        }

        private static NFeDoc CriarNFeComMonofasia()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Detalhe[0].Imposto.IbsCbs = new IbsCbs
            {
                Cst = "620",
                CClassTrib = "620001",
                IbsCbsMono = new IbsCbsMono
                {
                    CbsMonoAdValorem = new CbsMonoAdValorem
                    {
                        MonoPadrao = new CbsAdValoremPadrao {VBcMono = 100m, PAliqMonoCbs = 0.9m, VCbsMono = 0.9m}
                    },
                    VTotIbsMonoItem = 0m,
                    VTotCbsMonoItem = 0.9m
                }
            };
            return nfe;
        }

        private static string CaminhoGolden(string nome) => Path.Combine(SerializerTestHelper.CaminhoBase, "Fixtures", "golden", nome);

        private static string Normalizar(string s) => s.Replace("\r\n", "\n");

        private static void Comparar(string nomeGolden, NFeDoc nfe)
        {
            var esperado = File.ReadAllText(CaminhoGolden(nomeGolden));
            var atual = nfe.GetXml();

            Assert.Equal(Normalizar(esperado), Normalizar(atual));
        }

        [Fact]
        public void NFe55_SemCamposNovos_DeveGerarXmlIdenticoAoGolden() => Comparar("nfe-modelo55.xml", CriarNFe55());

        [Fact]
        public void NFCe65_SemCamposNovos_DeveGerarXmlIdenticoAoGolden() => Comparar("nfce-modelo65.xml", CriarNFCe65());

        [Fact]
        public void NFe55_ComMonofasiaIbsCbs_SemCamposNovos_DeveGerarXmlIdenticoAoGolden() => Comparar("nfe-modelo55-monofasia.xml", CriarNFeComMonofasia());
    }
}