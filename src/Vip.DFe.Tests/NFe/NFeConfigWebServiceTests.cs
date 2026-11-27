using System.Linq;
using Vip.DFe.NFe;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;
using Xunit;

namespace Vip.DFe.Tests.NFe
{
    public class NFeConfigWebServiceTests
    {
        [Theory]
        [InlineData(NFeModelo.NFe)]
        [InlineData(NFeModelo.NFCe)]
        public void Configurar_ContingenciaOffLine_UsaAutorizadoraNormal(NFeModelo modelo)
        {
            var config = new NFeService().Configuracoes;
            config.Modelo = modelo;
            config.Ambiente = TipoAmbiente.Homologacao;
            config.Webservices.UF = CodigoUF.SP;

            config.TipoEmissao = TipoEmissao.Normal;
            config.Webservices.Configurar();
            var normal = config.Webservices.Enderecos.Select(e => e.Url).OrderBy(u => u).ToList();

            config.TipoEmissao = TipoEmissao.OffLine;
            config.Webservices.Configurar();
            var offline = config.Webservices.Enderecos.Select(e => e.Url).OrderBy(u => u).ToList();

            Assert.NotEmpty(offline);
            Assert.Equal(normal, offline);
        }

        [Fact]
        public void Configurar_SvcAn_UsaAutorizadoraDeContingencia()
        {
            var config = new NFeService().Configuracoes;
            config.Modelo = NFeModelo.NFe;
            config.Ambiente = TipoAmbiente.Homologacao;
            config.Webservices.UF = CodigoUF.SP;
            config.TipoEmissao = TipoEmissao.SVCAN;

            config.Webservices.Configurar();

            Assert.NotEmpty(config.Webservices.Enderecos);
            Assert.All(config.Webservices.Enderecos, e => Assert.Equal("SVC-AN", e.Autorizadora));
        }
    }
}