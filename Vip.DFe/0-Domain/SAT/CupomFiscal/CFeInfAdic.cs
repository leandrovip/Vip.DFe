using System.ComponentModel;
using System.Linq;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de Informações Adicionais
    /// </summary>
    public sealed class CFeInfAdic : DFeParentItem<CFeInfAdic, CFe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CFeInfAdic()
        {
            ObsFisco = new DFeCollection<CFeObsFisco>();
        }

        public CFeInfAdic(CFe parent) : this()
        {
            Parent = parent;
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     Informações Complementares de interesse do Contribuinte
        /// </summary>
        [DFeElement(TipoCampo.Str, "infCpl", Id = "Z02", Min = 1, Max = 5000, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string InfCpl { get; set; }

        /// <summary>
        ///     Grupo do campo de uso livre do Fisco
        ///     <br /><b>Campo preenchido pelo SAT</b>
        /// </summary>
        [DFeCollection("obsFisco", Id = "Z03", MinSize = 0, MaxSize = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<CFeObsFisco> ObsFisco { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeObsFisco()
        {
            return Parent.InfCFe.Versao < 0.08M && ObsFisco.Any();
        }

        #endregion Methods
    }
}