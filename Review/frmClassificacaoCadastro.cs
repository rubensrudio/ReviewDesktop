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
    public partial class frmClassificacaoCadastro : Review.frmModeloIncluir
    {
        private int idempresas;
        private int idclassificacao;
        private int carrega;
        public frmClassificacaoCadastro(int idemp)
        {
            InitializeComponent();
            this.idempresas = idemp;
        }

        public frmClassificacaoCadastro(int idemp, int idclas)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idclassificacao = idclas;
        }

        private void frmClassificacaoCadastro_Load(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLClassificacao bll = new BLLClassificacao(cx);
            if (this.idclassificacao > 0)
            {
                txtCodigo.Text = this.idclassificacao.ToString();

                ModeloClassificacao modelo = bll.CarregaClassificacao(this.idclassificacao);
                txtIndice.Text = modelo.Indice;
                txtDescricao.Text = modelo.Descricao;
                cbAnaliticoSintetico.Text = modelo.AnaliticoSintetico;

                cbIndicePai.ValueMember = "indice";
                cbIndicePai.DisplayMember = "descr";
                cbIndicePai.DataSource = bll.LocalizarPai(idempresas);

                cbIndicePai.SelectedValue = modelo.IndicePai;
            }
            else
            {
                txtCodigo.Text = "";
                
                cbIndicePai.ValueMember = "indice";
                cbIndicePai.DisplayMember = "descr";
                cbIndicePai.DataSource = bll.LocalizarPai(idempresas);
                this.carrega = 1;
                cbIndicePai.SelectedIndex = (-1);

                cbIndicePai_SelectedIndexChanged(sender, e);
            }
        }

        private void cbIndicePai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.carrega == 1)
            {
                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLClassificacao bll = new BLLClassificacao(cx);
                String[] vl;
                String i,i2;
                int ind;
                if (cbIndicePai.SelectedIndex < 0)
                {
                    String str = bll.LocalizarFilho(idempresas);
                    vl = str.Split('.');

                    if (str != "")
                    {
                        ind = Convert.ToInt32(vl[0]) + 1;
                        if (ind.ToString().Length == 1)
                        {
                            vl[0] = "0" + ind.ToString();
                        }
                        txtIndice.Text = vl[0];
                    }
                    else
                    {
                        txtIndice.Text = "01";
                    }
                }
                else if (cbIndicePai.SelectedIndex >= 0)
                {
                    i = bll.LocalizarFilho(idempresas, cbIndicePai.SelectedValue.ToString());
                    i2 = bll.LocalizarFilho(idempresas, i);
                    vl = i.Split('.');
                    if (i2 == cbIndicePai.SelectedValue.ToString())
                    {
                        txtIndice.Text = i + ".01";
                    }
                    else
                    {
                        int tam = vl.Length - 1;
                        ind = Convert.ToInt32(vl[tam]) + 1;
                        if (ind.ToString().Length == 1)
                        {
                            vl[0] = "0" + ind.ToString();
                        }
                        int t = i.Length - 3;
                        i = i.Substring(0, t);
                        txtIndice.Text = i + "." + vl[0];
                    }
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
            try
            {
                ModeloClassificacao modelo = new ModeloClassificacao();
                modelo.Indice = txtIndice.Text;
                if (cbIndicePai.Text != "")
                {
                    modelo.IndicePai = cbIndicePai.SelectedValue.ToString();
                }
                else
                {
                    modelo.IndicePai = "";
                }
                modelo.AnaliticoSintetico = cbAnaliticoSintetico.Text;
                modelo.Descricao = txtDescricao.Text;
                modelo.IdEmpresas = this.idempresas;

                cx.Open();
                ts = cx.BeginTransaction();

                DALConexao cx1 = new DALConexao(DadosConexao.StringConexao);
                BLLClassificacao bll = new BLLClassificacao(cx1);
                if (this.idclassificacao > 0)
                {
                    modelo.IdClassificacao = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                }
                else
                {
                    bll.Incluir(modelo);
                    this.idclassificacao = modelo.IdClassificacao;
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
    }
}
