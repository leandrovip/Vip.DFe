using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.CupomFiscal.Imposto;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de Tributos incidentes no Produto ou Serviço (imposto)
    /// </summary>
    public sealed class CFeDetImposto : GenericClone<CFeDetImposto>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CFeDetImposto()
        {
            CofinsSt = new ImpostoCofinsSt();
            Cofins = new ImpostoCofins();
            PisSt = new ImpostoPisSt();
            Pis = new ImpostoPIS();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     Valor aproximado dos tributos do Produto ou serviço – Lei 12741/12. Valor deve ser maior ou igual a zero.
        /// </summary>
        [DFeElement(TipoCampo.De2, "vItem12741", Id = "M02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VItem12741 { get; set; }

        /// <summary>
        ///     Grupo do ICMS da Operação própria e ST. O grupo ISSQN é mutuamente exclusivo com o grupo ICMS, isto é se ISSQN for
        ///     informado o grupo ICMS não será informado e vice-versa.
        /// </summary>
        [DFeItem(typeof(ImpostoIcms), "ICMS")]
        [DFeItem(typeof(ImpostoIssqn), "ISSQN")]
        public ICFeImposto Imposto { get; set; }

        /// <summary>
        ///     Grupo do PIS
        /// </summary>
        [DFeElement("PIS", Id = "Q01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public ImpostoPIS Pis { get; set; }

        /// <summary>
        ///     Grupo de PIS Substituição Tributária
        /// </summary>
        [DFeElement("PISST", Id = "R01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public ImpostoPisSt PisSt { get; set; }

        /// <summary>
        ///     Grupo do COFINS
        /// </summary>
        [DFeElement("COFINS", Id = "S01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public ImpostoCofins Cofins { get; set; }

        /// <summary>
        ///     Grupo do COFINS Substituição Tributária
        /// </summary>
        [DFeElement("COFINSST", Id = "T01", Ocorrencia = Ocorrencia.Obrigatoria)]
        public ImpostoCofinsSt CofinsSt { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeImposto()
        {
            if (Imposto is ImpostoIssqn issqn)
                return issqn.VDeducIssqn > 0 || issqn.VBc > 0 || issqn.VAliq > 0 || issqn.VIssqn > 0;

            return true;
        }

        private bool ShouldSerializePisSt()
        {
            return PisSt.PPis > 0 || PisSt.VBc > 0 || PisSt.QBcProd > 0 || PisSt.VPis > 0;
        }

        private bool ShouldSerializeCofinsSt()
        {
            return CofinsSt.PCofins > 0 || CofinsSt.VBc > 0 || CofinsSt.QBcProd > 0 || CofinsSt.VCofins > 0;
        }

        #endregion Methods
    }
}