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
    public partial class frmUsuariosLista : Review.frmModeloLista
    {
        private int idempresas;
        private int idusuarios;
        public frmUsuariosLista(int codigo)
        {
            InitializeComponent();
            this.idempresas = codigo;
        }

        public frmUsuariosLista(int idemp, int idusu)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idusuarios = idusu;
        }

        private void frmUsuariosLista_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgDados.Columns.Add(checkColumn);

            cbBuscarPor.Items.Add("Usuário");
            cbBuscarPor.Items.Add("Nome");

            cbBuscarPor.SelectedIndex = 1;
            label2.Text = cbBuscarPor.Text;
            txtValor_TextChanged(sender, e);

            dgDados.Columns[1].Visible = false;
            dgDados.Columns[2].HeaderText = "Usuário";
            dgDados.Columns[2].ReadOnly = true;
            dgDados.Columns[2].Width = 100;
            dgDados.Columns[3].HeaderText = "Nome";
            dgDados.Columns[3].ReadOnly = true;
            dgDados.Columns[3].Width = 300;
            
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLUsuarios bll = new BLLUsuarios(cx);

            dgDados.DataSource = bll.Localizar(txtValor.Text, cbBuscarPor.Text, idempresas);

        }

        private void cbBuscaPor_SelectedIndexChanged(object sender, EventArgs e)
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
                        if ((Convert.ToInt32(item.Cells[0].Value) == 1) && (Convert.ToInt32(item.Cells[1].Value) != this.idusuarios))
                        {
                            //Exclui permissão nos itens dos módulos
                            com = new SqlCommand("delete from itens_modulos_usuarios where idusuarios=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Exclui permissão nos módulos
                            com = new SqlCommand("delete from modulos_usuarios where idusuarios=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Exclui permissão nos centros de custo
                            com = new SqlCommand("delete from centros_de_custos_usuarios where idusuarios=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            com = new SqlCommand("delete from usuarios where idusuarios=" + item.Cells[1].Value, cx);
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

        private void btInserir_Click(object sender, EventArgs e)
        {
            frmUsuariosCadastro f = new frmUsuariosCadastro(this.idempresas);
            f.ShowDialog();
            f.Dispose();
            this.txtValor_TextChanged(sender, e);
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
                this.idusuarios = Convert.ToInt32(dgDados.Rows[e.RowIndex].Cells[1].Value);
                frmUsuariosCadastro f = new frmUsuariosCadastro(this.idempresas, this.idusuarios);
                f.ShowDialog();
                f.Dispose();
                this.txtValor_TextChanged(sender, e);
            }
        }
    }
}
