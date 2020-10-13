using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.NFe.Interfaces;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    public sealed class Pis : GenericClone<Pis>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     <para>Q02 (PISAliq) - Grupo PIS tributado pela alíquota</para>
        ///     <para>Q03 (PISQtde) - Grupo PIS tributado por Qtde</para>
        ///     <para>Q04 (PISNT) - Grupo PIS não tributado</para>
        ///     <para>Q05 (PISOutr) - Grupo PIS Outras Operações</para>
        /// </summary>
        [DFeItem(typeof(PisAliq), "PISAliq")]
        [DFeItem(typeof(PisQtde), "PISQtde")]
        [DFeItem(typeof(PisNt), "PISNT")]
        [DFeItem(typeof(PisOutr), "PISOutr")]
        public INFePis Imposto { get; set; }

        #endregion
    }
}