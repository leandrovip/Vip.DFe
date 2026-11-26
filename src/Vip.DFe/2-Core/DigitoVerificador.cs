using System.Linq;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;

namespace Vip.DFe
{
    public sealed class DigitoVerificador
    {
        #region Methods

        /// <summary>
        ///     Obtem o dígito verificador
        /// </summary>
        public static int Obter(string documento)
        {
            documento = documento.SomenteAlfanumerico();
            Guard.Against<VipException>(documento.IsNullOrEmpty(), "Chave de Documento não informado");
            Guard.Against<VipException>(documento.Length != 43, "Chave de Documento inválida");

            const int multiplicadorInicial = 2;
            const int multiplicadorFinal = 9;
            var somaDigitos = 0;
            var valorBase = multiplicadorInicial;

            // calculo dos dígitos de traz pra frente com multiplicação pelo valor base
            // cada caractere vale (ASCII - 48): dígitos mantêm o valor, letras A=17, B=18...
            foreach (var number in documento.Reverse().Select(x => x - '0'))
            {
                var value = number * valorBase;
                somaDigitos += value;
                valorBase++;
                if (valorBase > multiplicadorFinal) valorBase = multiplicadorInicial;
            }

            var moduloFinal = somaDigitos % 11;
            return moduloFinal < 2 ? 0 : 11 - moduloFinal;
        }

        #endregion
    }
}