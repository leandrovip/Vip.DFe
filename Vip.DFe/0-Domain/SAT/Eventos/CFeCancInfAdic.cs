using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.SAT.CupomFiscal;

namespace Vip.DFe.SAT.Eventos
{
    public sealed class CFeCancInfAdic : GenericClone<CFeCancInfAdic>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CFeCancInfAdic()
        {
            ObsFisco = new DFeCollection<CFeObsFisco>();
        }

        #endregion Constructors

        #region Propriedades

        [DFeCollection("obsFisco", Id = "Z03", MinSize = 0, MaxSize = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<CFeObsFisco> ObsFisco { get; set; }

        #endregion Propriedades
    }
}