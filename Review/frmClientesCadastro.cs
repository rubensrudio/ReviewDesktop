using BLL;
using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tools;

namespace Review
{
    public partial class frmClientesCadastro : Review.frmModeloIncluir
    {
        private int idclientes;
        private int idempresas;
        private String clientefornecedor;
        public frmClientesCadastro(int idemp, String clifor)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.clientefornecedor = clifor;
        }

        public frmClientesCadastro(int idemp, int idcli, String clifor)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idclientes = idcli;
            this.clientefornecedor = clifor;
        }

        private void frmClienteCadastro_Load(object sender, EventArgs e)
        {
            if (this.idclientes > 0)
            {
                txtCodigo.Text = this.idclientes.ToString();

                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLCliente bll = new BLLCliente(cx);
                ModeloCliente modelo = bll.CarregaCliente(this.idclientes);
                txtRazao_Social.Text = modelo.Razao_Social;
                txtNome_Fantasia.Text = modelo.Nome_Fantasia;
                txtCNPJ.Text = modelo.CNPJ;
                txtInscEst.Text = modelo.Insc_Est;
                txtEmail.Text = modelo.Email;
                txtResponsavel.Text = modelo.Responsavel;
                txtTelefone1.Text = modelo.Telefone1;
                txtTelefone2.Text = modelo.Telefone2;
                txtRamo_Atividade.Text = modelo.Ramo_Atividade;
                if (modelo.ClienteFornecedor == "cl")
                {
                    radioButton1.Checked = true;
                }
                else if (modelo.ClienteFornecedor == "fo")
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton3.Checked = true;
                }

                DALEndereco DALObj = new DALEndereco(cx);
                ModeloEndereco modeloend = DALObj.CarregaEndereco(modelo.IdEnderecos);
                txtLogradouro.Text = modeloend.Logradouro;
                txtBairro.Text = modeloend.Bairro;
                txtCidade.Text = modeloend.Cidade;
                cbUF.Text = modeloend.UF;
                txtCEP.Text = modeloend.CEP;
            }
            else
            {
                if (clientefornecedor == "cl")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
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

                ModeloCliente modelo = new ModeloCliente();
                modelo.Razao_Social = txtRazao_Social.Text;
                modelo.Nome_Fantasia = txtNome_Fantasia.Text;
                modelo.CNPJ = txtCNPJ.Text;
                modelo.Insc_Est = txtInscEst.Text;
                modelo.Email = txtEmail.Text;
                modelo.Responsavel = txtResponsavel.Text;
                modelo.Telefone1 = txtTelefone1.Text;
                modelo.Telefone2 = txtTelefone2.Text;
                modelo.Ramo_Atividade = txtRamo_Atividade.Text;
                modelo.IdEmpresas = this.idempresas;
                if (radioButton1.Checked)
                {
                    modelo.ClienteFornecedor = "cl";
                }
                else if (radioButton2.Checked)
                {
                    modelo.ClienteFornecedor = "fo";
                }
                else
                {
                    modelo.ClienteFornecedor = "cf";
                }

                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLCliente bll = new BLLCliente(cx);
                if (this.idclientes > 0)
                {
                    modelo.IdClientes = Convert.ToInt32(txtCodigo.Text);
                    modeloEnd.IdEnderecos = bll.LocalizarEnd(Convert.ToInt32(txtCodigo.Text));
                    bll.Alterar(modelo, modeloEnd);
                }
                else
                {
                    bll.Incluir(modelo, modeloEnd, this.idempresas);
                }
                MessageBox.Show("Salvo com sucesso!");
                this.Close();

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
    }
}
