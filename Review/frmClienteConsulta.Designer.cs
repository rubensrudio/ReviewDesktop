﻿namespace Review
{
    partial class frmClienteConsulta
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
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDados)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgDados);
            // 
            // btIncluir
            // 
            this.btIncluir.Visible = false;
            // 
            // btExcluir
            // 
            this.btExcluir.Visible = false;
            // 
            // ckSelecionaTodos
            // 
            this.ckSelecionaTodos.Visible = false;
            // 
            // cbBuscarPor
            // 
            this.cbBuscarPor.Items.AddRange(new object[] {
            "Razão Social",
            "Nome Fantasia",
            "CNPJ"});
            this.cbBuscarPor.SelectedIndexChanged += new System.EventHandler(this.cbBuscaPor_SelectedIndexChanged);
            // 
            // txtValor
            // 
            this.txtValor.TextChanged += new System.EventHandler(this.txtValor_TextChanged);
            // 
            // label1
            // 
            this.label1.Visible = false;
            // 
            // dgDados
            // 
            this.dgDados.AllowUserToAddRows = false;
            this.dgDados.AllowUserToDeleteRows = false;
            this.dgDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDados.Location = new System.Drawing.Point(0, 0);
            this.dgDados.Name = "dgDados";
            this.dgDados.Size = new System.Drawing.Size(784, 461);
            this.dgDados.TabIndex = 0;
            this.dgDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDados_CellDoubleClick);
            // 
            // frmClienteConsulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Name = "frmClienteConsulta";
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.frmClienteConsulta_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDados;
    }
}
