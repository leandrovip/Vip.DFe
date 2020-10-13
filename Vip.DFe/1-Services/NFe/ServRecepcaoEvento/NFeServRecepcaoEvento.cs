using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Vip.DFe.Extensions;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Evento;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServRecepcaoEvento
{
    public class NFeServRecepcaoEvento : NFeServBase<INFeRecepcaoEvento>
    {
        #region Constructor

        public NFeServRecepcaoEvento(NFeConfig config, X509Certificate2 certificado) : base(config, NFeTipoServico.RecepcaoEvento, certificado)
        {
            ArquivoEnvio = "env-eve";
            ArquivoResposta = "ret-eve";
        }

        #endregion

        #region Methods

        public NFeRecepcaoEventoResposta RecepcaoEvento(NFeEvento evento)
        {
            lock (serviceLock)
            {
                switch (evento.InfEvento.TpEvento)
                {
                    case NFeTipoEvento.Cancelamento:
                        Schema = NFeSchema.EnvEventoCancNFe;
                        break;
                    case NFeTipoEvento.CartaCorrecao:
                        Schema = NFeSchema.EnvCCe;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("O evento informado é desconhecido ou não está implementado");
                }

                var request = new StringBuilder();
                request.Append($"<envEvento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"{evento.Versao.GetDFeValue()}\">");
                request.Append($"<idLote>{DFeHelper.GerarNumeroLote()}</idLote>");

                var saveOptions = Configuracoes.ObterOptions();
                evento.Assinar(Certificado, saveOptions);
                var xmlEvento = evento.GetXml(saveOptions);
                GravarXmlAssinado(xmlEvento, evento.ObterNomeXml());

                request.Append(xmlEvento);
                request.Append("</envEvento>");

                var dadosMsg = request.ToString();
                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var value = new RecepcaoEventoRequest(doc);
                var returnValue = Channel.RecepcaoEvento(value);
                return new NFeRecepcaoEventoResposta(dadosMsg, returnValue.Mensagem.OuterXml, EnvelopeSoap, RetornoWS);
            }
        }

        #endregion
    }
}