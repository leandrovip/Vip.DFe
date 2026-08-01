using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de Ajuste de Competência
    /// </summary>
    public class AjusteCompetencia : GenericClone<AjusteCompetencia>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB113 - Ano e mês referência do período de apuração (AAAA-MM)
        /// </summary>
        [DFeElement(TipoCampo.Str, "competApur", Id = "UB113", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CompetApur { get; set; }

        /// <summary>
        ///     UB114 - Valor do IBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBS", Id = "UB114", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbs { get; set; }

        /// <summary>
        ///     UB115 - Valor da CBS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBS", Id = "UB115", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbs { get; set; }

        #endregion
    }
}