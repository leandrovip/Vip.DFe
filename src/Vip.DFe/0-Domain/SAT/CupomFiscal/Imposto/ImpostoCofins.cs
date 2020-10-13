using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.SAT.Interfaces;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    public sealed class ImpostoCofins : GenericClone<ImpostoCofins>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        [DFeItem(typeof(ImpostoCofinsAliq), "COFINSAliq")]
        [DFeItem(typeof(ImpostoCofinsNt), "COFINSNT")]
        [DFeItem(typeof(ImpostoCofinsOutr), "COFINSOutr")]
        [DFeItem(typeof(ImpostoCofinsQtde), "COFINSQtde")]
        [DFeItem(typeof(ImpostoCofinsSn), "COFINSSN")]
        public ICFeCofins Cofins { get; set; }

        #endregion Properties
    }
}