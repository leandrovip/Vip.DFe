using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using Vip.DFe.Control;
using Vip.DFe.Enum;
using Vip.DFe.Exception;
using Vip.DFe.Extensions;
using Vip.DFe.SAT.Configuration;
using Vip.DFe.SAT.CupomFiscal;
using Vip.DFe.SAT.Enum;
using Vip.DFe.SAT.Eventos;
using Vip.DFe.SAT.Events;
using Vip.DFe.SAT.Funcoes;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.SAT.Manager;
using Vip.DFe.SAT.Response;

namespace Vip.DFe.SAT
{
    /// <summary>
    ///     Classe CFeService, responsavel por comunicar-se com o CF-e-SAT.
    /// </summary>
    public sealed class CFeService : VipComponent
    {
        #region Fields

        private ISatLibrary satLibrary;
        private Encoding encoding;
        private ModeloSat modelo;
        private string pathDll;
        private string signAC;
        private string codigoAtivacao;
        private bool aguardandoResposta;

        #endregion Fields

        #region Events

        /// <summary>
        ///     Ocorre quando é necessario pegar o valor do Codigo de Ativação.
        /// </summary>
        public event EventHandler<ChaveEventArgs> OnGetCodigoDeAtivacao;

        /// <summary>
        ///     Ocorre quando é necessario pegar o valor do SignAC.
        /// </summary>
        public event EventHandler<ChaveEventArgs> OnGetSignAC;

        /// <summary>
        ///     Ocorre que é necessario pegar o número da sessão.
        /// </summary>
        public event EventHandler<NumeroSessaoEventArgs> OnGetNumeroSessao;

        /// <summary>
        ///     Ocorre antes de enviar os dados da venda para o Sat.
        /// </summary>
        public event EventHandler<EventoDadosEventArgs> OnEnviarDadosVenda;

        /// <summary>
        ///     Ocorre antes de cancelar uma venda.
        /// </summary>
        public event EventHandler<EventoDadosEventArgs> OnCancelarUltimaVenda;

        /// <summary>
        ///     Ocorre quando é chamado o comando ConsultarSat para caso o usuario queria tratar esta função.
        /// </summary>
        public event EventHandler<EventoEventArgs> OnConsultarSat;

        /// <summary>
        ///     Ocorre quando é chamado o comando ConsultaStatusOperacional para caso o usuario queria tratar esta função.
        /// </summary>
        public event EventHandler<EventoEventArgs> OnConsultaStatusOperacional;

        /// <summary>
        ///     Ocorre quando é chamado o comando ExtrairLogs para caso o usuario queria tratar esta função.
        /// </summary>
        public event EventHandler<EventoEventArgs> OnExtrairLogs;

        /// <summary>
        ///     Ocorre quando é chamado o comando ConsultarNumeroSessao para caso o usuario queria tratar esta função.
        /// </summary>
        public event EventHandler<EventoDadosEventArgs> OnConsultarNumeroSessao;

        /// <summary>
        ///     Ocorre quando tem alguma mensagem da Sefaz no retorno do SAT.
        /// </summary>
        public event EventHandler<SatMensagemEventArgs> OnMensagemSefaz;

        /// <summary>
        ///     Ocorre quando é necessario calcular o Path para salvar os Xmls.
        /// </summary>
        public event EventHandler<CalcPathEventEventArgs> OnCalcPath;

