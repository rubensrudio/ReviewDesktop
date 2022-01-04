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
    public partial class frmPrincipal : Form
    {
        private ModeloUsuario modusuario;
        private frmLogin login;
        public frmPrincipal(ModeloUsuario mod, frmLogin log)
        {
            InitializeComponent();
            this.modusuario = mod;
            this.login = log;
        }

        private void LimpaMenu()
        {
            //Oculta os menus
            pnGeral.Visible = false;
            pnPessoal.Visible = false;
            pnSuprimentos.Visible = false;
            pnOrcamentos.Visible = false;
            pnFinanceiro.Visible = false;
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public Image ResizeImage(Image originalFile, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            Image fullsizeImage = originalFile;

            // Prevent using images internal thumbnail
            fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            if (onlyResizeIfWider)
            {
                if (fullsizeImage.Width <= newWidth)
                {
                    newWidth = fullsizeImage.Width;
                }
            }

            int newHeight = fullsizeImage.Height * newWidth / fullsizeImage.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead
                newWidth = fullsizeImage.Width * maxHeight / fullsizeImage.Height;
                newHeight = maxHeight;
            }

            Image newImage = fullsizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            return newImage;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            SqlConnection cx = new SqlConnection(DadosConexao.StringConexao);
            SqlCommand cmd = new SqlCommand("select nome_fantasia,logo from empresas where idempresas=" + this.modusuario.IdEmpresas.ToString(), cx);

            cx.Open();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                if (registro["logo"].ToString() != "")
                {
                    ImageConverter converter = new ImageConverter();
                    Image img = (Image)converter.ConvertFrom(registro["logo"]);
                    img = ResizeImage(img, 180, 90, true);
                    btEmpresa.Image = img;
                    btEmpresa.Text = "";
                }
                else
                {
                    btEmpresa.Text = registro["nome_fantasia"].ToString();
                }

                Permissoes(modusuario);
            }
            cx.Close();

            this.LimpaMenu();
        }

        private void Permissoes(ModeloUsuario modelo)
        {
            SqlConnection cx = new SqlConnection(DadosConexao.StringConexao);
            SqlCommand cmd = new SqlCommand("select m.idmodulos,m.modulo from modulos m join modulos_usuarios mu on mu.idmodulos=m.idmodulos where mu.idusuarios=" + modusuario.IdUsuarios, cx);
            cx.Open();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                btAlmoxarifado.Enabled = false;
                btContratos.Enabled = false;
                btFinanceiro.Enabled = false;
                btGeral.Enabled = false;
                btOrcamentos.Enabled = false;
                btPessoal.Enabled = false;
                btPlanejamento.Enabled = false;
                btQualidade.Enabled = false;
                btSuprimentos.Enabled = false;
                while (registro.Read())
                {
                    //Monta menu Almoxarifado
                    if (registro["modulo"].ToString() == "Almoxarifado")
                    {
                        btAlmoxarifado.Enabled = true;
                    }
                    //Monta menu Contratos
                    if (registro["modulo"].ToString() == "Contratos")
                    {
                        btContratos.Enabled = true;
                    }
                    //Monta menu Suprimentos
                    if (registro["modulo"].ToString() == "Suprimentos")
                    {
                        btSuprimentos.Enabled = true;

                        btClassificacao.Enabled = false;

                        SqlConnection cx2 = new SqlConnection(DadosConexao.StringConexao);
                        SqlCommand cmd2 = new SqlCommand("select im.iditens_modulos,im.item from itens_modulos im join itens_modulos_usuarios imu on imu.iditens_modulos=im.iditens_modulos where imu.idusuarios=" + modusuario.IdUsuarios + " and im.idmodulos=" + registro["idmodulos"].ToString(), cx2);
                        cx2.Open();
                        SqlDataReader registro2 = cmd2.ExecuteReader();
                        if (registro2.HasRows)
                        {
                            while (registro2.Read())
                            {
                                if (registro2["item"].ToString() == "Classificação")
                                {
                                    btClassificacao.Enabled = true;
                                }
                            }
                        }
                        cx2.Close();
                    }
                    //Monta menu Financeiro
                    if (registro["modulo"].ToString() == "Financeiro")
                    {
                        btFinanceiro.Enabled = true;

                        btPlanoContas.Enabled = false;

                        SqlConnection cx2 = new SqlConnection(DadosConexao.StringConexao);
                        SqlCommand cmd2 = new SqlCommand("select im.iditens_modulos,im.item from itens_modulos im join itens_modulos_usuarios imu on imu.iditens_modulos=im.iditens_modulos where imu.idusuarios=" + modusuario.IdUsuarios + " and im.idmodulos=" + registro["idmodulos"].ToString(), cx2);
                        cx2.Open();
                        SqlDataReader registro2 = cmd2.ExecuteReader();
                        if (registro2.HasRows)
                        {
                            while (registro2.Read())
                            {
                                if (registro2["item"].ToString() == "Plano de Contas")
                                {
                                    btPlanoContas.Enabled = true;
                                }
                            }
                        }
                        cx2.Close();
                    }
                    //Monta menu Geral
                    if (registro["modulo"].ToString() == "Geral")
                    {
                        btGeral.Enabled = true;

                        btEmpresas.Enabled = false;
                        btClientes.Enabled = false;
                        btFornecedores.Enabled = false;
                        btUsuarios.Enabled = false;
                        btCentrosCusto.Enabled = false;
                        btVersoes.Enabled = false;

                        SqlConnection cx2 = new SqlConnection(DadosConexao.StringConexao);
                        SqlCommand cmd2 = new SqlCommand("select im.iditens_modulos,im.item from itens_modulos im join itens_modulos_usuarios imu on imu.iditens_modulos=im.iditens_modulos where imu.idusuarios=" + modusuario.IdUsuarios + " and im.idmodulos=" + registro["idmodulos"].ToString(), cx2);
                        cx2.Open();
                        SqlDataReader registro2 = cmd2.ExecuteReader();
                        if (registro2.HasRows)
                        {
                            while (registro2.Read())
                            {
                                if (registro2["item"].ToString() == "Empresas")
                                {
                                    btEmpresas.Enabled = true;
                                }
                                if (registro2["item"].ToString() == "Clientes")
                                {
                                    btClientes.Enabled = true;
                                }
                                if (registro2["item"].ToString() == "Fornecedores")
                                {
                                    btFornecedores.Enabled = true;
                                }
                                if (registro2["item"].ToString() == "Usuários")
                                {
                                    btUsuarios.Enabled = true;
                                }
                                if (registro2["item"].ToString() == "Centros de Custo")
                                {
                                    btCentrosCusto.Enabled = true;
                                }
                                if (registro2["item"].ToString() == "Versões")
                                {
                                    btVersoes.Enabled = true;
                                }
                            }
                        }
                        cx2.Close();
                    }
                    //Monta menu Orçamentos
                    if (registro["modulo"].ToString() == "Orçamentos")
                    {
                        btOrcamentos.Enabled = true;

                        btPropostas.Enabled = false;

                        SqlConnection cx2 = new SqlConnection(DadosConexao.StringConexao);
                        SqlCommand cmd2 = new SqlCommand("select im.iditens_modulos,im.item from itens_modulos im join itens_modulos_usuarios imu on imu.iditens_modulos=im.iditens_modulos where imu.idusuarios=" + modusuario.IdUsuarios + " and im.idmodulos=" + registro["idmodulos"].ToString(), cx2);
                        cx2.Open();
                        SqlDataReader registro2 = cmd2.ExecuteReader();
                        if (registro2.HasRows)
                        {
                            while (registro2.Read())
                            {
                                if (registro2["item"].ToString() == "Propostas")
                                {
                                    btPropostas.Enabled = true;
                                }
                            }
                        }
                        cx2.Close();
                    }
                    //Monta menu Pessoal
                    if (registro["modulo"].ToString() == "Pessoal")
                    {
                        btPessoal.Enabled = true;

                        btFuncionarios.Enabled = false;
                        btExames.Enabled = false;
                        btTreinamentos.Enabled = false;
                        btCargos.Enabled = false;

                        SqlConnection cx2 = new SqlConnection(DadosConexao.StringConexao);
                        SqlCommand cmd2 = new SqlCommand("select im.iditens_modulos,im.item from itens_modulos im join itens_modulos_usuarios imu on imu.iditens_modulos=im.iditens_modulos where imu.idusuarios=" + modusuario.IdUsuarios + " and im.idmodulos=" + registro["idmodulos"].ToString(), cx2);
                        cx2.Open();
                        SqlDataReader registro2 = cmd2.ExecuteReader();
                        if (registro2.HasRows)
                        {
                            while (registro2.Read())
                            {
                                if (registro2["item"].ToString() == "Funcionários")
                                {
                                    btFuncionarios.Enabled = true;
                                }
                                if (registro2["item"].ToString() == "Exames")
                                {
                                    btExames.Enabled = true;
                                }
                                if (registro2["item"].ToString() == "Treinamentos")
                                {
                                    btTreinamentos.Enabled = true;
                                }
                                if (registro2["item"].ToString() == "Cargos")
                                {
                                    btCargos.Enabled = true;
                                }
                            }
                        }
                        cx2.Close();
                    }
                    //Monta menu Planejamento
                    if (registro["modulo"].ToString() == "Planejamento")
                    {
                        btPlanejamento.Enabled = true;
                    }
                    //Monta menu Qualidade
                    if (registro["modulo"].ToString() == "Qualidade")
                    {
                        btQualidade.Enabled = true;
                    }
                }

            }
            cx.Close();

            this.LimpaMenu();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Deseja sair do sistema?", "Aviso", MessageBoxButtons.YesNo);
            if (d.ToString() == "Yes")
            {
                Application.Exit();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btEmpresas_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmEmpresasLista f = new frmEmpresasLista(modusuario.IdEmpresas, modusuario.IdUsuarios);
            f.ShowDialog();
            f.Dispose();
        }

        private void btGeral_MouseHover(object sender, EventArgs e)
        {
            this.LimpaMenu();
            pnGeral.Visible = true;
            pnGeral.Dock = DockStyle.Left;
            pnGeral.Top = 0;
            pnGeral.Width = 215;
            pnGeral.Height = 170;
        }

        private void btPessoal_MouseHover(object sender, EventArgs e)
        {
            this.LimpaMenu();
            pnPessoal.Visible = true;
            pnPessoal.Dock = DockStyle.Left;
            pnPessoal.Top = 0;
            pnPessoal.Width = 215;
            pnPessoal.Height = 170;
        }

        private void frmPrincipal_MouseHover(object sender, EventArgs e)
        {
            LimpaMenu();
        }

        private void btUsuarios_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmUsuariosLista f = new frmUsuariosLista(modusuario.IdEmpresas, modusuario.IdUsuarios);
            f.ShowDialog();
            f.Dispose();
            Permissoes(modusuario);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*LimpaMenu();
            frmClienteLista f = new frmClienteLista(modusuario.IdEmpresas, "cl");
            f.ShowDialog();
            f.Dispose();*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*LimpaMenu();
            frmClienteLista f = new frmClienteLista(modusuario.IdEmpresas, "fo");
            f.ShowDialog();
            f.Dispose();*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*LimpaMenu();
            frmCentrodeCustoLista f = new frmCentrodeCustoLista(modusuario.IdEmpresas);
            f.ShowDialog();
            f.Dispose();*/
        }

        private void btSuprimentos_MouseHover(object sender, EventArgs e)
        {
            this.LimpaMenu();
            pnSuprimentos.Visible = true;
            pnSuprimentos.Dock = DockStyle.Left;
            pnSuprimentos.Top = 0;
            pnSuprimentos.Width = 215;
            pnSuprimentos.Height = 170;
        }

        private void btClassificacao_Click(object sender, EventArgs e)
        {
            /*LimpaMenu();
            frmClassificacaoLista f = new frmClassificacaoLista(modusuario.IdEmpresas);
            f.ShowDialog();
            f.Dispose();*/
        }

        private void btOrcamentos_MouseHover(object sender, EventArgs e)
        {
            this.LimpaMenu();
            pnOrcamentos.Visible = true;
            pnOrcamentos.Dock = DockStyle.Left;
            pnOrcamentos.Top = 0;
            pnOrcamentos.Width = 215;
            pnOrcamentos.Height = 170;
        }

        private void btPropostas_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmPropostasLista f = new frmPropostasLista(modusuario.IdEmpresas, modusuario.IdUsuarios);
            f.ShowDialog();
            f.Dispose();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmBancoDeDados f = new frmBancoDeDados();
            f.ShowDialog();
            f.Dispose();
        }

        private void btCargos_Click(object sender, EventArgs e)
        {
            /*LimpaMenu();
            frmCargosLista f = new frmCargosLista(modusuario.IdEmpresas);
            f.ShowDialog();
            f.Dispose();*/
        }

        private void btClientes_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmClientesLista f = new frmClientesLista(modusuario.IdEmpresas, "cl");
            f.ShowDialog();
            f.Dispose();
        }

        private void btFornecedores_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmClientesLista f = new frmClientesLista(modusuario.IdEmpresas, "fo");
            f.ShowDialog();
            f.Dispose();
        }

        private void btCentrosCusto_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmCentrosdeCustoLista f = new frmCentrosdeCustoLista(modusuario.IdEmpresas);
            f.ShowDialog();
            f.Dispose();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            login.Visible = true;
            this.Close();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBox3, "Configurações");
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBox4, "Logoff");
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBox2, "Minimizar");
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBox1, "Sair do Sistema");
        }

        private void btVersoes_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmVersoes f = new frmVersoes();
            f.ShowDialog();
            f.Dispose();
        }

        private void btClassificacao_Click_1(object sender, EventArgs e)
        {
            LimpaMenu();
            frmClassificacaoLista f = new frmClassificacaoLista(modusuario.IdEmpresas);
            f.ShowDialog();
            f.Dispose();
        }

        private void btFinanceiro_MouseHover(object sender, EventArgs e)
        {
            this.LimpaMenu();
            pnFinanceiro.Visible = true;
            pnFinanceiro.Dock = DockStyle.Left;
            pnFinanceiro.Top = 0;
            pnFinanceiro.Width = 215;
            pnFinanceiro.Height = 170;
        }

        private void btPlanoContas_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmPlanodeContasLista f = new frmPlanodeContasLista(modusuario.IdEmpresas);
            f.ShowDialog();
            f.Dispose();
        }

        private void btInsumos_Click(object sender, EventArgs e)
        {
            LimpaMenu();
            frmInsumosLista f = new frmInsumosLista(modusuario.IdEmpresas);
            f.ShowDialog();
            f.Dispose();
        }
    }
}
