using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo COFINS Outras Operações - CST 49 a 56, 60 a 67, 70 a 75, 98 e 99.
    ///     Leiaute: CST → choice obrigatório entre (vBC + pCOFINS) ou (qBCProd + vAliqProd) → vCOFINS.
    ///     Usa o par por quantidade quando QBcProd ou VAliqProd &gt; 0; caso contrário, o par por alíquota
    ///     (gerado mesmo zerado).
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
        [DFeElement(TipoCampo.De2, "vBC", Id = "S07", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     S08 - Alíquota da COFINS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCOFINS", Id = "S08", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCofins { get; set; }

        /// <summary>
        ///     S09 - Quantidade Vendida
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "S09", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QBcProd { get; set; }

        /// <summary>
        ///     S10 - Alíquota da COFINS (em reais)
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "S10", Min = 5, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VAliqProd { get; set; }

        /// <summary>
        ///     S11 - Valor da COFINS (TDec_1302: 2 casas decimais)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "S11", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCofins { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Indica se o grupo usa o par por quantidade (qBCProd + vAliqProd) em vez do par por alíquota (vBC + pCOFINS).
        /// </summary>
        private bool UsaQuantidade => QBcProd > 0 || VAliqProd > 0;

        private bool ShouldSerializeVBc() => !UsaQuantidade;

        private bool ShouldSerializePCofins() => !UsaQuantidade;

        private bool ShouldSerializeQBcProd() => UsaQuantidade;

        private bool ShouldSerializeVAliqProd() => UsaQuantidade;

        #endregion
    }
}