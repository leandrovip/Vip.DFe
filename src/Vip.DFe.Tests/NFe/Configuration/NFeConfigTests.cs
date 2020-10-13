using Vip.DFe.NFe;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;
using Xunit;

namespace Vip.DFe.Tests.NFe.Configuration
{
    public class NFeConfigTests
    {
        [Fact]
        public void NFeConfigTests_ConfiguracaoWebservice_DeveRetornarRegistrosValidosParaProducaoEHomologacao()
        {
            // Arrange
            var serviceNFe = new NFeService();
            serviceNFe.Configuracoes.Modelo = NFeModelo.NFe;
            serviceNFe.Configuracoes.TipoEmissao = TipoEmissao.Normal;
            serviceNFe.Configuracoes.Versao = NFeVersao.v400;

            // Act
            serviceNFe.Configuracoes.Validar();

            // Assert
            Assert.NotEmpty(serviceNFe.Configuracoes.Webservices.Enderecos);
        }
    }
}