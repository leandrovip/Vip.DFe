using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Vip.DFe.Danfe;
using Vip.DFe.Danfe.Blocos;
using Vip.DFe.Danfe.Elementos;
using Vip.DFe.Danfe.Enum;
using Vip.DFe.Danfe.Modelo;
using Xunit;

namespace Vip.DFe.Tests.Danfe
{
    /// <summary>
    ///     DANFE NF-e no leiaute da NT 2026.010 (tributos da Reforma Tributária).
    /// </summary>
    public class DanfeLeiauteRtcTests
    {
        private static readonly string DiretorioGolden = Path.Combine(AppContext.BaseDirectory, "Fixtures", "golden");

        [WindowsFact]
        public void DanfeService_Gerar_LeiauteNovoEAnterior_RetratoEPaisagem_DeveConcluirSemErro()
        {
            foreach (var arquivo in new[] {"nfe-modelo55.xml", "nfe-modelo55-monofasia.xml"})
            foreach (var orientacao in new[] {Orientacao.Retrato, Orientacao.Paisagem})
            foreach (var leiauteAnterior in new[] {false, true})
            {
                var model = DanfeViewModel.CriarDoConteudoXml(LerGolden(arquivo));
                model.Orientacao = orientacao;
                model.UsarLeiauteAnterior = leiauteAnterior;

                using var danfe = new DanfeService(model);
                using var stream = new MemoryStream();

                danfe.Gerar();
                danfe.Salvar(stream);

                Assert.True(stream.Length > 0, $"{arquivo} {orientacao} anterior={leiauteAnterior}");
            }
        }

        [Fact]
        public void DanfeViewModelCreator_SemGRed_DeveUsarAliquotasVigentes()
        {
            var model = DanfeViewModel.CriarDoConteudoXml(LerGolden("nfe-modelo55.xml"));
            var produto = Assert.Single(model.Produtos);

            Assert.Equal(0.1D, produto.AliquotaIbsUf);
            Assert.Equal(0D, produto.AliquotaIbsMunicipio);
            Assert.Equal(0.9D, produto.AliquotaCbs);
            Assert.Equal(100D, produto.BaseIbsCbs);
            Assert.Equal(0.9D, produto.ValorCbs);
            Assert.Equal("000001", produto.ClassificacaoTributaria);
            Assert.Equal("3 - REGIME NORMAL", model.Emitente.CrtDescricao);
        }

        [Fact]
        public void DanfeViewModelCreator_ComGRed_DeveUsarAliquotaEfetiva()
        {
            var xml = LerGolden("nfe-modelo55.xml")
                .Replace("<pIBSUF>0.1000</pIBSUF>", "<pIBSUF>0.1000</pIBSUF><gRed><pRedAliq>60.0000</pRedAliq><pAliqEfet>0.0400</pAliqEfet></gRed>")
                .Replace("<pCBS>0.9000</pCBS>", "<pCBS>0.9000</pCBS><gRed><pRedAliq>60.0000</pRedAliq><pAliqEfet>0.3600</pAliqEfet></gRed>");

            var produto = Assert.Single(DanfeViewModel.CriarDoConteudoXml(xml).Produtos);

            Assert.Equal(0.04D, produto.AliquotaIbsUf);
            Assert.Equal(0.36D, produto.AliquotaCbs);
            Assert.Equal(0D, produto.AliquotaIbsMunicipio);
        }

        [Fact]
        public void DanfeViewModelCreator_SemTotaisIbsCbsIs_DeveZerarTotais()
        {
            var xml = Regex.Replace(LerGolden("nfe-modelo55.xml"), "<IBSCBSTot>.*</IBSCBSTot>", string.Empty);

            var m = DanfeViewModel.CriarDoConteudoXml(xml).CalculoImposto;

            Assert.Equal(0D, m.ValorCbs);
            Assert.Equal(0D, m.ValorIbsUf);
            Assert.Equal(0D, m.ValorIbsMunicipio);
            Assert.Equal(0D, m.ValorIs);
            Assert.Equal(0D, m.ValorCbsMono);
        }

        [WindowsFact]
        public void BlocosTotais_LinhasFacultativas_DevemSerOmitidasQuandoZeradas()
        {
            var model = DanfeViewModel.CriarDoConteudoXml(LerGolden("nfe-modelo55.xml"));

            using (var danfe = new DanfeService(model))
            {
                Assert.Equal(1, ContarLinhas<BlocoTotalIcmsIpi>(danfe));
                Assert.Equal(1, ContarLinhas<BlocoTotalIbsCbsIs>(danfe));
                Assert.DoesNotContain(danfe.Blocos, b => b is BlocoCalculoImposto);
            }

            model.CalculoImposto.ValorFcp = 1.5;
            model.CalculoImposto.ValorIcmsMonoRetencao = 2;
            model.CalculoImposto.ValorCbsMono = 3;

            using (var danfe = new DanfeService(model))
            {
                Assert.Equal(3, ContarLinhas<BlocoTotalIcmsIpi>(danfe));
                Assert.Equal(2, ContarLinhas<BlocoTotalIbsCbsIs>(danfe));
            }
        }

        [WindowsFact]
        public void DanfeService_LeiauteNovo_IssqnSoComValor()
        {
            var model = DanfeViewModel.CriarDoConteudoXml(LerGolden("nfe-modelo55.xml"));

            using (var danfe = new DanfeService(model))
                Assert.DoesNotContain(danfe.Blocos, b => b is BlocoCalculoIssqn);

            model.CalculoIssqn.ValorTotalServicos = 150;

            using (var danfe = new DanfeService(model))
                Assert.Contains(danfe.Blocos, b => b is BlocoCalculoIssqn);

            model.CalculoIssqn.ValorTotalServicos = 0;
            model.UsarLeiauteAnterior = true;

            using (var danfe = new DanfeService(model))
                Assert.Contains(danfe.Blocos, b => b is BlocoCalculoIssqn);
        }

        [WindowsFact]
        public void DanfeService_LeiauteAnterior_DeveManterBlocoCalculoImposto()
        {
            var model = DanfeViewModel.CriarDoConteudoXml(LerGolden("nfe-modelo55.xml"));
            model.UsarLeiauteAnterior = true;

            using var danfe = new DanfeService(model);

            Assert.Contains(danfe.Blocos, b => b is BlocoCalculoImposto);
            Assert.DoesNotContain(danfe.Blocos, b => b is BlocoTotalIbsCbsIs);
        }

        private static int ContarLinhas<T>(DanfeService danfe) where T : BlocoBase => danfe.Blocos.OfType<T>().Single().MainVerticalStack.Drawables.OfType<LinhaCampos>().Count();

        private static string LerGolden(string arquivo) => File.ReadAllText(Path.Combine(DiretorioGolden, arquivo));
    }
}