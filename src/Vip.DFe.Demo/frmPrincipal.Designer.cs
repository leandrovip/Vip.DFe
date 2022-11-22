
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
            this.txtProtocolo = new System.Windows.Forms.TextBox();
            this.lblProtocolo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCertificado
            // 
            this.btnCertificado.Location = new System.Drawing.Point(348, 27);
            this.btnCertificado.Name = "btnCertificado";
            this.btnCertificado.Size = new System.Drawing.Size(135, 31);
            this.btnCertificado.TabIndex = 0;
            this.btnCertificado.Text = "Selecionar Certificado";
            this.btnCertificado.UseVisualStyleBackColor = true;
            this.btnCertificado.Click += new System.EventHandler(this.btnCertificado_Click);
            // 
            // txtCertificado
            // 
            this.txtCertificado.Location = new System.Drawing.Point(8, 31);
            this.txtCertificado.Name = "txtCertificado";
            this.txtCertificado.Size = new System.Drawing.Size(330, 27);
            this.txtCertificado.TabIndex = 1;
            this.txtCertificado.Text = "13FFC0E2C3EC83ABBE2C7E2F75C2504D";
            this.txtCertificado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCertificado
            // 
            this.lblCertificado.AutoSize = true;
            this.lblCertificado.Location = new System.Drawing.Point(8, 11);
            this.lblCertificado.Name = "lblCertificado";
            this.lblCertificado.Size = new System.Drawing.Size(131, 20);
            this.lblCertificado.TabIndex = 2;
            this.lblCertificado.Text = "Certificado Digital";
            // 
            // txtSenhaCertificado
            // 
            this.txtSenhaCertificado.Location = new System.Drawing.Point(489, 31);
            this.txtSenhaCertificado.Name = "txtSenhaCertificado";
            this.txtSenhaCertificado.Size = new System.Drawing.Size(126, 27);
            this.txtSenhaCertificado.TabIndex = 1;
            this.txtSenhaCertificado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSenhaCertificado
            // 
            this.lblSenhaCertificado.AutoSize = true;
            this.lblSenhaCertificado.Location = new System.Drawing.Point(489, 9);
            this.lblSenhaCertificado.Name = "lblSenhaCertificado";
            this.lblSenhaCertificado.Size = new System.Drawing.Size(126, 20);
            this.lblSenhaCertificado.TabIndex = 2;
            this.lblSenhaCertificado.Text = "Senha Certificado";
            // 
            // btnAutorizacao
            // 
            this.btnAutorizacao.Location = new System.Drawing.Point(12, 95);
            this.btnAutorizacao.Name = "btnAutorizacao";
            this.btnAutorizacao.Size = new System.Drawing.Size(326, 39);
            this.btnAutorizacao.TabIndex = 0;
            this.btnAutorizacao.Text = "1 - Autorização";
            this.btnAutorizacao.UseVisualStyleBackColor = true;
            this.btnAutorizacao.Click += new System.EventHandler(this.btnAutorizacao_Click);
            // 
            // btnConsultarAutorizacao
            // 
            this.btnConsultarAutorizacao.Location = new System.Drawing.Point(12, 140);
            this.btnConsultarAutorizacao.Name = "btnConsultarAutorizacao";
            this.btnConsultarAutorizacao.Size = new System.Drawing.Size(326, 39);
            this.btnConsultarAutorizacao.TabIndex = 0;
            this.btnConsultarAutorizacao.Text = "2 - Consultar Autorização";
            this.btnConsultarAutorizacao.UseVisualStyleBackColor = true;
            this.btnConsultarAutorizacao.Click += new System.EventHandler(this.btnConsultarAutorizacao_Click);
            // 
            // txtNumeroRecibo
            // 
            this.txtNumeroRecibo.Location = new System.Drawing.Point(436, 97);
            this.txtNumeroRecibo.Name = "txtNumeroRecibo";
            this.txtNumeroRecibo.Size = new System.Drawing.Size(275, 27);
            this.txtNumeroRecibo.TabIndex = 1;
            this.txtNumeroRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNumeroRecibo
            // 
            this.lblNumeroRecibo.AutoSize = true;
            this.lblNumeroRecibo.Location = new System.Drawing.Point(351, 104);
            this.lblNumeroRecibo.Name = "lblNumeroRecibo";
            this.lblNumeroRecibo.Size = new System.Drawing.Size(79, 20);
            this.lblNumeroRecibo.TabIndex = 2;
            this.lblNumeroRecibo.Text = "Nº Recibo:";
            // 
            // btnConsultarChave
            // 
            this.btnConsultarChave.Location = new System.Drawing.Point(12, 185);
            this.btnConsultarChave.Name = "btnConsultarChave";
            this.btnConsultarChave.Size = new System.Drawing.Size(326, 39);
            this.btnConsultarChave.TabIndex = 0;
            this.btnConsultarChave.Text = "3 - Consultar por chave de acesso";
            this.btnConsultarChave.UseVisualStyleBackColor = true;
            this.btnConsultarChave.Click += new System.EventHandler(this.btnConsultarChave_Click);
            // 
            // txtChaveAcesso
            // 
            this.txtChaveAcesso.Location = new System.Drawing.Point(436, 142);
            this.txtChaveAcesso.Name = "txtChaveAcesso";
            this.txtChaveAcesso.Size = new System.Drawing.Size(699, 27);
            this.txtChaveAcesso.TabIndex = 1;
            this.txtChaveAcesso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblChaveAcesso
            // 
            this.lblChaveAcesso.AutoSize = true;
            this.lblChaveAcesso.Location = new System.Drawing.Point(348, 149);
            this.lblChaveAcesso.Name = "lblChaveAcesso";
            this.lblChaveAcesso.Size = new System.Drawing.Size(82, 20);
            this.lblChaveAcesso.TabIndex = 2;
            this.lblChaveAcesso.Text = "Ch. Acesso:";
            // 
            // btnConsultarServico
            // 
            this.btnConsultarServico.Location = new System.Drawing.Point(12, 230);
            this.btnConsultarServico.Name = "btnConsultarServico";
            this.btnConsultarServico.Size = new System.Drawing.Size(326, 39);
            this.btnConsultarServico.TabIndex = 0;
            this.btnConsultarServico.Text = "4 - Consultar  Serviço";
            this.btnConsultarServico.UseVisualStyleBackColor = true;
            this.btnConsultarServico.Click += new System.EventHandler(this.btnConsultarServico_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(12, 275);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(326, 39);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "5 - Cancelar NFe";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnInutilizar
            // 
            this.btnInutilizar.Location = new System.Drawing.Point(12, 320);
            this.btnInutilizar.Name = "btnInutilizar";
            this.btnInutilizar.Size = new System.Drawing.Size(326, 39);
            this.btnInutilizar.TabIndex = 0;
            this.btnInutilizar.Text = "6 - Inutilizar NFe";
            this.btnInutilizar.UseVisualStyleBackColor = true;
            this.btnInutilizar.Click += new System.EventHandler(this.btnInutilizar_Click);
            // 
            // btnCartaCorrecao
            // 
            this.btnCartaCorrecao.Location = new System.Drawing.Point(12, 365);
            this.btnCartaCorrecao.Name = "btnCartaCorrecao";
            this.btnCartaCorrecao.Size = new System.Drawing.Size(326, 39);
            this.btnCartaCorrecao.TabIndex = 0;
            this.btnCartaCorrecao.Text = "7 - Carta de Correção";
            this.btnCartaCorrecao.UseVisualStyleBackColor = true;
            this.btnCartaCorrecao.Click += new System.EventHandler(this.btnCartaCorrecao_Click);
            // 
            // btnCarregarNFeConsultar
            // 
            this.btnCarregarNFeConsultar.Location = new System.Drawing.Point(12, 410);
            this.btnCarregarNFeConsultar.Name = "btnCarregarNFeConsultar";
            this.btnCarregarNFeConsultar.Size = new System.Drawing.Size(326, 39);
            this.btnCarregarNFeConsultar.TabIndex = 3;
            this.btnCarregarNFeConsultar.Text = "Carregar NFe e Consultar";
            this.btnCarregarNFeConsultar.UseVisualStyleBackColor = true;
            this.btnCarregarNFeConsultar.Click += new System.EventHandler(this.btnCarregarNFeConsultar_Click);
            // 
            // btnGerarDanfe
            // 
            this.btnGerarDanfe.Location = new System.Drawing.Point(12, 455);
            this.btnGerarDanfe.Name = "btnGerarDanfe";
            this.btnGerarDanfe.Size = new System.Drawing.Size(326, 39);
            this.btnGerarDanfe.TabIndex = 3;
            this.btnGerarDanfe.Text = "Gerar DANFE";
            this.btnGerarDanfe.UseVisualStyleBackColor = true;
            this.btnGerarDanfe.Click += new System.EventHandler(this.btnGerarDanfe_Click);
            // 
            // btnGerarDanfeEvento
            // 
            this.btnGerarDanfeEvento.Location = new System.Drawing.Point(12, 500);
            this.btnGerarDanfeEvento.Name = "btnGerarDanfeEvento";
            this.btnGerarDanfeEvento.Size = new System.Drawing.Size(326, 39);
            this.btnGerarDanfeEvento.TabIndex = 3;
            this.btnGerarDanfeEvento.Text = "Gerar DANFE Evento";
            this.btnGerarDanfeEvento.UseVisualStyleBackColor = true;
            this.btnGerarDanfeEvento.Click += new System.EventHandler(this.btnGerarDanfeEvento_Click);
            // 
            // txtRetorno
            // 
            this.txtRetorno.Location = new System.Drawing.Point(348, 215);
            this.txtRetorno.Multiline = true;
            this.txtRetorno.Name = "txtRetorno";
            this.txtRetorno.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRetorno.Size = new System.Drawing.Size(787, 369);
            this.txtRetorno.TabIndex = 1;
            // 
            // lblRetornos
            // 
            this.lblRetornos.AutoSize = true;
            this.lblRetornos.Location = new System.Drawing.Point(348, 185);
            this.lblRetornos.Name = "lblRetornos";
            this.lblRetornos.Size = new System.Drawing.Size(71, 20);
            this.lblRetornos.TabIndex = 2;
            this.lblRetornos.Text = "Retornos:";
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(12, 545);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(326, 39);
            this.btnSair.TabIndex = 0;
            this.btnSair.Text = "SAIR";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtProtocolo
            // 
            this.txtProtocolo.Location = new System.Drawing.Point(832, 97);
            this.txtProtocolo.Name = "txtProtocolo";
            this.txtProtocolo.Size = new System.Drawing.Size(302, 27);
            this.txtProtocolo.TabIndex = 1;
            this.txtProtocolo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblProtocolo
            // 
            this.lblProtocolo.AutoSize = true;
            this.lblProtocolo.Location = new System.Drawing.Point(728, 104);
            this.lblProtocolo.Name = "lblProtocolo";
            this.lblProtocolo.Size = new System.Drawing.Size(98, 20);
            this.lblProtocolo.TabIndex = 2;
            this.lblProtocolo.Text = "Nº Protocolo:";
            // 
            // frmPrincipal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1147, 594);
            this.Controls.Add(this.btnGerarDanfeEvento);
            this.Controls.Add(this.btnGerarDanfe);
            this.Controls.Add(this.btnCarregarNFeConsultar);
            this.Controls.Add(this.lblRetornos);
            this.Controls.Add(this.lblChaveAcesso);
            this.Controls.Add(this.lblProtocolo);
            this.Controls.Add(this.lblNumeroRecibo);
            this.Controls.Add(this.lblSenhaCertificado);
            this.Controls.Add(this.lblCertificado);
            this.Controls.Add(this.txtSenhaCertificado);
            this.Controls.Add(this.txtRetorno);
            this.Controls.Add(this.txtChaveAcesso);
            this.Controls.Add(this.txtProtocolo);
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
            this.Text = "Vip.DFe - Demo - .Net 6.0";
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
        private System.Windows.Forms.TextBox txtProtocolo;
        private System.Windows.Forms.Label lblProtocolo;
    }
}

