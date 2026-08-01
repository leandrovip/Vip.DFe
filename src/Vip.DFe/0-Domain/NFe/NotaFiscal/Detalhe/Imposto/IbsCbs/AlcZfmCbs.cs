using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de operações em áreas incentivadas (ALC/ZFM) - CBS (alíquota zero)
    /// </summary>
    public class AlcZfmCbs : GenericClone<AlcZfmCbs>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB66b - Tipo de aplicação da alíquota zero da CBS
        /// </summary>
        [DFeElement(TipoCampo.Int, "tpALCZFMCBS", Id = "UB66b", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int TpAlcZfmCbs { get; set; }

        /// <summary>
        ///     UB66c - Número do processo na Suframa para o item comercializado
        /// </summary>
        [DFeElement(TipoCampo.Str, "nProcSuframa", Id = "UB66c", Min = 8, Max = 12, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NProcSuframa { get; set; }

        /// <summary>
        ///     UB66d - Percentual efetivo sem a redução
        ///     <br /> Alíquota efetiva de referência da CBS aplicável à operação fora de áreas ou regimes incentivados.
        /// </summary>
        [DFeElement(TipoCampo.De4, "pAliqEfetRegCBS", Id = "UB66d", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PAliqEfetRegCbs { get; set; }

        /// <summary>
        ///     UB66e - Valor efetivo sem a redução
        ///     <br /> Valor da CBS calculado para a operação fora de áreas ou regimes incentivados.
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTribRegCBS", Id = "UB66e", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTribRegCbs { get; set; }

        #endregion
    }
}