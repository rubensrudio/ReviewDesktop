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
    public partial class frmPlanodeContasCadastro : Review.frmModeloIncluir
    {
        private int idempresas;
        private int idplanodecontas;
        private int carrega;
        public frmPlanodeContasCadastro(int idemp)
        {
            InitializeComponent();
            this.idempresas = idemp;
        }

        public frmPlanodeContasCadastro(int idemp, int idplano)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idplanodecontas= idplano;
        }

        private void frmPlanodeContasCadastro_Load(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLPlanodeContas bll = new BLLPlanodeContas(cx);
            if (this.idplanodecontas > 0)
            {
                txtCodigo.Text = this.idplanodecontas.ToString();

                ModeloPlanodeContas modelo = bll.CarregaPlanodeContas(this.idplanodecontas);
                txtIndice.Text = modelo.Indice;
                txtDescricao.Text = modelo.Descricao;
                cbAnaliticoSintetico.Text = modelo.AnaliticoSintetico;
                if (modelo.Ativo == 0)
                {
                    cbAtivo.Text = "Não";
                }
                else
                {
                    cbAtivo.Text = "Sim";
                }
                cbDebitoCredito.Text = modelo.DebitoCredito;
                if (modelo.GerarPago == 1)
                {
                    cbGerarPago.Text = "Sim";
                }
                else
                {
                    cbGerarPago.Text = "Não";
                }
                if (modelo.Liberado == 1)
                {
                    cbLiberado.Text = "Sim";
                }
                else
                {
                    cbLiberado.Text = "Não";
                }

                cbIndicePai.ValueMember = "indice";
                cbIndicePai.DisplayMember = "descr";
                cbIndicePai.DataSource = bll.LocalizarPai(idempresas);

                cbIndicePai.SelectedValue = modelo.PlanoPai;
            }
            else
            {
                txtCodigo.Text = "";
                cbAtivo.Text = "Sim";
                cbGerarPago.Text = "Não";
                cbLiberado.Text = "Não";

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
                BLLPlanodeContas bll = new BLLPlanodeContas(cx);
                String[] vl;
                String i, i2;
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
                ModeloPlanodeContas modelo = new ModeloPlanodeContas();
                modelo.Indice = txtIndice.Text;
                if (cbIndicePai.Text != "")
                {
                    modelo.PlanoPai = cbIndicePai.SelectedValue.ToString();
                }
                else
                {
                    modelo.PlanoPai = "";
                }
                modelo.AnaliticoSintetico = cbAnaliticoSintetico.Text;
                if(cbLiberado.Text == "Sim")
                {
                    modelo.Liberado = 1;
                }
                else
                {
                    modelo.Liberado = 0;
                }
                if (cbGerarPago.Text == "Sim")
                {
                    modelo.GerarPago = 1;
                }
                else
                {
                    modelo.GerarPago = 0;
                }
                if (cbAtivo.Text == "Não")
                {
                    modelo.Ativo = 0;
                }
                else
                {
                    modelo.Ativo = 1;
                }
                modelo.DebitoCredito = cbDebitoCredito.Text;
                modelo.Descricao = txtDescricao.Text;
                modelo.IdEmpresas = this.idempresas;

                cx.Open();
                ts = cx.BeginTransaction();

                DALConexao cx1 = new DALConexao(DadosConexao.StringConexao);
                BLLPlanodeContas bll = new BLLPlanodeContas(cx1);
                if (this.idplanodecontas > 0)
                {
                    modelo.IdPlanodeContas = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                }
                else
                {
                    bll.Incluir(modelo);
                    this.idplanodecontas = modelo.IdPlanodeContas;
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
