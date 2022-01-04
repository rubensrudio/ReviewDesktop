using BLL;
using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Review
{
    public partial class frmLogin : Form
    {
        private int majVersion = 0;
        private int minVersion = 0;
        private int BuildVersion = 0;
        private int majVersionServer = 0;
        private int minVersionServer = 0;
        private int BuildVersionServer = 0;

        
        public frmLogin()
        {
            InitializeComponent();
            
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //desabilita os botões enquanto a tarefa é executada.
            //btnTarefaDeterminada.Enabled = false;
            //btnTarefaIndeterminada.Enabled = false;

            progressBar1.Visible = true;
            label4.Visible = true;
            backgroundWorker1.RunWorkerAsync();

            //define a progressBar para Marquee
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 20;

            //informa que a tarefa esta sendo executada.
            label4.Text = "Processando...";

            /*conectar();*/

            /*DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
            BLLLogin bll = new BLLLogin(cx2);
            ModeloUsuario modelo = bll.Login(textBox1.Text, textBox2.Text);
            if (modelo.IdUsuarios > 0)
            {
                frmPrincipal f = new frmPrincipal(modelo, this);
                f.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválido!");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }*/

        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        private void acesso()
        {
            try
            {
                if (File.Exists("config.txt"))
                {
                    StreamReader arquivo = new StreamReader("config.txt");
                    DadosConexao.servidor = arquivo.ReadLine();
                    DadosConexao.banco = arquivo.ReadLine();
                    DadosConexao.usuario = arquivo.ReadLine();
                    DadosConexao.senha = arquivo.ReadLine();
                    arquivo.Close();

                    //testar conexão
                    SqlConnection conexao = new SqlConnection();
                    conexao.ConnectionString = DadosConexao.StringConexao;
                    conexao.Open();
                    conexao.Close();
                }
                else
                {
                    progressBar1.Visible = false;
                    label4.Visible = false;

                    //abrir configuração
                    frmBancoDeDados f = new frmBancoDeDados();
                    f.ShowDialog();
                    f.Dispose();
                }
            }
            catch (SqlException errosql)
            {
                MessageBox.Show("Erro ao conectar no Banco de Dados! \n Verifique as configurações! \n\n " + errosql);
                progressBar1.Visible = false;
                label4.Visible = false;
                //abrir configuração
                frmBancoDeDados f = new frmBancoDeDados();
                f.ShowDialog();
                f.Dispose();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro! Verifique as configurações! \n\n " + erro.Message);
                progressBar1.Visible = false;
                label4.Visible = false;
                //abrir configuração
                frmBancoDeDados f = new frmBancoDeDados();
                f.ShowDialog();
                f.Dispose();
            }

            try
            {
                SqlConnection cx = new SqlConnection(DadosConexao.StringConexao);
                SqlCommand com = null;
                cx.Open();

                com = new SqlCommand("select arquivo,nome_arquivo,maior,menor,build from versao order by idversao desc", cx);
                var bytes = com.ExecuteScalar() as byte[];
                if (bytes != null)
                {
                    SqlDataReader registro = com.ExecuteReader();
                    registro.Read();
                    //Lê cada atributo e os armazena em variaveis.
                    majVersionServer = Convert.ToInt32(registro["maior"]);
                    minVersionServer = Convert.ToInt32(registro["menor"]);
                    BuildVersionServer = Convert.ToInt32(registro["build"]);

                    int versao = Convert.ToInt32(majVersion.ToString() + minVersion.ToString() + BuildVersion.ToString());
                    int versaoServer = Convert.ToInt32(majVersionServer.ToString() + minVersionServer.ToString() + BuildVersionServer.ToString());

                    if (versao < versaoServer)
                    {
                        MessageBox.Show("Tem uma versão mais recente que deverá ser atualizada!");

                        progressBar1.Visible = false;
                        label4.Visible = false;
                        //Implementar atualização automática

                        string nome_arquivo = registro["nome_arquivo"].ToString();
                        var arquivotemp = Path.GetTempFileName();
                        arquivotemp = Path.ChangeExtension(arquivotemp, Path.GetExtension(nome_arquivo));
                        File.WriteAllBytes(arquivotemp, bytes);
                        Process.Start(arquivotemp);
                        Application.Exit();
                    }
                }

                cx.Close();
            }
            catch (Exception erro)
            {
                progressBar1.Visible = false;
                label4.Visible = false;

                MessageBox.Show("Erro ao conectar no servidor! \n\n " + erro.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                Application.Exit();
            }
            
        }

       
        private void frmLogin_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Sair do sistema");
            toolTip1.SetToolTip(button1, "Acessar o sistema");
            toolTip1.SetToolTip(label3, "Versão atual do sistema");

            try
            {
                //Coloca o cursor em modo de espera, indicando processamento.
                Cursor.Current = Cursors.WaitCursor;
                
                try
                {
                    //Obtem os valores de versão do assembly em execução.
                    majVersion = Assembly.GetExecutingAssembly().GetName().Version.Major;
                    minVersion = Assembly.GetExecutingAssembly().GetName().Version.Minor;
                    BuildVersion = Assembly.GetExecutingAssembly().GetName().Version.Build;

                    label3.Text = "Versão: " + majVersion + "." + minVersion + "." + BuildVersion;
                }
                catch
                {
                    MessageBox.Show("Não foi possível determinar a versão da aplicação.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    Application.Exit();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                Application.Exit();
            }
            finally
            {
                //Coloca o cursor em modo default, indicando fim do processamento.
                Cursor.Current = Cursors.Default;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void TarefaLonga(int p)
        {
            /*for (int i = 0; i <= 10; i++)
            {
                // faz a thread dormir por "p" milissegundos a cada passagem do loop
                Thread.Sleep(p);
                label5.BeginInvoke(
                   new Action(() =>
                   {
                       label5.Text = "Tarefa: " + i.ToString() + " concluída";
                   }
                ));
            }*/
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //executa a tarefa a primeira vez
            /*TarefaLonga(100);
            
            //Verifica se houve uma requisição para cancelar a operação.
            if (backgroundWorker1.CancellationPending)
            {
                //se sim, define a propriedade Cancel para true
                //para que o evento WorkerCompleted saiba que a tarefa foi cancelada.
                e.Cancel = true;
                return;
            }*/

            //executa a tarefa pela segunda vez
            //TarefaLonga(500);
            acesso();
            if (backgroundWorker1.CancellationPending)
            {
                //se sim, define a propriedade Cancel para true
                //para que o evento WorkerCompleted saiba que a tarefa foi cancelada.
                e.Cancel = true;
                return;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Caso cancelado...
            if (e.Cancelled)
            {
                // reconfigura a progressbar para o padrao.
                progressBar1.MarqueeAnimationSpeed = 0;
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;

                //caso a operação seja cancelada, informa ao usuario.
                label4.Text = "Operação Cancelada pelo Usuário!";

                //habilita o botao cancelar
                //btnCancelar.Enabled = true;
                //limpa a label
                label4.Text = string.Empty;
            }
            else if (e.Error != null)
            {
                //informa ao usuario do acontecimento de algum erro.
                label4.Text = "Aconteceu um erro durante a execução do processo!";

                // reconfigura a progressbar para o padrao.
                progressBar1.MarqueeAnimationSpeed = 0;
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
            }
            else
            {
                //informa que a tarefa foi concluida com sucesso.
                label4.Text = "Tarefa Concluida com sucesso!";

                //Carrega todo progressbar.
                progressBar1.MarqueeAnimationSpeed = 0;
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                //label4.Text = progressBar1.Value.ToString() + "%";
            }
            //habilita os botões.
            //btnTarefaDeterminada.Enabled = true;
            //btnTarefaIndeterminada.Enabled = true;

            DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
            BLLLogin bll = new BLLLogin(cx2);
            ModeloUsuario modelo = bll.Login(textBox1.Text, textBox2.Text);
            if (modelo.IdUsuarios > 0)
            {
                frmPrincipal f = new frmPrincipal(modelo, this);
                f.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválido!");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
        }

    }
}
