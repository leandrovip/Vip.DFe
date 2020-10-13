using System;
using System.Text;
using System.Text.RegularExpressions;
using Vip.DFe.Extensions;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe
{
    public sealed class ChaveDFe
    {
        #region Properties

        public string Chave { get; }
        public int Digito { get; }

        #endregion Properties

        #region Constructors

        internal ChaveDFe(CodigoUF ufEmitente, DateTime dataEmissao, string cnpjEmitente, int modelo, int serie, long numero, TipoEmissao tipoEmissao, int codigoNumerico)
        {
            var chave = new StringBuilder();

            chave.Append(ufEmitente.GetDFeValue())
                .Append(dataEmissao.ToString("yyMM"))
                .Append(cnpjEmitente)
                .Append(modelo.ToString("D2"))
                .Append(serie.ToString("D3"))
                .Append(numero.ToString("D9"))
                .Append(tipoEmissao.GetDFeValue())
                .Append(codigoNumerico.ToString("D8"));

            var digitoVerificador = DigitoVerificador.Obter(chave.ToString());
            chave.Append(digitoVerificador);

            Chave = chave.ToString();
            Digito = digitoVerificador;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Gera a chave do documento fiscal
        /// </summary>
        /// <param name="ufEmitente">UF do emitente do DF-e</param>
        /// <param name="dataEmissao">Data de emissão do DF-e</param>
        /// <param name="cnpjEmitente">CNPJ do emitente do DF-e</param>
        /// <param name="modelo">Modelo do DF-e</param>
        /// <param name="serie">Série do DF-e</param>
        /// <param name="numero">Numero do DF-e</param>
        /// <param name="tipoEmissao">
        ///     Tipo de emissão do DF-e. Informar inteiro conforme consta no manual de orientação do contribuinte para o DF-e
        /// </param>
        /// <param name="codigoNumerico">Código numérico que compõe a Chave de Acesso. Número gerado pelo emitente para cada DF-e</param>
        /// <returns>Retorna a chave DFe</returns>
        public static ChaveDFe Gerar(CodigoUF ufEmitente, DateTime dataEmissao, string cnpjEmitente, int modelo, int serie, long numero, TipoEmissao tipoEmissao, int codigoNumerico)
        {
            return new ChaveDFe(ufEmitente, dataEmissao, cnpjEmitente, modelo, serie, numero, tipoEmissao, codigoNumerico);
        }

        /// <summary>
        ///     Informa se a chave de um DF-e é válida
        /// </summary>
        /// <param name="chave"></param>
        /// <returns></returns>
        public static bool Validar(string chave)
        {
            chave = chave.TrimVip();

            if (chave.IsNullOrEmpty()) return false;
            if (chave.Trim().Length != 44) return false;

            var digitoVerificador = chave.Substring(43, 1).ToInt32();
            var digitoEsperado = DigitoVerificador.Obter(chave.Substring(0, 43));
            return digitoVerificador == digitoEsperado;
        }

        /// <summary>
        ///     Formata a chave do documento fiscal.
        /// </summary>
        /// <param name="chave"></param>
        /// <returns></returns>
        public static string Formatar(string chave)
        {
            return Regex.Replace(chave, ".{4}", "$0 ");
        }

        #endregion Methods
    }
}