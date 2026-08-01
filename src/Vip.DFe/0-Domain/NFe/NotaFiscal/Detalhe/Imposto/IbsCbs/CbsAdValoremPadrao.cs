using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica padrão da CBS (ad valorem)
    /// </summary>
    public class CbsAdValoremPadrao : GenericClone<CbsAdValoremPadrao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB101a - Valor da Base de Cálculo da monofasia
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCMono", Id = "UB101a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcMono { get; set; }

        /// <summary>
        ///     UB101b - Alíquota ad valorem da CBS
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCBSMono", Id = "UB101b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCbsMono { get; set; }

        /// <summary>
        ///     UB101c - Valor da CBS monofásica
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMono", Id = "UB101c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMono { get; set; }

        #endregion
    }
}