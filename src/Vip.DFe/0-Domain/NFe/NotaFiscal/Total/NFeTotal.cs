using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

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

        /// <summary>
        ///     W31 - Grupo do Imposto Seletivo
        /// </summary>
        [DFeElement("ISTot", Id = "W31", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeIsTot IsTot { get; set; }

        /// <summary>
        ///     W34 - Grupo do IBS/CBS total
        /// </summary>
        [DFeElement("IBSCBSTot", Id = "W34", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeIbsCbsTot IbsCbsTot { get; set; }

        /// <summary>
        ///     W60 - Valor Total da NF-e
        /// </summary>
        [DFeElement(TipoCampo.De2, "vNFTot", Id = "W60", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VNfTot { get; set; }

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

        private bool ShouldSerializeIsTot()
        {
            return IsTot != null && IsTot.VIS > 0;
        }

        private bool ShouldSerializeIbsCbsTot()
        {
            return IbsCbsTot != null && (IbsCbsTot.VBcIbsCbs > 0 || IbsCbsTot.Ibs != null || IbsCbsTot.Cbs != null || IbsCbsTot.Mono != null || IbsCbsTot.EstornoCredito != null);
        }

        private bool ShouldSerializeVNfTot()
        {
            return VNfTot > 0;
        }

        #endregion
    }
}