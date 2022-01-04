using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Review
{
    public partial class frmClienteConsulta : Review.frmModeloLista
    {
        private int idempresas;
        private int idclientes;
        private String clientefornecedor;
        public frmClienteConsulta(int idemp, String clifor)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.clientefornecedor = clifor;
        }

        public int ConsultaCliente()
        {
            return this.idclientes;
        }

        private void cbBuscaPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValor_TextChanged(sender, e);
        }

        private void frmClienteConsulta_Load(object sender, EventArgs e)
        {
            cbBuscarPor.SelectedIndex = 1;
            txtValor_TextChanged(sender, e);

            dgDados.Columns[0].Visible = false;
            dgDados.Columns[1].Visible = false;
            dgDados.Columns[2].HeaderText = "Razão Social";
            dgDados.Columns[2].ReadOnly = true;
            dgDados.Columns[2].Width = 250;
            dgDados.Columns[3].HeaderText = "Nome Fantasia";
            dgDados.Columns[3].ReadOnly = true;
            dgDados.Columns[3].Width = 250;
            dgDados.Columns[4].HeaderText = "CNPJ";
            dgDados.Columns[4].ReadOnly = true;
            dgDados.Columns[4].Width = 130;
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            label2.Text = cbBuscarPor.Text;

            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLCliente bll = new BLLCliente(cx);

            dgDados.DataSource = bll.Localizar(txtValor.Text, cbBuscarPor.Text, idempresas, clientefornecedor);
        }

        private void dgDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.idclientes = Convert.ToInt32(dgDados.Rows[e.RowIndex].Cells[0].Value);
                Close();
            }
        }
    }
}
