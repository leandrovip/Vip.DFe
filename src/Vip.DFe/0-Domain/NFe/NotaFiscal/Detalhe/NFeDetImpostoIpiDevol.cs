using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    /// <summary>
    ///     Informação do IPI devolvido
    /// </summary>
    public class NFeDetImpostoIpiDevol : GenericClone<NFeDetImpostoIpiDevol>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UA04 - Valor do IPI devolvido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIPIDevol", Id = "UA04", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIpiDevol { get; set; }

        #endregion
    }
}