using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Review
{
    public partial class frmBancoDeDados : Review.frmModeloIncluir
    {
        public frmBancoDeDados()
        {
            InitializeComponent();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            String servidor = DadosConexao.servidor;
            String banco = DadosConexao.banco;
            String usuario = DadosConexao.usuario;
            String senha = DadosConexao.senha;
            try
            {
                DadosConexao.servidor = txtServidor.Text;
                DadosConexao.banco = txtBanco.Text;
                DadosConexao.usuario = txtUsuario.Text;
                DadosConexao.senha = txtSenha.Text;

                //testar conexão
                SqlConnection conexao = new SqlConnection();
                conexao.ConnectionString = DadosConexao.StringConexao;
                conexao.Open();
                conexao.Close();

                StreamWriter arquivo = new StreamWriter("config.txt", false);
                arquivo.WriteLine(txtServidor.Text);
                arquivo.WriteLine(txtBanco.Text);
                arquivo.WriteLine(txtUsuario.Text);
                arquivo.WriteLine(txtSenha.Text);
                arquivo.Close();

                MessageBox.Show("Conexão no banco de dados alterada com Sucesso!");
                this.Close();
            }
            catch (SqlException errosql)
            {
                DadosConexao.servidor = servidor;
                DadosConexao.banco = banco;
                DadosConexao.usuario = usuario;
                DadosConexao.senha = senha;

                MessageBox.Show("Erro ao conectar no Banco de Dados! \n\n " + errosql.Message);
            }
            catch (Exception erro)
            {
                DadosConexao.servidor = servidor;
                DadosConexao.banco = banco;
                DadosConexao.usuario = usuario;
                DadosConexao.senha = senha;

                MessageBox.Show(erro.Message);
            }
        }

        private void frmBancoDeDados_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader arquivo = new StreamReader("config.txt");
                txtServidor.Text = arquivo.ReadLine();
                txtBanco.Text = arquivo.ReadLine();
                txtUsuario.Text = arquivo.ReadLine();
                txtSenha.Text = arquivo.ReadLine();
                arquivo.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        
    }
}
