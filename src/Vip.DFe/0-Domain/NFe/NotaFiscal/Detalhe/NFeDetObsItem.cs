using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    /// <summary>
    ///     Grupo de observações de uso livre para o item da NF-e
    /// </summary>
    public sealed class NFeDetObsItem : GenericClone<NFeDetObsItem>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     VA02 - Grupo de observações de uso livre do contribuinte para o item da NF-e
        /// </summary>
        [DFeElement("obsCont", Id = "VA02", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDetObs ObsCont { get; set; }

        /// <summary>
        ///     VA04 - Grupo de observações de uso livre do Fisco para o item da NF-e
        /// </summary>
        [DFeElement("obsFisco", Id = "VA04", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeDetObs ObsFisco { get; set; }

        #endregion
    }
}