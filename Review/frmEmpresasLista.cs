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
    public partial class frmEmpresasLista : Review.frmModeloLista
    {
        private int idempresas;
        private int idusuarios;
        public frmEmpresasLista()
        {
            InitializeComponent();
        }

        public frmEmpresasLista(int idemp, int idusu)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idusuarios = idusu;
        }

        private void frmEmpresasLista_Load(object sender, EventArgs e)
        {
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
            txtValor_TextChanged(sender, e);

            dgDados.Columns[1].Visible = false;
            dgDados.Columns[2].HeaderText = "Razão Social";
            dgDados.Columns[2].ReadOnly = true;
            dgDados.Columns[2].Width = 200;
            dgDados.Columns[3].HeaderText = "Nome Fantasia";
            dgDados.Columns[3].ReadOnly = true;
            dgDados.Columns[3].Width = 200;
            dgDados.Columns[4].HeaderText = "CNPJ";
            dgDados.Columns[4].ReadOnly = true;
            dgDados.Columns[4].Width = 80;
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            if (cbBuscarPor.SelectedIndex != -1)
            {
                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLEmpresas bll = new BLLEmpresas(cx);

                dgDados.DataSource = bll.Localizar(txtValor.Text, cbBuscarPor.Text);
            }
        }

        private void cbBuscarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = cbBuscarPor.Text;
            txtValor_TextChanged(sender, e);
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
                this.idempresas = Convert.ToInt32(dgDados.Rows[e.RowIndex].Cells[1].Value);
                frmEmpresasCadastro f = new frmEmpresasCadastro(this.idempresas, this.idusuarios);
                f.ShowDialog();
                f.Dispose();
                this.txtValor_TextChanged(sender, e);
            }
        }

        private void btIncluir_Click(object sender, EventArgs e)
        {
            frmEmpresasCadastro f = new frmEmpresasCadastro(this.idempresas, this.idusuarios);
            f.ShowDialog();
            f.Dispose();
            this.txtValor_TextChanged(sender, e);
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
                            /* Outras Exclusões
                            //Serviços
                            com = new SqlCommand("delete from serv_propostas where idpropostas=" + item.Cells[2].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Materiais
                            com = new SqlCommand("delete from mat_propostas where idpropostas=" + item.Cells[2].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Equipamentos
                            com = new SqlCommand("delete from equip_propostas where idpropostas=" + item.Cells[2].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Anexos
                            com = new SqlCommand("delete from anexos_propostas where idpropostas=" + item.Cells[2].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();
                            */

                            com = new SqlCommand("delete from empresas where idempresas=" + item.Cells[1].Value, cx);
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

                txtValor_TextChanged(sender, e);
            }
        }
    }
}
