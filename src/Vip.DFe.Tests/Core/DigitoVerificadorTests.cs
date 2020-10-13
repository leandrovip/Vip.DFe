using Vip.DFe.Exception;
using Xunit;

namespace Vip.DFe.Tests.Core
{
    public class DigitoVerificadorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DigitoVerificador_Calcular_DeveRetornarErroCasoDocumentoForNuloOuVazio(string documento)
        {
            // Assert
            var exception = Assert.Throws<VipException>(() => DigitoVerificador.Obter(documento));
            Assert.Equal("Chave de Documento não informado", exception.Message);
        }

        [Fact]
        public void DigitoVerificadorTests_Calcular_DeveRetornarErroCasoTamanhoDoDocumentoForDiferenteDe43Caracteres()
        {
            // Arrange
            const string documento = "35160810873538000245550010000000";

            // Assert
            var exception = Assert.Throws<VipException>(() => DigitoVerificador.Obter(documento));
            Assert.Equal("Chave de Documento inválida", exception.Message);
        }

        [Theory]
        [InlineData("35160810873538000245550010000000111504749126")]
        [InlineData("35180727076697000130550010000000511185520005")]
        [InlineData("35160810873538000245550010000000131096515483")]
        public void DigitoVerificador_Calcular_DeveRetornarDigitoValido(string documento)
        {
            int.TryParse(documento.Substring(43, 1), out var digitoEsperado);

            // Act
            var digito = DigitoVerificador.Obter(documento.Substring(0, 43));

            // Assert
            Assert.Equal(digitoEsperado, digito);
        }
    }
}