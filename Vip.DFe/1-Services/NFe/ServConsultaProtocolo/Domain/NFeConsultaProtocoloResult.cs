using System;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Evento;
using Vip.DFe.NFe.Protocolo;
using Vip.DFe.NFe.ServBase;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.ServConsultaProtocolo.Domain
{
    [DFeRoot("retConsSitNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public sealed class NFeConsultaProtocoloResult : NFeResultBase<NFeConsultaProtocoloResult>
    {
        #region Constructors

        public NFeConsultaProtocoloResult()
        {
            ProtNFe = new NFeProtNFe();
            EventoNFe = new DFeCollection<NFeProcEvento>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     ER07a - Data e hora do processamento
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhRecbto", Id = "ER07a", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhRecbto { get; set; }

        /// <summary>
        ///     ER07b - Chave de Acesso da NFe consultada
        /// </summary>
        [DFeElement(TipoCampo.StrNumber, "chNFe", Id = "ER07b", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string ChaveNFe { get; set; }

        /// <summary>
        ///     ER08 - Protocolo de autorização ou denegação de uso da NFe
        /// </summary>
        [DFeElement("protNFe", Id = "ER08", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeProtNFe ProtNFe { get; set; }

        /// <summary>
        ///     ER10 - Informação do evento e respectivo Protocolo de registro de Evento
        /// </summary>
        [DFeCollection("procEventoNFe", Id = "ER10", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeProcEvento> EventoNFe { get; set; }

        #endregion Properties
    }
}