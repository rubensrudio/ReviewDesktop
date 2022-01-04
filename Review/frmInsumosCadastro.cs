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
using Tools;

namespace Review
{
    public partial class frmInsumosCadastro : Review.frmModeloIncluir
    {
        private int idempresas;
        private int idinsumos;
        public frmInsumosCadastro(int idemp)
        {
            InitializeComponent();
            this.idempresas = idemp;
        }

        public frmInsumosCadastro(int idemp, int idins)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idinsumos = idins;
        }

        private void frmInsumosCadastro_Load(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLInsumos bll = new BLLInsumos(cx);
            BLLClassificacao bll2 = new BLLClassificacao(cx);
            BLLPlanodeContas bll3 = new BLLPlanodeContas(cx);

            cbClassificacao.ValueMember = "idclassificacao";
            cbClassificacao.DisplayMember = "descr";
            cbClassificacao.DataSource = bll2.LocalizarAnalitico(idempresas);

            cbPlanodeContas.ValueMember = "idplanosdecontas";
            cbPlanodeContas.DisplayMember = "descr";
            cbPlanodeContas.DataSource = bll3.LocalizarAnalitico(idempresas);

            if (this.idinsumos > 0)
            {
                txtCodigo.Text = this.idinsumos.ToString();

                ModeloInsumos modelo = bll.CarregaInsumos(this.idinsumos);
                txtDescricao.Text = modelo.Descricao;
                txtUnidade.Text = modelo.Unidade;
                txtDt_Atualizacao.Text = modelo.Dt_Atualizacao.ToString();
                txtPreco.Text = Validacao.MaskFloatMoeda(modelo.Preco.ToString());
                cbClassificacao.SelectedValue = modelo.IdClassificacao;
                cbPlanodeContas.SelectedValue = modelo.IdPlanosdeContas;
                cbTipo.Text = modelo.Tipo;
            }
            else
            {
                txtCodigo.Text = "";
                txtDt_Atualizacao.Text = DateTime.Now.Date.ToString();
                cbClassificacao.SelectedValue = -1;
                cbPlanodeContas.SelectedValue = -1;
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
                ModeloInsumos modelo = new ModeloInsumos();
                modelo.IdClassificacao = Convert.ToInt32(cbClassificacao.SelectedValue);
                modelo.IdPlanosdeContas = Convert.ToInt32(cbPlanodeContas.SelectedValue);
                modelo.Tipo = cbTipo.Text;
                modelo.Descricao = txtDescricao.Text;
                modelo.IdEmpresas = this.idempresas;
                modelo.Unidade = txtUnidade.Text;
                modelo.Preco = Convert.ToDouble(Validacao.MaskMoedaFloat(txtPreco.Text));
                modelo.Dt_Atualizacao = Convert.ToDateTime(txtDt_Atualizacao.Text);

                cx.Open();
                ts = cx.BeginTransaction();

                DALConexao cx1 = new DALConexao(DadosConexao.StringConexao);
                BLLInsumos bll = new BLLInsumos(cx1);
                if (this.idinsumos > 0)
                {
                    modelo.IdInsumos = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                }
                else
                {
                    bll.Incluir(modelo);
                    this.idinsumos = modelo.IdInsumos;
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

        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)22)
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') &&
               (e.KeyChar != ',' && e.KeyChar != '.' &&
                e.KeyChar != (Char)13 && e.KeyChar != (Char)8))
                {
                    e.KeyChar = (Char)0;
                }
                else
                {
                    if (e.KeyChar == '.' || e.KeyChar == ',')
                    {
                        if (!txtPreco.Text.Contains(","))
                        {
                            e.KeyChar = ',';
                        }
                        else
                        {
                            e.KeyChar = (Char)0;
                        }
                    }
                }
            }
        }

        private void txtPreco_Leave(object sender, EventArgs e)
        {
            txtPreco.Text = Validacao.MaskFloatMoeda(Validacao.MaskMoedaFloat(txtPreco.Text));
        }
    }
}
