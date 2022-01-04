using BLL;
using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Tools;

namespace Review
{
    public partial class frmEmpresasCadastro : Review.frmModeloIncluir
    {
        private int idempresas;
        private int idusuarios;
        private String Logo;
        private byte[] logob;
        public frmEmpresasCadastro(int idemp, int idusu)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idusuarios = idusu;
        }

        private void frmCadastroEmpresa_Load(object sender, EventArgs e)
        {
            if (this.idempresas != 0)
            {
                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLEmpresas bll = new BLLEmpresas(cx);
                ModeloEmpresa modelo = bll.CarregaEmpresa(this.idempresas);
                txtCodigo.Text = modelo.IdEmpresas.ToString();
                txtRazao_Social.Text = modelo.Razao_Social;
                txtNome_Fantasia.Text = modelo.Nome_Fantasia;
                txtCNPJ.Text = modelo.CNPJ;
                txtInscEst.Text = modelo.Insc_Est;
                txtEmail.Text = modelo.Email;
                txtResponsavel.Text = modelo.Responsavel;
                txtTelefone1.Text = modelo.Telefone1;
                txtTelefone2.Text = modelo.Telefone2;
                txtRamo_Atividade.Text = modelo.Ramo_Atividade;
                if (modelo.Logo != null)
                {
                    ImageConverter converter = new ImageConverter();
                    Image img = (Image)converter.ConvertFrom(modelo.Logo);
                    img = Validacao.ResizeImage(img, 200, 100);
                    pictureBox1.Image = img;
                    this.logob = modelo.Logo;
                    this.Logo = modelo.Nome_Arquivo;
                }

                DALEndereco DALObj = new DALEndereco(cx);
                ModeloEndereco modeloend = DALObj.CarregaEndereco(modelo.IdEnderecos);
                txtLogradouro.Text = modeloend.Logradouro;
                txtBairro.Text = modeloend.Bairro;
                txtCidade.Text = modeloend.Cidade;
                cbUF.Text = modeloend.UF;
                txtCEP.Text = modeloend.CEP;
                
                btUsuarios.Enabled = true;
            }
            
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloEndereco modeloEnd = new ModeloEndereco();
                modeloEnd.Logradouro = txtLogradouro.Text;
                modeloEnd.Bairro = txtBairro.Text;
                modeloEnd.Cidade = txtCidade.Text;
                modeloEnd.UF = cbUF.Text;
                modeloEnd.CEP = txtCEP.Text;

                ModeloEmpresa modelo = new ModeloEmpresa();
                modelo.Razao_Social = txtRazao_Social.Text;
                modelo.Nome_Fantasia = txtNome_Fantasia.Text;
                modelo.CNPJ = txtCNPJ.Text;
                modelo.Insc_Est = txtInscEst.Text;
                modelo.Email = txtEmail.Text;
                modelo.Responsavel = txtResponsavel.Text;
                modelo.Telefone1 = txtTelefone1.Text;
                modelo.Telefone2 = txtTelefone2.Text;
                modelo.Ramo_Atividade = txtRamo_Atividade.Text;

                if (this.logob != null)
                {
                    modelo.Logo = this.logob;
                    modelo.Nome_Arquivo = Path.GetFileName(this.Logo);
                }


                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLEmpresas bll = new BLLEmpresas(cx);
                if (this.idempresas <= 0)
                {
                    //Inserir
                    bll.Incluir(modelo, modeloEnd);
                }
                else
                {
                    //Alterar
                    modelo.IdEmpresas = Convert.ToInt32(txtCodigo.Text);
                    modeloEnd.IdEnderecos = bll.LocalizarEnd(Convert.ToInt32(txtCodigo.Text));
                    bll.Alterar(modelo, modeloEnd);
                }
                MessageBox.Show("Salvo com sucesso!");
                this.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuariosLista f = new frmUsuariosLista(Convert.ToInt32(txtCodigo.Text), idusuarios);
            f.ShowDialog();
            f.Dispose();
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

        private void btLogo_Click(object sender, EventArgs e)
        {
            try
            {
                var arquivo = EscolherArquivo();

                if (!string.IsNullOrWhiteSpace(arquivo))
                {
                    this.Logo = arquivo;
                    this.logob = File.ReadAllBytes(this.Logo);
                    Image img = Image.FromFile(arquivo);
                    img = Validacao.ResizeImage(img, 200, 100);
                    pictureBox1.Image = img;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        
    }
}
