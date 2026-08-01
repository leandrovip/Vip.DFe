using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica padrão da CBS (ad rem)
    /// </summary>
    public class CbsAdRemPadrao : GenericClone<CbsAdRemPadrao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB96a - Quantidade tributada na monofasia
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCMono", Id = "UB96a", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QbcMono { get; set; }

        /// <summary>
        ///     UB96b - Alíquota ad rem da CBS
        /// </summary>
        [DFeElement(TipoCampo.De4, "adRemCBS", Id = "UB96b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal AdRemCbs { get; set; }

        /// <summary>
        ///     UB96c - Valor da CBS monofásica
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMono", Id = "UB96c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMono { get; set; }

        #endregion
    }
}