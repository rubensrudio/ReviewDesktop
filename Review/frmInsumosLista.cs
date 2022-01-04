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
    public partial class frmInsumosLista : Review.frmModeloLista
    {
        private int idempresas;
        private int idinsumos;
        public frmInsumosLista(int codigo)
        {
            InitializeComponent();
            this.idempresas = codigo;
        }

        private void frmInsumosLista_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgDados.Columns.Add(checkColumn);

            cbBuscarPor.Items.Add("Descrição");
            cbBuscarPor.Items.Add("Tipo");

            cbBuscarPor.SelectedIndex = 0;
            label2.Text = cbBuscarPor.Text;
            txtValor_TextChanged(sender, e);

            dgDados.Columns[1].Visible = false;
            dgDados.Columns[2].HeaderText = "Descrição";
            dgDados.Columns[2].ReadOnly = true;
            dgDados.Columns[2].Width = 300;
            dgDados.Columns[3].HeaderText = "Unidade";
            dgDados.Columns[3].ReadOnly = true;
            dgDados.Columns[3].Width = 70;
            dgDados.Columns[4].HeaderText = "Preço";
            dgDados.Columns[4].ReadOnly = true;
            dgDados.Columns[4].Width = 70;
            dgDados.Columns[5].HeaderText = "Data";
            dgDados.Columns[5].ReadOnly = true;
            dgDados.Columns[5].Width = 70;
            dgDados.Columns[6].HeaderText = "Classificação";
            dgDados.Columns[6].ReadOnly = true;
            dgDados.Columns[6].Width = 150;
            dgDados.Columns[7].HeaderText = "Plano de Contas";
            dgDados.Columns[7].ReadOnly = true;
            dgDados.Columns[7].Width = 150;
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLInsumos bll = new BLLInsumos(cx);

            dgDados.DataSource = bll.Localizar(txtValor.Text, cbBuscarPor.Text, idempresas);
        }

        private void cbBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = cbBuscarPor.Text;
            txtValor_TextChanged(sender, e);
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
                            com = new SqlCommand("delete from insumos where idinsumos=" + item.Cells[1].Value, cx);
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
            frmInsumosCadastro f = new frmInsumosCadastro(this.idempresas);
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
                this.idinsumos = Convert.ToInt32(dgDados.Rows[e.RowIndex].Cells[1].Value);
                frmInsumosCadastro f = new frmInsumosCadastro(this.idempresas, this.idinsumos);
                f.ShowDialog();
                f.Dispose();
                this.txtValor_TextChanged(sender, e);
            }
        }
    }
}
