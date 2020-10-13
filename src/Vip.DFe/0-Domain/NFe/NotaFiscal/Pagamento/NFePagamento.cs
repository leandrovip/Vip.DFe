using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Pagamento
{
    /// <summary>
    ///     Grupo de Formas de Pagamento
    /// </summary>
    public sealed class NFePagamento : GenericClone<NFePagamento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFePagamento()
        {
            DetPag = new DFeCollection<NFeDetPag>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     YA01a - Grupo Detalhamento da Forma de Pagamento 1-100
        /// </summary>
        [DFeCollection("detPag", Id = "YA01a", MinSize = 1, MaxSize = 100, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCollection<NFeDetPag> DetPag { get; set; }

        /// <summary>
        ///     YA09 - Valor do troco
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTroco", Id = "YA09", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VTroco { get; set; }

        #endregion
    }
}