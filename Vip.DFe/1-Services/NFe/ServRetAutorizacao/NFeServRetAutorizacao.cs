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

namespace Vip.DFe.NFe.ServRetAutorizacao
{
    public class NFeServRetAutorizacao : NFeServBase<INFeRetAutorizacao>
    {
        #region Constructors

        public NFeServRetAutorizacao(NFeConfig config, X509Certificate2 certificado = null) : base(config, NFeTipoServico.NFeRetAutorizacao, certificado)
        {
            Schema = NFeSchema.ConsReciNFe;
            ArquivoEnvio = "env-rec";
            ArquivoResposta = "ret-rec";
        }

        #endregion

        #region Methods

        public NFeRetAutorizacaoResposta RetAutorizacao(string recibo)
        {
            Guard.Against<ArgumentNullException>(recibo.IsNullOrEmpty(), "Número de recibo inválido");

            lock (serviceLock)
            {
                var request = new StringBuilder();
                request.Append($"<consReciNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"{Configuracoes.Versao.GetDescription()}\">");
                request.Append($"<tpAmb>{Configuracoes.Ambiente.GetDFeValue()}</tpAmb>");
                request.Append($"<nRec>{recibo}</nRec>");
                request.Append("</consReciNFe>");

                var dadosMsg = request.ToString();

                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var value = new RetAutorizacaoRequest(doc);
                var returnValue = Channel.ConsultaAutorizacao(value);
                return new NFeRetAutorizacaoResposta(dadosMsg, returnValue.Mensagem.OuterXml, EnvelopeSoap, RetornoWS);
            }
        }

        #endregion
    }
}