using BLL;
using DAL;
using Microsoft.VisualBasic;
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
    public partial class frmPropostasLista : Review.frmModeloLista
    {
        private int idempresas;
        private int idpropostas;
        private int idusuarios;
        private int totalregistros;
        private int quanttotalregistros;
        private decimal quantpagina = 5;
        private decimal pagina = 1;
        private decimal totalpaginas;
        public frmPropostasLista(int codigo, int idusu)
        {
            InitializeComponent();
            this.idempresas = codigo;
            this.idusuarios = idusu;
        }

        private void frmPropostasLista_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgDados.Columns.Add(checkColumn);

            cbBuscarPor.Items.Add("Número Proposta");
            cbBuscarPor.Items.Add("Descrição");
            cbBuscarPor.Items.Add("Cliente");
            cbBuscarPor.Items.Add("Elaborada por");

            cbBuscarPor.SelectedIndex = 0;
            cbSituacao.SelectedIndex = 0;
            txtBusca_TextChanged(sender, e);

            dgDados.Columns[1].Visible = false;
            dgDados.Columns[2].HeaderText = "Proposta";
            dgDados.Columns[2].ReadOnly = true;
            dgDados.Columns[2].Width = 100;
            dgDados.Columns[3].HeaderText = "Descrição";
            dgDados.Columns[3].ReadOnly = true;
            dgDados.Columns[3].Width = 300;
            dgDados.Columns[4].HeaderText = "Cliente";
            dgDados.Columns[4].ReadOnly = true;
            dgDados.Columns[4].Width = 200;
            dgDados.Columns[5].HeaderText = "Valor";
            dgDados.Columns[5].ReadOnly = true;
            dgDados.Columns[5].Width = 200;
            dgDados.Columns[6].HeaderText = "Elaborada por";
            dgDados.Columns[6].ReadOnly = true;
            dgDados.Columns[6].Width = 200;
            dgDados.Columns[7].Visible = false;
            

        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            if ((cbBuscarPor.SelectedIndex != -1) && (cbSituacao.SelectedIndex != -1))
            {
                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLPropostas bll = new BLLPropostas(cx);
                DataTable dt = bll.Localizar(txtValor.Text, cbBuscarPor.Text, idempresas, cbSituacao.Text, idusuarios, numericUpDown1.Value, this.pagina);

                dgDados.DataSource = dt;
                this.quantpagina = numericUpDown1.Value;

                totalregistros = dt.Rows.Count;

                this.quanttotalregistros = bll.TotalRegistros(idempresas);
                totalpaginas = this.quanttotalregistros / this.quantpagina;
                label6.Text = this.pagina + " de " + Math.Ceiling(totalpaginas);
                
                foreach (DataGridViewRow item in dgDados.Rows)
                {
                    if (item.Cells[7].Value.ToString() != "1")
                    {
                        item.ReadOnly = true;
                        item.Cells[2].Style.ForeColor = Color.Gray;
                        item.Cells[3].Style.ForeColor = Color.Gray;
                        item.Cells[4].Style.ForeColor = Color.Gray;
                        item.Cells[5].Style.ForeColor = Color.Gray;
                        item.Cells[5].Value = "";
                        item.Cells[6].Style.ForeColor = Color.Gray;
                    }
                    else
                    {
                        item.Cells[5].Value = Validacao.MaskFloatMoeda(item.Cells[5].Value.ToString());
                    }
                }
            }
        }

        private void cbBuscaPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = cbBuscarPor.Text;
            txtBusca_TextChanged(sender, e);
        }

        private void cbSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusca_TextChanged(sender, e);
        }

        private void ckMarcar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgDados.Rows)
            {
                if (Convert.ToInt32(Row.Cells[0].Value) == 0)
                    Row.Cells[0].Value = 1;
                else
                    Row.Cells[0].Value = 0;
            }
        }

        private void dgDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgDados.Rows[e.RowIndex].Cells[7].Value.ToString() == "1")
                {
                    this.idpropostas = Convert.ToInt32(dgDados.Rows[e.RowIndex].Cells[1].Value);
                    frmPropostasCadastro f = new frmPropostasCadastro(this.idempresas, this.idpropostas, this.idusuarios);
                    f.ShowDialog();
                    f.Dispose();
                    this.txtBusca_TextChanged(sender, e);
                }
                else
                {
                    MessageBox.Show("Você não possui permissão de acesso a proposta "+ dgDados.Rows[e.RowIndex].Cells[2].Value + "!");
                }
            }
        }

        private void btInserir_Click(object sender, EventArgs e)
        {
            frmPropostasCadastro f = new frmPropostasCadastro(this.idempresas, this.idusuarios);
            f.ShowDialog();
            f.Dispose();
            this.txtBusca_TextChanged(sender, e);
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

                    foreach (DataGridViewRow item in dgDados.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            //Serviços
                            com = new SqlCommand("delete from serv_propostas where idpropostas=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Materiais
                            com = new SqlCommand("delete from mat_propostas where idpropostas=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Equipamentos
                            com = new SqlCommand("delete from equip_propostas where idpropostas=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Anexos
                            com = new SqlCommand("delete from anexos_propostas where idpropostas=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            //Permissões
                            com = new SqlCommand("delete from usuarios_propostas where idpropostas=" + item.Cells[1].Value, cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();

                            com = new SqlCommand("delete from propostas where idpropostas=" + item.Cells[1].Value, cx);
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

                txtBusca_TextChanged(sender, e);
            }
        }

        private void btCopia_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Deseja criar cópia do registro selecionado? Somente o primeiro registro selecionado será copiado.", "Aviso", MessageBoxButtons.YesNo);
            if (d.ToString() == "Yes")
            {
                SqlTransaction ts = null;
                SqlConnection cx = new SqlConnection(DadosConexao.StringConexao);
                SqlCommand com = null;

                try
                {


                    foreach (DataGridViewRow item in dgDados.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            cx.Open();
                            ts = cx.BeginTransaction();

                            String prop = Interaction.InputBox("Digite o número da proposta", "Proposta", item.Cells[2].Value.ToString());
                            DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
                            BLLPropostas bll = new BLLPropostas(cx2);

                            if (bll.PropostaExiste(idempresas, prop) == false)
                            {

                                com = new SqlCommand("insert into propostas (idempresas,idclientes,idorcamentos,idusuarios,num_proposta,localidade,tipo_proposta,contato,telefone," +
                    "e_mail,descricao,prazo,validade,info_contratada,info_contratante,cond_pag,garantia,observacoes,vl_servicos,vl_materiais,vl_total,situacao," +
                    "data_criacao,data_aprovacao,cod_fiscal,edital,prob_fechamento,intro_empresa,despesas_inclusas,despesas_exclusas,documentacao,premissas,acrescimos_escopo,multa," +
                    "responsaveis,leis_sociais,irrf,csll,cps,pis,cofins,iss,mes_base,ano_base,escopo,eventos_pagamento) " +
                    " select idempresas,idclientes,idorcamentos,"+idusuarios+",'" + prop + "',localidade,tipo_proposta,contato,telefone," +
                    "e_mail,descricao,prazo,validade,info_contratada,info_contratante,cond_pag,garantia,observacoes,vl_servicos,vl_materiais,vl_total,'Em Elaboração'," +
                    "data_criacao,data_aprovacao,cod_fiscal,edital,prob_fechamento,intro_empresa,despesas_inclusas,despesas_exclusas,documentacao,premissas,acrescimos_escopo,multa," +
                    "responsaveis,leis_sociais,irrf,csll,cps,pis,cofins,iss,mes_base,ano_base,escopo,eventos_pagamento from propostas where idpropostas=" + item.Cells[1].Value + "; select @@IDENTITY; ", cx);
                                com.Transaction = ts;
                                int idprop = Convert.ToInt32(com.ExecuteScalar());

                                /*
                                //Serviços
                                com = new SqlCommand("insert into serv_propostas (idpropostas,servico,valor) " +
                                " select " + idprop + ",servico,valor from serv_propostas where idpropostas=" + item.Cells[1].Value, cx);
                                com.Transaction = ts;
                                com.ExecuteNonQuery();

                                //Materiais
                                com = new SqlCommand("insert into mat_propostas (idpropostas,material,unidade,quantidade,vl_unitario,vl_total) " +
                                " select " + idprop + ",material,unidade,quantidade,vl_unitario,vl_total from mat_propostas where idpropostas=" + item.Cells[1].Value, cx);
                                com.Transaction = ts;
                                com.ExecuteNonQuery();

                                //Equipamentos
                                com = new SqlCommand("insert into equip_propostas (idpropostas,equipamento,quantidade) " +
                                " select " + idprop + ",equipamento,quantidade from equip_propostas where idpropostas=" + item.Cells[1].Value, cx);
                                com.Transaction = ts;
                                com.ExecuteNonQuery();

                                //Anexos
                                com = new SqlCommand("insert into anexos_propostas (idpropostas,descricao,anexo,nome_arquivo) " +
                                " select " + idprop + ",descricao,anexo,nome_arquivo from anexos_propostas where idpropostas=" + item.Cells[1].Value, cx);
                                com.Transaction = ts;
                                com.ExecuteNonQuery();*/

                                //Permissão ao usuário
                                com = new SqlCommand("insert into usuarios_propostas (idpropostas,idusuarios) values (" + idprop + "," + idusuarios + ")", cx);
                                com.Transaction = ts;
                                com.ExecuteNonQuery();

                                ts.Commit();
                                MessageBox.Show("Cópia criada com sucesso!");
                            }
                            else
                            {
                                ts.Rollback();
                                MessageBox.Show("Número de Proposta já existe!");
                            }

                            break;
                        }
                    }
                }
                catch (Exception erro)
                {
                    ts.Rollback();
                    MessageBox.Show("Erro ao copiar! \n\n" + erro.Message);
                }



                txtBusca_TextChanged(sender, e);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.pagina = 1;
            txtBusca_TextChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.pagina > 1)
            {
                this.pagina--;
                txtBusca_TextChanged(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.pagina <= this.totalpaginas)
            {
                this.pagina++;
                txtBusca_TextChanged(sender, e);
            }
        }
    }
}
