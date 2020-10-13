using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServStatusServico
{
    public class NFeServStatusServico : NFeServBase<INFeStatusServico>
    {
        #region Constructor

        public NFeServStatusServico(NFeConfig config, X509Certificate2 certificado = null) : base(config, NFeTipoServico.NfeStatusServico, certificado)
        {
            Schema = NFeSchema.ConsStatServNFe;
            ArquivoEnvio = "env-sta";
            ArquivoResposta = "ret-sta";
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Consulta a situação do serviço
        /// </summary>
        public NFeStatusServicoResposta StatusServico()
        {
            lock (serviceLock)
            {
                var request = new StringBuilder();
                request.Append($"<consStatServ xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"{Configuracoes.Versao.GetDescription()}\">");
                request.Append($"<tpAmb>{Configuracoes.Ambiente.GetDFeValue()}</tpAmb>");
                request.Append($"<cUF>{Configuracoes.Webservices.UF.GetDFeValue()}</cUF>");
                request.Append("<xServ>STATUS</xServ>");
                request.Append("</consStatServ>");

                var dadosMsg = request.ToString();
                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var value = new StatusServicoRequest(doc);
                var returnValue = Channel.StatusServico(value);
                return new NFeStatusServicoResposta(dadosMsg, returnValue.Mensagem.OuterXml, EnvelopeSoap, RetornoWS);
            }
        }

        #endregion
    }
}