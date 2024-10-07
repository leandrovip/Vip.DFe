using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.Evento
{
    public sealed class NFeDetEvento : GenericClone<NFeDetEvento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     HP18 - Versão do Pedido de Cancelamento, da carta de correção ou EPEC, deve ser informado com a mesma informação da
        ///     tag verEvento (HP16)
        /// </summary>
        [DFeAttribute(TipoCampo.Enum, "versao", Id = "HP18", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeVersao Versao { get; set; }

        /// <summary>
        ///     HP19 - "Cancelamento", "Carta de Correção", "Carta de Correcao" ou "EPEC"
        /// </summary>
        [DFeElement(TipoCampo.Str, "descEvento", Id = "HP19", Min = 5, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string DescEvento { get; set; }

        #endregion

        #region Carta de Correção

        /// <summary>
        ///     HP20 - Correção a ser considerada, texto livre. A correção mais recente substitui as anteriores.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xCorrecao", Id = "HP20", Min = 15, Max = 1000, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCorrecao { get; set; }

        /// <summary>
        ///     HP20a - Condições de uso da Carta de Correção
        /// </summary>

        [DFeElement(TipoCampo.Custom, "xCondUso", Id = "HP20a", Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XCondUso { get; set; }

        #endregion

        #region Cancelamento

        /// <summary>
        ///     HP20 - Informar o número do Protocolo de Autorização da NF-e a ser Cancelada.
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "nProt", Id = "HP20", Min = 15, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NProt { get; set; }

        /// <summary>
        ///     HP21 - Informar a justificativa do cancelamento
        /// </summary>
        [DFeElement(TipoCampo.Str, "xJust", Id = "HP21", Min = 15, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XJust { get; set; }

        #endregion

        #region EPEC

        //private Estado? _cOrgaoAutor;

        ///// <summary>
        /////     P20 - Código do Órgão do Autor do Evento.
        /////     Nota: Informar o código da UF do Emitente para este evento.
        ///// </summary>
        //public Estado? cOrgaoAutor
        //{
        //    get => _cOrgaoAutor;
        //    set
        //    {
        //        if (value == null) return;
        //        descEvento = "EPEC";
        //        LimpaDadosCancelamento();
        //        LimpaDadosCartaCorrecao();
        //        _cOrgaoAutor = value;
        //    }
        //}

        ///// <summary>
        /////     P21 - Informar "1=Empresa Emitente" para este evento.
        /////     Nota: 1=Empresa Emitente; 2=Empresa Destinatária;
        /////     3=Empresa; 5=Fisco; 6=RFB; 9=Outros Órgãos.
        ///// </summary>
        //public TipoAutor? tpAutor { get; set; }

        ///// <summary>
        /////     P22 - Versão do aplicativo do Autor do Evento.
        ///// </summary>
        //public string verAplic { get; set; }

        ///// <summary>
        /////     P23 - Data e hora
        ///// </summary>
        //[XmlIgnore]
        //public DateTimeOffset? dhEmi { get; set; }

        ///// <summary>
        /////     Proxy para dhEmi no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        ///// </summary>
        //[XmlElement(ElementName = "dhEmi")]
        //public string ProxyDhEmi
        //{
        //    get => dhEmi.ParaDataHoraStringUtc();
        //    set => dhEmi = DateTimeOffset.Parse(value);
        //}

        ///// <summary>
        /////     P24 - 0=Entrada; 1=Saída;
        ///// </summary>
        //public TipoNFe? tpNF { get; set; }

        ///// <summary>
        /////     P25 - IE do Emitente
        ///// </summary>
        //public string IE { get; set; }

        ///// <summary>
        /////     P26
        ///// </summary>
        //public dest dest { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeXCorrecao()
        {
            return XCorrecao.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeXCondUso()
        {
            return XCondUso.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeNProt()
        {
            return NProt.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeXJust()
        {
            return XJust.IsNotNullOrEmpty();
        }

        private string SerializeXCondUso()
        {
            return DFeConstantes.CondicaoUso;
        }

        private object DeserializeXCondUso(string value)
        {
            return value;
        }

        private string SerializeXCorrecao() => XCorrecao.RemoveBreakline();

        private object DeserializeXCorrecao(string value) => value;

        #endregion
    }
}