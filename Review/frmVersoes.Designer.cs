namespace Review
{
    partial class frmVersoes
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
            this.txtVersao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgVersoes = new System.Windows.Forms.DataGridView();
            this.btIncluir = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgVersoes)).BeginInit();
            this.SuspendLayout();
            // 
            // txtVersao
            // 
            this.txtVersao.Location = new System.Drawing.Point(15, 87);
            this.txtVersao.Name = "txtVersao";
            this.txtVersao.Size = new System.Drawing.Size(124, 20);
            this.txtVersao.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Versão:";
            // 
            // dgVersoes
            // 
            this.dgVersoes.AllowUserToAddRows = false;
            this.dgVersoes.AllowUserToDeleteRows = false;
            this.dgVersoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVersoes.Location = new System.Drawing.Point(13, 114);
            this.dgVersoes.Name = "dgVersoes";
            this.dgVersoes.Size = new System.Drawing.Size(759, 435);
            this.dgVersoes.TabIndex = 2;
            this.dgVersoes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgVersoes_CellDoubleClick);
            // 
            // btIncluir
            // 
            this.btIncluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btIncluir.Image = global::Review.Properties.Resources.Novo;
            this.btIncluir.Location = new System.Drawing.Point(576, 12);
            this.btIncluir.Name = "btIncluir";
            this.btIncluir.Size = new System.Drawing.Size(95, 95);
            this.btIncluir.TabIndex = 3;
            this.btIncluir.Text = "Incluir";
            this.btIncluir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btIncluir.UseVisualStyleBackColor = true;
            this.btIncluir.Click += new System.EventHandler(this.btInserir_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::Review.Properties.Resources.Trash;
            this.button1.Location = new System.Drawing.Point(677, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 95);
            this.button1.TabIndex = 4;
            this.button1.Text = "Excluir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btExcluir_Click);
            // 
            // frmVersoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btIncluir);
            this.Controls.Add(this.dgVersoes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVersao);
            this.Name = "frmVersoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Versões";
            this.Load += new System.EventHandler(this.frmVersoes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgVersoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtVersao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgVersoes;
        private System.Windows.Forms.Button btIncluir;
        private System.Windows.Forms.Button button1;
    }
}