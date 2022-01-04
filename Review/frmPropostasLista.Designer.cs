namespace Review
{
    partial class frmPropostasLista
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
            this.dgDados = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSituacao = new System.Windows.Forms.ComboBox();
            this.btCopia = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btCopia);
            this.panel1.Controls.Add(this.cbSituacao);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.SetChildIndex(this.btIncluir, 0);
            this.panel1.Controls.SetChildIndex(this.btExcluir, 0);
            this.panel1.Controls.SetChildIndex(this.ckSelecionaTodos, 0);
            this.panel1.Controls.SetChildIndex(this.label1, 0);
            this.panel1.Controls.SetChildIndex(this.txtValor, 0);
            this.panel1.Controls.SetChildIndex(this.label2, 0);
            this.panel1.Controls.SetChildIndex(this.label3, 0);
            this.panel1.Controls.SetChildIndex(this.cbBuscarPor, 0);
            this.panel1.Controls.SetChildIndex(this.label4, 0);
            this.panel1.Controls.SetChildIndex(this.cbSituacao, 0);
            this.panel1.Controls.SetChildIndex(this.btCopia, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgDados);
            this.panel2.Location = new System.Drawing.Point(0, 123);
            this.panel2.Size = new System.Drawing.Size(784, 438);
            // 
            // btIncluir
            // 
            this.btIncluir.Location = new System.Drawing.Point(484, 2);
            this.btIncluir.Click += new System.EventHandler(this.btInserir_Click);
            // 
            // btExcluir
            // 
            this.btExcluir.Click += new System.EventHandler(this.btExcluir_Click);
            // 
            // ckSelecionaTodos
            // 
            this.ckSelecionaTodos.CheckedChanged += new System.EventHandler(this.ckMarcar_CheckedChanged);
            // 
            // cbBuscarPor
            // 
            this.cbBuscarPor.Location = new System.Drawing.Point(250, 20);
            this.cbBuscarPor.SelectedIndexChanged += new System.EventHandler(this.cbBuscaPor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(247, 5);
            // 
            // txtValor
            // 
            this.txtValor.Size = new System.Drawing.Size(228, 20);
            this.txtValor.TextChanged += new System.EventHandler(this.txtBusca_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(306, 84);
            // 
            // dgDados
            // 
            this.dgDados.AllowUserToAddRows = false;
            this.dgDados.AllowUserToDeleteRows = false;
            this.dgDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDados.Location = new System.Drawing.Point(0, 0);
            this.dgDados.Name = "dgDados";
            this.dgDados.Size = new System.Drawing.Size(784, 438);
            this.dgDados.TabIndex = 0;
            this.dgDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDados_CellDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Situação";
            // 
            // cbSituacao
            // 
            this.cbSituacao.FormattingEnabled = true;
            this.cbSituacao.Items.AddRange(new object[] {
            "Todas",
            "Aguardando",
            "Aprovada",
            "Arquivada"});
            this.cbSituacao.Location = new System.Drawing.Point(250, 60);
            this.cbSituacao.Name = "cbSituacao";
            this.cbSituacao.Size = new System.Drawing.Size(155, 21);
            this.cbSituacao.TabIndex = 10;
            this.cbSituacao.SelectedIndexChanged += new System.EventHandler(this.cbSituacao_SelectedIndexChanged);
            // 
            // btCopia
            // 
            this.btCopia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCopia.Image = global::Review.Properties.Resources.Copy;
            this.btCopia.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btCopia.Location = new System.Drawing.Point(585, 2);
            this.btCopia.Name = "btCopia";
            this.btCopia.Size = new System.Drawing.Size(95, 95);
            this.btCopia.TabIndex = 11;
            this.btCopia.Text = "Copiar";
            this.btCopia.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btCopia.UseVisualStyleBackColor = true;
            this.btCopia.Click += new System.EventHandler(this.btCopia_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(737, 103);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(628, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "<<";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(661, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "1 de 1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(704, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = ">>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmPropostasLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "frmPropostasLista";
            this.Text = "Propostas";
            this.Load += new System.EventHandler(this.frmPropostasLista_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.numericUpDown1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSituacao;
        private System.Windows.Forms.Button btCopia;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}