        /// <summary>
        ///     Ocorre quando o componente entra ou sai de um processamento.
        /// </summary>
        public event EventHandler<EventArgs> AguardandoRespostaChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Configurações do Vip.CFe
        /// </summary>
        [Category("Configurações")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SatConfig Configuracoes { get; private set; }

        /// <summary>
        ///     Configurações de como a biblioteca vai se comportar com os arquivos gerados e recebidos.
        /// </summary>
        [Category("Configurações")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SatArquivos Arquivos { get; private set; }

        /// <summary>
        ///     Enconding usado para tratar as string que são enviadas/recebidas.
        /// </summary>
        /// <exception cref="Exception">Não é possível definir a propriedade com o componente ativo</exception>
        [Browsable(false)]
        public Encoding Encoding
        {
            get => encoding;
            set
            {
                Guard.Against<VipException>(Ativo, "Não é possível definir a propriedade com o componente ativo");
                encoding = value;
            }
        }

        /// <summary>
        ///     Define/retorna o modelo a ser utilizado pelo VipCFe.
        /// </summary>
        /// <value>The modelo.</value>
        [Category("Configurações")]
        public ModeloSat Modelo
        {
            get => modelo;
            set
            {
                Guard.Against<VipException>(Ativo, "Não é possível definir a propriedade com o componente ativo");
                modelo = value;
            }
        }

        /// <summary>
        ///     Retorna o indicador se o componente esta ativo ou não.
        /// </summary>
        [Browsable(false)]
        public bool Ativo { get; private set; }

        /// <summary>
        ///     Retorna o indicador se o componente esta em aguardando uma resposta ou não.
        /// </summary>
        [Browsable(false)]
        public bool AguardandoResposta
        {
            get => aguardandoResposta;
            private set
            {
                if (aguardandoResposta == value) return;

                aguardandoResposta = value;
                AguardandoRespostaChanged.Raise(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Retorna o número da sessão atual.
        /// </summary>
        [Browsable(false)]
        public int Sessao { get; private set; }

        /// <summary>
        ///     Define/retorna o código usado para ativar o Sat
        /// </summary>
        [Category("Configurações")]
        public string CodigoAtivacao
        {
            get
            {
                var e = new ChaveEventArgs {Chave = codigoAtivacao};
                if (!DesignMode) OnGetCodigoDeAtivacao.Raise(this, e);

                codigoAtivacao = e.Chave;
                return codigoAtivacao;
            }
            set => codigoAtivacao = value;
        }

        /// <summary>
        ///     Define/retorna a assinatura de (CNPJ Software House + CNPJ Emitente) que gerou o CF-e
        /// </summary>
        [Category("Configurações")]
        public string SignAC
        {
            get
            {
                var e = new ChaveEventArgs {Chave = signAC};

                if (!DesignMode) OnGetSignAC.Raise(this, e);

                signAC = e.Chave;
                return signAC;
            }
            set => signAC = value;
        }

        /// <summary>
        ///     Define/retorna o caminho onde se encontra a biblioteca do Sat.
        /// </summary>
        [Category("Configurações")]
        public string PathDll
        {
            get => pathDll;
            set
            {
                Guard.Against<VipException>(Ativo, "Não é possível definir a propriedade com o componente ativo");
                pathDll = value;
            }
        }

        #endregion Properties

        #region Methods

        #region Public

        /// <summary>
        ///     Ativa o VipCFe, obrigatorio antes de chamar qualquer metodo.
        /// </summary>
        public void Ativar()
        {
            satLibrary = SatManager.GetLibrary(Modelo, Configuracoes, PathDll, Encoding);
            Ativo = true;
        }

        /// <summary>
        ///     Desativa o VipCFe e libera a lib nativa
        /// </summary>
        public void Desativar()
        {
            if (satLibrary != null)
            {
                satLibrary.Dispose();
                satLibrary = null;
            }

            Ativo = false;
        }

        /// <summary>
        ///     Retorna uma nova instancia da classe CFe com os dados inciais preenchidos.
        /// </summary>
        /// <returns>CFe Iniciada.</returns>
        public CFe NewCFe()
        {
            return PreencherCFe(new CFe());
        }

        private CFe PreencherCFe(CFe cfe)
        {
            cfe.InfCFe.Ide.CNPJ = Configuracoes.IdeCNPJ;
            cfe.InfCFe.Ide.TpAmb = null;
            cfe.InfCFe.Ide.NumeroCaixa = Configuracoes.IdeNumeroCaixa;
            cfe.InfCFe.Ide.SignAC = SignAC;
            cfe.InfCFe.Emit.CNPJ = Configuracoes.EmitCNPJ;
            cfe.InfCFe.Emit.IM = Configuracoes.EmitIM;
            cfe.InfCFe.Emit.IE = Configuracoes.EmitIE;
            cfe.InfCFe.Emit.CRegTribISSQN = Configuracoes.EmitCRegTribISSQN;
            cfe.InfCFe.Emit.IndRatISSQN = Configuracoes.EmitIndRatISSQN;
            cfe.InfCFe.VersaoDadosEnt = Configuracoes.InfCFeVersaoDadosEnt;
            return cfe;
        }

        /// <summary>
        ///     Associa a sua assinatura ao Sat
        /// </summary>
        public SatResponse AssociarAssinatura(string cnpj, string assinaturaCnpj)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var ret = satLibrary.AssociarAssinatura(Sessao, CodigoAtivacao, cnpj, assinaturaCnpj);
            return FinalizaComando<SatResponse>(ret);
        }

        /// <summary>
        ///     Função para ativa o Sat.
        /// </summary>
        /// <param name="subComando">Identificador do tipo de Certificado.</param>
        /// <param name="cnpj">CNPJ do contribuinte.</param>
        /// <param name="uf">Código do Estado da Federação</param>
        public SatResponse AtivarSAT(int subComando, string cnpj, int uf)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var ret = satLibrary.AtivarSAT(Sessao, subComando, CodigoAtivacao, cnpj.OnlyNumbers(), uf);
            return FinalizaComando<SatResponse>(ret);
        }

        /// <summary>
        ///     Envia um comando para o Sat se atualizar.
        /// </summary>
        public SatResponse AtualizarSoftwareSAT()
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var ret = satLibrary.AtualizarSoftwareSAT(Sessao, CodigoAtivacao);
            return FinalizaComando<SatResponse>(ret);
        }

        /// <summary>
        ///     Bloquea o Sat.
        /// </summary>
        public SatResponse BloquearSAT()
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var ret = satLibrary.BloquearSAT(Sessao, CodigoAtivacao);
            return FinalizaComando<SatResponse>(ret);
        }

        /// <summary>
        ///     Desbloquear o Sat.
        /// </summary>
        public SatResponse DesbloquearSAT()
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var ret = satLibrary.DesbloquearSAT(Sessao, CodigoAtivacao);
            return FinalizaComando<SatResponse>(ret);
        }

