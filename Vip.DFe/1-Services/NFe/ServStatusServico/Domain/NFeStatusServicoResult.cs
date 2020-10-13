using System;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.ServBase;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.ServStatusServico.Domain
{
    [DFeRoot("retConsStatServ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class NFeStatusServicoResult : NFeResultBase<NFeStatusServicoResult>
    {
        /// <summary>
        ///     FR08 - Data e hora do processamento
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhRecbto", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhRecbto { get; set; }

        /// <summary>
        ///     FR09 - Tempo médio de resposta do serviço (em segundos)
        /// </summary>
        [DFeElement(TipoCampo.Int, "tMed", Min = 0, Max = 255, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int TMed { get; set; }

        /// <summary>
        ///     FR10 - Preencher com data e hora previstas para o retorno do Web Service
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhRetorno", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhRetorno { get; set; }

        /// <summary>
        ///     FR11 - Informações adicionais para o Contribuinte
        /// </summary>
        [DFeElement(TipoCampo.Str, "xObs", Min = 0, Max = 255, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Obs { get; set; }
    }
}