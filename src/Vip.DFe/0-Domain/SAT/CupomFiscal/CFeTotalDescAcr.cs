using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de valores de entrada de Desconto/Acréscimo sobre Subtotal (DescAcrEntr)
    /// </summary>
    public sealed class CFeTotalDescAcr : GenericClone<CFeTotalDescAcr>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        /// <summary>
        ///     Valor de Entrada de Desconto sobre Subtotal
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDescSubtot", Id = "W20", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VDescSubtot { get; set; }

        /// <summary>
        ///     Valor de Entrada de Acréscimo sobre Subtotal
        /// </summary>
        [DFeElement(TipoCampo.De2, "vAcresSubtot", Id = "W21", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAcresSubtot { get; set; }

        #endregion Propriedades
    }
}