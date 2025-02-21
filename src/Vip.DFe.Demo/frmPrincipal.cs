using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vip.DFe.Danfe;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.Demo.Data;
using Vip.DFe.Demo.Enums;
using Vip.DFe.Demo.Extensions;
using Vip.DFe.Demo.Helpers;
using Vip.DFe.Demo.Models;
using Vip.DFe.Document;
using Vip.DFe.NFe;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.NFe.NotaFiscal;
using Vip.DFe.NFe.NotaFiscal.Destinatario;
using Vip.DFe.NFe.NotaFiscal.Detalhe;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual;
using Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal;
using Vip.DFe.NFe.NotaFiscal.Emitente;
using Vip.DFe.NFe.NotaFiscal.Identificacao;
using Vip.DFe.NFe.NotaFiscal.InformacaoAdicional;
using Vip.DFe.NFe.NotaFiscal.Pagamento;
using Vip.DFe.NFe.NotaFiscal.Total;
using Vip.DFe.NFe.NotaFiscal.Transporte;
using Vip.DFe.Shared.Enum;
using Icms00 = Vip.Fiscal.Imposto.Icms.Icms00;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Vip.DFe.Demo
{
    public partial class frmPrincipal : Form
    {
        #region Propriedades

        private readonly ConfiguracaoService _serviceConfiguracao;
        private readonly Configuracao _configuracao;
        private readonly BindingList<Item> _itens;
        private readonly BindingList<MeioPagamento> _meioPagamentos;
        private NFeService _service;

        #endregion

        #region Construtores

        public frmPrincipal()
        {
            InitializeComponent();

            #region Combobox

            cboVersao.DataSource(NFeVersao.v400);
            cboAmbiente.DataSource(TipoAmbiente.Homologacao);
            cboTipoEmissao.DataSource(TipoEmissao.Normal);
            cboModelo.DataSource(NFeModelo.NFe);
            cboEstado.DataSource<CodigoUF>();
            cboFinalidadeEmissao.DataSource(NFeFinalidade.Normal);
            cboTipoMovimento.DataSource(NFeTipo.Saida);
            cboDestinoOperacao.DataSource(NFeDestinoOperacao.Interna);
            cboIndicadorPresenca.DataSource(NFePresencaComprador.Presencial);
            cboEmitenteRegimeTributario.DataSource(RegimeTributario.Normal);
            cboDestinatarioIndicadorInscricaoEstadual.DataSource(NFeIndIeDest.NaoContribuinte);
            cboTransporteModalidadeFrete.DataSource(NFeModalidadeFrete.SemFrete);
            cboFuncoes.DataSource<FuncaoServico>();
            cboFormaPagamento.DataSource<MeioPagamento>();

            #endregion

            #region DataGridView

            _itens = new BindingList<Item>();

            dgItem.DataSource = _itens;
            dgItem.Columns["TotalItem"].ReadOnly = true;
            dgItem.CellValueChanged += (sender, args) =>
            {
                if (args.RowIndex >= 0 && (args.ColumnIndex == dgItem.Columns["Quantidade"].Index || args.ColumnIndex == dgItem.Columns["ValorItem"].Index)) AtualizarGrid();
            };

            #endregion

            #region ListBox

            _meioPagamentos = new BindingList<MeioPagamento>();
            lstFormasPagamento.DataSource = _meioPagamentos;
            lstFormasPagamento.Refresh();

            #endregion

            _serviceConfiguracao = new ConfiguracaoService();
            _configuracao = _serviceConfiguracao.Obter();
        }

        #endregion

        #region Eventos

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            CarregarCampos();

            var diretorioBase = AppDomain.CurrentDomain.BaseDirectory;

            if (txtDiretorioArquivos.Text.IsNullOrEmpty())
                txtDiretorioArquivos.Text = Path.Combine(diretorioBase, "arquivos");

            if (txtDiretorioSchemas.Text.IsNullOrEmpty())
                txtDiretorioSchemas.Text = Path.Combine(diretorioBase, "schemas");
        }

        private void btnSalvarConfiguracao_Click(object sender, EventArgs e)
        {
            SalvarConfiguracao();
            MessageBox.Show("Configurações salva com sucesso!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente finalizar a aplicação de testes?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            SalvarConfiguracao();
            Application.Exit();
        }

        private void btnGerarItem_Click(object sender, EventArgs e)
        {
            _itens.Add(Item.Obter());
            AtualizarGrid();
        }

        private void btnGerarDezItens_Click(object sender, EventArgs e)
        {
            var itens = Item.ObterLista();
            itens.ForEach(x => _itens.Add(x));
            AtualizarGrid();
        }

        private void btnApagarItens_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja apagar todos os itens?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;

            _itens.Clear();
            AtualizarGrid();
        }

        private void btnAdicionarFormaPagamento_Click(object sender, EventArgs e)
        {
            var meioPagamento = cboFormaPagamento.GetEnumValue<MeioPagamento>();
            _meioPagamentos.Add(meioPagamento);

            AtualizarListaMeioPagamento();
        }

        private void btnRemoverFormaPagamento_Click(object sender, EventArgs e)
        {
            if (_meioPagamentos.IsEmpty()) return;
            if (lstFormasPagamento.SelectedItem == null) return;

            var meioPagamento = (MeioPagamento) lstFormasPagamento.SelectedItem;
            _meioPagamentos.Remove(meioPagamento);

            AtualizarListaMeioPagamento();
        }

        private void btnSelecionarCertificado_Click(object sender, EventArgs e)
        {
            var certificado = CertificadoHelper.Obter();
            txtCertificadoNumeroSerie.Text = certificado == null ? "" : certificado.NumeroSerie;
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            _service = ObterServico();

            #region Validações

            var mensagemValidacao = "";

            if (txtDiretorioSchemas.Text.IsNullOrEmpty())
                mensagemValidacao += "Diretório de schemas não informado\r\n";

            if (txtCertificadoNumeroSerie.Text.IsNullOrEmpty())
                mensagemValidacao += "Número de série do certificado não informado\r\n";

            if (_service.IsNull())
                mensagemValidacao += "Classe de serviço da NFe não preenchida\r\n";

            if (mensagemValidacao.IsNotNullOrEmpty())
            {
                MessageBox.Show("Verifique as validações abaixo:\r\n\r\n" + mensagemValidacao, "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion

            #region Limpar Campos

            txtEnvio.Clear();
            txtRetorno.Clear();
            txtEnvelopSoap.Clear();
            txtResultado.Clear();
            txtErro.Clear();

            #endregion

            try
            {
                var funcao = cboFuncoes.GetEnumValue<FuncaoServico>();
                switch (funcao)
                {
                    case FuncaoServico.AutorizacaoLote:
                        FuncaoAutorizacaoLote();
                        break;
                    case FuncaoServico.Autorizacao:
                        FuncaoAutorizacao();
                        break;
                    case FuncaoServico.ConsultarAutorizacao:
                        FuncaoConsultarAutorizacao();
                        break;
                    case FuncaoServico.ConsultarPorChaveAcesso:
                        FuncaoConsultarPorChaveAcesso();
                        break;
                    case FuncaoServico.ConsultarStatusServico:
                        FuncaoConsultarStatusServico();
                        break;
                    case FuncaoServico.CancelarDocumento:
                        FuncaoCancelarDocumento();
                        break;
                    case FuncaoServico.InutilizarNumeracao:
                        FuncaoInutilizarNumeracao();
                        break;
                    case FuncaoServico.CartaCorrecao:
                        FuncaoCartaCorrecao();
                        break;
                    case FuncaoServico.GerarDanfe:
                        FuncaoGerarDanfe();
                        break;
                    case FuncaoServico.GerarDanfeEvento:
                        FuncaoGerarDanfeEvento();
                        break;
                    case FuncaoServico.GerarXml:
                        FuncaoGerarXml();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                MessageBox.Show($"Função: {funcao.GetDescription()} executada", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        #endregion

        #region Métodos Função

        private void FuncaoGerarXml()
        {
            var documento = ObterDocumento();
            _service.Documentos.Add(documento);
            _service.Documentos.Assinar();

            txtXml.Text = _service.Documentos.IsEmpty() ? "" : _service.Documentos.NFe.FirstOrDefault().GetXml().FormatarXml();
        }

        private void FuncaoAutorizacao()
        {
            var gerarNovoXml = true;

            if (txtXml.Text.IsNotEmpty())
            {
                gerarNovoXml = MessageBox.Show("Deseja utilizar o XML preenchido?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
            }

            var documento = gerarNovoXml ? ObterDocumento() : NFe.NotaFiscal.NFe.Load(txtXml.Text);

            _service.Documentos.Add(documento);
            txtXml.Text = _service.Documentos.IsEmpty() ? "" : _service.Documentos.NFe.FirstOrDefault().GetXml().FormatarXml();

            try
            {
                var retorno = _service.Autorizacao();
                txtEnvio.Text = retorno.XmlEnvio.FormatarXml();
                txtRetorno.Text = retorno.XmlRetorno.FormatarXml();
                txtEnvelopSoap.Text = retorno.EnvelopeSoap.FormatarXml();
                txtResultado.Text = retorno.Resultado.Xml.FormatarXml();

                txtParametro.Text = retorno.Resultado.InfRec.NRec;

                var xmlAssinado = _service.Documentos.NFe.FirstOrDefault();
                if (xmlAssinado.IsNotNull()) txtXml.Text = xmlAssinado.Assinado ? xmlAssinado.GetXml().FormatarXml() : "";

                if (retorno.NFeAutorizada.IsNotNull())
                    txtResultado.Text = retorno.NFeAutorizada.Xml.FormatarXml();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Houve um erro ao executar o método Autorização. Verifique");

                tbcEvento.SelectTab(tbpErro);
                tbpErro.Focus();
                txtErro.Text = ex.Message;
                txtErro.Text += ex.InnerException.IsNotNull() ? "\r\n\r\n" + ex.InnerException.Message : "";
            }
        }

        private void FuncaoGerarDanfeEvento()
        {
            if (txtXml.Text.IsNullOrEmpty()) txtParametro.Text = ObterCaminhoArquivo();

            if (txtXml.Text.IsNullOrEmpty() && txtParametro.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Informe o conteúdo do XML na aba XML ou o caminho do xml autorizado no campo Parâmetro");
                return;
            }

            var dataHora = DateTime.Now.ToString("ddMMyymmss");
            var caminhoPdf = $@"{txtDiretorioArquivos.Text}\danfe{dataHora}.pdf";

            var modelo = txtXml.Text.IsNotNullOrEmpty() ? DanfeEventoViewModel.CriarDoConteudoXml(txtXml.Text) : DanfeEventoViewModel.CriarDeArquivoXml(txtParametro.Text);
            modelo.DefinirTextoCreditos("Emitido pelo software VipERP - www.vipsolucoes.com");

            using (var danfe = new DanfeEventoService(modelo))
            {
                //danfe.AdicionarLogoImagem(@"C:\caminhoLogotipo.jpg");
                danfe.Gerar();
                danfe.Salvar(caminhoPdf);
            }

            if (MessageBox.Show("Deseja abrir o arquivo gerado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Process.Start(new ProcessStartInfo {FileName = caminhoPdf, UseShellExecute = true, WorkingDirectory = "D:\\"});
        }

        private void FuncaoGerarDanfe()
        {
            if (txtXml.Text.IsNullOrEmpty()) txtParametro.Text = ObterCaminhoArquivo();

            if (txtXml.Text.IsNullOrEmpty() && txtParametro.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Informe o conteúdo do XML na aba XML ou o caminho do xml autorizado no campo Parâmetro");
                return;
            }

            var dataHora = DateTime.Now.ToString("ddMMyymmss");
            var caminhoPdf = $@"{txtDiretorioArquivos.Text}\danfe{dataHora}.pdf";

            var modelo = txtXml.Text.IsNotNullOrEmpty() ? DanfeViewModel.CriarDoConteudoXml(txtXml.Text) : DanfeViewModel.CriarDeArquivoXml(txtParametro.Text);
            modelo.DefinirTextoCreditos("Emitido pelo software VipERP - www.vipsolucoes.com");
            modelo.PreferirEmitenteNomeFantasia = true;
            modelo.ExibirBlocoLocalEntrega = false;
            modelo.ExibirBlocoLocalRetirada = false;

            using (var danfe = new DanfeService(modelo))
            {
                //danfe.AdicionarLogoImagem(@"C:\caminhoLogotipo.jpg");
                danfe.Gerar();
                danfe.Salvar(caminhoPdf);
            }

            if (MessageBox.Show("Deseja abrir o arquivo gerado?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Process.Start(new ProcessStartInfo {FileName = caminhoPdf, UseShellExecute = true, WorkingDirectory = "D:\\"});
        }

        private void FuncaoCartaCorrecao()
        {
            if (txtParametro.Text.IsNullOrEmpty() || txtParametro.Text.Length < 44)
            {
                MessageBox.Show("Informe chave de acesso para registrar a carta de correção do documento");
                return;
            }

            try
            {
                var retorno = _service.CartaoCorrecao(_configuracao.EmitenteCnpj, txtParametro.Text, 1, "CARTA DE CORREÇÃO DA NOTA FISCAL ELETRONICA");
                txtEnvio.Text = retorno.XmlEnvio.FormatarXml();
                txtRetorno.Text = retorno.XmlRetorno.FormatarXml();
                txtEnvelopSoap.Text = retorno.EnvelopeSoap.FormatarXml();
                txtResultado.Text = retorno.Resultado.Xml.FormatarXml();
            }
            catch (System.Exception ex)
            {
                tbcEvento.SelectTab(tbpErro);
                tbpErro.Focus();
                txtErro.Text = ex.Message;
                txtErro.Text += ex.InnerException.IsNotNull() ? "\r\n\r\n" + ex.InnerException.Message : "";
            }
        }

        private void FuncaoInutilizarNumeracao()
        {
            if (txtParametro.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Informe o número a ser inutilizado");
                return;
            }

            var numero = txtParametro.Text.ToInt();
            if (numero <= 0)
            {
                MessageBox.Show("Número inválido, por favor tente novamente");
                return;
            }

            try
            {
                var retorno = _service.Inutilizar(_configuracao.EmitenteCnpj, "NUMERACAO INUTILIZADA", _configuracao.Modelo, _configuracao.Serie, numero, numero);
                txtEnvio.Text = retorno.XmlEnvio.FormatarXml();
                txtRetorno.Text = retorno.XmlRetorno.FormatarXml();
                txtEnvelopSoap.Text = retorno.EnvelopeSoap.FormatarXml();
                txtResultado.Text = retorno.Resultado.Xml.FormatarXml();
            }
            catch (System.Exception ex)
            {
                tbcEvento.SelectTab(tbpErro);
                tbpErro.Focus();
                txtErro.Text = ex.Message;
                txtErro.Text += ex.InnerException.IsNotNull() ? "\r\n\r\n" + ex.InnerException.Message : "";
            }
        }

        private void FuncaoCancelarDocumento()
        {
            if (txtParametro.Text.IsNullOrEmpty() || txtParametro.Text.Length < 60)
            {
                MessageBox.Show("Informe chave de acesso + número do protocolo para cancelar o documento\r\n\r\nExemplo: chaveAcesso;numeroProtocolo");
                return;
            }

            var texto = txtParametro.Text.Split(';');
            var chaveAcesso = texto[0];
            var protocolo = texto[1];

            try
            {
                var retorno = _service.Cancelar(_configuracao.EmitenteCnpj, chaveAcesso, protocolo, 1, "NOTA FISCAL ELETRONICA CANCELADA");
                txtEnvio.Text = retorno.XmlEnvio.FormatarXml();
                txtRetorno.Text = retorno.XmlRetorno.FormatarXml();
                txtEnvelopSoap.Text = retorno.EnvelopeSoap.FormatarXml();
                txtResultado.Text = retorno.Resultado.Xml.FormatarXml();
            }
            catch (System.Exception ex)
            {
                tbcEvento.SelectTab(tbpErro);
                tbpErro.Focus();
                txtErro.Text = ex.Message;
                txtErro.Text += ex.InnerException.IsNotNull() ? "\r\n\r\n" + ex.InnerException.Message : "";
            }
        }

        private void FuncaoConsultarStatusServico()
        {
            try
            {
                var retorno = _service.ConsultaSituacaoServico();
                txtEnvio.Text = retorno.XmlEnvio.FormatarXml();
                txtRetorno.Text = retorno.XmlRetorno.FormatarXml();
                txtEnvelopSoap.Text = retorno.EnvelopeSoap.FormatarXml();
                txtResultado.Text = retorno.Resultado.Xml.FormatarXml();
            }
            catch (System.Exception ex)
            {
                tbcEvento.SelectTab(tbpErro);
                tbpErro.Focus();
                txtErro.Text = ex.Message;
                txtErro.Text += ex.InnerException.IsNotNull() ? "\r\n\r\n" + ex.InnerException.Message : "";
            }
        }

        private void FuncaoConsultarPorChaveAcesso()
        {
            if (txtParametro.Text.IsNullOrEmpty() || txtParametro.Text.Length < 40)
            {
                MessageBox.Show("Informe o número da chave de acesso para executar a consulta");
                return;
            }

            try
            {
                var retorno = _service.Consultar(txtParametro.Text);
                txtEnvio.Text = retorno.XmlEnvio.FormatarXml();
                txtRetorno.Text = retorno.XmlRetorno.FormatarXml();
                txtEnvelopSoap.Text = retorno.EnvelopeSoap.FormatarXml();
                txtResultado.Text = retorno.Resultado.Xml.FormatarXml();
            }
            catch (System.Exception ex)
            {
                tbcEvento.SelectTab(tbpErro);
                tbpErro.Focus();
                txtErro.Text = ex.Message;
                txtErro.Text += ex.InnerException.IsNotNull() ? "\r\n\r\n" + ex.InnerException.Message : "";
            }
        }

        private void FuncaoConsultarAutorizacao()
        {
            if (txtParametro.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Informe o número do recibo para executar a consulta");
                return;
            }

            if (txtXml.IsNotNullOrEmpty())
                _service.Documentos.LoadXml(txtXml.Text);

            try
            {
                var retorno = _service.ConsultaAutorizacao(txtParametro.Text);
                txtEnvio.Text = retorno.XmlEnvio.FormatarXml();
                txtRetorno.Text = retorno.XmlRetorno.FormatarXml();
                txtEnvelopSoap.Text = retorno.EnvelopeSoap.FormatarXml();

                if (retorno.NFeAutorizadas.IsNotEmpty())
                    txtResultado.Text = retorno.NFeAutorizadas[0].Xml.FormatarXml();
            }
            catch (System.Exception ex)
            {
                tbcEvento.SelectTab(tbpErro);
                tbpErro.Focus();
                txtErro.Text = ex.Message;
                txtErro.Text += ex.InnerException.IsNotNull() ? "\r\n\r\n" + ex.InnerException.Message : "";
            }
        }

        private void FuncaoAutorizacaoLote()
        {
            var documento = ObterDocumento();

            _service.Documentos.Add(documento);
            txtXml.Text = _service.Documentos.IsEmpty() ? "" : _service.Documentos.NFe.FirstOrDefault().Xml.FormatarXml();

            try
            {
                var retorno = _service.AutorizacaoLote();
                txtEnvio.Text = retorno.XmlEnvio.FormatarXml();
                txtRetorno.Text = retorno.XmlRetorno.FormatarXml();
                txtEnvelopSoap.Text = retorno.EnvelopeSoap.FormatarXml();
                txtResultado.Text = retorno.Resultado.Xml.FormatarXml();

                txtParametro.Text = retorno.Resultado.InfRec.NRec;

                var xmlAssinado = _service.Documentos.NFe.FirstOrDefault();
                if (xmlAssinado.IsNotNull()) txtXml.Text = xmlAssinado.Assinado ? xmlAssinado.GetXml().FormatarXml() : "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Houve um erro ao executar o método Autorização. Verifique");

                tbcEvento.SelectTab(tbpErro);
                tbpErro.Focus();
                txtErro.Text = ex.Message;
                txtErro.Text += ex.InnerException.IsNotNull() ? "\r\n\r\n" + ex.InnerException.Message : "";
            }
        }

        #endregion

        #region Métodos Documento

        private NFe.NotaFiscal.NFe ObterDocumento()
        {
            var nfe = new NFe.NotaFiscal.NFe();

            #region Identificação

            nfe.InfNFe.Versao = _configuracao.Versao;

            // Verificar aqui se passa novamente a chave de acesso;
            nfe.InfNFe.Id = "";

            var ide = new NFeIde
            {
                CodigoUF = _configuracao.Estado,
                NatOp = _configuracao.NaturezaOperacao,
                Modelo = _configuracao.Modelo,
                Serie = _configuracao.Serie,
                NumeroNFe = ObterProximoNumeroFiscal(),
                TipoNFe = _configuracao.TipoMovimento,
                CMunFG = _configuracao.EmitenteCodigoIbge,
                TipoEmissao = _configuracao.TipoEmissao,
                CNf = ObterCNF(),
                TpAmb = _configuracao.Ambiente,
                FinNFe = _configuracao.FinalidadeEmissao,
                VerProc = "1.1.0",
                DhEmi = DateTime.Now,
                DhSaiEnt = DateTime.Now,
                IdDest = _configuracao.DestinoOperacao,
                ProcEmi = ProcessoEmissao.AplicativoContribuinte,
                IndFinal = _configuracao.ConsumidorFinal ? NFeConsumidorFinal.Sim : NFeConsumidorFinal.Nao,
                TipoImpressao = _configuracao.Modelo.Equals(NFeModelo.NFe) ? TipoImpressao.NormalRetrato : TipoImpressao.NFCe,
                IndPres = _configuracao.IndicadorPresenca,
                CDV = 0 //notaFiscal.ChaveAcesso.IsNotNullOrEmpty() ? notaFiscal.ChaveAcesso.Right(1).ToInt() : 0
            };

            if (ide.TipoEmissao != TipoEmissao.Normal)
            {
                ide.DhCont = ide.DhEmi;
                ide.XJust = _configuracao.JustificativaContingencia;
            }

            nfe.InfNFe.Ide = ide;

            #endregion

            #region Emitente

            var emit = new NFeEmit
            {
                XNome = _configuracao.EmitenteRazaoSocial,
                XFant = _configuracao.EmitenteRazaoSocial,
                CNPJ = _configuracao.EmitenteCnpj.Length == 14 ? _configuracao.EmitenteCnpj : "",
                CPF = _configuracao.EmitenteCnpj.Length == 11 ? _configuracao.EmitenteCnpj : "",
                CRT = _configuracao.EmitenteRegimeTributario,
                IE = _configuracao.EmitenteInscricaoEstadual,
                Endereco = new NFeEmitEndereco
                {
                    Logradouro = _configuracao.EmitenteLogradouro,
                    Numero = _configuracao.EmitenteNumero,
                    Bairro = _configuracao.EmitenteBairro,
                    CodigoIBGE = _configuracao.EmitenteCodigoIbge,
                    Municipio = _configuracao.EmitenteCidade,
                    UF = _configuracao.EmitenteUf,
                    CEP = _configuracao.EmitenteCep,
                    CodigoPais = 1058,
                    Pais = "BRASIL",
                    Fone = _configuracao.EmitenteTelefone
                }
            };

            nfe.InfNFe.Emitente = emit;

            #endregion

            #region Destinatário

            if (_configuracao.DestinatarioCnpj.IsNotNullOrEmpty())
            {
                var dest = new NFeDest
                {
                    Nome = _configuracao.DestinatarioRazaoSocial,
                    CPF = _configuracao.DestinatarioCnpj.Length == 11 ? _configuracao.DestinatarioCnpj : "",
                    CNPJ = _configuracao.DestinatarioCnpj.Length == 14 ? _configuracao.DestinatarioCnpj : "",
                    IE = _configuracao.DestinatarioIndicadorInscricaoEstadual.Equals(NFeIndIeDest.Contribuinte) ? _configuracao.DestinatarioInscricaoEstadual : "",
                    Endereco = new NFeDestEndereco
                    {
                        Logradouro = _configuracao.DestinatarioLogradouro,
                        Numero = _configuracao.DestinatarioNumero,
                        Bairro = _configuracao.DestinatarioBairro,
                        CodigoIBGE = _configuracao.DestinatarioCodigoIbge,
                        Municipio = _configuracao.DestinatarioCidade,
                        UF = _configuracao.DestinatarioUf,
                        CEP = _configuracao.DestinatarioCep,
                        CodigoPais = 1058,
                        Pais = "BRASIL",
                        Fone = _configuracao.DestinatarioTelefone
                    },
                    IndIEDest = _configuracao.DestinatarioIndicadorInscricaoEstadual,
                    Email = ""
                };

                nfe.InfNFe.Destinatario = dest;
            }

            #endregion

            #region Transportador

            var transp = new NFeTransporte();
            transp.ModFrete = _configuracao.TransporteModalidadeFrete;

            if (transp.ModFrete != NFeModalidadeFrete.SemFrete && _configuracao.TransporteCnpj.IsNotNullOrEmpty())
                transp.Transporta = new NFeTransporta
                {
                    XNome = _configuracao.TransporteRazaoSocial,
                    Cnpj = _configuracao.TransporteCnpj.Length == 14 ? _configuracao.TransporteCnpj : "",
                    Cpf = _configuracao.TransporteCnpj.Length == 11 ? _configuracao.TransporteCnpj : "",
                    IE = "",
                    UF = _configuracao.TransporteUf,
                    XEnder = _configuracao.TransporteEndereco,
                    XMun = _configuracao.TransporteCidade
                };

            if (_configuracao.DestinoOperacao.Equals(NFeDestinoOperacao.Interna) && _configuracao.TransporteInformarPlacaVeiculo)
                transp.VeicTransp = new NFeVeicTransp
                {
                    Placa = "AAA1234",
                    UF = _configuracao.EmitenteUf
                };

            if (_configuracao.VolumeEspecie.IsNotNullOrEmpty()
                || _configuracao.VolumeMarca.IsNotNullOrEmpty()
                || _configuracao.VolumeNumeracao.IsNotNullOrEmpty()
                || _configuracao.VolumeQuantidade > 0
                || _configuracao.VolumePesoBruto > 0
                || _configuracao.VolumePesoLiquido > 0)
                transp.Vol = new DFeCollection<NFeVolTransp>
                {
                    new NFeVolTransp
                    {
                        Especie = _configuracao.VolumeEspecie,
                        Marca = _configuracao.VolumeMarca,
                        NVol = _configuracao.VolumeNumeracao,
                        QVol = _configuracao.VolumeQuantidade,
                        PesoB = _configuracao.VolumePesoBruto,
                        PesoL = _configuracao.VolumePesoLiquido
                    }
                };

            nfe.InfNFe.Transporte = transp;

            #endregion

            #region Detalhes

            var listaDetalhes = new DFeCollection<NFeDetalhe>();
            var numeroItem = 1;
            foreach (var item in _itens)
            {
                #region Se item for GLP

                var itemGLP = item.ItemGLP;
                var unidadeTributada = itemGLP ? "KG" : item.Medida;
                var quantidadeTributada = itemGLP ? 13 * item.Quantidade : item.Quantidade;
                var valorItemTributado = itemGLP ? item.ValorItem.Division(13).Round(5) : item.ValorItem;

                #endregion

                #region Detalhe Item

                var det = new NFeDetalhe
                {
                    NItem = numeroItem++,
                    Produto = new NFeDetProduto
                    {
                        Codigo = item.Codigo,
                        CEAN = "SEM GTIN",
                        CEANTrib = "SEM GTIN",
                        XProd = item.Descricao,
                        NCM = item.NCM,
                        CEST = item.CEST,
                        CBenef = "",
                        CFOP = item.CFOP,
                        UCom = item.Medida,
                        UTrib = unidadeTributada,
                        QCom = item.Quantidade,
                        QTrib = quantidadeTributada,
                        VUnCom = item.ValorItem,
                        VUnTrib = valorItemTributado,
                        VProd = (item.Quantidade * item.ValorItem).Round(4),
                        VFrete = item.Frete,
                        VSeg = item.Seguro,
                        VDesc = item.Desconto,
                        VOutro = item.Outros,
                        IndTot = NFeIndTotal.ValorItemCompoeTotalNota,
                        XPed = "",
                        NItemPed = 0,
                    },
                    InfAdProd = "INFORMACAO ADICIONAL PRODUTO",
                    Imposto = GerarImposto(item),
                    //ImpostoDevol = GerarImpostoDevolvido(item, natureza)
                };

                #endregion

                #region Difal

                //if (item.CstIcms == "00" && notaFiscal.DestinoOperacao == DestinoOperacao.Interestadual && destinatario.IndicadorIE == IndicadorIE.NaoContribuinte)
                //{
                //    var aliqInterna = TributacaoService.ObterAliquotaInterna(empresa.Cidade.Estado);
                //    var aliqDestino = TributacaoService.ObterAliquotaDestino(endereco.Cidade.Estado);

                //    var fiscal = new IcmsDifal(item.SubTotal, item.Frete, item.Seguro, item.Outros, 0, item.Desconto, aliqInterna, aliqDestino, 0);
                //    var icmsDifal = new IcmsUfDest
                //    {
                //        VBcUfDest = fiscal.BaseIcms(),
                //        PFcpUfDest = item.AliquotaFcp,
                //        PIcmsUfDest = aliqInterna,
                //        PIcmsInter = aliqDestino,
                //        PIcmsInterPart = 100,
                //        VFcpUfDest = fiscal.ValorFcpDestino(),
                //        VIcmsUfDest = fiscal.ValorIcmsDestino(),
                //        VIcmsUfRemet = fiscal.ValorIcmsRemetente()
                //    };
                //    det.Imposto.IcmsUfDest = icmsDifal;
                //}

                #endregion

                #region Combustivel

                //if (item.Produto.AnpId.HasValue && item.Produto.AnpId.Value.IsNotEmpty())
                //{
                //    var prodCombustivel = new NFeDetProdutoCombustivel
                //    {
                //        CProdANP = item.Produto.Anp.Codigo,
                //        DescANP = item.Produto.Anp.Descricao,
                //        UFCons = ide.CodigoUF.GetDescription(),
                //        PGLP = item.Produto.PercentualGLP,
                //        PGNn = item.Produto.PercentualGNN,
                //        PGNi = item.Produto.PercentualGNI,
                //        VPart = item.Produto.ValorPartidaGLP
                //    };
                //    det.Produto.ProdutoEspecifico = prodCombustivel;
                //}

                #endregion

                #region Icms Monofásico

                //var informacaoMonofasico = det.InfAdProd.IsNotNullOrEmpty() ? "; " : "";
                //switch (item.CstIcms)
                //{
                //    case "02":
                //        informacaoMonofasico += $"ICMS monofásico próprio: BC {item.QuantidadeMonofasico:N4} (em litros); Alíquota: R$ {item.AliquotaAdRem:N4}; ICMS mono: R$ {item.ValorIcmsMonofasico:N}";
                //        det.InfAdProd += informacaoMonofasico;
                //        break;
                //    case "61":
                //        informacaoMonofasico += "ICMS monofásico sobre combustíveis cobrado anteriormente conforme Convênio ICMS 199/2022";
                //        det.InfAdProd += informacaoMonofasico;
                //        break;
                //}

                #endregion

                #region Valor Tributos

                if (_configuracao.Modelo == NFeModelo.NFCe)
                {
                    const decimal federal = 12;
                    const decimal estadual = 18;
                    const decimal municipal = 0;

                    var vProd = det.Produto.VProd;
                    det.Imposto.VTotTrib = vProd * (federal + estadual + municipal) / 100;
                }

                #endregion

                listaDetalhes.Add(det);
            }

            nfe.InfNFe.Detalhe.AddRange(listaDetalhes);

            #endregion

            #region Total

            var detItens = listaDetalhes.Where(x => x.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).ToList();

            var valorFrete = _itens.Sum(x => x.Frete);
            var valorOutro = _itens.Sum(x => x.Outros);
            var valorSeguro = _itens.Sum(x => x.Seguro);
            var totalItens = detItens.Where(p => p.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).Sum(p => p.Produto.VProd);
            var totalDesconto = detItens.Where(p => p.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).Sum(p => p.Produto.VDesc);
            var totalOutros = detItens.Where(p => p.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).Sum(e => e.Produto.VOutro);
            var totalTrib = detItens.Where(p => p.Produto.IndTot == NFeIndTotal.ValorItemCompoeTotalNota).Sum(p => p.Imposto.VTotTrib);

            var valorFCPST = 0;
            var valorIcmsST = 0;
            var valorIPI = 0;
            var valorPIS = 0;
            var valorCOFINS = 0;
            var valorFCP = 0;
            var valorFCPSTRet = 0;

            var icmsTot = new NFeIcmsTot
            {
                VBcSt = 0,
                VSt = 0,
                VBc = 0,
                VIcms = 0,
                VProd = totalItens,
                VNf = totalItens - totalDesconto + valorFCPST + valorIcmsST + valorIPI + valorFrete + totalOutros + valorSeguro,
                VDesc = totalDesconto,
                VTotTrib = totalTrib,
                VPis = valorPIS,
                VCofins = valorCOFINS,
                VIpi = valorIPI,
                VSeg = valorSeguro,
                VOutro = valorOutro,
                VFcp = valorFCP,
                VFcpSt = valorFCPST,
                VFcpStRet = valorFCPSTRet,
                VIpiDevol = 0,
                VFrete = valorFrete,
                VIcmsDeson = 0
            };

            var total = new NFeTotal {IcmsTot = icmsTot};
            nfe.InfNFe.Total = total;

            #endregion

            #region Pagamento

            var pag = new NFePagamento {DetPag = new DFeCollection<NFeDetPag>()};
            if (_meioPagamentos.IsEmpty())
            {
                if (_configuracao.Modelo == NFeModelo.NFe && _configuracao.FinalidadeEmissao == NFeFinalidade.Devolucao)
                    pag.DetPag.Add(new NFeDetPag {TPag = MeioPagamento.SemPagamento});
                else
                    pag.DetPag.Add(new NFeDetPag
                    {
                        TPag = MeioPagamento.Dinheiro,
                        VPag = icmsTot.VNf
                    });
            }
            else
            {
                var valorPagamnento = (icmsTot.VNf / _meioPagamentos.Count).Round();

                foreach (var pagamento in _meioPagamentos)
                {
                    var detPag = new NFeDetPag
                    {
                        TPag = pagamento,
                        VPag = valorPagamnento,
                        XPag = pagamento == MeioPagamento.Outros ? "IFOOD" : ""
                    };

                    if (pagamento == MeioPagamento.CartaodeCredito || pagamento == MeioPagamento.CartaodeDebito)
                    {
                        var detCartao = new NFeCardPag {TpIntegra = NFeTipoIntegracaoPagamento.PagamentoNaoIntegradoPOS};
                        detPag.Card = detCartao;
                    }

                    pag.DetPag.Add(detPag);
                }
            }

            nfe.InfNFe.Pagamento = pag;

            #endregion

            #region Fatura

            //if (notaFiscal.NotaFiscalFaturas.IsNotEmpty())
            //{
            //    var valorFaturas = notaFiscal.NotaFiscalFaturas.Sum(x => x.ValorDuplicata);
            //    var cobranca = new NFeCobranca
            //    {
            //        Duplicata = new DFeCollection<NFeCobrDuplicata>(),
            //        Fatura = new NFeCobrFatura
            //        {
            //            NFat = notaFiscal.Numero.ToString(),
            //            VOrig = valorFaturas,
            //            VDesc = 0,
            //            VLiq = valorFaturas
            //        }
            //    };

            //    numeroItem = 1;
            //    foreach (var fatura in notaFiscal.NotaFiscalFaturas.OrderBy(x => x.DataVencimento))
            //        cobranca.Duplicata.Add(new NFeCobrDuplicata
            //            {
            //                NDup = $"{numeroItem++:000}",
            //                DVenc = fatura.DataVencimento,
            //                VDup = fatura.ValorDuplicata
            //            }
            //        );

            //    nfe.InfNFe.Cobranca = cobranca;
            //}

            #endregion

            #region Referencia

            if (_configuracao.Modelo == NFeModelo.NFe && _configuracao.FinalidadeEmissao == NFeFinalidade.Devolucao) nfe.InfNFe.Ide.NFref.AddRange(ObterReferenciadas());

            #endregion

            #region Informação Adicional

            #region Complementar

            var informacaoComplementar = "INFORMACAO COMPLEMENTAR DE EMISSAO\r\n\r\n";

            const decimal impostoFederal = 10m;
            const decimal impostoEstadual = 20m;
            const decimal impostoMunicipal = 0m;

            const decimal totalImpostoAproximado = impostoMunicipal + impostoEstadual + impostoFederal;
            var mensagem = $"Valor aproximado dos tributos Mun.: {impostoMunicipal:C} Est.: {impostoEstadual:C} Fed.: {impostoFederal:C} Total: {totalImpostoAproximado:C} Fonte: IBPT";
            informacaoComplementar += informacaoComplementar.IsNotNullOrEmpty() ? $" ;{mensagem}" : mensagem;

            #endregion

            var infAdic = new NFeInformacaoAdicional {InformacaoComplementar = informacaoComplementar};
            nfe.InfNFe.InformacaoAdicional = infAdic;

            #endregion

            #region Responsável Técnica

            var resp = new NFeResponsavelTecnico
            {
                Email = "leandro@vipsolucoes.com",
                Cnpj = "12332134000199",
                Fone = "5517991602494",
                XContato = "Leandro dos Santos Ferreira"
            };
            nfe.InfNFe.ResponsavelTecnico = resp;

            #endregion

            return nfe;
        }

        private IEnumerable<NFeNfRef> ObterReferenciadas()
        {
            var notasRefs = new List<NFeNfRef>
            {
                new NFeNfRef {RefNFe = "35210338707682000140550010000010521975982021"},
                new NFeNfRef {RefNFe = "35210338707682000140550010000010521975982020"}
            };

            return notasRefs;
        }

        private NFeDetImposto GerarImposto(Item item)
        {
            var detImposto = new NFeDetImposto
            {
                Imposto = GerarIcms(item),
                //Ipi = GerarIpi(item),
                Pis = GerarPis(),
                Cofins = GerarCofins()
            };

            return detImposto;
        }

        private INFeImposto GerarIcms(Item item)
        {
            INFeIcms icms = null;
            const OrigemMercadoria origem = OrigemMercadoria.Nacional;

            var cstIcms = _configuracao.EmitenteRegimeTributario.Equals(RegimeTributario.Normal) ? "00" : "102";

            switch (cstIcms)
            {
                case "00":
                    var fiscal = new Icms00((item.Quantidade + item.ValorItem).Round(), item.Frete, item.Seguro, item.Outros, 0, item.Desconto, 18);
                    icms = new NFe.NotaFiscal.Detalhe.Imposto.Estadual.Icms00
                    {
                        Origem = origem,
                        ModBC = NFeModalidadeBC.ValorOperacao,
                        VBc = fiscal.BaseIcms(),
                        PIcms = 18,
                        VIcms = fiscal.ValorIcms(),
                        PFcp = 0,
                        VFcp = 0
                    };
                    break;
                case "102":
                case "103":
                case "300":
                case "400":
                    icms = new IcmsSn102
                    {
                        Origem = origem,
                        Csosn = cstIcms
                    };
                    break;
            }

            #region Possibilidades Tributação

            //case "02":
            //    icms = new Icms02
            //    {
            //        Origem = origem,
            //        QtdeBCMono = item.QuantidadeMonofasico,
            //        AdRemICMS = item.AliquotaAdRem,
            //        VICMSMono = item.ValorIcmsMonofasico
            //    };
            //    break;
            //case "10":
            //    icms = new Icms10
            //    {
            //        Origem = origem,
            //        ModBC = modalidadeBc,
            //        VBc = item.BcIcms,
            //        PIcms = item.AliquotaIcms,
            //        VIcms = item.ValorIcms,
            //        ModBcSt = modalidadeBcSt,
            //        PMvaSt = item.MvaSt,
            //        PRedBcSt = item.ReducaoBcIcmsSt,
            //        VBcSt = item.BcIcmsSt,
            //        PIcmsSt = item.AliquotaIcmsSt,
            //        VIcmsSt = item.ValorIcmsSt,
            //        VBcFcpSt = item.ValorFcp > 0 ? item.BcIcmsSt : 0,
            //        PFcpSt = item.AliquotaFcp,
            //        VFcpSt = item.ValorFcp
            //    };
            //    break;
            //case "20":
            //    icms = new Icms20
            //    {
            //        Origem = origem,
            //        ModBc = modalidadeBc,
            //        PRedBc = item.ReducaoBcIcms,
            //        VBc = item.BcIcms,
            //        PIcms = item.AliquotaIcms,
            //        VIcms = item.ValorIcms,
            //        VBcFcp = item.ValorFcp > 0 ? item.BcIcms : 0,
            //        PFcp = item.AliquotaFcp,
            //        VFcp = item.ValorFcp,
            //        VIcmsDeson = item.ValorIcmsDesonerado,
            //        MotDesIcms = NFeMotivoDesoneracao.Outros
            //    };
            //    break;
            //case "30":
            //    icms = new Icms30
            //    {
            //        Origem = origem,
            //        ModBcSt = modalidadeBcSt,
            //        PMvaSt = item.MvaSt,
            //        PRedBcSt = item.ReducaoBcIcmsSt,
            //        VBcSt = item.BcIcmsSt,
            //        PIcmsSt = item.AliquotaIcmsSt,
            //        VIcmsSt = item.ValorIcmsSt,
            //        VBcFcpSt = item.ValorFcp > 0 ? item.BcIcmsSt : 0,
            //        PFcpSt = item.AliquotaFcp,
            //        VFcpSt = item.ValorFcp,
            //        VIcmsDeson = item.ValorIcmsDesonerado,
            //        MotDesIcms = NFeMotivoDesoneracao.Outros
            //    };
            //    break;
            //case "40":
            //case "41":
            //case "50":
            //    icms = new Icms40
            //    {
            //        Origem = origem,
            //        Cst = item.CstIcms,
            //        VIcmsDeson = item.ValorIcmsDesonerado,
            //        MotDesIcms = NFeMotivoDesoneracao.Outros
            //    };
            //    break;
            //case "51":
            //    icms = new Icms51
            //    {
            //        Origem = origem,
            //        ModBc = modalidadeBc,
            //        PRedBc = item.ReducaoBcIcms,
            //        VBc = item.BcIcms,
            //        PIcms = item.AliquotaIcms,
            //        VIcmsOp = item.ValorIcms,
            //        PDif = 0,
            //        VIcmsDif = 0,
            //        VIcms = item.ValorIcms,
            //        VBcFcp = item.ValorFcp > 0 ? item.BcIcms : 0,
            //        PFcp = item.AliquotaFcp,
            //        VFcp = item.ValorFcp
            //    };
            //    break;
            //case "60":
            //    icms = new Icms60
            //    {
            //        Origem = origem,
            //    };
            //    break;
            //case "61":
            //    icms = new Icms61
            //    {
            //        Origem = origem,
            //        QtdeBCMonoRef = item.QuantidadeMonofasicoRet,
            //        AdRemICMSRet = item.AliquotaAdRemRet,
            //        VICMSMonoRef = item.ValorIcmsMonofasicoRet
            //    };
            //    break;
            //case "70":
            //    icms = new Icms70
            //    {
            //        Origem = origem,
            //        ModBC = modalidadeBc,
            //        PRedBc = item.ReducaoBcIcms,
            //        VBc = item.BcIcms,
            //        PIcms = item.AliquotaIcms,
            //        vICMS = item.ValorIcms,
            //        ModBcSt = modalidadeBcSt,
            //        PMvaSt = item.MvaSt,
            //        PRedBcSt = item.ReducaoBcIcmsSt,
            //        VBcSt = item.BcIcmsSt,
            //        PIcmsSt = item.AliquotaIcmsSt,
            //        VIcmsSt = item.ValorIcmsSt,
            //        VBcFcpSt = item.ValorFcp > 0 ? item.BcIcmsSt : 0,
            //        PFcpSt = item.AliquotaFcp,
            //        VFcpSt = item.ValorFcp,
            //        VIcmsDeson = item.ValorIcmsDesonerado,
            //        MotDesIcms = NFeMotivoDesoneracao.Outros
            //    };
            //    break;
            //case "90":
            //    icms = new Icms90
            //    {
            //        Origem = origem,
            //        ModBC = item.AliquotaIcms > 0 ? modalidadeBc : (NFeModalidadeBC?)null,
            //        VBc = item.BcIcms,
            //        PRedBc = item.ReducaoBcIcms,
            //        PIcms = item.AliquotaIcms,
            //        VIcms = item.ValorIcms,
            //        VBcFcp = item.ValorFcp > 0 ? item.BcIcms : 0,
            //        PFcp = item.AliquotaFcp,
            //        VFcp = item.ValorFcp,
            //        ModBcSt = item.AliquotaIcmsSt > 0 ? modalidadeBcSt : (NFeModalidadeBCST?)null,
            //        PMvaSt = item.MvaSt,
            //        PRedBcSt = item.ReducaoBcIcmsSt,
            //        VBcSt = item.BcIcmsSt,
            //        PIcmsSt = item.AliquotaIcmsSt,
            //        VIcmsSt = item.ValorIcmsSt,
            //        VBcFcpSt = item.ValorFcp > 0 ? item.BcIcmsSt : 0,
            //        PFcpSt = item.AliquotaFcp,
            //        VFcpSt = item.ValorFcp,
            //        VIcmsDeson = item.ValorIcmsDesonerado,
            //        MotDesIcms = NFeMotivoDesoneracao.Outros
            //    };
            //    break;
            //case "101":
            //    icms = new IcmsSn101
            //    {
            //        Origem = origem,
            //        PCredSn = item.AliquotaCreditoIcms,
            //        VCredIcmsSn = item.ValorCreditoIcms
            //    };
            //    break;
            //case "201":
            //    icms = new IcmsSn201
            //    {
            //        Origem = origem,
            //        ModBcSt = modalidadeBcSt,
            //        PMvaSt = item.MvaSt,
            //        PRedBcSt = item.ReducaoBcIcmsSt,
            //        VBcSt = item.BcIcmsSt,
            //        PIcmsSt = item.AliquotaIcmsSt,
            //        VIcmsSt = item.ValorIcmsSt,
            //        VBcFcpSt = item.ValorFcp > 0 ? item.BcIcmsSt : 0,
            //        PFcpSt = item.AliquotaFcp,
            //        VFcpSt = item.ValorFcp,
            //        PCredSn = item.AliquotaCreditoIcms,
            //        VCredIcmsSn = item.ValorCreditoIcms
            //    };
            //    break;
            //case "202":
            //case "203":
            //    icms = new IcmsSn202
            //    {
            //        Origem = origem,
            //        Csosn = item.CstIcms,
            //        ModBcSt = modalidadeBcSt,
            //        PMvaSt = item.MvaSt,
            //        PRedBcSt = item.ReducaoBcIcmsSt,
            //        VBcSt = item.BcIcmsSt,
            //        PIcmsSt = item.AliquotaIcmsSt,
            //        VIcmsSt = item.ValorIcmsSt,
            //        VBcFcpSt = item.ValorFcp > 0 ? item.BcIcmsSt : 0,
            //        PFcpSt = item.AliquotaFcp,
            //        VFcpSt = item.ValorFcp
            //    };
            //    break;
            //case "500":
            //    icms = new IcmsSn500
            //    {
            //        Origem = origem,
            //        VBcStRet = 0,
            //        PSt = 0,
            //        VIcmsSubstituto = 0,
            //        VIcmsStRet = 0
            //    };
            //    break;
            //case "900":
            //    icms = new IcmsSn900
            //    {
            //        Origem = origem,
            //        ModBc = item.AliquotaIcms > 0 ? modalidadeBc : (NFeModalidadeBC?)null,
            //        VBc = item.BcIcms,
            //        PRedBc = item.ReducaoBcIcms,
            //        PIcms = item.AliquotaIcms,
            //        VIcms = item.ValorIcms,
            //        ModBcSt = item.AliquotaIcmsSt > 0 ? modalidadeBcSt : (NFeModalidadeBCST?)null,
            //        PMvaSt = item.MvaSt,
            //        PRedBcSt = item.ReducaoBcIcmsSt,
            //        VBcSt = item.BcIcmsSt,
            //        PIcmsSt = item.AliquotaIcmsSt,
            //        VIcmsSt = item.ValorIcmsSt,
            //        VBcFcpSt = item.ValorFcp > 0 ? item.BcIcmsSt : 0,
            //        PFcpSt = item.AliquotaFcp,
            //        VFcpSt = item.ValorFcp,
            //        PCredSn = item.AliquotaCreditoIcms,
            //        VCredIcmsSn = item.ValorCreditoIcms
            //    };
            //    break;

            #endregion

            return new Icms {Tipo = icms};
        }

        private Pis GerarPis()
        {
            INFePis pis = new PisNt {Cst = "09"};
            return new Pis {Imposto = pis};
        }

        private Cofins GerarCofins()
        {
            INFeCofins cofins = new CofinsNt {Cst = "09"};
            return new Cofins {Imposto = cofins};
        }

        #endregion

        #region Métodos

        private void CarregarCampos()
        {
            cboVersao.Text = _configuracao.Versao.GetDescription();
            cboAmbiente.Text = _configuracao.Ambiente.GetDescription();
            cboTipoEmissao.Text = _configuracao.TipoEmissao.GetDescription();
            cboModelo.Text = _configuracao.Modelo.GetDescription();
            cboEstado.Text = _configuracao.Estado.GetDescription();
            cboFinalidadeEmissao.Text = _configuracao.FinalidadeEmissao.GetDescription();
            cboTipoMovimento.Text = _configuracao.TipoMovimento.GetDescription();
            cboDestinoOperacao.Text = _configuracao.DestinoOperacao.GetDescription();
            cboIndicadorPresenca.Text = _configuracao.IndicadorPresenca.GetDescription();
            cboEmitenteRegimeTributario.Text = _configuracao.EmitenteRegimeTributario.GetDescription();
            cboDestinatarioIndicadorInscricaoEstadual.Text = _configuracao.DestinatarioIndicadorInscricaoEstadual.GetDescription();
            cboTransporteModalidadeFrete.Text = _configuracao.TransporteModalidadeFrete.GetDescription();

            this.PreencherTextBox(_configuracao);
            this.PreencherCheckBox(_configuracao);
        }

        private void SalvarConfiguracao()
        {
            _configuracao.Versao = cboVersao.GetEnumValue<NFeVersao>();
            _configuracao.Ambiente = cboAmbiente.GetEnumValue<TipoAmbiente>();
            _configuracao.TipoEmissao = cboTipoEmissao.GetEnumValue<TipoEmissao>();
            _configuracao.Modelo = cboModelo.GetEnumValue<NFeModelo>();
            _configuracao.Estado = cboEstado.GetEnumValue<CodigoUF>();
            _configuracao.FinalidadeEmissao = cboFinalidadeEmissao.GetEnumValue<NFeFinalidade>();
            _configuracao.TipoMovimento = cboTipoMovimento.GetEnumValue<NFeTipo>();
            _configuracao.DestinoOperacao = cboDestinoOperacao.GetEnumValue<NFeDestinoOperacao>();

            _configuracao.IntervaloTentativas = txtIntervaloTentativas.Text.ToInt(1600);
            _configuracao.NumeroTentativas = txtNumeroTentativas.Text.ToInt(3);
            _configuracao.Timeout = txtTimeout.Text.ToInt(30);

            _configuracao.RemoverAcentos = ckbRemoverAcentos.Checked;
            _configuracao.RemoverEspacos = ckbRemoverEspacos.Checked;
            _configuracao.ExibirErroSchema = ckbExibirErroSchema.Checked;
            _configuracao.EnviarModoSincrono = ckbEnviarModoSincrono.Checked;

            _configuracao.CertificadoNumeroSerie = txtCertificadoNumeroSerie.Text;
            _configuracao.CertificadoSenha = txtCertificadoSenha.Text;
            _configuracao.CertificadoManterCache = ckbCertificadoManterCache.Checked;

            _configuracao.DiretorioArquivos = txtDiretorioArquivos.Text;
            _configuracao.DiretorioSchemas = txtDiretorioSchemas.Text;
            _configuracao.SalvarArquivos = ckbSalvarArquivos.Checked;

            _configuracao.NaturezaOperacao = txtNaturezaOperacao.Text;
            _configuracao.IndicadorPresenca = cboIndicadorPresenca.GetEnumValue<NFePresencaComprador>();
            _configuracao.Serie = txtSerie.Text.ToInt(1);
            _configuracao.UltimoNumeroNFe = txtUltimoNumeroNFe.Text.ToInt();
            _configuracao.UltimoNumeroNFCe = txtUltimoNumeroNFCe.Text.ToInt();
            _configuracao.JustificativaContingencia = txtJustificativaContingencia.Text;
            _configuracao.IdToken = txtIdToken.Text;
            _configuracao.CscToken = txtCscToken.Text;
            _configuracao.ConsumidorFinal = ckbConsumidorFinal.Checked;

            _configuracao.EmitenteCnpj = txtEmitenteCnpj.Text;
            _configuracao.EmitenteInscricaoEstadual = txtEmitenteInscricaoEstadual.Text;
            _configuracao.EmitenteRazaoSocial = txtEmitenteRazaoSocial.Text;
            _configuracao.EmitenteRegimeTributario = cboEmitenteRegimeTributario.GetEnumValue<RegimeTributario>();
            _configuracao.EmitenteLogradouro = txtEmitenteLogradouro.Text;
            _configuracao.EmitenteNumero = txtEmitenteNumero.Text;
            _configuracao.EmitenteBairro = txtEmitenteBairro.Text;
            _configuracao.EmitenteCep = txtEmitenteCep.Text;
            _configuracao.EmitenteCodigoIbge = txtEmitenteCodigoIbge.Text.ToInt();
            _configuracao.EmitenteCidade = txtEmitenteCidade.Text;
            _configuracao.EmitenteUf = txtEmitenteUf.Text;
            _configuracao.EmitenteTelefone = txtEmitenteTelefone.Text;

            _configuracao.DestinatarioCnpj = txtDestinatarioCnpj.Text;
            _configuracao.DestinatarioInscricaoEstadual = txtDestinatarioInscricaoEstadual.Text;
            _configuracao.DestinatarioRazaoSocial = txtDestinatarioRazaoSocial.Text;
            _configuracao.DestinatarioIndicadorInscricaoEstadual = cboDestinatarioIndicadorInscricaoEstadual.GetEnumValue<NFeIndIeDest>();
            _configuracao.DestinatarioLogradouro = txtDestinatarioLogradouro.Text;
            _configuracao.DestinatarioNumero = txtDestinatarioNumero.Text;
            _configuracao.DestinatarioBairro = txtDestinatarioBairro.Text;
            _configuracao.DestinatarioCep = txtDestinatarioCep.Text;
            _configuracao.DestinatarioCodigoIbge = txtDestinatarioCodigoIbge.Text.ToInt();
            _configuracao.DestinatarioCidade = txtDestinatarioCidade.Text;
            _configuracao.DestinatarioUf = txtDestinatarioUf.Text;
            _configuracao.DestinatarioTelefone = txtDestinatarioTelefone.Text;

            _configuracao.TransporteModalidadeFrete = cboTransporteModalidadeFrete.GetEnumValue<NFeModalidadeFrete>();
            _configuracao.TransporteCnpj = txtTransporteCnpj.Text;
            _configuracao.TransporteRazaoSocial = txtTransporteRazaoSocial.Text;
            _configuracao.TransporteEndereco = txtTransporteEndereco.Text;
            _configuracao.TransporteCidade = txtTransporteCidade.Text;
            _configuracao.TransporteUf = txtTransporteUf.Text;
            _configuracao.TransporteInformarPlacaVeiculo = ckbTransporteInformarPlacaVeiculo.Checked;

            _configuracao.VolumeEspecie = txtVolumeEspecie.Text;
            _configuracao.VolumeMarca = txtVolumeMarca.Text;
            _configuracao.VolumeNumeracao = txtVolumeNumeracao.Text;
            _configuracao.VolumeQuantidade = txtVolumeQuantidade.Text.ToInt();
            _configuracao.VolumePesoBruto = txtVolumePesoBruto.Text.ToDecimal();
            _configuracao.VolumePesoLiquido = txtVolumePesoLiquido.Text.ToDecimal();

            _serviceConfiguracao.Salvar(_configuracao);
        }

        private void AtualizarGrid()
        {
            dgItem.Refresh();

            txtRegistros.Text = _itens.Count.ToString();
            txtTotalItens.Text = _itens.Sum(x => x.TotalItem).ToString("C");
        }

        private string ObterCaminhoArquivo()
        {
            var caminhoArquivo = "";
            using (var dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.Filter = "Arquivos XML (*.xml)|*.xml";
                dialog.ShowDialog();
                caminhoArquivo = dialog.FileName;
            }

            return caminhoArquivo.IsNullOrEmpty() ? "" : caminhoArquivo;
        }

        private string ObterCNF()
        {
            var codigosInvalido = new[]
            {
                "0", "11111111", "22222222", "33333333", "44444444", "55555555", "66666666", "77777777", "88888888", "99999999", "12345678", "23456789", "34567890", "45678901", "56789012", "67890123", "78901234", "89012345", "90123456", "01234567"
            };

            var random = new Random();
            string result;
            do
            {
                result = random.Next(10000000, 99999999).ToString();
            } while (codigosInvalido.Contains(result));

            return result;
        }

        private int ObterProximoNumeroFiscal()
        {
            switch (_configuracao.Modelo)
            {
                case NFeModelo.NFe:
                    return _configuracao.UltimoNumeroNFe + 1;
                case NFeModelo.NFCe:
                    return _configuracao.UltimoNumeroNFCe + 1;
                default:
                    return 1;
            }
        }

        private void AtualizarListaMeioPagamento()
        {
            lstFormasPagamento.Refresh();
        }

        private NFeService ObterServico()
        {
            SalvarConfiguracao();

            var service = new NFeService();

            service.Configuracoes.CNPJ = _configuracao.EmitenteCnpj;
            service.Configuracoes.Ambiente = _configuracao.Ambiente;
            service.Configuracoes.TipoEmissao = _configuracao.TipoEmissao;
            service.Configuracoes.Modelo = _configuracao.Modelo;
            service.Configuracoes.Versao = _configuracao.Versao;

            service.Configuracoes.NFCeIdToken = _configuracao.IdToken;
            service.Configuracoes.NFCeCSC = _configuracao.CscToken;

            service.Configuracoes.RemoverAcentos = _configuracao.RemoverAcentos;
            service.Configuracoes.RemoverEspacos = _configuracao.RemoverEspacos;
            service.Configuracoes.ExibeErrosSchema = _configuracao.ExibirErroSchema;
            service.Configuracoes.EnviarModoSincrono = _configuracao.EnviarModoSincrono;

            service.Configuracoes.Certificado.Certificado = _configuracao.CertificadoNumeroSerie;
            service.Configuracoes.Certificado.Senha = _configuracao.CertificadoSenha;
            service.Configuracoes.Certificado.ManterEmCache = _configuracao.CertificadoManterCache;

            service.Configuracoes.Arquivos.Salvar = _configuracao.SalvarArquivos;
            service.Configuracoes.Arquivos.Diretorio = _configuracao.DiretorioArquivos;
            service.Configuracoes.Arquivos.DiretorioSchemas = _configuracao.DiretorioSchemas;

            service.Configuracoes.Webservices.UF = _configuracao.Estado;
            service.Configuracoes.Webservices.IntervaloTentativas = _configuracao.IntervaloTentativas;
            service.Configuracoes.Webservices.NumeroTentativas = _configuracao.NumeroTentativas;
            service.Configuracoes.Webservices.Timeout = new TimeSpan(0, 0, _configuracao.Timeout);
            service.Configuracoes.Webservices.Configurar();

            return service;
        }

        #endregion
    }
}