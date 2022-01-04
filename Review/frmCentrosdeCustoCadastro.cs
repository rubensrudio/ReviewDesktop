using BLL;
using DAL;
using Modelo;
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
    public partial class frmCentrosdeCustoCadastro : Review.frmModeloIncluir
    {
        private int idempresas;
        private int idcentrosdecustos;

        public frmCentrosdeCustoCadastro(int idemp)
        {
            InitializeComponent();
            this.idempresas = idemp;
        }
        public frmCentrosdeCustoCadastro(int idemp, int idccust)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idcentrosdecustos = idccust;
        }

        private void frmCentrosdeCustoCadastro_Load(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLCentrosdeCusto bll = new BLLCentrosdeCusto(cx);
            if (this.idcentrosdecustos > 0)
            {
                txtCodigo.Text = this.idcentrosdecustos.ToString();

                ModeloCentrodeCusto modelo = bll.CarregaCentrosdeCusto(this.idcentrosdecustos);
                txtCentrodeCusto.Text = modelo.Codigo;
                txtDescricao.Text = modelo.Descricao;
            }
            else
            {
                txtCodigo.Text = "";
            }

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgUsuarios.Columns.Add(checkColumn);

            BLLUsuarios bll2 = new BLLUsuarios(cx);
            dgUsuarios.DataSource = bll2.Localizar(idempresas, idcentrosdecustos);

            dgUsuarios.Columns[0].Visible = true;
            dgUsuarios.Columns[1].Visible = false;
            dgUsuarios.Columns[2].HeaderText = "Usuário";
            dgUsuarios.Columns[2].ReadOnly = true;
            dgUsuarios.Columns[2].Width = 100;
            dgUsuarios.Columns[3].HeaderText = "Nome";
            dgUsuarios.Columns[3].ReadOnly = true;
            dgUsuarios.Columns[3].Width = 150;
            dgUsuarios.Columns[4].Visible = false;

            foreach (DataGridViewRow item in dgUsuarios.Rows)
            {
                if (item.Cells[4].Value.ToString() == "1")
                {
                    item.Cells[0].Value = 1;
                }
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            SqlTransaction ts = null;
            SqlConnection cx = new SqlConnection(DadosConexao.StringConexao);
            SqlCommand com = null;
            try
            {
                ModeloCentrodeCusto modelo = new ModeloCentrodeCusto();
                modelo.Codigo = txtCentrodeCusto.Text;
                modelo.Descricao = txtDescricao.Text;
                modelo.IdEmpresas = this.idempresas;

                cx.Open();
                ts = cx.BeginTransaction();

                DALConexao cx1 = new DALConexao(DadosConexao.StringConexao);
                BLLCentrosdeCusto bll = new BLLCentrosdeCusto(cx1);
                if (this.idcentrosdecustos > 0)
                {
                    modelo.IdCentros_de_Custos = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                }
                else
                {
                    bll.Incluir(modelo);
                    this.idcentrosdecustos = modelo.IdCentros_de_Custos;
                }

                com = new SqlCommand("delete from centros_de_custos_usuarios where idcentros_de_custos=" + idcentrosdecustos.ToString(), cx);
                com.Transaction = ts;
                com.ExecuteNonQuery();

                foreach (DataGridViewRow item in dgUsuarios.Rows)
                {
                    if (Convert.ToInt32(item.Cells[0].Value) == 1)
                    {
                        com = new SqlCommand("insert into centros_de_custos_usuarios (idcentros_de_custos,idusuarios) values (" + idcentrosdecustos.ToString() + "," + Convert.ToInt32(item.Cells[1].Value) + ")", cx);
                        com.Transaction = ts;
                        com.ExecuteNonQuery();
                    }
                }

                ts.Commit();
                MessageBox.Show("Salvo com sucesso!");
                this.Close();

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void ckMarcar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgUsuarios.Rows)
            {
                if (ckMarcar.Checked == true)
                {
                    Row.Cells[0].Value = 1;
                }
                else
                {
                    Row.Cells[0].Value = 0;
                }
            }
        }
    }
}
