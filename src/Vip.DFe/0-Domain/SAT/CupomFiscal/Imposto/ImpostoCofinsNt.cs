using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    /// <summary>
    ///     Grupo de COFINS não tributado
    /// </summary>
    public sealed class ImpostoCofinsNt : GenericClone<ImpostoCofinsNt>, ICFeCofins
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Código de Situação Tributária da COFINS
        /// </summary>
        [DFeElement(TipoCampo.Str, "CST", Id = "S07", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        #endregion Properties
    }
}