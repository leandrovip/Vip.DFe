using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Total
{
    /// <summary>
    ///     W31 - Grupo do Imposto Seletivo
    /// </summary>
    public sealed class NFeIsTot : GenericClone<NFeIsTot>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     W33 - Valor do Imposto Seletivo
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIS", Id = "W33", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIS { get; set; }

        #endregion
    }
}