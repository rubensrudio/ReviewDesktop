using BLL;
using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public partial class frmVersoes : Form
    {
        public frmVersoes()
        {
            InitializeComponent();
        }

        private string EscolherArquivo()
        {
            var retorno = string.Empty;

            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    retorno = dialog.FileName;
                }
            }

            return retorno;
        }

        private void btInserir_Click(object sender, EventArgs e)
        {
            try
            {
                var arquivo = EscolherArquivo();

                if (!string.IsNullOrWhiteSpace(arquivo))
                {
                    ModeloVersoes modelo = new ModeloVersoes();
                    String[] ver = txtVersao.Text.Split('.');
                    modelo.Maior = Convert.ToInt32(ver[0]);
                    modelo.Menor = Convert.ToInt32(ver[1]);
                    modelo.Build = Convert.ToInt32(ver[2]);
                    modelo.Arquivo = arquivo;
                    modelo.Nome_Arquivo = arquivo;
                    modelo.Data_Atualizacao = DateTime.Now;

                    DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                    BLLVersoes bll = new BLLVersoes(cx);

                    bll.Incluir(modelo);

                    txtVersao.Text = "";

                    DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
                    BLLVersoes bll2 = new BLLVersoes(cx2);
                    dgVersoes.DataSource = bll2.CarregaVersoes();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void dgVersoes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
                BLLVersoes bll2 = new BLLVersoes(cx2);
                bll2.AbrirVersao(Convert.ToInt32(dgVersoes.Rows[e.RowIndex].Cells[2].Value));
            }
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

                    foreach (DataGridViewRow item in dgVersoes.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            com = new SqlCommand("delete from versao where idversao=" + item.Cells[1].Value, cx);
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
                DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
                BLLVersoes bll2 = new BLLVersoes(cx2);
                dgVersoes.DataSource = bll2.CarregaVersoes();
            }
        }

        private void frmVersoes_Load(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);

            DataGridViewCheckBoxColumn checkColumn4 = new DataGridViewCheckBoxColumn();
            checkColumn4.Name = "X";
            checkColumn4.HeaderText = "";
            checkColumn4.Width = 50;
            checkColumn4.ReadOnly = false;
            checkColumn4.TrueValue = 1;
            checkColumn4.FalseValue = 0;
            dgVersoes.Columns.Add(checkColumn4);

            BLLVersoes bll8 = new BLLVersoes(cx);
            dgVersoes.DataSource = bll8.CarregaVersoes();

            dgVersoes.Columns[1].Visible = false;
            dgVersoes.Columns[2].HeaderText = "Versão";
            dgVersoes.Columns[2].ReadOnly = true;
            dgVersoes.Columns[2].Width = 100;
            dgVersoes.Columns[3].HeaderText = "Data Atualização";
            dgVersoes.Columns[3].ReadOnly = true;
            dgVersoes.Columns[3].Width = 100;
        }
    }
}
