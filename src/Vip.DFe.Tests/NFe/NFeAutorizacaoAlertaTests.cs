using System;
using System.IO;
using System.Linq;
using Vip.DFe.NFe;
using Vip.DFe.NFe.NotaFiscal;
using Vip.DFe.NFe.Protocolo;
using Vip.DFe.Shared.Enum;
using Vip.DFe.Tests.Serializer;
using Xunit;

namespace Vip.DFe.Tests.NFe
{
    public class NFeAutorizacaoAlertaTests
    {
        private const string Chave = "35160810873538000245550010000000111504749126";

        private static string ProtXml(int cStat, string motivo, params (string cod, string msg)[] alertas)
        {
            var msgs = string.Concat(alertas.Select(a => $"<cMsg>{a.cod}</cMsg><xMsg>{a.msg}</xMsg>"));
            return "<protNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"4.00\"><infProt><tpAmb>1</tpAmb><verAplic>SVRS2026</verAplic>" +
                   $"<chNFe>{Chave}</chNFe><dhRecbto>2026-03-26T10:15:00-03:00</dhRecbto><nProt>135260000000000</nProt>" +
                   $"<digVal>AAAAAAAAAAAAAAAAAAAAAAAAAAAA</digVal><cStat>{cStat}</cStat><xMotivo>{motivo}</xMotivo>{msgs}</infProt></protNFe>";
        }

        private static NFeProc CriarProc(int cStat)
        {
            return new NFeProc
            {
                NFe = SerializerTestHelper.CarregarNFeModelo(),
                ProtNFe = NFeProtNFe.Load(ProtXml(cStat, "Teste"))
            };
        }

        [Theory]
        [InlineData(100, true, "-procNFe.xml")]
        [InlineData(120, true, "-procNFe.xml")]
        [InlineData(150, true, "-procNFe.xml")]
        [InlineData(110, true, "-den.xml")]
        [InlineData(301, true, "-den.xml")]
        [InlineData(302, true, "-den.xml")]
        [InlineData(303, true, "-den.xml")]
        public void NFeProc_CStat_DeveDefinirProcessadoENomeDoArquivo(int cStat, bool processado, string sufixo)
        {
            var proc = CriarProc(cStat);
            Assert.Equal(processado, proc.Processado);

            var dir = Path.Combine(Path.GetTempPath(), "vipdfe-" + Guid.NewGuid().ToString("N"));
            try
            {
                var config = new NFeService().Configuracoes;
                config.Arquivos.Salvar = true;
                config.Arquivos.Diretorio = dir;

                proc.Gravar(config);

                var arquivos = Directory.GetFiles(dir, "*.xml", SearchOption.AllDirectories);
                Assert.Single(arquivos);
                Assert.EndsWith(Chave + sufixo, arquivos[0]);
            }
            finally
            {
                if (Directory.Exists(dir)) Directory.Delete(dir, true);
            }
        }

        [Fact]
        public void NFeProc_CStatNaoProcessado_NaoDeveSerProcessado()
        {
            Assert.False(CriarProc(999).Processado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void NFeInfProt_ComAlertas_DevePreservarPrimeiroParERegerarXmlValido(int quantidade)
        {
            var alertas = Enumerable.Range(0, quantidade).Select(i => ((100 + i).ToString(), $"Alerta {i}")).ToArray();

            var xml = ProtXml(120, "Autorizado o uso da NF-e, com alerta", alertas);
            var prot = NFeProtNFe.Load(xml);

            Assert.Equal(120, prot.InfProt.CStat);
            Assert.Equal("100", prot.InfProt.CMsg);
            Assert.Equal("Alerta 0", prot.InfProt.XMsg);

            var reserializado = prot.GetXml();
            var infRegerado = SerializerTestHelper.Elemento(reserializado, "infProt");

            Assert.Equal(new[] {"tpAmb", "verAplic", "chNFe", "dhRecbto", "nProt", "digVal", "cStat", "xMotivo", "cMsg", "xMsg"},
                SerializerTestHelper.NomesFilhos(infRegerado));

            if (quantidade == 1)
                Assert.Equal(SerializerTestHelper.Elemento(xml, "infProt").ToString(), infRegerado.ToString());
        }

        [Fact]
        public void NFeInfProt_SemAlertas_NaoDeveEmitirCMsgNemXMsg()
        {
            var xml = ProtXml(100, "Autorizado o uso da NF-e");
            var prot = NFeProtNFe.Load(xml);

            Assert.Null(prot.InfProt.CMsg);
            Assert.Null(prot.InfProt.XMsg);

            var reserializado = prot.GetXml();
            Assert.False(SerializerTestHelper.ContemTag(reserializado, "cMsg"));
            Assert.False(SerializerTestHelper.ContemTag(reserializado, "xMsg"));
            Assert.Equal(SerializerTestHelper.Elemento(xml, "infProt").ToString(), SerializerTestHelper.Elemento(reserializado, "infProt").ToString());
        }

        [Fact]
        public void TipoImpressao_SimplificadoTipo2_DeveSerSerializadoComoSeis()
        {
            Assert.Equal(6, (int) TipoImpressao.SimplificadoTipo2);

            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Ide.TipoImpressao = TipoImpressao.SimplificadoTipo2;
            var xml = nfe.GetXml();
            Assert.Equal("6", SerializerTestHelper.Elemento(xml, "infNFe", "ide", "tpImp").Value);

            var recarregada = DFe.NFe.NotaFiscal.NFe.Load(xml);
            Assert.Equal(TipoImpressao.SimplificadoTipo2, recarregada.InfNFe.Ide.TipoImpressao);
        }
    }
}