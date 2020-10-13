using System;
using System.Text;
using Vip.DFe.SAT.Configuration;

namespace Vip.DFe.SAT.Interfaces
{
    public interface ISatLibrary : IDisposable
    {
        #region Properties

        SatConfig Config { get; }

        Encoding Encoding { get; }

        string ModeloStr { get; }

        string PathDll { get; }

        #endregion Properties

        #region Methods

        string AssociarAssinatura(int numeroSessao, string codigoAtivacao, string cnpjValue, string assinaturacnpj);

        string AtivarSAT(int numeroSessao, int subComando, string codigoDeAtivacao, string cnpj, int cUF);

        string AtualizarSoftwareSAT(int numeroSessao, string codigoDeAtivacao);

        string BloquearSAT(int numeroSessao, string codigoDeAtivacao);

        string CancelarUltimaVenda(int numeroSessao, string codigoDeAtivacao, string chave, string dadosCancelamento);

        string ComunicarCertificadoIcpBrasil(int numeroSessao, string codigoDeAtivacao, string certificado);

        string ConfigurarInterfaceDeRede(int numeroSessao, string codigoDeAtivacao, string dadosConfiguracao);

        string ConsultarNumeroSessao(int numeroSessao, string codigoDeAtivacao, int cNumeroDeSessao);

        string ConsultarUltimaSessaoFiscal(int numeroSessao, string codigoDeAtivacao);

        string ConsultarSAT(int numeroSessao);

        string ConsultarStatusOperacional(int numeroSessao, string codigoDeAtivacao);

        string DesbloquearSAT(int numeroSessao, string codigoDeAtivacao);

        string EnviarDadosVenda(int numeroSessao, string codigoDeAtivacao, string dadosVenda);

        string ExtrairLogs(int numeroSessao, string codigoDeAtivacao);

        string TesteFimAFim(int numeroSessao, string codigoDeAtivacao, string dadosVenda);

        string TrocarCodigoDeAtivacao(int numeroSessao, string codigoDeAtivacao, int opcao, string novoCodigo, string confNovoCodigo);

        #endregion Methods
    }
}