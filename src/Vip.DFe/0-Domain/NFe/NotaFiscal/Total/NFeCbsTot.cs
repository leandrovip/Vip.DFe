using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    public sealed class NFeCbsTot : GenericClone<NFeCbsTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W53 - Valor do diferimento da CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDif", Id = "W53", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDif { get; set; }

        /// <summary>
        ///     W54 - Valor da devolução de tributos da CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDevTrib", Id = "W54", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDevTrib { get; set; }

        /// <summary>
        ///     W56 - Valor da CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBS", Id = "W56", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbs { get; set; }

        /// <summary>
        ///     W56a - Valor do Crédito Presumido da CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredPres", Id = "W56a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCredPres { get; set; }

        /// <summary>
        ///     W56b - Valor do Crédito Presumido da CBS com Condição Suspensiva
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredPresCondSus", Id = "W56b", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCredPresCondSus { get; set; }

        #endregion
    }
}