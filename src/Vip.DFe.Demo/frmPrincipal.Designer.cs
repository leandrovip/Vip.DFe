
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
            btnCertificado = new System.Windows.Forms.Button();
            txtCertificado = new System.Windows.Forms.TextBox();
            lblCertificado = new System.Windows.Forms.Label();
            txtSenhaCertificado = new System.Windows.Forms.TextBox();
            lblSenhaCertificado = new System.Windows.Forms.Label();
            btnAutorizacao = new System.Windows.Forms.Button();
            btnConsultarAutorizacao = new System.Windows.Forms.Button();
            txtNumeroRecibo = new System.Windows.Forms.TextBox();
            lblNumeroRecibo = new System.Windows.Forms.Label();
            btnConsultarChave = new System.Windows.Forms.Button();
            txtChaveAcesso = new System.Windows.Forms.TextBox();
            lblChaveAcesso = new System.Windows.Forms.Label();
            btnConsultarServico = new System.Windows.Forms.Button();
            btnCancelar = new System.Windows.Forms.Button();
            btnInutilizar = new System.Windows.Forms.Button();
            btnCartaCorrecao = new System.Windows.Forms.Button();
            btnCarregarNFeConsultar = new System.Windows.Forms.Button();
            btnGerarDanfe = new System.Windows.Forms.Button();
            btnGerarDanfeEvento = new System.Windows.Forms.Button();
            txtRetorno = new System.Windows.Forms.TextBox();
            lblRetornos = new System.Windows.Forms.Label();
            btnSair = new System.Windows.Forms.Button();
            txtProtocolo = new System.Windows.Forms.TextBox();
            lblProtocolo = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // btnCertificado
            // 
            btnCertificado.Location = new System.Drawing.Point(348, 27);
            btnCertificado.Name = "btnCertificado";
            btnCertificado.Size = new System.Drawing.Size(135, 31);
            btnCertificado.TabIndex = 0;
            btnCertificado.Text = "Selecionar Certificado";
            btnCertificado.UseVisualStyleBackColor = true;
            btnCertificado.Click += btnCertificado_Click;
            // 
            // txtCertificado
            // 
            txtCertificado.Location = new System.Drawing.Point(8, 31);
            txtCertificado.Name = "txtCertificado";
            txtCertificado.Size = new System.Drawing.Size(330, 23);
            txtCertificado.TabIndex = 1;
            txtCertificado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCertificado
            // 
            lblCertificado.AutoSize = true;
            lblCertificado.Location = new System.Drawing.Point(8, 11);
            lblCertificado.Name = "lblCertificado";
            lblCertificado.Size = new System.Drawing.Size(270, 15);
            lblCertificado.TabIndex = 2;
            lblCertificado.Text = "Certificado Digital - Caminho ou Número de Série";
            // 
            // txtSenhaCertificado
            // 
            txtSenhaCertificado.Location = new System.Drawing.Point(489, 31);
            txtSenhaCertificado.Name = "txtSenhaCertificado";
            txtSenhaCertificado.Size = new System.Drawing.Size(126, 23);
            txtSenhaCertificado.TabIndex = 1;
            txtSenhaCertificado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSenhaCertificado
            // 
            lblSenhaCertificado.AutoSize = true;
            lblSenhaCertificado.Location = new System.Drawing.Point(489, 9);
            lblSenhaCertificado.Name = "lblSenhaCertificado";
            lblSenhaCertificado.Size = new System.Drawing.Size(100, 15);
            lblSenhaCertificado.TabIndex = 2;
            lblSenhaCertificado.Text = "Senha Certificado";
            // 
            // btnAutorizacao
            // 
            btnAutorizacao.Location = new System.Drawing.Point(12, 95);
            btnAutorizacao.Name = "btnAutorizacao";
            btnAutorizacao.Size = new System.Drawing.Size(326, 39);
            btnAutorizacao.TabIndex = 0;
            btnAutorizacao.Text = "1 - Autorização";
            btnAutorizacao.UseVisualStyleBackColor = true;
            btnAutorizacao.Click += btnAutorizacao_Click;
            // 
            // btnConsultarAutorizacao
            // 
            btnConsultarAutorizacao.Location = new System.Drawing.Point(12, 140);
            btnConsultarAutorizacao.Name = "btnConsultarAutorizacao";
            btnConsultarAutorizacao.Size = new System.Drawing.Size(326, 39);
            btnConsultarAutorizacao.TabIndex = 0;
            btnConsultarAutorizacao.Text = "2 - Consultar Autorização";
            btnConsultarAutorizacao.UseVisualStyleBackColor = true;
            btnConsultarAutorizacao.Click += btnConsultarAutorizacao_Click;
            // 
            // txtNumeroRecibo
            // 
            txtNumeroRecibo.Location = new System.Drawing.Point(436, 97);
            txtNumeroRecibo.Name = "txtNumeroRecibo";
            txtNumeroRecibo.Size = new System.Drawing.Size(275, 23);
            txtNumeroRecibo.TabIndex = 1;
            txtNumeroRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNumeroRecibo
            // 
            lblNumeroRecibo.AutoSize = true;
            lblNumeroRecibo.Location = new System.Drawing.Point(351, 104);
            lblNumeroRecibo.Name = "lblNumeroRecibo";
            lblNumeroRecibo.Size = new System.Drawing.Size(63, 15);
            lblNumeroRecibo.TabIndex = 2;
            lblNumeroRecibo.Text = "Nº Recibo:";
            // 
            // btnConsultarChave
            // 
            btnConsultarChave.Location = new System.Drawing.Point(12, 185);
            btnConsultarChave.Name = "btnConsultarChave";
            btnConsultarChave.Size = new System.Drawing.Size(326, 39);
            btnConsultarChave.TabIndex = 0;
            btnConsultarChave.Text = "3 - Consultar por chave de acesso";
            btnConsultarChave.UseVisualStyleBackColor = true;
            btnConsultarChave.Click += btnConsultarChave_Click;
            // 
            // txtChaveAcesso
            // 
            txtChaveAcesso.Location = new System.Drawing.Point(436, 142);
            txtChaveAcesso.Name = "txtChaveAcesso";
            txtChaveAcesso.Size = new System.Drawing.Size(699, 23);
            txtChaveAcesso.TabIndex = 1;
            txtChaveAcesso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblChaveAcesso
            // 
            lblChaveAcesso.AutoSize = true;
            lblChaveAcesso.Location = new System.Drawing.Point(348, 149);
            lblChaveAcesso.Name = "lblChaveAcesso";
            lblChaveAcesso.Size = new System.Drawing.Size(68, 15);
            lblChaveAcesso.TabIndex = 2;
            lblChaveAcesso.Text = "Ch. Acesso:";
            // 
            // btnConsultarServico
            // 
            btnConsultarServico.Location = new System.Drawing.Point(12, 230);
            btnConsultarServico.Name = "btnConsultarServico";
            btnConsultarServico.Size = new System.Drawing.Size(326, 39);
            btnConsultarServico.TabIndex = 0;
            btnConsultarServico.Text = "4 - Consultar  Serviço";
            btnConsultarServico.UseVisualStyleBackColor = true;
            btnConsultarServico.Click += btnConsultarServico_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new System.Drawing.Point(12, 275);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size(326, 39);
            btnCancelar.TabIndex = 0;
            btnCancelar.Text = "5 - Cancelar NFe";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnInutilizar
            // 
            btnInutilizar.Location = new System.Drawing.Point(12, 320);
            btnInutilizar.Name = "btnInutilizar";
            btnInutilizar.Size = new System.Drawing.Size(326, 39);
            btnInutilizar.TabIndex = 0;
            btnInutilizar.Text = "6 - Inutilizar NFe";
            btnInutilizar.UseVisualStyleBackColor = true;
            btnInutilizar.Click += btnInutilizar_Click;
            // 
            // btnCartaCorrecao
            // 
            btnCartaCorrecao.Location = new System.Drawing.Point(12, 365);
            btnCartaCorrecao.Name = "btnCartaCorrecao";
            btnCartaCorrecao.Size = new System.Drawing.Size(326, 39);
            btnCartaCorrecao.TabIndex = 0;
            btnCartaCorrecao.Text = "7 - Carta de Correção";
            btnCartaCorrecao.UseVisualStyleBackColor = true;
            btnCartaCorrecao.Click += btnCartaCorrecao_Click;
            // 
            // btnCarregarNFeConsultar
            // 
            btnCarregarNFeConsultar.Location = new System.Drawing.Point(12, 410);
            btnCarregarNFeConsultar.Name = "btnCarregarNFeConsultar";
            btnCarregarNFeConsultar.Size = new System.Drawing.Size(326, 39);
            btnCarregarNFeConsultar.TabIndex = 3;
            btnCarregarNFeConsultar.Text = "Carregar NFe e Consultar";
            btnCarregarNFeConsultar.UseVisualStyleBackColor = true;
            btnCarregarNFeConsultar.Click += btnCarregarNFeConsultar_Click;
            // 
            // btnGerarDanfe
            // 
            btnGerarDanfe.Location = new System.Drawing.Point(12, 455);
            btnGerarDanfe.Name = "btnGerarDanfe";
            btnGerarDanfe.Size = new System.Drawing.Size(326, 39);
            btnGerarDanfe.TabIndex = 3;
            btnGerarDanfe.Text = "Gerar DANFE";
            btnGerarDanfe.UseVisualStyleBackColor = true;
            btnGerarDanfe.Click += btnGerarDanfe_Click;
            // 
            // btnGerarDanfeEvento
            // 
            btnGerarDanfeEvento.Location = new System.Drawing.Point(12, 500);
            btnGerarDanfeEvento.Name = "btnGerarDanfeEvento";
            btnGerarDanfeEvento.Size = new System.Drawing.Size(326, 39);
            btnGerarDanfeEvento.TabIndex = 3;
            btnGerarDanfeEvento.Text = "Gerar DANFE Evento";
            btnGerarDanfeEvento.UseVisualStyleBackColor = true;
            btnGerarDanfeEvento.Click += btnGerarDanfeEvento_Click;
            // 
            // txtRetorno
            // 
            txtRetorno.Location = new System.Drawing.Point(348, 215);
            txtRetorno.Multiline = true;
            txtRetorno.Name = "txtRetorno";
            txtRetorno.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtRetorno.Size = new System.Drawing.Size(787, 369);
            txtRetorno.TabIndex = 1;
            // 
            // lblRetornos
            // 
            lblRetornos.AutoSize = true;
            lblRetornos.Location = new System.Drawing.Point(348, 185);
            lblRetornos.Name = "lblRetornos";
            lblRetornos.Size = new System.Drawing.Size(57, 15);
            lblRetornos.TabIndex = 2;
            lblRetornos.Text = "Retornos:";
            // 
            // btnSair
            // 
            btnSair.Location = new System.Drawing.Point(12, 545);
            btnSair.Name = "btnSair";
            btnSair.Size = new System.Drawing.Size(326, 39);
            btnSair.TabIndex = 0;
            btnSair.Text = "SAIR";
            btnSair.UseVisualStyleBackColor = true;
            btnSair.Click += btnSair_Click;
            // 
            // txtProtocolo
            // 
            txtProtocolo.Location = new System.Drawing.Point(832, 97);
            txtProtocolo.Name = "txtProtocolo";
            txtProtocolo.Size = new System.Drawing.Size(302, 23);
            txtProtocolo.TabIndex = 1;
            txtProtocolo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblProtocolo
            // 
            lblProtocolo.AutoSize = true;
            lblProtocolo.Location = new System.Drawing.Point(728, 104);
            lblProtocolo.Name = "lblProtocolo";
            lblProtocolo.Size = new System.Drawing.Size(79, 15);
            lblProtocolo.TabIndex = 2;
            lblProtocolo.Text = "Nº Protocolo:";
            // 
            // frmPrincipal
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1139, 589);
            Controls.Add(btnGerarDanfeEvento);
            Controls.Add(btnGerarDanfe);
            Controls.Add(btnCarregarNFeConsultar);
            Controls.Add(lblRetornos);
            Controls.Add(lblChaveAcesso);
            Controls.Add(lblProtocolo);
            Controls.Add(lblNumeroRecibo);
            Controls.Add(lblSenhaCertificado);
            Controls.Add(lblCertificado);
            Controls.Add(txtSenhaCertificado);
            Controls.Add(txtRetorno);
            Controls.Add(txtChaveAcesso);
            Controls.Add(txtProtocolo);
            Controls.Add(txtNumeroRecibo);
            Controls.Add(txtCertificado);
            Controls.Add(btnCartaCorrecao);
            Controls.Add(btnInutilizar);
            Controls.Add(btnCancelar);
            Controls.Add(btnConsultarServico);
            Controls.Add(btnConsultarChave);
            Controls.Add(btnConsultarAutorizacao);
            Controls.Add(btnAutorizacao);
            Controls.Add(btnSair);
            Controls.Add(btnCertificado);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "frmPrincipal";
            Text = "Vip.DFe - Demo - .Net 6.0";
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.TextBox txtProtocolo;
        private System.Windows.Forms.Label lblProtocolo;
    }
}

