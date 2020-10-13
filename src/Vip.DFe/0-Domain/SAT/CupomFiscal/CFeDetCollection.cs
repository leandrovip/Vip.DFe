using System.ComponentModel;
using Vip.DFe.Document;

namespace Vip.DFe.SAT.CupomFiscal
{
    public sealed class CFeDetCollection : DFeParentCollection<CFeDet, CFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CFeDetCollection()
        {
            Parent = null;
        }

        public CFeDetCollection(CFe parent)
        {
            Parent = parent;
        }

        #endregion Constructors
    }
}