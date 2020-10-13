using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServConsultaProtocolo
{
    public class NFeServConsultaProtocolo : NFeServBase<INFeConsultaProtocolo>
    {
        #region Constructors

        public NFeServConsultaProtocolo(NFeConfig config, X509Certificate2 certificado = null) : base(config, NFeTipoServico.NfeConsultaProtocolo, certificado)
        {
            Schema = NFeSchema.ConsSitNFe;
            ArquivoEnvio = "env-ped";
            ArquivoResposta = "ret-ped";
        }

        #endregion

        #region Methods

        public NFeConsultaProtocoloResposta Consulta(string chave)
        {
            Guard.Against<ArgumentNullException>(chave.IsNullOrEmpty(), "Chave inválida para consulta");

            lock (serviceLock)
            {
                var request = new StringBuilder();
                request.Append($"<consSitNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"{Configuracoes.Versao.GetDescription()}\">");
                request.Append($"<tpAmb>{Configuracoes.Ambiente.GetDFeValue()}</tpAmb>");
                request.Append("<xServ>CONSULTAR</xServ>");
                request.Append($"<chNFe>{chave.TrimVip()}</chNFe>");
                request.Append("</consSitNFe>");

                var dadosMsg = request.ToString();
                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var value = new ConsultaProtocoloRequest(doc);
                var returnValue = Channel.Consultar(value);
                return new NFeConsultaProtocoloResposta(dadosMsg, returnValue.Mensagem.OuterXml, EnvelopeSoap, RetornoWS);
            }
        }

        #endregion
    }
}