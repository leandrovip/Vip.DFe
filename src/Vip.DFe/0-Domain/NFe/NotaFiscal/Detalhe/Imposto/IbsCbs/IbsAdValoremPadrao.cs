using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica padrão do IBS (ad valorem)
    /// </summary>
    public class IbsAdValoremPadrao : GenericClone<IbsAdValoremPadrao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB91a - Valor da Base de Cálculo da monofasia
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCMono", Id = "UB91a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcMono { get; set; }

        /// <summary>
        ///     UB91b - Alíquota ad valorem do IBS
        /// </summary>
        [DFeElement(TipoCampo.De4, "pIBSMono", Id = "UB91b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIbsMono { get; set; }

        /// <summary>
        ///     UB91c - Valor do IBS monofásico
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMono", Id = "UB91c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMono { get; set; }

        #endregion
    }
}