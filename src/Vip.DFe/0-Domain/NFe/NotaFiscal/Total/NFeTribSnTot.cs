using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    /// <summary>
    ///     W59h - Grupo de totais da composição do valor do IBS e da CBS para o Simples Nacional (NT 2026.005)
    /// </summary>
    public sealed class NFeTribSnTot : GenericClone<NFeTribSnTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W59i - Valor total do IBS para os optantes do Simples Nacional
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSSN", Id = "W59i", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsSN { get; set; }

        /// <summary>
        ///     W59j - Valor total da CBS para os optantes do Simples Nacional
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSSN", Id = "W59j", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsSN { get; set; }

        #endregion
    }
}