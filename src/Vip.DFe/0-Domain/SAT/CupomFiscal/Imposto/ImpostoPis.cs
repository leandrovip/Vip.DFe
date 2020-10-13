using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.SAT.Interfaces;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    public sealed class ImpostoPIS : GenericClone<ImpostoPIS>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        [DFeItem(typeof(ImpostoPisAliq), "PISAliq")]
        [DFeItem(typeof(ImpostoPisNt), "PISNT")]
        [DFeItem(typeof(ImpostoPisOutr), "PISOutr")]
        [DFeItem(typeof(ImpostoPisQtde), "PISQtde")]
        [DFeItem(typeof(ImpostoPisSn), "PISSN")]
        public ICFePis Pis { get; set; }

        #endregion Properties
    }
}