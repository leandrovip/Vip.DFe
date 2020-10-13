using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    /// <summary>
    ///     W23 - Grupo Retenções de Tributos
    /// </summary>
    public sealed class NFeRetTrib : GenericClone<NFeRetTrib>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W24 - Valor Retido de PIS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vRetPIS", Id = "W24", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VRetPis { get; set; }

        /// <summary>
        ///     W25 - Valor Retido de COFINS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vRetCOFINS", Id = "W25", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VRetCofins { get; set; }

        /// <summary>
        ///     W26 - Valor Retido de CSLL
        /// </summary>
        [DFeElement(TipoCampo.De2, "vRetCSLL", Id = "W26", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VRetCSLL { get; set; }

        /// <summary>
        ///     W27 - Base de Cálculo do IRRF
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCIRRF", Id = "W27", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcIrrf { get; set; }

        /// <summary>
        ///     W28 - Valor Retido do IRRF
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIRRF", Id = "W28", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIrrf { get; set; }

        /// <summary>
        ///     W29 - Base de Cálculo da Retenção da Previdência Social
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCRetPrev", Id = "W29", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcRetPrev { get; set; }

        /// <summary>
        ///     W30 - Valor da Retenção da Previdência Social
        /// </summary>
        [DFeElement(TipoCampo.De2, "vRetPrev", Id = "W30", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VRetPrev { get; set; }

        #endregion
    }
}