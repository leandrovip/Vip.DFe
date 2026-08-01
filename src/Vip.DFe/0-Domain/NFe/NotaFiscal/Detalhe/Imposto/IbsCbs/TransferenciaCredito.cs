using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de Transferência de Crédito
    /// </summary>
    public class TransferenciaCredito : GenericClone<TransferenciaCredito>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB107 - Valor do IBS a ser transferido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBS", Id = "UB107", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbs { get; set; }

        /// <summary>
        ///     UB108 - Valor da CBS a ser transferida
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBS", Id = "UB108", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbs { get; set; }

        #endregion
    }
}