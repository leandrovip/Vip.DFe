using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    /// <summary>
    ///     ISSQNtot Grupo Totais referentes ao ISSQN
    /// </summary>
    public sealed class NFeIssqnTot : GenericClone<NFeIssqnTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W18 - Valor total dos Serviços sob não-incidência ou não tributados pelo ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vServ", Id = "W18", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VServ { get; set; }

        /// <summary>
        ///     W19 - Valor total Base de Cálculo do ISS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "W19", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     W20 - Valor total do ISS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vISS", Id = "W20", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIss { get; set; }

        /// <summary>
        ///     W21 - Valor total do PIS sobre serviços
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPIS", Id = "W21", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VPis { get; set; }

        /// <summary>
        ///     W22 - Valor total da COFINS sobre serviços
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "W22", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VCofins { get; set; }

        /// <summary>
        ///     W22a - Data da prestação do serviço
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dCompet", Id = "W22a", Min = 10, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string DCompet { get; set; }

        /// <summary>
        ///     W22b - Valor total dedução para redução da Base de Cálculo
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDeducao", Id = "W22b", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDeducao { get; set; }

        /// <summary>
        ///     W22c - Valor total outras retenções
        /// </summary>
        [DFeElement(TipoCampo.De2, "vOutro", Id = "W22c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal vOutro { get; set; }

        /// <summary>
        ///     W22d - Valor total desconto incondicionado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDescIncond", Id = "W22d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDescIncond { get; set; }

        /// <summary>
        ///     W22e - Valor total desconto condicionado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDescCond", Id = "W22e", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDescCond { get; set; }

        /// <summary>
        ///     W22f - Valor total retenção ISS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vISSRet", Id = "W22f", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIssRet { get; set; }

        /// <summary>
        ///     W22g - Código do Regime Especial de Tributação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cRegTrib", Id = "W22g", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public RegimeTributario? CRegTrib { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeDCompet()
        {
            return DCompet.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCRegTrib()
        {
            return CRegTrib.HasValue;
        }

        #endregion
    }
}