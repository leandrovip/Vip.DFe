using System;
using System.Linq;
using Vip.DFe.Shared.Enum;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.Core
{
    /// <summary>
    ///     CNPJ alfanumérico (NT Conjunta DF-e 2025.001 / NT 2026.004): DV da chave com ASCII-48 e CNPJ [0-9A-Z]{12}[0-9]{2}.
    /// </summary>
    public class CnpjAlfanumericoTests
    {
        private const string CnpjReceita = "12ABC34501DE35";

        // Porte independente do exemplo da NT: valor = ASCII - 48, pesos 2..9 da direita para a esquerda.
        private static int DvNt(string chave43)
        {
            var soma = 0;
            var peso = 2;
            for (var i = chave43.Length - 1; i >= 0; i--)
            {
                soma += (chave43[i] - 48) * peso;
                peso = peso == 9 ? 2 : peso + 1;
            }

            var dv = 11 - soma % 11;
            return dv >= 10 ? 0 : dv;
        }

        [Theory]
        [InlineData("3526" + "12" + "12ABC34501DE35" + "55" + "001" + "000000011" + "1" + "50474912")]
        [InlineData("3526" + "12" + "ZZZZZZZZZZZZ99" + "65" + "002" + "000000123" + "1" + "00000001")]
        [InlineData("3516" + "08" + "10873538000245" + "55" + "001" + "000000011" + "1" + "50474912")]
        public void Obter_DeveSerIgualAoAlgoritmoDaNt(string chave43)
        {
            Assert.Equal(43, chave43.Length);
            Assert.Equal(DvNt(chave43), DigitoVerificador.Obter(chave43));
        }

        [Fact]
        public void Gerar_ComCnpjAlfaMascarado_DeveNormalizarEValidar()
        {
            var chave = ChaveDFe.Gerar(CodigoUF.SP, new DateTime(2026, 7, 1), "12.abc.345/01de-35", 55, 1, 11,
                TipoEmissao.Normal, 50474912);

            Assert.Equal(44, chave.Chave.Length);
            Assert.Equal(CnpjReceita, chave.Chave.Substring(6, 14));
            Assert.Equal(DvNt(chave.Chave.Substring(0, 43)), chave.Digito);
            Assert.True(ChaveDFe.Validar(chave.Chave));
        }

        [Fact]
        public void Validar_ChaveAlfaComDvErrado_DeveRetornarFalse()
        {
            var chave = ChaveDFe.Gerar(CodigoUF.SP, new DateTime(2026, 7, 1), CnpjReceita, 55, 1, 11,
                TipoEmissao.Normal, 50474912).Chave;
            var dvErrado = (chave[43] - '0' + 1) % 10;

            Assert.False(ChaveDFe.Validar(chave.Substring(0, 43) + dvErrado));
        }

        [Fact]
        public void Gerar_ComCnpjNumerico_DeveManterChaveHistorica()
        {
            var chave = ChaveDFe.Gerar(CodigoUF.SP, new DateTime(2016, 08, 01), "10873538000245", 55, 1, 11,
                TipoEmissao.Normal, 50474912);

            Assert.Equal("35160810873538000245550010000000111504749126", chave.Chave);
        }

        [Fact]
        public void ObterNomeXml_ChaveNumerica_DeveUsarApenasAChave()
        {
            var nfe = new NFeDoc();
            nfe.InfNFe.Id = "NFe35160810873538000245550010000000111504749126";

            Assert.Equal("35160810873538000245550010000000111504749126-nfe.xml", nfe.ObterNomeXml());
        }

        [Fact]
        public void ObterNomeXml_ChaveAlfa_DevePreservarLetras()
        {
            var nfe = new NFeDoc();
            nfe.InfNFe.Id = "NFe3526" + "0112ABC34501DE35" + "55001000000011150474912".Substring(0, 0) + "5500100000001115047491" + "2";

            Assert.StartsWith("352601" + "12ABC34501DE35", nfe.ObterNomeXml());
            Assert.EndsWith("-nfe.xml", nfe.ObterNomeXml());
            Assert.DoesNotContain("NFe", nfe.ObterNomeXml());
        }
    }
}
