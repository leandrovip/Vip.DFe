using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    public class CofinsSt : GenericClone<CofinsSt>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     T02 - Valor da Base de Cálculo da COFINS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "T02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     T03 - Alíquota da COFINS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCOFINS", Id = "T03", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PCofins { get; set; }

        /// <summary>
        ///     T04 - Quantidade Vendida
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "T04", Min = 5, Max = 16, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QBcProd { get; set; }

        /// <summary>
        ///     T05 - Alíquota da COFINS (em reais)
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "T05", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAliqProd { get; set; }

        /// <summary>
        ///     T06 - Valor da COFINS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "T06", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCofins { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeVBc()
        {
            return PCofins > 0;
        }

        private bool ShouldSerializeQBcProd()
        {
            return VAliqProd > 0;
        }

        #endregion
    }
}