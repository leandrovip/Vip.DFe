using Vip.DFe.Enum;
using Vip.DFe.NFe.Evento;
using Vip.DFe.Serializer;
using Xunit;
using NFeDoc = Vip.DFe.NFe.NotaFiscal.NFe;

namespace Vip.DFe.Tests.Serializer
{
    /// <summary>
    ///     TipoCampo.StrAlfaNumFill: CNPJ alfanumérico e chave de acesso (NT Conjunta DF-e 2025.001 / NT 2026.004).
    /// </summary>
    public class StrAlfaNumFillTests
    {
        private static string CnpjEmitente(string cnpj)
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Emitente.CPF = null;
            nfe.InfNFe.Emitente.CNPJ = cnpj;
            return SerializerTestHelper.Elemento(nfe.GetXml(), "infNFe", "emit", "CNPJ")?.Value;
        }

        [Fact]
        public void Emitente_CnpjAlfaMascarado_DeveSerializarSemMascaraEmMaiusculas()
        {
            Assert.Equal("12ABC34501DE35", CnpjEmitente("12.abc.345/01de-35"));
        }

        [Fact]
        public void Emitente_CnpjNumericoCurto_DevePreencherComZeroEsquerda()
        {
            Assert.Equal("01234567000195", CnpjEmitente("1234567000195"));
        }

        [Fact]
        public void Emitente_CnpjNumericoMascarado_DeveRemoverMascara()
        {
            Assert.Equal("10873538000245", CnpjEmitente("10.873.538/0002-45"));
        }

        [Fact]
        public void Emitente_Cpf_DeveSerializarSomenteDigitos()
        {
            var nfe = SerializerTestHelper.CarregarNFeModelo();
            nfe.InfNFe.Emitente.CNPJ = null;
            nfe.InfNFe.Emitente.CPF = "1234567890";

            Assert.Equal("01234567890", SerializerTestHelper.Elemento(nfe.GetXml(), "infNFe", "emit", "CPF")?.Value);
        }

        [Fact]
        public void Evento_ChaveAlfa_DevePreservarLetras()
        {
            const string chave = "352601" + "12ABC34501DE35" + "55001000000011" + "15047491" + "26";
            var evento = new NFeEvento();
            evento.InfEvento.Chave = chave;
            evento.InfEvento.CNPJ = "12.abc.345/01de-35";

            var xml = evento.GetXml();

            Assert.Equal(chave, SerializerTestHelper.Elemento(xml, "infEvento", "chNFe")?.Value);
            Assert.Equal("12ABC34501DE35", SerializerTestHelper.Elemento(xml, "infEvento", "CNPJ")?.Value);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("./-")]
        public void ValorSoComMascara_DeveSerTratadoComoVazio(string valor)
        {
            // PrimitiveSerializer é internal: chamado por reflexão.
            var metodo = typeof(NFeDoc).Assembly.GetType("Vip.DFe.Serializer.PrimitiveSerializer")
                .GetMethod("ProcessValue");
            var args = new object[] { false, TipoCampo.StrAlfaNumFill, valor, Ocorrencia.NaoObrigatoria, 14, null, null };

            var conteudo = (string)metodo.Invoke(null, args);

            Assert.Equal(string.Empty, conteudo);
            Assert.True((bool)args[0]);
        }
    }
}
