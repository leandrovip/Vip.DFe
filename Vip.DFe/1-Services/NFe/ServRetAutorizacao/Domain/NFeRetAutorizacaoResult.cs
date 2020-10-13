using System;
using Vip.DFe.Attributes;
using Vip.DFe.Document;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Protocolo;
using Vip.DFe.NFe.ServBase;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.ServRetAutorizacao.Domain
{
    [DFeRoot("retConsReciNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class NFeRetAutorizacaoResult : NFeResultBase<NFeRetAutorizacaoResult>
    {
        #region Constructors

        public NFeRetAutorizacaoResult()
        {
            ProtNFe = new DFeCollection<NFeProtNFe>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     BR06a1 - Data e hora do processamento, Formato: “AAAA-MM-DDThh:mm:ssTZD
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhRecbto", Id = "BR06a1", Min = 1, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhRecbto { get; set; }

        /// <summary>
        ///     BR06b - Código da Mensagem (v2.0) Campo de uso da SEFAZ para enviar mensagem de interesse da SEFAZ para o emissor.
        /// </summary>
        [DFeElement(TipoCampo.Int, "cMsg", Id = "BR06b", Min = 1, Max = 4, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int CMsg { get; set; }

        /// <summary>
        ///     BR06c -  Mensagem da SEFAZ para o emissor.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMsg", Id = "BR06c", Min = 1, Max = 2000, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XMsg { get; set; }

        [DFeCollection("protNFe", Id = "BR07", MinSize = 0, MaxSize = 50, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<NFeProtNFe> ProtNFe { get; set; }

        #endregion
    }
}