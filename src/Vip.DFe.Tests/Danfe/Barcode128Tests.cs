using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vip.DFe.Danfe.Modelo;
using Xunit;

namespace Vip.DFe.Tests.Danfe
{
    /// <summary>
    ///     Testes do Code 128 híbrido C/A (NT Conjunta DF-e 2025.001, seção 6).
    ///     A classe Barcode128C é internal e o projeto não expõe InternalsVisibleTo; o acesso é via reflexão.
    /// </summary>
    public class Barcode128Tests
    {
        private static readonly Type TipoBarcode = typeof(DanfeViewModel).Assembly.GetType("Vip.DFe.Danfe.Elementos.Barcode128C", true);

        private static List<byte> Codificar(string code)
        {
            var m = TipoBarcode.GetMethod("Codificar", BindingFlags.Static | BindingFlags.NonPublic);
            return (List<byte>) m.Invoke(null, new object[] {code});
        }

        private static byte Checksum(IList<byte> valores)
        {
            var m = TipoBarcode.GetMethod("CalcularChecksum", BindingFlags.Static | BindingFlags.NonPublic);
            return (byte) m.Invoke(null, new object[] {valores});
        }

        // Algoritmo original (somente CODE-128C, prefixo "0" em tamanho ímpar) para regressão.
        private static List<byte> Legado(string code)
        {
            if (code.Length % 2 != 0) code = "0" + code;
            var l = new List<byte> {105};
            for (int i = 0; i < code.Length; i += 2) l.Add(byte.Parse(code.Substring(i, 2)));
            return l;
        }

        private static byte ChecksumLegado(List<byte> l)
        {
            int cd = 105;
            for (int i = 1; i < l.Count; i++)
            {
                cd += i * l[i];
                cd %= 103;
            }

            return (byte) cd;
        }

        [Fact]
        public void Checksum_ExemploOficialDaNT_DeveSer30()
        {
            var valores = new List<byte> {105, 52, 25, 101, 33, 34, 99, 83};

            Assert.Equal(30, Checksum(valores));
        }

        [Theory]
        [InlineData("35160810873538000245550010000000111504749126")]
        [InlineData("43210512345678000195550010000012341000012346")]
        [InlineData("26260100000000000191550020000000011000000019")]
        [InlineData("12345678901")]
        public void Numerico_DeveManterSaidaIdenticaAoAlgoritmoOriginal(string code)
        {
            // O construtor adiciona "0" em código numérico ímpar; Codificar recebe o código já normalizado.
            var normalizado = code.Length % 2 != 0 ? "0" + code : code;

            var novo = Codificar(normalizado);
            var antigo = Legado(code);

            Assert.Equal(antigo, novo);
            Assert.Equal(ChecksumLegado(antigo), Checksum(novo));
        }

        [Fact]
        public void ChaveNumerica_DeveTerPrimeirosValoresEsperados()
        {
            var valores = Codificar("35160810873538000245550010000000111504749126");

            Assert.Equal(new byte[] {105, 35, 16, 8, 10, 87}, valores.Take(6).ToArray());
            Assert.Equal(23, valores.Count);
        }

        [Fact]
        public void ChaveAlfanumerica_DeveUsarCodeAParaLetrasECodeCParaDigitosFinais()
        {
            // 352607 + CNPJ 12ABC34501DE35 + 24 dígitos = 44 posições
            const string chave = "352607" + "12ABC34501DE35" + "550010000000111504749126";
            Assert.Equal(44, chave.Length);

            var valores = Codificar(chave);

            var esperado = new byte[]
            {
                105, // Start C
                35, 26, 7, 12,
                101, 33, 34, 35, // CODE A, A B C
                19, 99, 45, 1, // '3' em A (ímpar), CODE C, 45, 01
                101, 36, 37, // CODE A, D E
                99, 35, 55, 0, 10, 0, 0, 0, 11, 15, 4, 74, 91, 26 // CODE C, dígitos finais
            };

            Assert.Equal(esperado, valores.ToArray());
            Assert.Equal(105, valores[0]);
            Assert.True(valores.IndexOf(101) < valores.IndexOf(33));
            Assert.Equal(99, valores[valores.Count - 13 - 1]);

            int soma = 105;
            for (int i = 1; i < esperado.Length; i++) soma += i * esperado[i];
            Assert.Equal(soma % 103, Checksum(valores));
        }

        [Fact]
        public void DigitosAntesDeLetra_Impar_UltimoDigitoVaiEmCodeA()
        {
            // "123A": C "12", CODE A, '3', 'A'
            Assert.Equal(new byte[] {105, 12, 101, 19, 33}, Codificar("123A").ToArray());
        }

        [Fact]
        public void Construtor_CaractereInvalido_DeveLancarArgumentException()
        {
            // Estilo é internal e exige fontes PDF; o construtor do código de barras só guarda a referência.
            var estilo = System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(
                TipoBarcode.Assembly.GetType("Vip.DFe.Danfe.Elementos.Estilo", true));
            var ex = Assert.Throws<TargetInvocationException>(() =>
                Activator.CreateInstance(TipoBarcode, "1234-5", estilo, 75F));

            Assert.IsType<ArgumentException>(ex.InnerException);
        }
    }
}
