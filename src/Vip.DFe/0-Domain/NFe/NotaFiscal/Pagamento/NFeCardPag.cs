using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Pagamento
{
    public sealed class NFeCardPag : GenericClone<NFeCardPag>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     YA04a - Tipo de Integração para pagamento
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpIntegra", Id = "YA04a", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeTipoIntegracaoPagamento TpIntegra { get; set; }

        /// <summary>
        ///     YA05 - CNPJ da instituição de pagamento, adquirente ou subadquirente. Caso o pagamento seja processado pelo intermediador da transação, informar o CNPJ deste
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "YA05", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Cnpj { get; set; }

        /// <summary>
        ///     YA06 - Bandeira da operadora de cartão de crédito e/ou débito
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tBand", Id = "YA06", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeBandeiraCartao? Bandeira { get; set; }

        /// <summary>
        ///     YA07 - Número de autorização da operação cartão de crédito e/ou débito
        /// </summary>
        [DFeElement(TipoCampo.Str, "cAut", Id = "YA07", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CAut { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeBandeira()
        {
            return Bandeira.HasValue;
        }

        #endregion
    }
}