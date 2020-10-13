using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo COFINS tributado pela alíquota - CST 01, 02
    /// </summary>
    public sealed class CofinsAliq : GenericClone<CofinsAliq>, INFeCofins
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     S06 - Código de Situação Tributária da COFINS - CST 01, 02
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
        ///     S09 - Valor da COFINS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCOFINS", Id = "S09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCofins { get; set; }

        #endregion
    }
}