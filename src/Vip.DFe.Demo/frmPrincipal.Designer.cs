
namespace Vip.DFe.Demo
{
    partial class frmPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCertificado = new System.Windows.Forms.Button();
            this.txtCertificado = new System.Windows.Forms.TextBox();
            this.lblCertificado = new System.Windows.Forms.Label();
            this.txtSenhaCertificado = new System.Windows.Forms.TextBox();
            this.lblSenhaCertificado = new System.Windows.Forms.Label();
            this.btnAutorizacao = new System.Windows.Forms.Button();
            this.btnConsultarAutorizacao = new System.Windows.Forms.Button();
            this.txtNumeroRecibo = new System.Windows.Forms.TextBox();
            this.lblNumeroRecibo = new System.Windows.Forms.Label();
            this.btnConsultarChave = new System.Windows.Forms.Button();
            this.txtChaveAcesso = new System.Windows.Forms.TextBox();
            this.lblChaveAcesso = new System.Windows.Forms.Label();
            this.btnConsultarServico = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnInutilizar = new System.Windows.Forms.Button();
            this.btnCartaCorrecao = new System.Windows.Forms.Button();
            this.btnCarregarNFeConsultar = new System.Windows.Forms.Button();
            this.btnGerarDanfe = new System.Windows.Forms.Button();
            this.btnGerarDanfeEvento = new System.Windows.Forms.Button();
            this.txtRetorno = new System.Windows.Forms.TextBox();
            this.lblRetornos = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCertificado
            // 
            this.btnCertificado.Location = new System.Drawing.Point(348, 27);
            this.btnCertificado.Name = "btnCertificado";
            this.btnCertificado.Size = new System.Drawing.Size(135, 23);
            this.btnCertificado.TabIndex = 0;
            this.btnCertificado.Text = "Selecionar Certificado";
            this.btnCertificado.UseVisualStyleBackColor = true;
            this.btnCertificado.Click += new System.EventHandler(this.btnCertificado_Click);
            // 
            // txtCertificado
            // 
            this.txtCertificado.Location = new System.Drawing.Point(12, 27);
            this.txtCertificado.Name = "txtCertificado";
            this.txtCertificado.Size = new System.Drawing.Size(330, 23);
            this.txtCertificado.TabIndex = 1;
            this.txtCertificado.Text = "121A1809255AA983";
            this.txtCertificado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCertificado
            // 
            this.lblCertificado.AutoSize = true;
            this.lblCertificado.Location = new System.Drawing.Point(12, 9);
            this.lblCertificado.Name = "lblCertificado";
            this.lblCertificado.Size = new System.Drawing.Size(102, 15);
            this.lblCertificado.TabIndex = 2;
            this.lblCertificado.Text = "Certificado Digital";
            // 
            // txtSenhaCertificado
            // 
            this.txtSenhaCertificado.Location = new System.Drawing.Point(489, 27);
            this.txtSenhaCertificado.Name = "txtSenhaCertificado";
            this.txtSenhaCertificado.Size = new System.Drawing.Size(102, 23);
            this.txtSenhaCertificado.TabIndex = 1;
            this.txtSenhaCertificado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSenhaCertificado
            // 
            this.lblSenhaCertificado.AutoSize = true;
            this.lblSenhaCertificado.Location = new System.Drawing.Point(489, 9);
            this.lblSenhaCertificado.Name = "lblSenhaCertificado";
            this.lblSenhaCertificado.Size = new System.Drawing.Size(100, 15);
            this.lblSenhaCertificado.TabIndex = 2;
            this.lblSenhaCertificado.Text = "Senha Certificado";
            // 
            // btnAutorizacao
            // 
            this.btnAutorizacao.Location = new System.Drawing.Point(12, 104);
            this.btnAutorizacao.Name = "btnAutorizacao";
            this.btnAutorizacao.Size = new System.Drawing.Size(151, 23);
            this.btnAutorizacao.TabIndex = 0;
            this.btnAutorizacao.Text = "1 - Autorização";
            this.btnAutorizacao.UseVisualStyleBackColor = true;
            this.btnAutorizacao.Click += new System.EventHandler(this.btnAutorizacao_Click);
            // 
            // btnConsultarAutorizacao
            // 
            this.btnConsultarAutorizacao.Location = new System.Drawing.Point(12, 133);
            this.btnConsultarAutorizacao.Name = "btnConsultarAutorizacao";
            this.btnConsultarAutorizacao.Size = new System.Drawing.Size(151, 23);
            this.btnConsultarAutorizacao.TabIndex = 0;
            this.btnConsultarAutorizacao.Text = "2 - Consultar Autorização";
            this.btnConsultarAutorizacao.UseVisualStyleBackColor = true;
            this.btnConsultarAutorizacao.Click += new System.EventHandler(this.btnConsultarAutorizacao_Click);
            // 
            // txtNumeroRecibo
            // 
            this.txtNumeroRecibo.Location = new System.Drawing.Point(235, 133);
            this.txtNumeroRecibo.Name = "txtNumeroRecibo";
            this.txtNumeroRecibo.Size = new System.Drawing.Size(356, 23);
            this.txtNumeroRecibo.TabIndex = 1;
            this.txtNumeroRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNumeroRecibo
            // 
            this.lblNumeroRecibo.AutoSize = true;
            this.lblNumeroRecibo.Location = new System.Drawing.Point(169, 141);
            this.lblNumeroRecibo.Name = "lblNumeroRecibo";
            this.lblNumeroRecibo.Size = new System.Drawing.Size(63, 15);
            this.lblNumeroRecibo.TabIndex = 2;
            this.lblNumeroRecibo.Text = "Nº Recibo:";
            // 
            // btnConsultarChave
            // 
            this.btnConsultarChave.Location = new System.Drawing.Point(12, 162);
            this.btnConsultarChave.Name = "btnConsultarChave";
            this.btnConsultarChave.Size = new System.Drawing.Size(151, 23);
            this.btnConsultarChave.TabIndex = 0;
            this.btnConsultarChave.Text = "3 - Consultar por chave de acesso";
            this.btnConsultarChave.UseVisualStyleBackColor = true;
            this.btnConsultarChave.Click += new System.EventHandler(this.btnConsultarChave_Click);
            // 
            // txtChaveAcesso
            // 
            this.txtChaveAcesso.Location = new System.Drawing.Point(235, 163);
            this.txtChaveAcesso.Name = "txtChaveAcesso";
            this.txtChaveAcesso.Size = new System.Drawing.Size(356, 23);
            this.txtChaveAcesso.TabIndex = 1;
            this.txtChaveAcesso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblChaveAcesso
            // 
            this.lblChaveAcesso.AutoSize = true;
            this.lblChaveAcesso.Location = new System.Drawing.Point(164, 170);
            this.lblChaveAcesso.Name = "lblChaveAcesso";
            this.lblChaveAcesso.Size = new System.Drawing.Size(68, 15);
            this.lblChaveAcesso.TabIndex = 2;
            this.lblChaveAcesso.Text = "Ch. Acesso:";
            // 
            // btnConsultarServico
            // 
            this.btnConsultarServico.Location = new System.Drawing.Point(12, 191);
            this.btnConsultarServico.Name = "btnConsultarServico";
            this.btnConsultarServico.Size = new System.Drawing.Size(151, 23);
            this.btnConsultarServico.TabIndex = 0;
            this.btnConsultarServico.Text = "4 - Consultar  Serviço";
            this.btnConsultarServico.UseVisualStyleBackColor = true;
            this.btnConsultarServico.Click += new System.EventHandler(this.btnConsultarServico_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(12, 220);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(151, 23);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "5 - Cancelar NFe";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnInutilizar
            // 
            this.btnInutilizar.Location = new System.Drawing.Point(12, 249);
            this.btnInutilizar.Name = "btnInutilizar";
            this.btnInutilizar.Size = new System.Drawing.Size(151, 23);
            this.btnInutilizar.TabIndex = 0;
            this.btnInutilizar.Text = "6 - Inutilizar NFe";
            this.btnInutilizar.UseVisualStyleBackColor = true;
            this.btnInutilizar.Click += new System.EventHandler(this.btnInutilizar_Click);
            // 
            // btnCartaCorrecao
            // 
            this.btnCartaCorrecao.Location = new System.Drawing.Point(12, 278);
            this.btnCartaCorrecao.Name = "btnCartaCorrecao";
            this.btnCartaCorrecao.Size = new System.Drawing.Size(151, 23);
            this.btnCartaCorrecao.TabIndex = 0;
            this.btnCartaCorrecao.Text = "7 - Carta de Correção";
            this.btnCartaCorrecao.UseVisualStyleBackColor = true;
            this.btnCartaCorrecao.Click += new System.EventHandler(this.btnCartaCorrecao_Click);
            // 
            // btnCarregarNFeConsultar
            // 
            this.btnCarregarNFeConsultar.Location = new System.Drawing.Point(12, 307);
            this.btnCarregarNFeConsultar.Name = "btnCarregarNFeConsultar";
            this.btnCarregarNFeConsultar.Size = new System.Drawing.Size(151, 23);
            this.btnCarregarNFeConsultar.TabIndex = 3;
            this.btnCarregarNFeConsultar.Text = "Carregar NFe e Consultar";
            this.btnCarregarNFeConsultar.UseVisualStyleBackColor = true;
            this.btnCarregarNFeConsultar.Click += new System.EventHandler(this.btnCarregarNFeConsultar_Click);
            // 
            // btnGerarDanfe
            // 
            this.btnGerarDanfe.Location = new System.Drawing.Point(12, 336);
            this.btnGerarDanfe.Name = "btnGerarDanfe";
            this.btnGerarDanfe.Size = new System.Drawing.Size(151, 23);
            this.btnGerarDanfe.TabIndex = 3;
            this.btnGerarDanfe.Text = "Gerar DANFE";
            this.btnGerarDanfe.UseVisualStyleBackColor = true;
            this.btnGerarDanfe.Click += new System.EventHandler(this.btnGerarDanfe_Click);
            // 
            // btnGerarDanfeEvento
            // 
            this.btnGerarDanfeEvento.Location = new System.Drawing.Point(12, 365);
            this.btnGerarDanfeEvento.Name = "btnGerarDanfeEvento";
            this.btnGerarDanfeEvento.Size = new System.Drawing.Size(151, 23);
            this.btnGerarDanfeEvento.TabIndex = 3;
            this.btnGerarDanfeEvento.Text = "Gerar DANFE Evento";
            this.btnGerarDanfeEvento.UseVisualStyleBackColor = true;
            this.btnGerarDanfeEvento.Click += new System.EventHandler(this.btnGerarDanfeEvento_Click);
            // 
            // txtRetorno
            // 
            this.txtRetorno.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRetorno.Location = new System.Drawing.Point(235, 192);
            this.txtRetorno.Multiline = true;
            this.txtRetorno.Name = "txtRetorno";
            this.txtRetorno.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRetorno.Size = new System.Drawing.Size(575, 265);
            this.txtRetorno.TabIndex = 1;
            // 
            // lblRetornos
            // 
            this.lblRetornos.AutoSize = true;
            this.lblRetornos.Location = new System.Drawing.Point(172, 278);
            this.lblRetornos.Name = "lblRetornos";
            this.lblRetornos.Size = new System.Drawing.Size(57, 15);
            this.lblRetornos.TabIndex = 2;
            this.lblRetornos.Text = "Retornos:";
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(12, 434);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(151, 23);
            this.btnSair.TabIndex = 0;
            this.btnSair.Text = "SAIR";
            this.btnSair.UseVisualStyleBackColor = true;
            // 
            // frmPrincipal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(822, 465);
            this.Controls.Add(this.btnGerarDanfeEvento);
            this.Controls.Add(this.btnGerarDanfe);
            this.Controls.Add(this.btnCarregarNFeConsultar);
            this.Controls.Add(this.lblRetornos);
            this.Controls.Add(this.lblChaveAcesso);
            this.Controls.Add(this.lblNumeroRecibo);
            this.Controls.Add(this.lblSenhaCertificado);
            this.Controls.Add(this.lblCertificado);
            this.Controls.Add(this.txtSenhaCertificado);
            this.Controls.Add(this.txtRetorno);
            this.Controls.Add(this.txtChaveAcesso);
            this.Controls.Add(this.txtNumeroRecibo);
            this.Controls.Add(this.txtCertificado);
            this.Controls.Add(this.btnCartaCorrecao);
            this.Controls.Add(this.btnInutilizar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConsultarServico);
            this.Controls.Add(this.btnConsultarChave);
            this.Controls.Add(this.btnConsultarAutorizacao);
            this.Controls.Add(this.btnAutorizacao);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnCertificado);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmPrincipal";
            this.Text = "Vip.DFe - Demo - .Net 5.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCertificado;
        private System.Windows.Forms.TextBox txtCertificado;
        private System.Windows.Forms.Label lblCertificado;
        private System.Windows.Forms.TextBox txtSenhaCertificado;
        private System.Windows.Forms.Label lblSenhaCertificado;
        private System.Windows.Forms.Button btnAutorizacao;
        private System.Windows.Forms.Button btnConsultarAutorizacao;
        private System.Windows.Forms.TextBox txtNumeroRecibo;
        private System.Windows.Forms.Label lblNumeroRecibo;
        private System.Windows.Forms.Button btnConsultarChave;
        private System.Windows.Forms.TextBox txtChaveAcesso;
        private System.Windows.Forms.Label lblChaveAcesso;
        private System.Windows.Forms.Button btnConsultarServico;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnInutilizar;
        private System.Windows.Forms.Button btnCartaCorrecao;
        private System.Windows.Forms.Button btnCarregarNFeConsultar;
        private System.Windows.Forms.Button btnGerarDanfe;
        private System.Windows.Forms.Button btnGerarDanfeEvento;
        private System.Windows.Forms.TextBox txtRetorno;
        private System.Windows.Forms.Label lblRetornos;
        private System.Windows.Forms.Button btnSair;
    }
}

