using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServAutorizacao
{
    public sealed class NFeServAutorizacao : NFeServBase<INFeAutorizacao>
    {
        #region Constructors

        public NFeServAutorizacao(NFeConfig config, X509Certificate2 certificado = null) : base(config, NFeTipoServico.NFeAutorizacao, certificado)
        {
            Schema = NFeSchema.EnviNFe;
            ArquivoEnvio = "env-lot";
            ArquivoResposta = "ret-lot";
        }

        #endregion Constructors

        #region Methods

        public NFeAutorizacaoResposta AutorizacaoLote(NotaFiscal.NFe[] nfes, string loteId)
        {
            loteId = ValidarNumeroLote(loteId);

            Guard.Against<ArgumentNullException>(nfes == null, nameof(nfes));
            Guard.Against<ArgumentException>(nfes.Length < 1, "Precisa de pelo menos 1 NFe por lote");
            Guard.Against<ArgumentException>(nfes.Length > 50, "É permitido somente 50 NFes por lote");

            lock (serviceLock)
            {
                var request = new StringBuilder();
                request.Append($"<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"{Configuracoes.Versao.GetDescription()}\">");
                request.Append($"<idLote>{loteId}</idLote>");

                // 0 = Assincrono, 1 = Sincrono
                var indiSinc = Configuracoes.EnviarModoSincrono ? "1" : "0";
                request.Append($"<indSinc>{indiSinc}</indSinc>");

                foreach (var nfe in nfes)
                {
                    var nfeXml = nfe.Xml.IsNullOrEmpty() ? nfe.GetXml(Configuracoes.ObterOptions()) : nfe.Xml;
                    GravarXmlAssinado(nfeXml, nfe.ObterNomeXml());
                    request.Append(nfeXml);
                }

                request.Append("</enviNFe>");

                var dadosMsg = request.ToString();

                Guard.Against<VipException>(dadosMsg.Length > 500 * 1024, $"Tamanho do XML de Dados superior a 500 Kbytes. Tamanho atual: {(dadosMsg.Length / 1024M).Trunc()} Kbytes");
                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var value = new AutorizacaoRequest(doc);
                var returnValue = Channel.AutorizacaoLote(value);
                var retorno = new NFeAutorizacaoResposta(dadosMsg, returnValue.Mensagem.OuterXml, EnvelopeSoap, RetornoWS);
                return retorno;
            }
        }

        public NFeAutorizacaoResposta Autorizacao(NotaFiscal.NFe nfe)
        {
            var loteId = ValidarNumeroLote("");

            Guard.Against<ArgumentNullException>(nfe == null, "Nota fiscal inválida: objeto nulo");

            lock (serviceLock)
            {
                var request = new StringBuilder();
                request.Append($"<enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"{Configuracoes.Versao.GetDescription()}\">");
                request.Append($"<idLote>{loteId}</idLote>");

                // 0 = Assincrono, 1 = Sincrono
                var indiSinc = Configuracoes.EnviarModoSincrono ? "1" : "0";
                request.Append($"<indSinc>{indiSinc}</indSinc>");

                var nfeXml = nfe.Xml.IsNullOrEmpty() ? nfe.GetXml(Configuracoes.ObterOptions()) : nfe.Xml;
                GravarXmlAssinado(nfeXml, nfe.ObterNomeXml());
                request.Append(nfeXml);

                request.Append("</enviNFe>");

                var dadosMsg = request.ToString();

                Guard.Against<VipException>(dadosMsg.Length > 500 * 1024, $"Tamanho do XML de Dados superior a 500 Kbytes. Tamanho atual: {(dadosMsg.Length / 1024M).Trunc()} Kbytes");
                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var value = new AutorizacaoRequest(doc);
                var returnValue = Channel.AutorizacaoLote(value);
                var retorno = new NFeAutorizacaoResposta(dadosMsg, returnValue.Mensagem.OuterXml, EnvelopeSoap, RetornoWS);
                return retorno;
            }
        }

        private string ValidarNumeroLote(string numeroLote)
        {
            if (numeroLote.IsNullOrEmpty())
                numeroLote = DFeHelper.GerarNumeroLote();

            return numeroLote;
        }

        #endregion Methods
    }
}