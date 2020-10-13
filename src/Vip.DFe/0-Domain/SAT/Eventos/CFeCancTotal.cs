using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.Eventos
{
    public sealed class CFeCancTotal : GenericClone<CFeCancTotal>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        [DFeElement(TipoCampo.De2, "vCFe", Id = "W11", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VCFe { get; set; }

        #endregion Propriedades
    }
}