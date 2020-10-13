using System.Runtime.InteropServices;
using System.Text;
using Vip.DFe.Interops;
using Vip.DFe.SAT.Configuration;

namespace Vip.DFe.SAT.Manager
{
    internal sealed class SatStdCall : SatLibrary
    {
        #region InnerTypes

        private class Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string AssociarAssinatura(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                [MarshalAs(UnmanagedType.LPStr)] string cnpjValue,
                [MarshalAs(UnmanagedType.LPStr)] string assinaturaCnpj);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string AtivarSAT(int numeroSessao, int subComando,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                [MarshalAs(UnmanagedType.LPStr)] string cnpj,
                int cUF);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string AtualizarSoftwareSAT(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string BloquearSAT(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string CancelarUltimaVenda(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                [MarshalAs(UnmanagedType.LPStr)] string chave,
                [MarshalAs(UnmanagedType.LPStr)] string dadosCancelamento);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ComunicarCertificadoICPBRASIL(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                [MarshalAs(UnmanagedType.LPStr)] string certificado);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ConfigurarInterfaceDeRede(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                [MarshalAs(UnmanagedType.LPStr)] string dadosConfiguracao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ConsultarNumeroSessao(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                int cNumeroDeSessao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ConsultarSAT(int numeroSessao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ConsultarStatusOperacional(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string DesbloquearSAT(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string EnviarDadosVenda(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                [MarshalAs(UnmanagedType.LPStr)] string dadosVenda);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ExtrairLogs(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string TesteFimAFim(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                [MarshalAs(UnmanagedType.LPStr)] string dadosVenda);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string TrocarCodigoDeAtivacao(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao,
                int opcao,
                [MarshalAs(UnmanagedType.LPStr)] string novoCodigo,
                [MarshalAs(UnmanagedType.LPStr)] string confNovoCodigo);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VipLPStr))]
            public delegate string ConsultarUltimaSessaoFiscal(int numeroSessao,
                [MarshalAs(UnmanagedType.LPStr)] string codigoDeAtivacao);
        }

        #endregion InnerTypes

        #region Constructors

        public SatStdCall(SatConfig config, string pathDll, Encoding encoding) : base(config, pathDll, encoding)
        {
            ModeloStr = "StdCall";

            AddMethod<Delegates.AssociarAssinatura>("AssociarAssinatura");
            AddMethod<Delegates.AtivarSAT>("AtivarSAT");
            AddMethod<Delegates.AtualizarSoftwareSAT>("AtualizarSoftwareSAT");
            AddMethod<Delegates.BloquearSAT>("BloquearSAT");
            AddMethod<Delegates.CancelarUltimaVenda>("CancelarUltimaVenda");
            AddMethod<Delegates.ComunicarCertificadoICPBRASIL>("ComunicarCertificadoICPBRASIL");
            AddMethod<Delegates.ConfigurarInterfaceDeRede>("ConfigurarInterfaceDeRede");
            AddMethod<Delegates.ConsultarNumeroSessao>("ConsultarNumeroSessao");
            AddMethod<Delegates.ConsultarSAT>("ConsultarSAT");
            AddMethod<Delegates.ConsultarStatusOperacional>("ConsultarStatusOperacional");
            AddMethod<Delegates.DesbloquearSAT>("DesbloquearSAT");
            AddMethod<Delegates.EnviarDadosVenda>("EnviarDadosVenda");
            AddMethod<Delegates.ExtrairLogs>("ExtrairLogs");
            AddMethod<Delegates.TesteFimAFim>("TesteFimAFim");
            AddMethod<Delegates.TrocarCodigoDeAtivacao>("TrocarCodigoDeAtivacao");
            AddMethod<Delegates.ConsultarUltimaSessaoFiscal>("ConsultarUltimaSessaoFiscal");
        }

        #endregion Constructors

        #region Methods

        public override string AssociarAssinatura(int numeroSessao, string codigoAtivacao, string cnpjValue, string assinaturacnpj)
        {
            var funcaoSat = GetMethod<Delegates.AssociarAssinatura>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoAtivacao), ToEncoding(cnpjValue), ToEncoding(assinaturacnpj)));
        }

        public override string AtivarSAT(int numeroSessao, int subComando, string codigoDeAtivacao, string cnpj, int cUF)
        {
            var funcaoSat = GetMethod<Delegates.AtivarSAT>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, subComando, ToEncoding(codigoDeAtivacao), ToEncoding(cnpj), cUF));
        }

