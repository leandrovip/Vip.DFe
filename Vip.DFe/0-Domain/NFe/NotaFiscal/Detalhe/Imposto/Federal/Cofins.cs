using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.NFe.Interfaces;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     Grupo COFINS
    /// </summary>
    public sealed class Cofins : GenericClone<Cofins>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     <para>S02 (COFINSAliq) - Grupo COFINS tributado pela alíquota</para>
        ///     <para>S03 (COFINSQtde) - Grupo COFINS tributado por Qtde</para>
        ///     <para>S04 (COFINSNT) - Grupo COFINS não tributado</para>
        ///     <para>S05 (COFINSOutr) - Grupo COFINS Outras Operações</para>
        /// </summary>
        [DFeItem(typeof(CofinsAliq), "COFINSAliq")]
        [DFeItem(typeof(CofinsQtde), "COFINSQtde")]
        [DFeItem(typeof(CofinsNt), "COFINSNT")]
        [DFeItem(typeof(CofinsOutr), "COFINSOutr")]
        public INFeCofins Imposto { get; set; }

        #endregion
    }
}