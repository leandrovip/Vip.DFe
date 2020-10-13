using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.Helpers;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;

namespace Vip.DFe.Service
{
    public abstract class ServiceClient<TConfig, TService> : Soap12ServiceClientBase<TService>
        where TConfig : NFeConfig
        where TService : class
    {
        #region Fields

        protected readonly object serviceLock;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="config"></param>
        /// <param name="url"></param>
        /// <param name="certificado"></param>
        protected ServiceClient(TConfig config, string url, X509Certificate2 certificado = null) :
            base(url, config.Webservices.Timeout, certificado)
        {
            serviceLock = new object();
            Configuracoes = config;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// </summary>
        public TConfig Configuracoes { get; }

        /// <summary>
        /// </summary>
        public NFeSchema Schema { get; protected set; }

        /// <summary>
        /// </summary>
        public string ArquivoEnvio { get; protected set; }

        /// <summary>
        /// </summary>
        public string ArquivoResposta { get; protected set; }

        /// <summary>
        /// </summary>
        public string EnvelopeSoap { get; protected set; }

        /// <summary>
        /// </summary>
        public string RetornoWS { get; protected set; }

        #endregion Properties

        #region Methods

        /// <summary>
        ///     Função para validar a menssagem a ser enviada para o webservice.
        /// </summary>
        /// <param name="xml"></param>
        protected virtual void ValidateMessage(string xml)
        {
            ValidateMessage(xml, Schema);
        }

        /// <summary>
        ///     Função para validar a menssagem a ser enviada para o webservice.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="schema"></param>
        protected virtual void ValidateMessage(string xml, NFeSchema schema)
        {
            var schemaFile = Configuracoes.Arquivos.ObterSchema(schema);
            SchemaHelper.ValidarXml(xml, schemaFile, out var erros, out _);
            Guard.Against<VipException>(erros.Any(), $"Erros de validação do xml.{(Configuracoes.ExibeErrosSchema ? Environment.NewLine + erros.AsString() : "")}");
        }

        /// <summary>
        ///     Salvar o arquivo SOAP xml de envio ou retorno
        /// </summary>
        protected void GravarSoap(string conteudoArquivo, string nomeArquivo, bool seArquivoEnvio)
        {
            if (!Configuracoes.Arquivos.Salvar) return;
            var caminhoDiretorio = seArquivoEnvio ? Configuracoes.Arquivos.ObterCaminhoEnviado() : Configuracoes.Arquivos.ObterCaminhoRetorno();
            nomeArquivo = Path.Combine(caminhoDiretorio, nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        protected override void BeforeSendDFeRequest(string message)
        {
            EnvelopeSoap = message;
            GravarSoap(message, $"{DateTime.Now:yyyyMMddHHmmssfff}-{ArquivoEnvio}.xml", true);
        }

        protected override void AfterReceiveDFeReply(string message)
        {
            RetornoWS = message;
            GravarSoap(message, $"{DateTime.Now:yyyyMMddHHmmssfff}-{ArquivoResposta}.xml", false);
        }

        #endregion Methods
    }
}