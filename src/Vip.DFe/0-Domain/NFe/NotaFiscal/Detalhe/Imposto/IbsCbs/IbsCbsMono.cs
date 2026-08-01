using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da monofasia do IBS e da CBS
    /// </summary>
    public class IbsCbsMono : GenericClone<IbsCbsMono>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB85a - Grupo de informações da monofasia do IBS por aliquota ad rem
        /// </summary>
        [DFeElement("gIBSMonoAdRem", Id = "UB85a", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsMonoAdRem IbsMonoAdRem { get; set; }

        /// <summary>
        ///     UB90 - Grupo de informações da monofasia do IBS por aliquota ad valorem
        /// </summary>
        [DFeElement("gIBSMonoAdValorem", Id = "UB90", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IbsMonoAdValorem IbsMonoAdValorem { get; set; }

        /// <summary>
        ///     UB95a - Grupo de informações da monofasia da CBS por aliquota ad rem
        /// </summary>
        [DFeElement("gCBSMonoAdRem", Id = "UB95a", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CbsMonoAdRem CbsMonoAdRem { get; set; }

        /// <summary>
        ///     UB100 - Grupo de informações da monofasia da CBS por aliquota ad valorem
        /// </summary>
        [DFeElement("gCBSMonoAdValorem", Id = "UB100", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CbsMonoAdValorem CbsMonoAdValorem { get; set; }

        /// <summary>
        ///     UB105a - Total de IBS monofásico do item
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTotIBSMonoItem", Id = "UB105a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTotIbsMonoItem { get; set; }

        /// <summary>
        ///     UB105b - Total da CBS monofásica do item
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTotCBSMonoItem", Id = "UB105b", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTotCbsMonoItem { get; set; }

        #endregion
    }
}