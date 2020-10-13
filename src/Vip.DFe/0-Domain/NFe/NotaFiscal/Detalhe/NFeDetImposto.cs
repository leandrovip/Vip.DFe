using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Municipal;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetImposto : GenericClone<NFeDetImposto>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDetImposto()
        {
            Ipi = new Ipi();
            II = new II();
            Pis = new Pis();
            PisSt = new PisSt();
            Cofins = new Cofins();
            CofinsSt = new CofinsSt();
            IcmsUfDest = new IcmsUfDest();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     M02 - Valor estimado total de impostos federais, estaduais e municipais
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTotTrib", Id = "M02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VTotTrib { get; set; }

        /// <summary>
        ///     <br /> N01 - Dados do ICMS Normal e ST
        ///     <br /> U01 - Grupo ISSQN
        /// </summary>
        [DFeItem(typeof(Icms), "ICMS")]
        [DFeItem(typeof(Issqn), "ISSQN")]
        public INFeImposto Imposto { get; set; }

        /// <summary>
        ///     O01 - Grupo IPI
        /// </summary>
        [DFeElement("IPI", Id = "O01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public Ipi Ipi { get; set; }

        /// <summary>
        ///     P01 - Grupo Imposto de Importação
        /// </summary>
        [DFeElement("II", Id = "P01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public II II { get; set; }

        /// <summary>
        ///     Q01 - Grupo PIS
        /// </summary>
        [DFeElement("PIS", Id = "Q01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public Pis Pis { get; set; }

        /// <summary>
        ///     R01 - Grupo PIS Substituição Tributária
        /// </summary>
        [DFeElement("PISST", Id = "R01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public PisSt PisSt { get; set; }

        /// <summary>
        ///     S01 - Grupo COFINS
        /// </summary>
        [DFeElement("COFINS", Id = "S01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public Cofins Cofins { get; set; }

        /// <summary>
        ///     T01 - Grupo COFINS Substituição Tributária
        /// </summary>
        [DFeElement("COFINSST", Id = "T01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CofinsSt CofinsSt { get; set; }

        /// <summary>
        ///     NA01 - Informação do ICMS Interestadua
        /// </summary>
        [DFeElement("ICMSUFDest", Id = "NA01", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public IcmsUfDest IcmsUfDest { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeIpi()
        {
            return Ipi.CEnq.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeII()
        {
            return II.VIi > 0 || II.VIof > 0;
        }

        private bool ShouldSerializePisSt()
        {
            return PisSt.VPis > 0;
        }

        private bool ShouldSerializeCofinsSt()
        {
            return CofinsSt.VCofins > 0;
        }

        private bool ShouldSerializeIcmsUfDest()
        {
            return IcmsUfDest.VIcmsUfDest > 0 || IcmsUfDest.VIcmsUfRemet > 0;
        }

        #endregion
    }
}