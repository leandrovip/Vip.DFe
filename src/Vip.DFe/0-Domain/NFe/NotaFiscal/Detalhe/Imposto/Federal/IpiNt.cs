using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     O08 (IPINT) - Grupo CST 01, 02, 03, 04, 51, 52, 53, 54 e 55
    /// </summary>
    public sealed class IpiNt : GenericClone<IpiNt>, INFeIpi
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     O09 - Código da Situação Tributária do IPI: CST 01, 02, 03, 04, 51, 52, 53, 54 e 55
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "O09", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        #endregion
    }
}