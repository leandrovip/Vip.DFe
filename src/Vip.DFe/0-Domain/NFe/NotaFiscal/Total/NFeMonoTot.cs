using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    public sealed class NFeMonoTot : GenericClone<NFeMonoTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W58 - Valor do IBS monofásico próprio
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMono", Id = "W58", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMono { get; set; }

        /// <summary>
        ///     W59 - Valor da CBS monofásica própria
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMono", Id = "W59", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMono { get; set; }

        /// <summary>
        ///     W59a - Valor do IBS monofásico próprio sujeito a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMonoReten", Id = "W59a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMonoReten { get; set; }

        /// <summary>
        ///     W59b - Valor da CBS monofásica própria sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMonoReten", Id = "W59b", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMonoReten { get; set; }

        /// <summary>
        ///     W59c - Valor do IBS monofásico retido anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMonoRet", Id = "W59c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMonoRet { get; set; }

        /// <summary>
        ///     W59d - Valor da CBS monofásica retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMonoRet", Id = "W59d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMonoRet { get; set; }

        #endregion
    }
}