using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal
{
    /// <summary>
    ///     Grupo de informações sobre Pagamento do CFe
    /// </summary>
    public sealed class CFePgto : GenericClone<CFePgto>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CFePgto()
        {
            Pagamentos = new DFeCollection<CFePgtoMp>();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     Grupo de  informações dos Meios de Pagamento empregados na quitação do CF-e
        /// </summary>
        [DFeCollection("MP", Id = "WA02", MinSize = 1, MaxSize = 50, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCollection<CFePgtoMp> Pagamentos { get; set; }

        /// <summary>
        ///     Valor do troco
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTroco", Id = "WA06", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VTroco { get; set; }

        #endregion Propriedades
    }
}