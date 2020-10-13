using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    /// <summary>
    ///     Informação do Imposto devolvido
    /// </summary>
    public sealed class NFeDetImpostoDevol : GenericClone<NFeDetImpostoDevol>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDetImpostoDevol()
        {
            Ipi = new NFeDetImpostoIpiDevol();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     UA02 - Percentual da mercadoria devolvida
        /// </summary>
        [DFeElement(TipoCampo.De2, "pDevol", Id = "UA02", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PDevol { get; set; }

        /// <summary>
        ///     UA03 - Informação do IPI devolvido
        /// </summary>
        [DFeElement("IPI", Id = "UA03", Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeDetImpostoIpiDevol Ipi { get; set; }

        #endregion
    }
}