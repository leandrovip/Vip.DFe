using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.NFe.Configuration;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Service;

namespace Vip.DFe.NFe.ServBase
{
    public abstract class NFeServBase<TService> : ServiceClient<NFeConfig, TService> where TService : class
    {
        #region Constructors

        protected NFeServBase(NFeConfig config, NFeTipoServico service, X509Certificate2 certificado = null) :
            base(config, ObterUrl(config, service), certificado) { }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        ///     Retorna o certificado usado no serviço.
        /// </summary>
        public X509Certificate2 Certificado => ClientCredentials?.ClientCertificate.Certificate ?? Configuracoes.Certificado.ObterCertificado();

        #endregion Propriedades

        #region Methods

        /// <summary>
        ///     Salva arquivo xml assinado
        /// </summary>
        protected void GravarXmlAssinado(string conteudoArquivo, string nomeArquivo)
        {
            if (!Configuracoes.Arquivos.Salvar) return;

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + conteudoArquivo.RemoverDeclaracaoXml();
            nomeArquivo = Path.Combine(Configuracoes.Arquivos.ObterCaminhoAssinado(), nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        /// <summary>
        ///     Salva o arquivo xml da inutilização no disco de acordo com as propriedades.
        /// </summary>
        protected void GravarXmlInutilizacao(string conteudoArquivo, string nomeArquivo)
        {
            if (!Configuracoes.Arquivos.Salvar) return;

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + conteudoArquivo.RemoverDeclaracaoXml();
            nomeArquivo = Path.Combine(Configuracoes.Arquivos.ObterCaminhoInutilizado(), nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);

            #region Backup

            if (Configuracoes.Arquivos.DiretorioAutorizadasBackup.IsNotNullOrEmpty())
            {
                var nomeArquivoBackup = Path.Combine(Configuracoes.Arquivos.ObterCaminhoInutilizado(Configuracoes.Arquivos.DiretorioAutorizadasBackup), Path.GetFileName(nomeArquivo));
                File.WriteAllText(nomeArquivoBackup, conteudoArquivo, Encoding.UTF8);
            }

            #endregion
        }

        private static string ObterUrl(NFeConfig config, NFeTipoServico service)
        {
            if (config == null) return null;

            var endereco = config.Webservices.Enderecos.FirstOrDefault(x => x.TipoServico == service);
            if (endereco == null) throw new VipException($"Endereço do serviço {service} não encontrado para UF {config.Webservices.UF}, modelo {config.Modelo} e ambiente {config.Ambiente}.");

            return endereco.Url;
        }

        #endregion Methods
    }
}