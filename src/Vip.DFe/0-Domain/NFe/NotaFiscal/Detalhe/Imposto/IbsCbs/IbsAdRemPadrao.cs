using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica padrão do IBS (ad rem)
    /// </summary>
    public class IbsAdRemPadrao : GenericClone<IbsAdRemPadrao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB86a - Quantidade tributada na monofasia
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCMono", Id = "UB86a", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QbcMono { get; set; }

        /// <summary>
        ///     UB86b - Alíquota ad rem do IBS
        /// </summary>
        [DFeElement(TipoCampo.De4, "adRemIBS", Id = "UB86b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal AdRemIbs { get; set; }

        /// <summary>
        ///     UB86c - Valor do IBS monofásico
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMono", Id = "UB86c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMono { get; set; }

        #endregion
    }
}