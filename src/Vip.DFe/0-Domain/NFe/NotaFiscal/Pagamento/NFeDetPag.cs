using System;
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
        ///     YA02a - Descrição do meio de pagamento
        /// </summary>
        [DFeElement(TipoCampo.Str, "xPag", Id = "YA02a", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XPag { get; set; }

        /// <summary>
        ///     YA03 - Valor do Pagamento
        /// </summary>
        [DFeElement(TipoCampo.De2, "vPag", Id = "YA03", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPag { get; set; }

        /// <summary>
        ///     YA03a - Data do pagamento (TData, formato AAAA-MM-DD). NT 2023.004
        /// </summary>
        [DFeElement(TipoCampo.Dat, "dPag", Id = "YA03a", Min = 10, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime? DPag { get; set; }

        /// <summary>
        ///     YA03c - CNPJ transacional do pagamento: CNPJ do estabelecimento onde o pagamento foi
        ///     processado/transacionado/recebido, quando a emissão do documento fiscal ocorrer em estabelecimento distinto.
        ///     Só é emitido em conjunto com <see cref="UFPag" />. NT 2023.004
        /// </summary>
        [DFeElement(TipoCampo.StrAlfaNumFill, "CNPJPag", Id = "YA03c", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CNPJPag { get; set; }

        /// <summary>
        ///     YA03d - Sigla da UF do CNPJ do estabelecimento onde o pagamento foi processado/transacionado/recebido (TUfEmi).
        ///     Só é emitido em conjunto com <see cref="CNPJPag" />. NT 2023.004
        /// </summary>
        [DFeElement(TipoCampo.Str, "UFPag", Id = "YA03d", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string UFPag { get; set; }

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
            return Card.Cnpj.IsNotNullOrEmpty() || Card.CAut.IsNotNullOrEmpty() || Card.Bandeira.HasValue ||
                   Card.CNPJReceb.IsNotNullOrEmpty() || Card.IdTermPag.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeDPag()
        {
            return DPag.HasValue;
        }

        private bool ShouldSerializeCNPJPag()
        {
            return CNPJPag.IsNotNullOrEmpty() && UFPag.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeUFPag()
        {
            return CNPJPag.IsNotNullOrEmpty() && UFPag.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeXPag()
        {
            return XPag.IsNotNullOrEmpty();
        }

        #endregion
    }
}