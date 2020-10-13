using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo COFINS não tributado - CST 04, 05, 06, 07, 08, 09
    /// </summary>
    public sealed class CofinsNt : GenericClone<CofinsNt>, INFeCofins
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     S06 - Código de Situação Tributária da COFINS - CST 04, 05, 06, 07, 08, 09
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "S06", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        #endregion
    }
}