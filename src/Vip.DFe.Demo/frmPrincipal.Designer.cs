namespace Vip.DFe.Demo
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txtDados = new System.Windows.Forms.TextBox();
            this.txtCertificado = new System.Windows.Forms.TextBox();
            this.lblCertificado = new System.Windows.Forms.Label();
            this.btnCertificado = new System.Windows.Forms.Button();
            this.txtSenhaCertificado = new System.Windows.Forms.TextBox();
            this.lblSenhaCertificado = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCarregarNFeConsultar = new System.Windows.Forms.Button();
            this.btnConsultarStatusServico = new System.Windows.Forms.Button();
            this.btnConsultarChaveNFe = new System.Windows.Forms.Button();
            this.btnInutilizacao = new System.Windows.Forms.Button();
            this.btnCancelarNFe = new System.Windows.Forms.Button();
            this.btnCartaoDeCorrecao = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 100);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(288, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Autorização";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDados
            // 
            this.txtDados.Location = new System.Drawing.Point(16, 209);
            this.txtDados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDados.Multiline = true;
            this.txtDados.Name = "txtDados";
            this.txtDados.Size = new System.Drawing.Size(1033, 329);
            this.txtDados.TabIndex = 1;
            // 
            // txtCertificado
            // 
            this.txtCertificado.Location = new System.Drawing.Point(16, 32);
            this.txtCertificado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCertificado.Name = "txtCertificado";
            this.txtCertificado.Size = new System.Drawing.Size(287, 22);
            this.txtCertificado.TabIndex = 2;
            this.txtCertificado.Text = "121A1809255AA983";
            // 
            // lblCertificado
            // 
            this.lblCertificado.AutoSize = true;
            this.lblCertificado.Location = new System.Drawing.Point(16, 12);
            this.lblCertificado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCertificado.Name = "lblCertificado";
            this.lblCertificado.Size = new System.Drawing.Size(118, 17);
            this.lblCertificado.TabIndex = 3;
            this.lblCertificado.Text = "Certificado Digital";
            // 
            // btnCertificado
            // 
            this.btnCertificado.Location = new System.Drawing.Point(312, 30);
            this.btnCertificado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCertificado.Name = "btnCertificado";
            this.btnCertificado.Size = new System.Drawing.Size(172, 28);
            this.btnCertificado.TabIndex = 0;
            this.btnCertificado.Text = "Selecionar Certificado";
            this.btnCertificado.UseVisualStyleBackColor = true;
            this.btnCertificado.Click += new System.EventHandler(this.btnCertificado_Click);
            // 
            // txtSenhaCertificado
            // 
            this.txtSenhaCertificado.Location = new System.Drawing.Point(492, 33);
            this.txtSenhaCertificado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSenhaCertificado.Name = "txtSenhaCertificado";
            this.txtSenhaCertificado.PasswordChar = '*';
            this.txtSenhaCertificado.Size = new System.Drawing.Size(107, 22);
            this.txtSenhaCertificado.TabIndex = 2;
            // 
            // lblSenhaCertificado
            // 
            this.lblSenhaCertificado.AutoSize = true;
            this.lblSenhaCertificado.Location = new System.Drawing.Point(488, 14);
            this.lblSenhaCertificado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenhaCertificado.Name = "lblSenhaCertificado";
            this.lblSenhaCertificado.Size = new System.Drawing.Size(120, 17);
            this.lblSenhaCertificado.TabIndex = 3;
            this.lblSenhaCertificado.Text = "Senha Certificado";
            this.lblSenhaCertificado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(312, 100);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(288, 28);
            this.button2.TabIndex = 0;
            this.button2.Text = "Consulta Autorização";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCarregarNFeConsultar
            // 
            this.btnCarregarNFeConsultar.Location = new System.Drawing.Point(849, 33);
            this.btnCarregarNFeConsultar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCarregarNFeConsultar.Name = "btnCarregarNFeConsultar";
            this.btnCarregarNFeConsultar.Size = new System.Drawing.Size(201, 28);
            this.btnCarregarNFeConsultar.TabIndex = 4;
            this.btnCarregarNFeConsultar.Text = "Carregar NFe e Consultar";
            this.btnCarregarNFeConsultar.UseVisualStyleBackColor = true;
            this.btnCarregarNFeConsultar.Click += new System.EventHandler(this.btnCarregarNFeConsultar_Click);
            // 
            // btnConsultarStatusServico
            // 
            this.btnConsultarStatusServico.Location = new System.Drawing.Point(16, 135);
            this.btnConsultarStatusServico.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConsultarStatusServico.Name = "btnConsultarStatusServico";
            this.btnConsultarStatusServico.Size = new System.Drawing.Size(288, 28);
            this.btnConsultarStatusServico.TabIndex = 4;
            this.btnConsultarStatusServico.Text = "Consultar Status Serviço";
            this.btnConsultarStatusServico.UseVisualStyleBackColor = true;
            this.btnConsultarStatusServico.Click += new System.EventHandler(this.btnConsultarStatusServico_Click);
            // 
            // btnConsultarChaveNFe
            // 
            this.btnConsultarChaveNFe.Location = new System.Drawing.Point(312, 135);
            this.btnConsultarChaveNFe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConsultarChaveNFe.Name = "btnConsultarChaveNFe";
            this.btnConsultarChaveNFe.Size = new System.Drawing.Size(288, 28);
            this.btnConsultarChaveNFe.TabIndex = 4;
            this.btnConsultarChaveNFe.Text = "Consultar por Chave NFe";
            this.btnConsultarChaveNFe.UseVisualStyleBackColor = true;
            this.btnConsultarChaveNFe.Click += new System.EventHandler(this.btnConsultarChaveNFe_Click);
            // 
            // btnInutilizacao
            // 
            this.btnInutilizacao.Location = new System.Drawing.Point(849, 69);
            this.btnInutilizacao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInutilizacao.Name = "btnInutilizacao";
            this.btnInutilizacao.Size = new System.Drawing.Size(201, 28);
            this.btnInutilizacao.TabIndex = 4;
            this.btnInutilizacao.Text = "Inutilizar NFe";
            this.btnInutilizacao.UseVisualStyleBackColor = true;
            this.btnInutilizacao.Click += new System.EventHandler(this.btnInutilizacao_Click);
            // 
            // btnCancelarNFe
            // 
            this.btnCancelarNFe.Location = new System.Drawing.Point(849, 105);
            this.btnCancelarNFe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelarNFe.Name = "btnCancelarNFe";
            this.btnCancelarNFe.Size = new System.Drawing.Size(201, 28);
            this.btnCancelarNFe.TabIndex = 4;
            this.btnCancelarNFe.Text = "Cancelar NFe";
            this.btnCancelarNFe.UseVisualStyleBackColor = true;
            this.btnCancelarNFe.Click += new System.EventHandler(this.btnCancelarNFe_Click);
            // 
            // btnCartaoDeCorrecao
            // 
            this.btnCartaoDeCorrecao.Location = new System.Drawing.Point(849, 140);
            this.btnCartaoDeCorrecao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCartaoDeCorrecao.Name = "btnCartaoDeCorrecao";
            this.btnCartaoDeCorrecao.Size = new System.Drawing.Size(201, 28);
            this.btnCartaoDeCorrecao.TabIndex = 4;
            this.btnCartaoDeCorrecao.Text = "Carta de Correção";
            this.btnCartaoDeCorrecao.UseVisualStyleBackColor = true;
            this.btnCartaoDeCorrecao.Click += new System.EventHandler(this.btnCartaoDeCorrecao_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(849, 173);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(201, 28);
            this.button3.TabIndex = 4;
            this.button3.Text = "Gerar DANFE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnCartaoDeCorrecao);
            this.Controls.Add(this.btnCancelarNFe);
            this.Controls.Add(this.btnInutilizacao);
            this.Controls.Add(this.btnConsultarChaveNFe);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnConsultarStatusServico);
            this.Controls.Add(this.btnCarregarNFeConsultar);
            this.Controls.Add(this.lblSenhaCertificado);
            this.Controls.Add(this.lblCertificado);
            this.Controls.Add(this.txtSenhaCertificado);
            this.Controls.Add(this.txtCertificado);
            this.Controls.Add(this.txtDados);
            this.Controls.Add(this.btnCertificado);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmPrincipal";
            this.Text = "frmPrincipal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDados;
        private System.Windows.Forms.TextBox txtCertificado;
        private System.Windows.Forms.Label lblCertificado;
        private System.Windows.Forms.Button btnCertificado;
        private System.Windows.Forms.TextBox txtSenhaCertificado;
        private System.Windows.Forms.Label lblSenhaCertificado;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCarregarNFeConsultar;
        private System.Windows.Forms.Button btnConsultarStatusServico;
        private System.Windows.Forms.Button btnConsultarChaveNFe;
        private System.Windows.Forms.Button btnInutilizacao;
        private System.Windows.Forms.Button btnCancelarNFe;
        private System.Windows.Forms.Button btnCartaoDeCorrecao;
        private System.Windows.Forms.Button button3;
    }
}