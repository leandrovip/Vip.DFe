using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    public sealed class NFeIbsMunicipioTot : GenericClone<NFeIbsMunicipioTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W43 - Valor do diferimento do IBS Municipal
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDif", Id = "W43", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDif { get; set; }

        /// <summary>
        ///     W44 - Valor da devolução de tributos do IBS Municipal
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDevTrib", Id = "W44", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDevTrib { get; set; }

        /// <summary>
        ///     W46 - Valor do IBS Municipal
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMun", Id = "W46", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMun { get; set; }

        #endregion
    }
}