        public override string AtualizarSoftwareSAT(int numeroSessao, string codigoDeAtivacao)
        {
            var funcaoSat = GetMethod<Delegates.AtualizarSoftwareSAT>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao)));
        }

        public override string BloquearSAT(int numeroSessao, string codigoDeAtivacao)
        {
            var funcaoSat = GetMethod<Delegates.BloquearSAT>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao)));
        }

        public override string CancelarUltimaVenda(int numeroSessao, string codigoDeAtivacao, string chave, string dadosCancelamento)
        {
            var funcaoSat = GetMethod<Delegates.CancelarUltimaVenda>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao), ToEncoding(chave), ToEncoding(dadosCancelamento)));
        }

        public override string ComunicarCertificadoIcpBrasil(int numeroSessao, string codigoDeAtivacao, string certificado)
        {
            var funcaoSat = GetMethod<Delegates.ComunicarCertificadoICPBRASIL>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao), ToEncoding(certificado)));
        }

        public override string ConfigurarInterfaceDeRede(int numeroSessao, string codigoDeAtivacao, string dadosConfiguracao)
        {
            var funcaoSat = GetMethod<Delegates.ConfigurarInterfaceDeRede>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao), ToEncoding(dadosConfiguracao)));
        }

        public override string ConsultarNumeroSessao(int numeroSessao, string codigoDeAtivacao, int cNumeroDeSessao)
        {
            var funcaoSat = GetMethod<Delegates.ConsultarNumeroSessao>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao), cNumeroDeSessao));
        }

        public override string ConsultarUltimaSessaoFiscal(int numeroSessao, string codigoDeAtivacao)
        {
            var funcaoSat = GetMethod<Delegates.ConsultarUltimaSessaoFiscal>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao)));
        }

        public override string ConsultarSAT(int numeroSessao)
        {
            var funcaoSat = GetMethod<Delegates.ConsultarSAT>();
            return ExecuteMethod(() => funcaoSat(numeroSessao));
        }

        public override string ConsultarStatusOperacional(int numeroSessao, string codigoDeAtivacao)
        {
            var funcaoSat = GetMethod<Delegates.ConsultarStatusOperacional>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao)));
        }

        public override string DesbloquearSAT(int numeroSessao, string codigoDeAtivacao)
        {
            var funcaoSat = GetMethod<Delegates.DesbloquearSAT>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao)));
        }

        public override string EnviarDadosVenda(int numeroSessao, string codigoDeAtivacao, string dadosVenda)
        {
            var funcaoSat = GetMethod<Delegates.EnviarDadosVenda>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao), ToEncoding(dadosVenda)));
        }

        public override string ExtrairLogs(int numeroSessao, string codigoDeAtivacao)
        {
            var funcaoSat = GetMethod<Delegates.ExtrairLogs>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao)));
        }

        public override string TesteFimAFim(int numeroSessao, string codigoDeAtivacao, string dadosVenda)
        {
            var funcaoSat = GetMethod<Delegates.TesteFimAFim>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, codigoDeAtivacao, ToEncoding(dadosVenda)));
        }

        public override string TrocarCodigoDeAtivacao(int numeroSessao, string codigoDeAtivacao, int opcao, string novoCodigo, string confNovoCodigo)
        {
            var funcaoSat = GetMethod<Delegates.TrocarCodigoDeAtivacao>();
            return ExecuteMethod(() => funcaoSat(numeroSessao, ToEncoding(codigoDeAtivacao), opcao, ToEncoding(novoCodigo), ToEncoding(confNovoCodigo)));
        }

        #endregion Methods
    }
}