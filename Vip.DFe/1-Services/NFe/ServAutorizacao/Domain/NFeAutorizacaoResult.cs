using System;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Protocolo;
using Vip.DFe.NFe.ServBase;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.ServAutorizacao.Domain
{
    [DFeRoot("retEnviNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class NFeAutorizacaoResult : NFeResultBase<NFeAutorizacaoResult>
    {
        #region Constructors

        public NFeAutorizacaoResult()
        {
            InfRec = new NFeInfRec();
            ProtNFe = new NFeProtNFe();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     AR06b - Data e Hora do Recebimento Formato = AAAA-MM-DDTHH:MM:SS Preenchido com data e hora do recebimento do lote.
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhRecbto", Id = "AR06b", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhRecbto { get; set; }

        /// <summary>
        ///     AR07 - Dados do Recibo do Lote (Só é gerado se o Lote for aceito)
        /// </summary>
        [DFeElement("infRec", Id = "AR07", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeInfRec InfRec { get; set; }

        /// <summary>
        ///     AR11 - Dados do Protocolo de recebimento da NF-e gerado no caso do processamento síncrono do Lote de NF-e
        /// </summary>
        [DFeElement("protNFe", Id = "AR11", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeProtNFe ProtNFe { get; set; }

        #endregion
    }
}