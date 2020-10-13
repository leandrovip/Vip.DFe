using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    /// <summary>
    ///     Grupo Totais referentes ao ISSQN
    /// </summary>
    public sealed class NFeTotal : GenericClone<NFeTotal>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeTotal()
        {
            IcmsTot = new NFeIcmsTot();
            IssqnTot = new NFeIssqnTot();
            RetTrib = new NFeRetTrib();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     W02 - Grupo Totais referentes ao ICMS
        /// </summary>
        [DFeElement("ICMSTot", Id = "W02", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeIcmsTot IcmsTot { get; set; }

        /// <summary>
        ///     W17 - Grupo Totais referentes ao ISSQN
        /// </summary>
        [DFeElement("ISSQNtot", Id = "W17", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeIssqnTot IssqnTot { get; set; }

        /// <summary>
        ///     W23 - Grupo Retenções de Tributos
        /// </summary>
        [DFeElement("retTrib", Id = "W23", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeRetTrib RetTrib { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeIssqnTot()
        {
            return IssqnTot.VServ > 0 || IssqnTot.VBc > 0;
        }

        private bool ShouldSerializeRetTrib()
        {
            return RetTrib.VRetPis > 0 || RetTrib.VRetCofins > 0 || RetTrib.VRetCSLL > 0 || RetTrib.VBcIrrf > 0 || RetTrib.VIrrf > 0 || RetTrib.VBcRetPrev > 0 || RetTrib.VRetPrev > 0;
        }

        #endregion
    }
}