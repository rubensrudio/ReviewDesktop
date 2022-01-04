namespace Review
{
    partial class frmPlanodeContasCadastro
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
            this.cbAnaliticoSintetico = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIndice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbIndicePai = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDebitoCredito = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbLiberado = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbGerarPago = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbAtivo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSalvar
            // 
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbAtivo);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.cbGerarPago);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.cbLiberado);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cbDebitoCredito);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cbAnaliticoSintetico);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtDescricao);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtIndice);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbIndicePai);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtCodigo);
            this.panel2.Controls.Add(this.label1);
            // 
            // btCancelar
            // 
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // cbAnaliticoSintetico
            // 
            this.cbAnaliticoSintetico.FormattingEnabled = true;
            this.cbAnaliticoSintetico.Items.AddRange(new object[] {
            "Analítico",
            "Sintético"});
            this.cbAnaliticoSintetico.Location = new System.Drawing.Point(12, 192);
            this.cbAnaliticoSintetico.Name = "cbAnaliticoSintetico";
            this.cbAnaliticoSintetico.Size = new System.Drawing.Size(121, 21);
            this.cbAnaliticoSintetico.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Tipo:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(12, 148);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(363, 20);
            this.txtDescricao.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Descrição:";
            // 
            // txtIndice
            // 
            this.txtIndice.Location = new System.Drawing.Point(12, 108);
            this.txtIndice.Name = "txtIndice";
            this.txtIndice.ReadOnly = true;
            this.txtIndice.Size = new System.Drawing.Size(131, 20);
            this.txtIndice.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Índice:";
            // 
            // cbIndicePai
            // 
            this.cbIndicePai.FormattingEnabled = true;
            this.cbIndicePai.Location = new System.Drawing.Point(12, 68);
            this.cbIndicePai.Name = "cbIndicePai";
            this.cbIndicePai.Size = new System.Drawing.Size(363, 21);
            this.cbIndicePai.TabIndex = 13;
            this.cbIndicePai.SelectedIndexChanged += new System.EventHandler(this.cbIndicePai_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Índice Pai:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 29);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(59, 20);
            this.txtCodigo.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Código:";
            // 
            // cbDebitoCredito
            // 
            this.cbDebitoCredito.FormattingEnabled = true;
            this.cbDebitoCredito.Items.AddRange(new object[] {
            "Débito",
            "Crédito"});
            this.cbDebitoCredito.Location = new System.Drawing.Point(12, 236);
            this.cbDebitoCredito.Name = "cbDebitoCredito";
            this.cbDebitoCredito.Size = new System.Drawing.Size(121, 21);
            this.cbDebitoCredito.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Débito/Crédito:";
            // 
            // cbLiberado
            // 
            this.cbLiberado.FormattingEnabled = true;
            this.cbLiberado.Items.AddRange(new object[] {
            "Sim",
            "Não"});
            this.cbLiberado.Location = new System.Drawing.Point(12, 279);
            this.cbLiberado.Name = "cbLiberado";
            this.cbLiberado.Size = new System.Drawing.Size(79, 21);
            this.cbLiberado.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Liberado:";
            // 
            // cbGerarPago
            // 
            this.cbGerarPago.FormattingEnabled = true;
            this.cbGerarPago.Items.AddRange(new object[] {
            "Sim",
            "Não"});
            this.cbGerarPago.Location = new System.Drawing.Point(12, 325);
            this.cbGerarPago.Name = "cbGerarPago";
            this.cbGerarPago.Size = new System.Drawing.Size(79, 21);
            this.cbGerarPago.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 308);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Gerar Pago:";
            // 
            // cbAtivo
            // 
            this.cbAtivo.FormattingEnabled = true;
            this.cbAtivo.Items.AddRange(new object[] {
            "Sim",
            "Não"});
            this.cbAtivo.Location = new System.Drawing.Point(12, 371);
            this.cbAtivo.Name = "cbAtivo";
            this.cbAtivo.Size = new System.Drawing.Size(79, 21);
            this.cbAtivo.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 354);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Ativo:";
            // 
            // frmPlanodeContasCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "frmPlanodeContasCadastro";
            this.Text = "Cadastro de Plano de Contas";
            this.Load += new System.EventHandler(this.frmPlanodeContasCadastro_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbAnaliticoSintetico;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIndice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbIndicePai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDebitoCredito;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbLiberado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbGerarPago;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbAtivo;
        private System.Windows.Forms.Label label9;
    }
}
