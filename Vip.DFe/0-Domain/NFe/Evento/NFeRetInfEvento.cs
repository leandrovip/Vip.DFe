using System;
using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.Evento
{
    public sealed class NFeRetInfEvento : GenericClone<NFeRetInfEvento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     HR12 - Identificador da TAG a ser assinada
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "Id", Id = "HR12", Min = 17, Max = 17, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Id { get; set; }

        /// <summary>
        ///     HR13 - Identificação do Ambiente: 1=Produção /2=Homologação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Id = "HR13", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TipoAmbiente TpAmb { get; set; }

        /// <summary>
        ///     HR14 - Versão da aplicação que registrou o Evento
        /// </summary>
        [DFeElement(TipoCampo.Str, "verAplic", Id = "HR14", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VerAplic { get; set; }

        /// <summary>
        ///     HR15 - Código da UF que registrou o Evento. Utilizar 91 para o Ambiente Nacional.
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cOrgao", Id = "HR15", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CodigoUF COrgao { get; set; }

        /// <summary>
        ///     HR16 - Código do status da resposta.
        /// </summary>
        [DFeElement(TipoCampo.Int, "cStat", Id = "HR16", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CStat { get; set; }

        /// <summary>
        ///     HR17 - Descrição do status da resposta.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMotivo", Id = "HR17", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string xMotivo { get; set; }

        /// <summary>
        ///     HR18 - Chave de Acesso da NF-e vinculada ao evento.
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "chNFe", Id = "HR18", Min = 44, Max = 44, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Chave { get; set; }

        /// <summary>
        ///     HR19 - Código do Tipo do Evento.
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpEvento", Id = "HR19", Min = 6, Max = 6, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeTipoEvento? TpEvento { get; set; }

        /// <summary>
        ///     HR20 - Descrição do Evento – “Cancelamento homologado”
        /// </summary>
        [DFeElement(TipoCampo.Str, "xEvento", Id = "HR20", Min = 5, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XEvento { get; set; }

        /// <summary>
        ///     HR21 - Sequencial do evento para o mesmo tipo de evento.
        /// </summary>
        [DFeElement(TipoCampo.Int, "nSeqEvento", Id = "HR21", Min = 1, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? NSeqEvento { get; set; }

        /// <summary>
        ///     R22 - (EPEC) Idem a mensagem de entrada.
        /// </summary>
        [DFeElement(TipoCampo.Str, "cOrgaoAutor", Id = "R22", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string COrgaoAutor { get; set; }

        /// <summary>
        ///     HR22 - CNPJ do destinatário da NFe
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJDest", Id = "HR22", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CNPJDest { get; set; }

        /// <summary>
        ///     HR23 - CPF do destinatário da NFe
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CPFDest", Id = "HR23", Min = 11, Max = 11, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CPFDest { get; set; }

        /// <summary>
        ///     HR24 - e-mail do destinatário informado na NF-e.
        /// </summary>
        [DFeElement(TipoCampo.Str, "emailDest", Id = "HR24", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string EmailDest { get; set; }

        /// <summary>
        ///     HR25 - Data e hora de registro do evento. Se o evento for rejeitado informar a data e hora de recebimento do
        ///     evento.
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhRegEvento", Id = "HR25", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhRegEvento { get; set; }

        /// <summary>
        ///     HR26 - Número do Protocolo da NF-e
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "nProt", Id = "HR26", Min = 15, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NProt { get; set; }

        /// <summary>
        ///     R25 - (EPEC) Relação de Chaves de Acesso de EPEC pendentes de conciliação, existentes no AN.
        /// </summary>
        [DFeElement(TipoCampo.Str, "chNFePend", Id = "R25", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string ChNFePend { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeTpEvento()
        {
            return TpEvento.HasValue;
        }

        private bool ShouldSerializeNSeqEvento()
        {
            return NSeqEvento.HasValue;
        }

        private bool ShouldSerializeCOrgaoAutor()
        {
            return COrgaoAutor.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCNPJDest()
        {
            return CNPJDest.IsNotNullOrEmpty();
        }

        private bool ShouldSerializeCPFDest()
        {
            return CPFDest.IsNotNullOrEmpty();
        }

        #endregion
    }
}