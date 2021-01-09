using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo PIS tributado por Qtde - CST 03
    /// </summary>
    public sealed class PisQtde : GenericClone<PisQtde>, INFePis
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public PisQtde()
        {
            Cst = "03";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Q06 - Código de Situação Tributária do PIS
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "Q06", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; private set; }

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
    }
}