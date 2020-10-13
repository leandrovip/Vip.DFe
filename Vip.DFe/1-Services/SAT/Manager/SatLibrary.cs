using System.Text;
using Vip.DFe.Interops;
using Vip.DFe.SAT.Configuration;
using Vip.DFe.SAT.Interfaces;

namespace Vip.DFe.SAT.Manager
{
    public abstract class SatLibrary : VipSafeHandle, ISatLibrary
    {
        #region Constructors

        protected SatLibrary(SatConfig config, string pathDll, Encoding encoding) : base(pathDll)
        {
            PathDll = pathDll;
            Encoding = encoding;
            Config = config;
        }

        #endregion Constructors

        #region Propriedades

        public Encoding Encoding { get; protected set; }

        public string PathDll { get; protected set; }

        public string ModeloStr { get; protected set; }

        public SatConfig Config { get; protected set; }

        #endregion Propriedades

        #region Method

        public abstract string AssociarAssinatura(int numeroSessao, string codigoAtivacao, string cnpjValue, string assinaturacnpj);

        public abstract string AtivarSAT(int numeroSessao, int subComando, string codigoDeAtivacao, string cnpj, int cUF);

        public abstract string AtualizarSoftwareSAT(int numeroSessao, string codigoDeAtivacao);

        public abstract string BloquearSAT(int numeroSessao, string codigoDeAtivacao);

        public abstract string CancelarUltimaVenda(int numeroSessao, string codigoDeAtivacao, string chave, string dadosCancelamento);

        public abstract string ComunicarCertificadoIcpBrasil(int numeroSessao, string codigoDeAtivacao, string certificado);

        public abstract string ConfigurarInterfaceDeRede(int numeroSessao, string codigoDeAtivacao, string dadosConfiguracao);

        public abstract string ConsultarNumeroSessao(int numeroSessao, string codigoDeAtivacao, int cNumeroDeSessao);

        public abstract string ConsultarUltimaSessaoFiscal(int numeroSessao, string codigoDeAtivacao);

        public abstract string ConsultarSAT(int numeroSessao);

        public abstract string ConsultarStatusOperacional(int numeroSessao, string codigoDeAtivacao);

        public abstract string DesbloquearSAT(int numeroSessao, string codigoDeAtivacao);

        public abstract string EnviarDadosVenda(int numeroSessao, string codigoDeAtivacao, string dadosVenda);

        public abstract string ExtrairLogs(int numeroSessao, string codigoDeAtivacao);

        public abstract string TesteFimAFim(int numeroSessao, string codigoDeAtivacao, string dadosVenda);

        public abstract string TrocarCodigoDeAtivacao(int numeroSessao, string codigoDeAtivacao, int opcao, string novoCodigo, string confNovoCodigo);

        protected string FromEncoding(string str)
        {
            return Encoding.GetString(Encoding.Default.GetBytes(str));
        }

        protected string ToEncoding(string str)
        {
            return Encoding.Default.GetString(Encoding.GetBytes(str));
        }

        #endregion Method
    }
}