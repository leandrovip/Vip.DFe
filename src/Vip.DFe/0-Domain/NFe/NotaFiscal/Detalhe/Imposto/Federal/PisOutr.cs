using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo PIS Outras Operações - CST 49, 50, 51, 52, 53, 54, 55, 56, 60, 61, 62
    /// </summary>
    public class PisOutr : GenericClone<PisOutr>, INFePis
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Q06 - Código de Situação Tributária do PIS -  CST 49, 50, 51, 52, 53, 54, 55, 56, 60, 61, 62
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "Q06", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        /// <summary>
        ///     Q07 - Valor da Base de Cálculo do PIS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "Q07", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     Q08 - Alíquota do PIS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pPIS", Id = "Q08", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PPis { get; set; }

        /// <summary>
        ///     Q10 - Quantidade Vendida
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "Q10", Min = 5, Max = 16, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QBcProd { get; set; }

        /// <summary>
        ///     Q11 - Alíquota do PIS (em reais)
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "Q11", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAliqProd { get; set; }

        /// <summary>
        ///     Q09 - Valor do PIS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPIS", Id = "Q09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
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