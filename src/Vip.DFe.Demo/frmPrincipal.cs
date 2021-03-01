using System;
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
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nfeService = ObterService();

            var nfe = GerarNFe(1039);

            nfeService.Documentos.Add(nfe);

            var teste = nfeService.AutorizacaoLote();
            txtDados.Text = teste.XmlRetorno;
        }

        private NFeService ObterService()
        {
            var service = new NFeService();
            service.Configuracoes.Versao = NFeVersao.v400;
            service.Configuracoes.Ambiente = TipoAmbiente.Homologacao;

            service.Configuracoes.Certificado.Certificado = txtCertificado.Text;
            service.Configuracoes.Certificado.Senha = txtSenhaCertificado.Text;
            service.Configuracoes.Certificado.ManterEmCache = true;

            return service;
        }

        private void btnCertificado_Click(object sender, EventArgs e)
        {
            var certificado = CertificadoHelper.Obter();
            txtCertificado.Text = certificado.NumeroSerie;
        }

        private NFe.NotaFiscal.NFe GerarNFe(int numero)
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

            nfe.InfNFe.Ide.NFref.AddRange(informacoes.GetNotasReferenciadas());

            nfe.InfNFe.Detalhe.AddRange(informacoes.GetDetalhesProdutosNF()); //itens da NF
            nfe.InfNFe.Pagamento = informacoes.GetPagamentos(); //informações de pagamento da NF
            nfe.InfNFe.Total = informacoes.GetTotal(nfe.InfNFe.Detalhe);

            if (nfe.InfNFe.Pagamento.DetPag[0].TPag == MeioPagamento.CreditoLoja)
                nfe.InfNFe.Cobranca = informacoes.GetFinanceiroNota(nfe.InfNFe.Total.IcmsTot.VNf, nfe.InfNFe.Total.IcmsTot.VDesc);

            nfe.InfNFe.InformacaoAdicional = informacoes.GetInformacoesComplementares();

            return nfe;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var service = ObterService();
            var teste = service.ConsultaAutorizacao(txtNumeroRecibo.Text);

            txtDados.Text = "";
            txtDados.Text = teste.XmlRetorno;
        }

        private void btnCarregarNFeConsultar_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.ShowDialog();

            if (string.IsNullOrEmpty(openDialog.FileName))
            {
                MessageBox.Show("Arquivo xml não encontrado", "Carregar NFe e Consultar Recibo");
                return;
            }

            var service = ObterService();
            service.Documentos.Load(openDialog.FileName);

            var teste = service.ConsultaAutorizacao("351000147333676");

            txtDados.Text = "";
            txtDados.Text = teste.XmlRetorno;

            service.Dispose();
        }

        private void btnConsultarStatusServico_Click(object sender, EventArgs e)
        {
            try
            {
                var service = ObterService();
                var resultado = service.ConsultaSituacaoServico();
                txtDados.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.Motivo;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConsultarChaveNFe_Click(object sender, EventArgs e)
        {
            try
            {
                var service = ObterService();
                var resultado = service.Consultar("35200912332134000199550010000010001144736691");
                txtDados.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.Motivo;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInutilizacao_Click(object sender, EventArgs e)
        {
            var service = ObterService();
            var resultado = service.Inutilizar("12332134000199", "NOTA FISCAL ELETRONICA INUTILIZADA DE TESTE", NFeModelo.NFe, 1, 1002, 1002);
            txtDados.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.InfInut.XMotivo;
        }

        private void btnCancelarNFe_Click(object sender, EventArgs e)
        {
            var service = ObterService();
            var resultado = service.Cancelar("12332134000199", "35201012332134000199550010000010041163999898", "135200008492987", 1, "NOTA FISCAL ELETRONICA CANCELADA");
            txtDados.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.RetEvento.FirstOrDefault()?.InfEvento.xMotivo;
        }

        private void btnCartaoDeCorrecao_Click(object sender, EventArgs e)
        {
            var service = ObterService();
            var resultado = service.CartaoCorrecao("12332134000199", "35201012332134000199550010000010051614415132", 1, "CARTA DE CORRECAO DE TESTE PARA DESENVOLVIMENTO");
            txtDados.Text = resultado.XmlRetorno + "\r\n\r\n" + resultado.Resultado.RetEvento.FirstOrDefault()?.InfEvento.xMotivo;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            const string arquivo = @"D:\35210238707682000140550010000010411660728153-procNFe.xml";

            var modelo = DanfeViewModel.CriarDeArquivoXml(arquivo);
            modelo.DefinirTextoCreditos("Emitido pelo software VipERP - www.vipsolucoes.com");
            modelo.PreferirEmitenteNomeFantasia = true;

            using (var danfe = new DanfeService(modelo))
            {
                danfe.AdicionarLogoImagem(@"D:\vip.jpg");
                danfe.Gerar();
                danfe.Salvar(@"D:\danfeOk.pdf");
            }

            MessageBox.Show("Danfe Gerada");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            const string arquivo = @"D:\35210138707682000140550010000010371442332758_110111_01-procEventoNFe.xml";
            //const string arquivo = @"D:\35150413358435000154550010000000331365222750_01-procEventoNFe.xml";

            var modelo = DanfeEventoViewModel.CriarDeArquivoXml(arquivo);
            modelo.DefinirTextoCreditos("Emitido pelo software VipERP - www.vipsolucoes.com");

            using (var danfe = new DanfeEventoService(modelo))
            {
                danfe.Gerar();
                danfe.Salvar(@"D:\danfeEventoOk.pdf");
            }

            MessageBox.Show("Danfe Gerada");
        }
    }
}