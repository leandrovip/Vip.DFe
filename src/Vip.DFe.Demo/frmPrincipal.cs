using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Vip.DFe.Danfe;
using Vip.DFe.Danfe.Modelo;
using Vip.DFe.NFe;
using Vip.DFe.NFe.Enum;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.Demo
{
    public partial class frmPrincipal : Form
    {
        #region Construtores

        public frmPrincipal()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos

        private void btnCertificado_Click(object sender, EventArgs e)
        {
            var certificado = CertificadoHelper.Obter();
            txtCertificado.Text = certificado == null ? "" : certificado.NumeroSerie;
        }

        private void btnAutorizacao_Click(object sender, EventArgs e)
        {
            var nfeService = ObterService();

            var nfe = GerarNFe(1048, "");
            nfeService.Documentos.Add(nfe);

            var teste = nfeService.AutorizacaoLote();
            txtRetorno.Text = teste.XmlRetorno;
        }

        private void btnConsultarServico_Click(object sender, EventArgs e)
        {
            try
            {
                var service = ObterService();
                var resultado = service.ConsultaSituacaoServico();
                txtRetorno.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.Motivo;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConsultarAutorizacao_Click(object sender, EventArgs e)
        {
            var service = ObterService();
            var teste = service.ConsultaAutorizacao(txtNumeroRecibo.Text);

            txtRetorno.Text = "";
            txtRetorno.Text = teste.XmlRetorno;
        }

        private void btnCarregarNFeConsultar_Click(object sender, EventArgs e)
        {
            var arquivo = ObterCaminhoArquivo();
            if (string.IsNullOrEmpty(arquivo))
            {
                MessageBox.Show("Arquivo xml não encontrado", "Carregar NFe e Consultar Recibo");
                return;
            }

            var service = ObterService();
            service.Documentos.Load(arquivo);

            var teste = service.ConsultaAutorizacao(txtNumeroRecibo.Text);

            txtRetorno.Text = "";
            txtRetorno.Text = teste.XmlRetorno;

            service.Dispose();
        }

        private void btnConsultarChave_Click(object sender, EventArgs e)
        {
            try
            {
                var service = ObterService();
                var resultado = service.Consultar(txtChaveAcesso.Text);
                txtRetorno.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.Motivo;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInutilizar_Click(object sender, EventArgs e)
        {
            var service = ObterService();
            var resultado = service.Inutilizar("12332134000199", "NOTA FISCAL ELETRONICA INUTILIZADA DE TESTE", NFeModelo.NFe, 1, 1002, 1002);
            txtRetorno.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.InfInut.XMotivo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var service = ObterService();
            var resultado = service.Cancelar("12332134000199", txtChaveAcesso.Text, txtProtocolo.Text, 1, "NOTA FISCAL ELETRONICA CANCELADA");
            txtRetorno.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.RetEvento.FirstOrDefault()?.InfEvento.xMotivo;
        }

        private void btnCartaCorrecao_Click(object sender, EventArgs e)
        {
            var service = ObterService();
            var resultado = service.CartaoCorrecao("12332134000199", txtChaveAcesso.Text, 1, "CARTA DE CORRECAO DE TESTE PARA DESENVOLVIMENTO");
            txtRetorno.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.RetEvento.FirstOrDefault()?.InfEvento.xMotivo;
        }

        private void btnGerarDanfe_Click(object sender, EventArgs e)
        {
            const string arquivoXml = @"D:\35241012332134000199550020000001271982279432-procNFe.xml";
            const string caminhoArquivoPdf = @"D:\danfeOk.pdf";

            var modelo = DanfeViewModel.CriarDeArquivoXml(arquivoXml);
            modelo.DefinirTextoCreditos("Emitido pelo software VipERP - www.vipsolucoes.com");
            modelo.PreferirEmitenteNomeFantasia = true;
            modelo.ExibirBlocoLocalEntrega = false;
            modelo.ExibirBlocoLocalRetirada = false;

            using (var danfe = new DanfeService(modelo))
            {
                //danfe.AdicionarLogoImagem(@"D:\vip.jpg");
                danfe.Gerar();
                danfe.Salvar(caminhoArquivoPdf);
            }

            Process.Start(new ProcessStartInfo{FileName = caminhoArquivoPdf, UseShellExecute = true, WorkingDirectory = "D:\\"});
            MessageBox.Show("Danfe Gerada", "Vip.DFe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGerarDanfeEvento_Click(object sender, EventArgs e)
        {
            const string arquivo = @"D:\35241012332134000199550020000001281395463217_110110_01-procEventoNFe.xml";

            var modelo = DanfeEventoViewModel.CriarDeArquivoXml(arquivo);
            modelo.DefinirTextoCreditos("Emitido pelo software VipERP - www.vipsolucoes.com");

            using (var danfe = new DanfeEventoService(modelo))
            {
                danfe.Gerar();
                danfe.Salvar(@"D:\danfeEventoOk.pdf");
            }

            MessageBox.Show("Danfe Gerada");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Métodos

        private string ObterCaminhoArquivo()
        {
            using var openDialog = new OpenFileDialog {CheckFileExists = true, Filter = "*.xml|(xml)"};
            openDialog.ShowDialog();
            return string.IsNullOrEmpty(openDialog.FileName) ? string.Empty : openDialog.FileName;
        }

        private NFeService ObterService()
        {
            return new NFeService
            {
                Configuracoes =
                {
                    Versao = NFeVersao.v400,
                    Ambiente = TipoAmbiente.Homologacao,
                    Certificado =
                    {
                        Certificado = txtCertificado.Text,
                        Senha = txtSenhaCertificado.Text,
                        ManterEmCache = true
                    },
                    RemoverAcentos = true,
                    RemoverEspacos = true
                }
            };
        }

        private NFe.NotaFiscal.NFe GerarNFe(int numero, string infoProduto)
        {
            var informacoes = new DadosNFe(TipoAmbiente.Homologacao, NFeModelo.NFe, NFeFinalidade.Normal, 1, numero);
            var nfe = new NFe.NotaFiscal.NFe
            {
                InfNFe =
                {
                    Versao = NFeVersao.v400,
                    Ide = informacoes.GetIdentificacao(),
                    Emitente = informacoes.GetEmitente(),
                    Destinatario = informacoes.GetDestinatario(),
                    Transporte = informacoes.GetTransporte()
                }
            };

            /*
             * APENAS NFCe !!!! :
             * 
             * se o infNFe não possuir destinatário,
             * o indFinal deverá ser modificado para
             * cfConsumidorFinal
             * */
            //if (infNFe.Destinatario == null)
            //    if (configServico.ModeloDocumento == ModeloDocumento.NFCe)
            //        infNFe.ide.indFinal = ConsumidorFinal.cfConsumidorFinal;

            //if (configServico.ModeloDocumento == ModeloDocumento.NFe)
            //    if (infNFe.ide.finNFe == FinalidadeNFe.fnDevolucao)
            //        infNFe.ide.NFref = informacoes.GetNotasReferenciadas();

            //nfe.InfNFe.Ide.NFref.AddRange(informacoes.GetNotasReferenciadas());

            nfe.InfNFe.Detalhe.AddRange(informacoes.GetDetalhesProdutosNF(infoProduto)); //itens da NF
            nfe.InfNFe.Pagamento = informacoes.GetPagamentos();               //informações de pagamento da NF
            nfe.InfNFe.Total = informacoes.GetTotal(nfe.InfNFe.Detalhe);

            if (nfe.InfNFe.Pagamento.DetPag[0].TPag == MeioPagamento.CreditoLoja)
                nfe.InfNFe.Cobranca = informacoes.GetFinanceiroNota(nfe.InfNFe.Total.IcmsTot.VNf, nfe.InfNFe.Total.IcmsTot.VDesc);

            nfe.InfNFe.InformacaoAdicional = informacoes.GetInformacoesComplementares();

            return nfe;
        }

        #endregion
    }
}