        /// <summary>
        ///     Cancelar o CFe Informado
        /// </summary>
        /// <param name="cfe">CFe para cancelar.</param>
        public CancelamentoResponse CancelarUltimaVenda(CFe cfe)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");
            Guard.Against<ArgumentNullException>(cfe == null, nameof(cfe));

            var cfeCanc = new CFeCanc(cfe);
            return CancelarUltimaVenda(cfeCanc);
        }

        /// <summary>
        ///     Cancela a venda relacionada a classe de cancelamento informada.
        /// </summary>
        /// <param name="cfeCanc">Classe de CFe cancelar</param>
        /// <returns>CancelamentoSatResposta.</returns>
        public CancelamentoResponse CancelarUltimaVenda(CFeCanc cfeCanc)
        {
            Guard.Against<ArgumentNullException>(cfeCanc == null, nameof(cfeCanc));

            var options = SaveOptions.OmitDeclaration | SaveOptions.DisableFormatting;
            if (Configuracoes.RemoverAcentos)
                options |= SaveOptions.RemoveAccents;

            var dados = cfeCanc?.GetXml(options);
            return CancelarUltimaVenda(cfeCanc?.InfCFe.ChCanc, dados);
        }

        /// <summary>
        ///     Cancela a venda
        /// </summary>
        /// <param name="chave">A chave da CFe pra cancelar.</param>
        /// <param name="dadosCancelamento">XML de cancelamento.</param>
        public CancelamentoResponse CancelarUltimaVenda(string chave, string dadosCancelamento)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");
            Guard.Against<ArgumentException>(chave.IsNullOrEmpty(), "Chave não informada.");
            Guard.Against<ArgumentException>(dadosCancelamento.IsNullOrEmpty(), "Dados de cancelamento não informado.");

            IniciaComando();

            if (Arquivos.SalvarEnvio)
            {
                var envioPath = Arquivos.PastaEnvio;
                var fullName = Path.Combine(envioPath, $"{Arquivos.PrefixoArqCFeCanc}{DateTime.Now:yyyyMMddHHmmss}-{Sessao.ZeroFill(6)}-env.xml");
                if (!Directory.Exists(envioPath))
                    Directory.CreateDirectory(envioPath);

                File.WriteAllText(fullName, dadosCancelamento);
            }

            var e = new EventoDadosEventArgs {Dados = dadosCancelamento};
            OnCancelarUltimaVenda.Raise(this, e);

