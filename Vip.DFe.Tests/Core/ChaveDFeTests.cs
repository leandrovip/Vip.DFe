using System;
using Vip.DFe.Shared.Enum;
using Xunit;

namespace Vip.DFe.Tests.Core
{
    public class ChaveDFeTests
    {
        [Fact]
        public void ChaveDFe_Gerar_DeveGerarChaveValida()
        {
            // Arrange
            const string chaveEsperada = "35160810873538000245550010000000111504749126";

            // Act
            var chave = ChaveDFe.Gerar(
                CodigoUF.SP,
                new DateTime(2016, 08, 01),
                "10873538000245",
                55,
                1,
                11,
                TipoEmissao.Normal,
                50474912
            );

            // Assert
            Assert.Equal(chaveEsperada, chave.Chave);
        }

        [Theory]
        [InlineData("35160810873538000245550010000000111504749126")]
        [InlineData("35180727076697000130550010000000511185520005")]
        [InlineData("35160810873538000245550010000000131096515483")]
        public void ChaveDFe_Validar_DeveRetornarTrueCasoChaveForValida(string chaveValida)
        {
            // Assert
            Assert.True(ChaveDFe.Validar(chaveValida));
        }
    }
}