using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    public class PisSt : GenericClone<PisSt>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     R02 - Valor da Base de Cálculo do PIS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "R02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     R03 - Alíquota do PIS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pPIS", Id = "R03", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PPis { get; set; }

        /// <summary>
        ///     R04 - Quantidade Vendida
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "R04", Min = 5, Max = 16, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QBcProd { get; set; }

        /// <summary>
        ///     R05 - Alíquota do PIS (em reais)
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "R05", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAliqProd { get; set; }

        /// <summary>
        ///     R06 - Valor do PIS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPIS", Id = "R06", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPis { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeVBc()
        {
            return PPis > 0;
        }

        private bool ShouldSerializeQBcProd()
        {
            return VAliqProd > 0;
        }

        #endregion
    }
}