using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo PIS Outras Operações - CST 49 a 56, 60 a 67, 70 a 75, 98 e 99.
    ///     Leiaute: CST → choice obrigatório entre (vBC + pPIS) ou (qBCProd + vAliqProd) → vPIS.
    ///     Usa o par por quantidade quando QBcProd ou VAliqProd &gt; 0; caso contrário, o par por alíquota
    ///     (gerado mesmo zerado).
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
        [DFeElement(TipoCampo.De2, "vBC", Id = "Q07", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     Q08 - Alíquota do PIS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pPIS", Id = "Q08", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PPis { get; set; }

        /// <summary>
        ///     Q10 - Quantidade Vendida
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "Q10", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QBcProd { get; set; }

        /// <summary>
        ///     Q11 - Alíquota do PIS (em reais)
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "Q11", Min = 5, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VAliqProd { get; set; }

        /// <summary>
        ///     Q09 - Valor do PIS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPIS", Id = "Q09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPis { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Indica se o grupo usa o par por quantidade (qBCProd + vAliqProd) em vez do par por alíquota (vBC + pPIS).
        /// </summary>
        private bool UsaQuantidade => QBcProd > 0 || VAliqProd > 0;

        private bool ShouldSerializeVBc() => !UsaQuantidade;

        private bool ShouldSerializePPis() => !UsaQuantidade;

        private bool ShouldSerializeQBcProd() => UsaQuantidade;

        private bool ShouldSerializeVAliqProd() => UsaQuantidade;

        #endregion
    }
}