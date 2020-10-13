using System.ComponentModel;
using Vip.DFe.Document;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public sealed class NFeDetCollection : DFeParentCollection<NFeDetalhe, NFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDetCollection()
        {
            Parent = null;
        }

        public NFeDetCollection(NFe parent)
        {
            Parent = parent;
        }

        #endregion
    }
}