            var ret = satLibrary.CancelarUltimaVenda(Sessao, CodigoAtivacao, chave, dadosCancelamento);
            var resp = FinalizaComando<CancelamentoResponse>(ret);

            if (!Arquivos.SalvarCFeCanc || resp.CodigoDeRetorno != 7000)
                return resp;

            var cnpj = Arquivos.SepararPorCNPJ ? resp.Cancelamento.InfCFe.Emit.CNPJ : "";
            var data = Arquivos.SepararPorMes ? $"{resp.Cancelamento.InfCFe.Ide.DEmi:yyyy}\\{resp.Cancelamento.InfCFe.Ide.DEmi:MM}" : "";
            var path = Path.Combine(Arquivos.PastaCFeCancelamento, cnpj, data);
            var calcPathEventEventArgs = new CalcPathEventEventArgs
            {
                CNPJ = resp.Cancelamento.InfCFe.Emit.CNPJ,
                Data = resp.Cancelamento.InfCFe.Ide.DEmi,
                Path = path
            };

            OnCalcPath.Raise(this, calcPathEventEventArgs);

            if (!Directory.Exists(calcPathEventEventArgs.Path))
                Directory.CreateDirectory(calcPathEventEventArgs.Path);

            var nomeArquivo = $"{Arquivos.PrefixoArqCFeCanc}{resp.Cancelamento.InfCFe.Id.OnlyNumbers()}.xml";
            var fullPath = Path.Combine(calcPathEventEventArgs.Path, nomeArquivo);
            resp.Cancelamento.Save(fullPath);
            return resp;
        }

        /// <summary>
        ///     Seta o certificado para o Sat usa.
        ///     So use caso você utiliza certificado Icp Brasil (NFe).
        /// </summary>
        public SatResponse ComunicarCertificadoIcpBrasil(string certificado)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var ret = satLibrary.ComunicarCertificadoIcpBrasil(Sessao, CodigoAtivacao, certificado);
            return FinalizaComando<SatResponse>(ret);
        }

        /// <summary>
        ///     Configura a interface de rede do Sat.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public SatResponse ConfigurarInterfaceDeRede(SatRede config)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");
            Guard.Against<ArgumentNullException>(config == null, nameof(config));

            var configuracao = config?.GetXml();
            IniciaComando();
            var ret = satLibrary.ConfigurarInterfaceDeRede(Sessao, CodigoAtivacao, configuracao);
            return FinalizaComando<SatResponse>(ret);
        }

        /// <summary>
        ///     Consulta os dados da sessão informada.
        /// </summary>
        public ConsultaSessaoResponse ConsultarNumeroSessao(int numeroSessao)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var e = new EventoDadosEventArgs
            {
                Dados = numeroSessao.ToString(),
                Retorno = string.Empty
            };
            OnConsultarNumeroSessao.Raise(this, e);

            if (e.Retorno.IsNullOrEmpty())
                e.Retorno = satLibrary.ConsultarNumeroSessao(Sessao, CodigoAtivacao, numeroSessao);

            return FinalizaComando<ConsultaSessaoResponse>(e.Retorno);
        }

        /// <summary>
        ///     Consulta os dados da sessão informada.
        /// </summary>
        public ConsultaSessaoResponse ConsultarUltimaSessaoFiscal()
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var ret = satLibrary.ConsultarUltimaSessaoFiscal(Sessao, CodigoAtivacao);

            return FinalizaComando<ConsultaSessaoResponse>(ret);
        }

        /// <summary>
        ///     Consulta a situação do Sat.
        /// </summary>
        public SatResponse ConsultarSAT()
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");
            IniciaComando();
            var e = new EventoEventArgs {Retorno = string.Empty};
            OnConsultarSat.Raise(this, e);

            if (e.Retorno.IsNullOrEmpty())
                e.Retorno = satLibrary.ConsultarSAT(Sessao);

            return FinalizaComando<SatResponse>(e.Retorno);
        }

        /// <summary>
        ///     Consulta a situação operacional do Sat.
        /// </summary>
        public StatusOperacionalResponse ConsultarStatusOperacional()
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var e = new EventoEventArgs {Retorno = string.Empty};
            OnConsultaStatusOperacional.Raise(this, e);

            if (e.Retorno.IsNullOrEmpty())
                e.Retorno = satLibrary.ConsultarStatusOperacional(Sessao, CodigoAtivacao);

            return FinalizaComando<StatusOperacionalResponse>(e.Retorno);
        }

        /// <summary>
        ///     Envia os dados da venda para o Sat.
        /// </summary>
        /// <param name="cfe">The cfe.</param>
        public VendaResponse EnviarDadosVenda(CFe cfe)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");
            Guard.Against<ArgumentNullException>(cfe == null, nameof(cfe));

            var options = SaveOptions.OmitDeclaration | SaveOptions.DisableFormatting;
            if (Configuracoes.RemoverAcentos)
                options |= SaveOptions.RemoveAccents;

            if (cfe?.InfCFe.Versao == 0) cfe = PreencherCFe(cfe);
            var dadosVenda = cfe?.GetXml(options);

            IniciaComando();

            if (Arquivos.SalvarEnvio)
            {
                var envioPath = Arquivos.PastaEnvio;
                var fullName = Path.Combine(envioPath, $"{Arquivos.PrefixoArqCFe}{DateTime.Now:yyyyMMddHHmmss}-{Sessao.ZeroFill(6)}-env.xml");
                if (!Directory.Exists(envioPath))
                    Directory.CreateDirectory(envioPath);

                File.WriteAllText(fullName, dadosVenda);
            }

            var e = new EventoDadosEventArgs {Dados = dadosVenda};
            OnEnviarDadosVenda.Raise(this, e);

            var ret = satLibrary.EnviarDadosVenda(Sessao, CodigoAtivacao, e.Dados);
            var resp = FinalizaComando<VendaResponse>(ret);

            if (!Arquivos.SalvarCFe || resp.CodigoDeRetorno != 6000)
                return resp;

            var cnpj = Arquivos.SepararPorCNPJ ? resp.Venda.InfCFe.Emit.CNPJ : "";
            var data = Arquivos.SepararPorMes ? $"{resp.Venda.InfCFe.Ide.DEmi:yyyy}\\{resp.Venda.InfCFe.Ide.DEmi:MM}" : "";
            var path = Path.Combine(Arquivos.PastaCFeVenda, cnpj, data);
            var calcPathEventEventArgs = new CalcPathEventEventArgs
            {
                CNPJ = resp.Venda.InfCFe.Emit.CNPJ,
                Data = resp.Venda.InfCFe.Ide.DEmi,
                Path = path
            };

            OnCalcPath.Raise(this, calcPathEventEventArgs);

            if (!Directory.Exists(calcPathEventEventArgs.Path)) Directory.CreateDirectory(calcPathEventEventArgs.Path);

            var nomeArquivo = $"{Arquivos.PrefixoArqCFe}{resp.Venda.InfCFe.Id.OnlyNumbers()}.xml";
            var fullPath = Path.Combine(calcPathEventEventArgs.Path, nomeArquivo);
            resp.Venda.Save(fullPath);

            return resp;
        }

        /// <summary>
        ///     Extrai os logs do Sat.
        /// </summary>
        public LogResponse ExtrairLogs()
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();

            var e = new EventoEventArgs {Retorno = string.Empty};
            OnExtrairLogs.Raise(this, e);

            if (e.Retorno.IsNullOrEmpty())
                e.Retorno = satLibrary.ExtrairLogs(Sessao, CodigoAtivacao);

            return FinalizaComando<LogResponse>(e.Retorno);
        }

        /// <summary>
        ///     Realiza um teste de fim a fim no Sat.
        /// </summary>
        public TesteResponse TesteFimAFim(CFe cfe)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            var options = SaveOptions.OmitDeclaration | SaveOptions.DisableFormatting;
            if (Configuracoes.RemoverAcentos)
                options |= SaveOptions.RemoveAccents;

            var dadosVenda = cfe.GetXml(options);
            IniciaComando();

            if (Arquivos.SalvarEnvio)
            {
                var envioPath = Arquivos.PastaEnvio;
                var fullName = Path.Combine(envioPath, $"{Arquivos.PrefixoArqCFe}{DateTime.Now:yyyyMMddHHmmss}-{Sessao.ZeroFill(6)}-teste-env.xml");
                if (!Directory.Exists(envioPath))
                    Directory.CreateDirectory(envioPath);

                File.WriteAllText(fullName, dadosVenda);
            }

            var ret = satLibrary.TesteFimAFim(Sessao, CodigoAtivacao, dadosVenda);
            var resp = FinalizaComando<TesteResponse>(ret);
            if (!Arquivos.SalvarCFe || resp.CodigoDeRetorno != 9000)
                return resp;

            var cnpj = Arquivos.SepararPorCNPJ ? resp.VendaTeste.InfCFe.Emit.CNPJ : "";
            var data = Arquivos.SepararPorMes ? $"{resp.VendaTeste.InfCFe.Ide.DEmi:yyyy}\\{resp.VendaTeste.InfCFe.Ide.DEmi:MM}" : "";
            var path = Path.Combine(Arquivos.PastaCFeVenda, cnpj, data);
            var calcPathEventEventArgs = new CalcPathEventEventArgs
            {
                CNPJ = resp.VendaTeste.InfCFe.Emit.CNPJ,
                Data = resp.VendaTeste.InfCFe.Ide.DEmi,
                Path = path
            };

            OnCalcPath.Raise(this, calcPathEventEventArgs);

            if (!Directory.Exists(calcPathEventEventArgs.Path)) Directory.CreateDirectory(calcPathEventEventArgs.Path);

            var nomeArquivo = $"{Arquivos.PrefixoArqCFe}{resp.VendaTeste.InfCFe.Id}.xml";
            var fullPath = Path.Combine(calcPathEventEventArgs.Path, nomeArquivo);
            resp.VendaTeste.Save(fullPath);

            return resp;
        }

        /// <summary>
        ///     Troca o código de ativação do Sat.
        /// </summary>
        public SatResponse TrocarCodigoDeAtivacao(string codigo, int opcao, string novoCodigo)
        {
            Guard.Against<VipException>(!Ativo, "Componente não está ativo.");

            IniciaComando();
            var ret = satLibrary.TrocarCodigoDeAtivacao(Sessao, codigo, opcao, novoCodigo, novoCodigo);
            return FinalizaComando<SatResponse>(ret);
        }

        #endregion Public

        #region Private

        private void IniciaComando()
        {
            AguardandoResposta = true;
            GerarNumeroSessao();
        }

        private T FinalizaComando<T>(string resposta) where T : SatResponse
        {
            var resp = (T) Activator.CreateInstance(typeof(T), resposta, Encoding);

            if (resp.NumeroSessao != Sessao)
                if (Configuracoes.ValidarNumeroSessaoResposta)
                {
                    var fsSessaoAVerificar = Sessao;
                    var consultaCount = 0;

                    do
                    {
                        consultaCount++;
                        Guard.Against<VipException>(consultaCount > Configuracoes.NumeroTentativasValidarSessao, $"Sessao retornada pelo SAT [{resp.NumeroSessao}], diferente da enviada [{fsSessaoAVerificar}].");

                        IniciaComando();
                        var ret = satLibrary.ConsultarNumeroSessao(Sessao, CodigoAtivacao, fsSessaoAVerificar);
                        resp = (T) Activator.CreateInstance(typeof(T), ret, Encoding);
                    } while (resp.NumeroSessao != fsSessaoAVerificar);
                }

            if (resp.CodigoSEFAZ <= 0 || resp.MensagemSEFAZ.IsNullOrEmpty()) return resp;

            var e = new SatMensagemEventArgs(resp.CodigoSEFAZ, resp.MensagemSEFAZ);
            OnMensagemSefaz.Raise(this, e);
            AguardandoResposta = false;
            return resp;
        }

        private void GerarNumeroSessao()
        {
            Sessao = StaticRandom.Next(1, 999999);

            var e = new NumeroSessaoEventArgs(Sessao);
            OnGetNumeroSessao.Raise(this, e);
            Sessao = e.Sessao;
        }

        #endregion Private

        #region Override

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            Configuracoes = new SatConfig(this);
            Arquivos = new SatArquivos();
            Encoding = Encoding.UTF8;

            PathDll = @"C:\SAT\SAT.dll";
            CodigoAtivacao = "sefaz1234";
            signAC = string.Empty;
        }

        /// <inheritdoc />
        protected override void OnDisposing()
        {
            if (Ativo) Desativar();
        }

        #endregion Override

        #endregion Methods
    }
}