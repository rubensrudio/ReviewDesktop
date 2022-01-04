using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Review
{
    public partial class frmClientesLista : Review.frmModeloLista
    {
        private int idclientes;
        private int idempresas;
        private String clientefornecedor;
        public frmClientesLista(int idemp, String clifor)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.clientefornecedor = clifor;
        }

        private void frmClienteLista_Load(object sender, EventArgs e)
        {
            if(this.clientefornecedor == "cl")
            {
                this.Text = "Clientes";
            }
            else
            {
                this.Text = "Fornecedores";
            }

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgDados.Columns.Add(checkColumn);

            cbBuscarPor.Items.Add("Razão Social");
            cbBuscarPor.Items.Add("Nome Fantasia");
            cbBuscarPor.Items.Add("CNPJ");

            cbBuscarPor.SelectedIndex = 1;
            label2.Text = cbBuscarPor.Text;
            txtBusca_TextChanged(sender, e);

            dgDados.Columns[1].Visible = false;
            dgDados.Columns[2].Visible = false;
            dgDados.Columns[3].HeaderText = "Razão Social";
            dgDados.Columns[3].ReadOnly = true;
            dgDados.Columns[3].Width = 250;
            dgDados.Columns[4].HeaderText = "Nome Fantasia";
            dgDados.Columns[4].ReadOnly = true;
            dgDados.Columns[4].Width = 250;
            dgDados.Columns[5].HeaderText = "CNPJ";
            dgDados.Columns[5].ReadOnly = true;
            dgDados.Columns[5].Width = 130;
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLCliente bll = new BLLCliente(cx);

            dgDados.DataSource = bll.Localizar(txtValor.Text, cbBuscarPor.Text, idempresas, clientefornecedor);
        }

        private void cbBuscaPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = cbBuscarPor.Text;
            txtBusca_TextChanged(sender, e);
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Deseja excluir todos registros selecionados?", "Aviso", MessageBoxButtons.YesNo);
            if (d.ToString() == "Yes")
            {
                SqlTransaction ts = null;
                SqlConnection cx = new SqlConnection(DadosConexao.StringConexao);
                SqlCommand com = null;

                try
                {
                    cx.Open();
                    ts = cx.BeginTransaction();

                    foreach (DataGridViewRow item in dgDados.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            com = new SqlCommand("delete from clientes where idclientes=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            com = new SqlCommand("delete from enderecos where idenderecos=" + item.Cells[2].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();
                        }
                    }

                    ts.Commit();
                    MessageBox.Show("Excluido com sucesso!");
                }
                catch (Exception erro)
                {
                    ts.Rollback();
                    MessageBox.Show("Erro ao excluir! \n\n" + erro.Message);
                }

                txtBusca_TextChanged(sender, e);
            }
        }

        private void btInserir_Click(object sender, EventArgs e)
        {
            frmClientesCadastro f = new frmClientesCadastro(this.idempresas, this.clientefornecedor);
            f.ShowDialog();
            f.Dispose();
            this.txtBusca_TextChanged(sender, e);
        }

        private void ckMarcar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgDados.Rows)
            {
                if (Convert.ToInt32(Row.Cells[0].Value) == 0)
                    Row.Cells[0].Value = 1;
                else
                    Row.Cells[0].Value = 0;
            }
        }

        private void dgDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.idclientes = Convert.ToInt32(dgDados.Rows[e.RowIndex].Cells[1].Value);
                frmClientesCadastro f = new frmClientesCadastro(this.idempresas, this.idclientes, this.clientefornecedor);
                f.ShowDialog();
                f.Dispose();
                this.txtBusca_TextChanged(sender, e);
            }
        }
    }
}
