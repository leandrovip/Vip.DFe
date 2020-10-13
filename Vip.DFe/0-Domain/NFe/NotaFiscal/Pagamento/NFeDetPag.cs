using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Pagamento
{
    public sealed class NFeDetPag : GenericClone<NFeDetPag>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public NFeDetPag()
        {
            TPag = MeioPagamento.Outros;
            Card = new NFeCardPag();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     YA01b - Indicador da Forma de Pagamento
        /// </summary>
        [DFeElement(TipoCampo.Enum, "indPag", Id = "YA01b", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeIndFormaPagamento? IndPag { get; set; }

        /// <summary>
        ///     YA02 - Forma de pagamento
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tPag", Id = "YA02", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public MeioPagamento TPag { get; set; }

        /// <summary>
        ///     YA03 - Valor do Pagamento
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPag", Id = "YA03", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPag { get; set; }

        /// <summary>
        ///     YA04 - Grupo de Cartões
        /// </summary>
        [DFeElement("card", Id = "YA04", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeCardPag Card { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeIndPag()
        {
            return IndPag.HasValue;
        }

        private bool ShouldSerializeCard()
        {
            return Card.Cnpj.IsNotNullOrEmpty() || Card.CAut.IsNotNullOrEmpty() || Card.Bandeira.HasValue;
        }

        #endregion
    }
}