using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    /// <summary>
    ///     Grupo de COFINS Outras Operações
    /// </summary>
    public sealed class ImpostoCofinsOutr : GenericClone<ImpostoCofinsOutr>, ICFeCofins
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        /// <summary>
        ///     Código de Situação Tributária da COFINS
        /// </summary>
        [DFeElement(TipoCampo.Str, "CST", Id = "Q07", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CST { get; set; }

        /// <summary>
        ///     Quantidade Vendida
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "S11", Min = 1, Max = 16, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QBcProd { get; set; }

        /// <summary>
        ///     Alíquota da COFINS(em reais)
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "S12", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAliqProd { get; set; }

        [DFeElement(TipoCampo.De2, "vBC", Id = "S08", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     Alíquota da COFINS (em percentual)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCOFINS", Id = "S09", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCofins { get; set; }

        /// <summary>
        ///     Valor da COFINS
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "S10", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
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