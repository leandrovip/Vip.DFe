using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    public sealed class NFeIbsUfTot : GenericClone<NFeIbsUfTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W38 - Valor do diferimento do IBS na UF
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDif", Id = "W38", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDif { get; set; }

        /// <summary>
        ///     W39 - Valor da devolução de tributos do IBS na UF
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDevTrib", Id = "W39", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDevTrib { get; set; }

        /// <summary>
        ///     W41 - Valor do IBS na UF
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSUF", Id = "W41", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsUf { get; set; }

        #endregion
    }
}