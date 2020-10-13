using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    /// <summary>
    ///     Grupo de COFINS Substituição Tributária
    /// </summary>
    public sealed class ImpostoCofinsSt : GenericClone<ImpostoCofinsSt>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        /// <summary>
        ///     Valor da Base de Cálculo da COFINS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "T02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     Alíquota da COFINS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCOFINS", Id = "T03", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCofins { get; set; }

        /// <summary>
        ///     Quantidade Vendida
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "T04", Min = 1, Max = 16, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QBcProd { get; set; }

        /// <summary>
        ///     Alíquota da COFINS (em reais)
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "T05", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAliqProd { get; set; }

        /// <summary>
        ///     Valor da COFINS
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "T06", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VCofins { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeVBc()
        {
            return VAliqProd == 0;
        }

        private bool ShouldSerializePCofins()
        {
            return VAliqProd == 0;
        }

        #endregion Methods
    }
}