using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo PIS tributado pela alíquota
    /// </summary>
    public sealed class PisAliq : GenericClone<PisAliq>, INFePis
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Q06 - Código de Situação Tributária do PIS - 01, 02
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
        ///     Q09 - Valor do PIS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPIS", Id = "Q09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPis { get; set; }

        #endregion
    }
}