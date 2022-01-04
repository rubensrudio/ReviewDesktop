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
    public partial class frmClassificacaoLista : Review.frmModeloLista
    {
        private int idempresas;
        private int idclassificacao;
        public frmClassificacaoLista(int codigo)
        {
            InitializeComponent();
            this.idempresas = codigo;
        }

        private void frmClassificacaoLista_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgDados.Columns.Add(checkColumn);

            cbBuscarPor.Items.Add("Índice");
            cbBuscarPor.Items.Add("Descrição");

            cbBuscarPor.SelectedIndex = 1;
            label2.Text = cbBuscarPor.Text;
            txtValor_TextChanged(sender, e);

            dgDados.Columns[1].Visible = false;
            dgDados.Columns[2].HeaderText = "Índice";
            dgDados.Columns[2].ReadOnly = true;
            dgDados.Columns[2].Width = 100;
            dgDados.Columns[3].HeaderText = "Descrição";
            dgDados.Columns[3].ReadOnly = true;
            dgDados.Columns[3].Width = 300;
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLClassificacao bll = new BLLClassificacao(cx);

            dgDados.DataSource = bll.Localizar(txtValor.Text, cbBuscarPor.Text, idempresas);
        }

        private void cbBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = cbBuscarPor.Text;
            txtValor_TextChanged(sender, e);
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Atenção! Todos itens filhos serão excluídos. \nDeseja excluir os registros?", "Aviso", MessageBoxButtons.YesNo);
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
                            com = new SqlCommand("delete from classificacao where indice like '" + item.Cells[2].Value + "%'", cx);
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
                cx.Close();
                txtValor_TextChanged(sender, e);
            }
        }

        private void btIncluir_Click(object sender, EventArgs e)
        {
            frmClassificacaoCadastro f = new frmClassificacaoCadastro(this.idempresas);
            f.ShowDialog();
            f.Dispose();
            this.txtValor_TextChanged(sender, e);
        }

        private void ckSelecionaTodos_CheckedChanged(object sender, EventArgs e)
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
                this.idclassificacao = Convert.ToInt32(dgDados.Rows[e.RowIndex].Cells[1].Value);
                frmClassificacaoCadastro f = new frmClassificacaoCadastro(this.idempresas, this.idclassificacao);
                f.ShowDialog();
                f.Dispose();
                this.txtValor_TextChanged(sender, e);
            }
        }
    }
}
