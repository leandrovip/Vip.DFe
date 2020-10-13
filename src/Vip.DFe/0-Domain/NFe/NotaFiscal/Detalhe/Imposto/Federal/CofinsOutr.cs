using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo COFINS Outras Operações - CST 49, 50, 51, 52, 53, 54, 55
    /// </summary>
    public sealed class CofinsOutr : GenericClone<CofinsOutr>, INFeCofins
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     S06 - Código de Situação Tributária da COFINS - CST 49, 50, 51, 52, 53, 54, 55
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "S06", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        /// <summary>
        ///     S07 - Valor da Base de Cálculo da COFINS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "S07", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     S08 - Alíquota da COFINS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCOFINS", Id = "S08", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PCofins { get; set; }

        /// <summary>
        ///     S09 - Quantidade Vendida
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "S09", Min = 5, Max = 16, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QBcProd { get; set; }

        /// <summary>
        ///     S10 - Alíquota da COFINS (em reais)
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "S10", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAliqProd { get; set; }

        /// <summary>
        ///     S11 - Valor da COFINS
        /// </summary>
        [DFeElement(TipoCampo.De4, "vCOFINS", Id = "S11", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
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