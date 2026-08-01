using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    public sealed class NFeEstornoCreditoTot : GenericClone<NFeEstornoCreditoTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W59f - Valor do estorno de crédito do IBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSEstCred", Id = "W59f", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsEstCred { get; set; }

        /// <summary>
        ///     W59g - Valor do estorno de crédito da CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSEstCred", Id = "W59g", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsEstCred { get; set; }

        #endregion
    }
}