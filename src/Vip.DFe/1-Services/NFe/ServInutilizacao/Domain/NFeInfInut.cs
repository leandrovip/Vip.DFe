using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.ServInutilizacao.Domain
{
    public sealed class NFeInfInut : GenericClone<NFeInfInut>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     DR04 - Identificador da TAG a ser assinada, somente precisa ser informado se a UF assinar a resposta.
        ///     Em caso de assinatura da resposta pela SEFAZ preencher o campo com o Nro do Protocolo, precedido com o literal
        ///     “ID”.
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "Id", Min = 1, Max = 255, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Id { get; set; }

        /// <summary>
        ///     DR05 - Identificação do Ambiente: 1 – Produção / 2 – Homologação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoAmbiente TpAmb { get; set; }

        /// <summary>
        ///     Versão do Aplicativo que processou o pedido de inutilização.
        ///     A versão deve ser iniciada com a sigla da UF nos casos de WS próprio ou a sigla SCAN, SVAN ou SVRS nos demais
        ///     casos.
        /// </summary>
        [DFeElement(TipoCampo.Str, "verAplic", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VerAplic { get; set; }

        /// <summary>
        ///     DR07 - Código do status da resposta (vide item 5.1.1).
        /// </summary>
        [DFeElement(TipoCampo.Int, "cStat", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CStat { get; set; }

        /// <summary>
        ///     DR08 - Descrição literal do status da resposta.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMotivo", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XMotivo { get; set; }

        /// <summary>
        ///     DR09 - Código da UF que atendeu a solicitação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cUF", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CodigoUF CUF { get; set; }

        /// <summary>
        ///     DR10 - Ano de inutilização da numeração
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "ano", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Ano { get; set; }

        /// <summary>
        ///     DR11 - CNPJ do emitente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     DR12 - Modelo da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Enum, "mod", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeModelo? Mod { get; set; }

        /// <summary>
        ///     DR13 - Série da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Int, "serie", Min = 1, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? Serie { get; set; }

        /// <summary>
        ///     DR14 - Número da NF-e inicial a ser inutilizada
        /// </summary>
        [DFeElement(TipoCampo.Int, "nNFIni", Min = 1, Max = 9, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? NrNfIni { get; set; }

        /// <summary>
        ///     DR15 - Número da NF-e final a ser inutilizada
        /// </summary>
        [DFeElement(TipoCampo.Int, "nNFFin", Min = 1, Max = 9, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? NrNfFin { get; set; }

        /// <summary>
        ///     DR16 - Data e hora de processamento
        ///     Formato = AAAA-MM-DDTHH:MM:SS Preenchido com data e hora da gravação no Banco de Dados em caso de Confirmação.
        ///     Em caso de Rejeição, com data e hora do recebimento do Pedido.
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhRecbto", Min = 1, Max = 19, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTimeOffset? DhRecbto { get; set; }

        /// <summary>
        ///     DR17 - Número do Protocolo de Inutilização (vide item 5.6).
        /// </summary>
        [DFeElement(TipoCampo.Str, "nProt", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NProt { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeAno()
        {
            return Ano.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCNPJ()
        {
            return CNPJ.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeMod()
        {
            return Mod.HasValue;
        }

        #endregion
    }
}