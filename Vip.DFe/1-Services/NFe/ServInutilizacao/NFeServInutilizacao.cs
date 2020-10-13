using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Vip.DFe.Cryptography;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.ServBase;

namespace Vip.DFe.NFe.ServInutilizacao
{
    public sealed class NFeServInutilizacao : NFeServBase<INFeInutilizacao>
    {
        #region Constructors

        public NFeServInutilizacao(NFeConfig config, X509Certificate2 certificado = null) : base(config, NFeTipoServico.NfeInutilizacao, certificado)
        {
            Schema = NFeSchema.InutNFe;
            ArquivoEnvio = "env-inu";
            ArquivoResposta = "ret-inu";
        }

        #endregion

        #region Methods

        public NFeInutilizacaoResposta Inutilizar(string cnpj, string justificativa, NFeModelo modelo, int serie, int numeroInicial, int numeroFinal)
        {
            cnpj = cnpj.OnlyNumbers();

            var ano = DateTime.Now.Year.ToString();
            ano = ano.Substring(ano.Length - 2);

            Guard.Against<ArgumentNullException>(cnpj.IsNullOrEmpty(), "ERRO: CNPJ não informado");
            Guard.Against<ArgumentNullException>(justificativa.IsNullOrEmpty(), "ERRO: Justificativa para Inutilização de numeração não informada");
            Guard.Against<ArgumentException>(justificativa.Length < 15, "ERRO: A Justificativa para Inutilização de numeração deve ter no minimo 15 caracteres.");

            lock (serviceLock)
            {
                var idInutilizacao = $"ID{Configuracoes.Webservices.UF.GetDFeValue()}{ano:D2}{cnpj.OnlyNumbers()}{modelo.GetDFeValue()}{serie.ZeroFill(3)}{numeroInicial.ZeroFill(9)}{numeroFinal.ZeroFill(9)}";

                var request = new StringBuilder();
                request.Append($"<inutNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"{Configuracoes.Versao.GetDescription()}\">");
                request.Append($"<infInut Id=\"{idInutilizacao}\">");
                request.Append($"<tpAmb>{Configuracoes.Ambiente.GetDFeValue()}</tpAmb>");
                request.Append("<xServ>INUTILIZAR</xServ>");
                request.Append($"<cUF>{Configuracoes.Webservices.UF.GetDFeValue()}</cUF>");
                request.Append($"<ano>{ano}</ano>");
                request.Append($"<CNPJ>{cnpj}</CNPJ>");
                request.Append($"<mod>{modelo.GetDFeValue()}</mod>");
                request.Append($"<serie>{serie}</serie>");
                request.Append($"<nNFIni>{numeroInicial}</nNFIni>");
                request.Append($"<nNFFin>{numeroFinal}</nNFFin>");
                request.Append($"<xJust>{justificativa.TrimVip().RemoveAccent()}</xJust>");
                request.Append("</infInut>");
                request.Append("</inutNFe>");

                var dadosMsg = request.ToString();
                dadosMsg = SigningManager.AssinarXml(dadosMsg, "inutNFe", "infInut", ClientCredentials.ClientCertificate.Certificate);
                ValidateMessage(dadosMsg);
                GravarXmlAssinado(dadosMsg, $"{idInutilizacao.OnlyNumbers()}-inu-nfe.xml");

                var xml = new XmlDocument();
                xml.LoadXml(dadosMsg);

                var value = new InutilizacaoRequest(xml);
                var returnValue = Channel.Inutilizar(value);
                var resposta = new NFeInutilizacaoResposta(dadosMsg, returnValue.Mensagem.OuterXml, EnvelopeSoap, RetornoWS);

                if (resposta.Resultado.InfInut.CStat == 102)
                    GravarXmlInutilizacao(resposta.XmlRetorno, $"{idInutilizacao.OnlyNumbers()}-inu-nfe.xml");

                return resposta;
            }
        }

        #endregion
    }
}