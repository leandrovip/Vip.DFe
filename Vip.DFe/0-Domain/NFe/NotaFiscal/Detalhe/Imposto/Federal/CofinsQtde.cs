using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo de COFINS tributado por Qtde
    /// </summary>
    public sealed class CofinsQtde : GenericClone<CofinsQtde>, INFeCofins
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     S06 - Código de Situação Tributária da COFINS
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "S06", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst => "03";

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
        ///     S11 - Valor da COFINS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "S11", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCofins { get; set; }

        #endregion
    }
}