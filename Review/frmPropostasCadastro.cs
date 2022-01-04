using BLL;
using DAL;
using Modelo;
using Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Tools;

namespace Review
{
    public partial class frmPropostasCadastro : Review.frmModeloIncluir
    {
        private int idpropostas;
        private int idempresas;
        private int idusuarios;
        private int idclientes;
        private double vl_servicos;
        private String situacao;
        private String num_prop;
        private int id_mat, id_serv, id_equip;

        public frmPropostasCadastro(int idemp, int idusu)
        {
            InitializeComponent();

            this.idempresas = idemp;
            this.idusuarios = idusu;
        }

        public frmPropostasCadastro(int idemp, int idprop, int idusu)
        {
            InitializeComponent();

            this.idpropostas = idprop;
            this.idempresas = idemp;
            this.idusuarios = idusu;
        }

        private void frmPropostasCadastro_Load(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLPropostas bll = new BLLPropostas(cx);
            BLLCliente bll2 = new BLLCliente(cx);
            BLLEmpresas bll3 = new BLLEmpresas(cx);
            BLLUsuarios bll4 = new BLLUsuarios(cx);
            ModeloPropostas modelo = bll.CarregaPropostas(this.idpropostas);
            ModeloEmpresa modeloemp = bll3.CarregaEmpresa(this.idempresas);

            id_mat = 0;
            id_equip = 0;
            id_serv = 0;
            
            if (this.idpropostas > 0)
            {
                ModeloCliente modelocli = bll2.CarregaCliente(modelo.IdClientes);
                this.idclientes = modelo.IdClientes;
                ModeloUsuario modelousuario = bll4.CarregaUsuario(modelo.IdUsuarios);
                txtResponsavel.Text = modelousuario.Nome;
                txtCodigo.Text = modelo.IdPropostas.ToString();
                txtCliente.Text = modelocli.Nome_Fantasia;
                txtEmpresa.Text = modeloemp.Nome_Fantasia.ToString();
                cbSituacao.Text = modelo.Situacao.ToString();
                txtDescricao.Text = modelo.Descricao.ToString();
                txtNum_Proposta.Text = modelo.Num_Proposta.ToString();
                this.num_prop = modelo.Num_Proposta.ToString();
                txtLocalidade.Text = modelo.Localidade.ToString();
                cbTipo_Proposta.Text = modelo.Tipo_Proposta.ToString();
                txtContato.Text = modelo.Contato.ToString();
                txtTelefone.Text = modelo.Telefone.ToString();
                txtE_Mail.Text = modelo.E_Mail.ToString();
                txtPrazo.Text = modelo.Prazo.ToString();
                txtValidade.Text = modelo.Validade.ToString();
                txtInfo_Contratada.Text = modelo.Info_Contratada.ToString();
                txtInfo_Contratante.Text = modelo.Info_Contratante.ToString();
                txtCond_Pag.Text = modelo.Cond_Pag.ToString();
                txtGarantia.Text = modelo.Garantia.ToString();
                txtObservacoes.Text = modelo.Observacoes.ToString();
                txtIntroEmpresa.Text = modelo.IntroEmpresa.ToString();
                txtDespesasInclusas.Text = modelo.DespesasInclusas.ToString();
                txtDespesasExclusas.Text = modelo.DespesasExclusas.ToString();
                txtDocumentacao.Text = modelo.Documentacao.ToString();
                txtEscopo.Text = modelo.Escopo.ToString();
                txtEventosPagamento.Text = modelo.Eventos_Pagamento.ToString();
                txtPremissas.Text = modelo.Premissas.ToString();
                txtAcrescimosEscopo.Text = modelo.AcrescimosEscopo.ToString();
                txtEdital.Text = modelo.Edital.ToString();
                txtMulta.Text = modelo.Multa.ToString();
                cbProb_Fechamento.Text = modelo.Prob_Fechamento.ToString();
                txtResponsaveis.Text = modelo.Responsaveis.ToString();
                txtCodFiscal.Text = modelo.Cod_Fiscal.ToString();
                txtLeis_Sociais.Text = Validacao.MaskFloatMoeda2(modelo.Leis_Sociais.ToString());
                txtCPS.Text = Validacao.MaskFloatMoeda2(modelo.CPS.ToString());
                txtCSLL.Text = Validacao.MaskFloatMoeda2(modelo.CSLL.ToString());
                txtIRRF.Text = Validacao.MaskFloatMoeda2(modelo.IRRF.ToString());
                txtPIS.Text = Validacao.MaskFloatMoeda2(modelo.PIS.ToString());
                txtCOFINS.Text = Validacao.MaskFloatMoeda2(modelo.COFINS.ToString());
                txtISS.Text = Validacao.MaskFloatMoeda2(modelo.ISS.ToString());
                txtTotal_Material.Text = Validacao.MaskFloatMoeda(modelo.Vl_Materiais.ToString());
                txtTotal_Servico.Text = Validacao.MaskFloatMoeda(modelo.Vl_Servicos.ToString());
                txtVl_Material.Text = Validacao.MaskFloatMoeda(modelo.Vl_Materiais.ToString());
                txtVl_Servico.Text = Validacao.MaskFloatMoeda(modelo.Vl_Servicos.ToString());
                cbMes_Base.Text = modelo.Mes_Base;
                txtAno_Base.Text = Convert.ToString(modelo.Ano_Base);

                this.vl_servicos = modelo.Vl_Servicos;
                txtVl_Total.Text = Validacao.MaskFloatMoeda(modelo.Vl_Total.ToString());
                if ((modelo.Situacao == "Arquivada") || (modelo.Situacao == "Aprovada") || (modelo.Situacao == "Aguardando"))
                {                    
                    txtDescricao.Enabled = false;
                    txtCliente.Enabled = false;
                    txtNum_Proposta.Enabled = false;
                    txtLocalidade.Enabled = false;
                    cbTipo_Proposta.Enabled = false;
                    txtContato.Enabled = false;
                    txtTelefone.Enabled = false;
                    txtE_Mail.Enabled = false;
                    txtPrazo.Enabled = false;
                    txtValidade.Enabled = false;
                    txtInfo_Contratada.Enabled = false;
                    txtInfo_Contratante.Enabled = false;
                    txtCond_Pag.Enabled = false;
                    txtGarantia.Enabled = false;
                    txtObservacoes.Enabled = false;
                    txtIntroEmpresa.Enabled = false;
                    txtDespesasInclusas.Enabled = false;
                    txtDespesasExclusas.Enabled = false;
                    txtEscopo.Enabled = false;
                    txtEventosPagamento.Enabled = false;
                    txtDocumentacao.Enabled = false;
                    txtPremissas.Enabled = false;
                    txtAcrescimosEscopo.Enabled = false;
                    txtEdital.Enabled = false;
                    txtMulta.Enabled = false;
                    cbProb_Fechamento.Enabled = false;
                    txtResponsaveis.Enabled = false;
                    txtCodFiscal.Enabled = false;
                    txtLeis_Sociais.Enabled = false;
                    txtCSLL.Enabled = false;
                    txtCPS.Enabled = false;
                    txtIRRF.Enabled = false;
                    txtPIS.Enabled = false;
                    txtCOFINS.Enabled = false;
                    txtISS.Enabled = false;
                    cbMes_Base.Enabled = false;
                    txtAno_Base.Enabled = false;
                    if (modelo.Situacao != "Aguardando")
                    {
                        cbSituacao.Enabled = false;
                        btSalvar.Enabled = false;
                    }
                    btIncluirMat.Enabled = false;
                    btExcluirMat.Enabled = false;
                    btIncluirServ.Enabled = false;
                    btExcluirServ.Enabled = false;
                    btBuscaCliente.Enabled = false;
                    btIncluirAnexo.Enabled = false;
                    btExcluirAnexo.Enabled = false;
                    btIncluirEquipamento.Enabled = false;
                    btExcluirEquipamento.Enabled = false;
                    btImpEquip.Enabled = false;
                    btImpMat.Enabled = false;
                    btImpServ.Enabled = false;
                }

            }
            else
            {
                txtCodigo.Text = "";
                cbSituacao.Text = "Em Elaboração";
                cbSituacao.Enabled = false;
                cbProb_Fechamento.Text = "Média";
                cbTipo_Proposta.Text = "Empreitada";
                txtTotal_Material.Text = "R$0,00";
                txtTotal_Servico.Text = "R$0,00";
                txtVl_Material.Text = "R$0,00";
                txtVl_Servico.Text = "R$0,00";
                if (DateTime.Now.Month == 1)
                {
                    cbMes_Base.Text = "Janeiro";
                }
                else if (DateTime.Now.Month == 2)
                {
                    cbMes_Base.Text = "Fevereiro";
                }
                else if (DateTime.Now.Month == 3)
                {
                    cbMes_Base.Text = "Março";
                }
                else if (DateTime.Now.Month == 4)
                {
                    cbMes_Base.Text = "Abril";
                }
                else if (DateTime.Now.Month == 5)
                {
                    cbMes_Base.Text = "Maio";
                }
                else if (DateTime.Now.Month == 6)
                {
                    cbMes_Base.Text = "Junho";
                }
                else if (DateTime.Now.Month == 7)
                {
                    cbMes_Base.Text = "Julho";
                }
                else if (DateTime.Now.Month == 8)
                {
                    cbMes_Base.Text = "Agosto";
                }
                else if (DateTime.Now.Month == 9)
                {
                    cbMes_Base.Text = "Setembro";
                }
                else if (DateTime.Now.Month == 10)
                {
                    cbMes_Base.Text = "Outubro";
                }
                else if (DateTime.Now.Month == 11)
                {
                    cbMes_Base.Text = "Novembro";
                }
                else if (DateTime.Now.Month == 12)
                {
                    cbMes_Base.Text = "Dezembro";
                }
                txtAno_Base.Text = Convert.ToString(DateTime.Now.Year);
                ModeloUsuario modelousuario = bll4.CarregaUsuario(this.idusuarios);
                txtResponsavel.Text = modelousuario.Nome;
                txtEmpresa.Text = modeloemp.Nome_Fantasia.ToString();
            }

            this.situacao = cbSituacao.Text;

            //ServiÃ§os
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgServico.Columns.Add(checkColumn);

            AtualizaServicos();

            dgServico.Columns[1].Visible = false;
            dgServico.Columns[2].HeaderText = "Serviço";
            dgServico.Columns[2].ReadOnly = true;
            dgServico.Columns[2].Width = 300;
            dgServico.Columns[3].HeaderText = "Un";
            dgServico.Columns[3].ReadOnly = true;
            dgServico.Columns[3].Width = 50;
            dgServico.Columns[4].HeaderText = "Quant.";
            dgServico.Columns[4].ReadOnly = true;
            dgServico.Columns[4].Width = 50;
            dgServico.Columns[5].HeaderText = "Valor Un.";
            dgServico.Columns[5].ReadOnly = true;
            dgServico.Columns[5].Width = 100;
            dgServico.Columns[6].HeaderText = "Valor Total";
            dgServico.Columns[6].ReadOnly = true;
            dgServico.Columns[6].Width = 100;
            dgServico.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Materiais
            DataGridViewCheckBoxColumn checkColumn2 = new DataGridViewCheckBoxColumn();
            checkColumn2.Name = "X";
            checkColumn2.HeaderText = "";
            checkColumn2.Width = 50;
            checkColumn2.ReadOnly = false;
            checkColumn2.TrueValue = 1;
            checkColumn2.FalseValue = 0;
            dgMateriais.Columns.Add(checkColumn2);

            AtualizaMateriais();

            dgMateriais.Columns[1].Visible = false;
            dgMateriais.Columns[2].HeaderText = "Material";
            dgMateriais.Columns[2].ReadOnly = true;
            dgMateriais.Columns[2].Width = 300;
            dgMateriais.Columns[3].HeaderText = "Un";
            dgMateriais.Columns[3].ReadOnly = true;
            dgMateriais.Columns[3].Width = 50;
            dgMateriais.Columns[4].HeaderText = "Quant.";
            dgMateriais.Columns[4].ReadOnly = true;
            dgMateriais.Columns[4].Width = 50;
            dgMateriais.Columns[5].HeaderText = "Valor Un.";
            dgMateriais.Columns[5].ReadOnly = true;
            dgMateriais.Columns[5].Width = 100;
            dgMateriais.Columns[6].HeaderText = "Valor Total";
            dgMateriais.Columns[6].ReadOnly = true;
            dgMateriais.Columns[6].Width = 100;

            //Equipamentos / Recursos
            DataGridViewCheckBoxColumn checkColumn3 = new DataGridViewCheckBoxColumn();
            checkColumn3.Name = "X";
            checkColumn3.HeaderText = "";
            checkColumn3.Width = 50;
            checkColumn3.ReadOnly = false;
            checkColumn3.TrueValue = 1;
            checkColumn3.FalseValue = 0;
            dgEquipamentos.Columns.Add(checkColumn3);

            BLLEquipamentos bll7 = new BLLEquipamentos(cx);
            dgEquipamentos.DataSource = bll7.CarregaEquipamentos(idpropostas);

            dgEquipamentos.Columns[1].Visible = false;
            dgEquipamentos.Columns[2].HeaderText = "Equipamentos / Recursos";
            dgEquipamentos.Columns[2].ReadOnly = true;
            dgEquipamentos.Columns[2].Width = 500;
            dgEquipamentos.Columns[3].HeaderText = "Quantidade";
            dgEquipamentos.Columns[3].ReadOnly = true;
            dgEquipamentos.Columns[3].Width = 100;
            dgEquipamentos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Anexos
            DataGridViewCheckBoxColumn checkColumn4 = new DataGridViewCheckBoxColumn();
            checkColumn4.Name = "X";
            checkColumn4.HeaderText = "";
            checkColumn4.Width = 50;
            checkColumn4.ReadOnly = false;
            checkColumn4.TrueValue = 1;
            checkColumn4.FalseValue = 0;
            dgAnexos.Columns.Add(checkColumn4);

            DataGridViewCheckBoxColumn checkColumn6 = new DataGridViewCheckBoxColumn();
            checkColumn6.Name = "Técnica";
            checkColumn6.HeaderText = "Técnica";
            checkColumn6.Width = 50;
            checkColumn6.ReadOnly = false;
            checkColumn6.TrueValue = 1;
            checkColumn6.FalseValue = 0;
            dgAnexos.Columns.Add(checkColumn6);

            DataGridViewCheckBoxColumn checkColumn7 = new DataGridViewCheckBoxColumn();
            checkColumn7.Name = "Comercial";
            checkColumn7.HeaderText = "Comercial";
            checkColumn7.Width = 60;
            checkColumn7.ReadOnly = false;
            checkColumn7.TrueValue = 1;
            checkColumn7.FalseValue = 0;
            dgAnexos.Columns.Add(checkColumn7);

            BLLAnexos bll8 = new BLLAnexos(cx);
            dgAnexos.DataSource = bll8.CarregaAnexos(idpropostas);

            dgAnexos.Columns[3].Visible = false;
            dgAnexos.Columns[4].Visible = false;
            dgAnexos.Columns[5].Visible = false;
            dgAnexos.Columns[6].HeaderText = "Descrição do Anexo";
            dgAnexos.Columns[6].ReadOnly = true;
            dgAnexos.Columns[6].Width = 500;
            dgAnexos.Columns[7].Visible = false;
            
                        

            //Grid Permissões
            DataGridViewCheckBoxColumn checkColumn5 = new DataGridViewCheckBoxColumn();
            checkColumn5.Name = "X";
            checkColumn5.HeaderText = "";
            checkColumn5.Width = 50;
            checkColumn5.ReadOnly = false;
            checkColumn5.TrueValue = 1;
            checkColumn5.FalseValue = 0;
            dgUsuarios.Columns.Add(checkColumn5);

            BLLPropostasPermissoes bll9 = new BLLPropostasPermissoes(cx);
            dgUsuarios.DataSource = bll9.Localizar(idempresas, idusuarios, idpropostas);

            dgUsuarios.Columns[0].Visible = true;
            dgUsuarios.Columns[1].Visible = false;
            dgUsuarios.Columns[2].HeaderText = "Usuário";
            dgUsuarios.Columns[2].ReadOnly = true;
            dgUsuarios.Columns[2].Width = 100;
            dgUsuarios.Columns[3].HeaderText = "Nome";
            dgUsuarios.Columns[3].ReadOnly = true;
            dgUsuarios.Columns[3].Width = 150;
            dgUsuarios.Columns[4].Visible = false;

            

        }

        private void btBuscaCliente_Click(object sender, EventArgs e)
        {
            frmClienteConsulta f = new frmClienteConsulta(this.idempresas, "cl");
            f.ShowDialog();
            this.idclientes = f.ConsultaCliente();
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLCliente bll = new BLLCliente(cx);
            ModeloCliente modelocliente = bll.CarregaCliente(this.idclientes);
            txtCliente.Text = modelocliente.Nome_Fantasia;
            f.Dispose();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            SqlConnection cx2 = new SqlConnection(DadosConexao.StringConexao);
            SqlTransaction ts = null;
            SqlCommand com, com2, com3;

            try
            {
                cx2.Open();
                ts = cx2.BeginTransaction();

                ModeloPropostas modelo = new ModeloPropostas();
                modelo.IdEmpresas = this.idempresas;
                modelo.IdClientes = this.idclientes;
                //modelo.IdOrcamentos = Convert.ToInt32(registro["idorcamentos"]);
                modelo.Num_Proposta = txtNum_Proposta.Text;
                modelo.Localidade = txtLocalidade.Text;
                modelo.Tipo_Proposta = cbTipo_Proposta.Text;
                modelo.Contato = txtContato.Text;
                modelo.Telefone = txtTelefone.Text;
                modelo.E_Mail = txtE_Mail.Text;
                modelo.Descricao = txtDescricao.Text;
                if (txtPrazo.Text != "")
                {
                    modelo.Prazo = Convert.ToInt32(txtPrazo.Text);
                }
                if (txtValidade.Text != "")
                {
                    modelo.Validade = Convert.ToInt32(txtValidade.Text);
                }
                modelo.Info_Contratada = txtInfo_Contratada.Text;
                modelo.Info_Contratante = txtInfo_Contratante.Text;
                modelo.Cond_Pag = txtCond_Pag.Text;
                modelo.Garantia = txtGarantia.Text;
                modelo.Observacoes = txtObservacoes.Text;
                modelo.IntroEmpresa = txtIntroEmpresa.Text;
                modelo.DespesasInclusas = txtDespesasInclusas.Text;
                modelo.DespesasExclusas = txtDespesasExclusas.Text;
                modelo.Escopo = txtEscopo.Text;
                modelo.Eventos_Pagamento = txtEventosPagamento.Text;
                modelo.Documentacao = txtDocumentacao.Text;
                modelo.Premissas = txtPremissas.Text;
                modelo.AcrescimosEscopo = txtAcrescimosEscopo.Text;
                modelo.Cod_Fiscal = txtCodFiscal.Text;
                modelo.Edital = txtEdital.Text;
                modelo.Multa = txtMulta.Text;
                modelo.Prob_Fechamento = cbProb_Fechamento.Text;
                modelo.Responsaveis = txtResponsaveis.Text;
                if (txtVl_Servico.Text != "")
                {
                    modelo.Vl_Servicos = Convert.ToDouble(Validacao.MaskMoedaFloat(txtVl_Servico.Text));
                }
                if (txtVl_Material.Text != "")
                {
                    modelo.Vl_Materiais = Convert.ToDouble(Validacao.MaskMoedaFloat(txtVl_Material.Text));
                }
                if (txtVl_Total.Text != "")
                {
                    modelo.Vl_Total = Convert.ToDouble(Validacao.MaskMoedaFloat(txtVl_Total.Text));
                }
                modelo.Situacao = cbSituacao.Text;
                if ((this.situacao == "Aguardando") && (cbSituacao.Text == "Aprovada"))
                {
                    modelo.Data_Aprovacao = DateTime.Now;
                }
                if (txtLeis_Sociais.Text != "")
                {
                    modelo.Leis_Sociais = Convert.ToDouble(Validacao.MaskMoedaFloat(txtLeis_Sociais.Text));
                }
                if (txtIRRF.Text != "")
                {
                    modelo.IRRF = Convert.ToDouble(Validacao.MaskMoedaFloat(txtIRRF.Text));
                }
                if (txtCSLL.Text != "")
                {
                    modelo.CSLL = Convert.ToDouble(Validacao.MaskMoedaFloat(txtCSLL.Text));
                }
                if (txtCPS.Text != "")
                {
                    modelo.CPS = Convert.ToDouble(Validacao.MaskMoedaFloat(txtCPS.Text));
                }
                if (txtPIS.Text != "")
                {
                    modelo.PIS = Convert.ToDouble(Validacao.MaskMoedaFloat(txtPIS.Text));
                }
                if (txtCOFINS.Text != "")
                {
                    modelo.COFINS = Convert.ToDouble(Validacao.MaskMoedaFloat(txtCOFINS.Text));
                }
                if (txtISS.Text != "")
                {
                    modelo.ISS = Convert.ToDouble(Validacao.MaskMoedaFloat(txtISS.Text));
                }
                modelo.Mes_Base = cbMes_Base.Text;
                modelo.Ano_Base = Convert.ToInt32(txtAno_Base.Text);


                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLPropostas bll = new BLLPropostas(cx);
                BLLAnexos bll2 = new BLLAnexos(cx);

                if ((this.num_prop == modelo.Num_Proposta) || (bll.PropostaExiste(idempresas, modelo.Num_Proposta) == false))
                {
                    if (this.idpropostas > 0)
                    {
                        ModeloPropostas mpro = new ModeloPropostas();
                        mpro = bll.CarregaPropostas(this.idpropostas);

                        modelo.IdUsuarios = mpro.IdUsuarios;
                        modelo.IdPropostas = Convert.ToInt32(txtCodigo.Text);
                        bll.Alterar(modelo);
                    }
                    else
                    {
                        modelo.IdUsuarios = this.idusuarios;
                        modelo.Data_Criacao = DateTime.Now;

                        bll.Incluir(modelo);

                        this.idpropostas = modelo.IdPropostas;
                        txtCodigo.Text = this.idpropostas.ToString();
                        this.num_prop = modelo.Num_Proposta;
                    }

                    int tec, comer;
                    foreach (DataGridViewRow item in dgAnexos.Rows)
                    {
                        tec = 0;
                        comer = 0;
                        if (Convert.ToInt32(item.Cells[1].Value) == 1)
                        {
                            tec = 1;
                        }

                        if (Convert.ToInt32(item.Cells[2].Value) == 1)
                        {
                            comer = 1;
                        }

                        com2 = new SqlCommand("update anexos_propostas set tecnica=" + tec + ", comercial=" + comer + " where idanexos="+ item.Cells[3].Value, cx2);
                        com2.Transaction = ts;
                        com2.ExecuteNonQuery();
                    }

                    

                    foreach (DataGridViewRow item in dgUsuarios.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            com3 = new SqlCommand("select * from usuarios_propostas where idusuarios=" + item.Cells[1].Value.ToString() + " and idpropostas=" + idpropostas.ToString(), cx2);
                            com3.Transaction = ts;
                            SqlDataReader registro2 = com3.ExecuteReader();
                            if (registro2.HasRows == false)
                            {
                                registro2.Close();
                                //Inclui permissões em módulos
                                com2 = new SqlCommand("insert into usuarios_propostas (idpropostas,idusuarios) values (" + idpropostas.ToString() + "," + Convert.ToInt32(item.Cells[1].Value) + ")", cx2);
                                com2.Transaction = ts;
                                com2.ExecuteNonQuery();
                            }
                            else
                            {
                                registro2.Close();
                            }
                        }
                        else
                        {
                            //Exclui permissões em módulos
                            com = new SqlCommand("delete from usuarios_propostas where idusuarios=" + Convert.ToInt32(item.Cells[1].Value) + " and idpropostas=" + idpropostas.ToString(), cx2);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();
                        }
                    }

                    com3 = new SqlCommand("select * from usuarios_propostas where idusuarios=" + modelo.IdUsuarios + " and idpropostas=" + modelo.IdPropostas, cx2);
                    com3.Transaction = ts;
                    SqlDataReader registro = com3.ExecuteReader();
                    if (registro.HasRows == false)
                    {
                        registro.Close();
                        //Inclui permissões em módulos
                        com2 = new SqlCommand("insert into usuarios_propostas (idpropostas,idusuarios) values (" + modelo.IdPropostas + "," + Convert.ToInt32(modelo.IdUsuarios) + ")", cx2);
                        com2.Transaction = ts;
                        com2.ExecuteNonQuery();
                    }
                    else
                    {
                        registro.Close();
                    }

                    ts.Commit();
                    cx2.Close();

                    MessageBox.Show("Salvo com sucesso!");

                    dgAnexos.DataSource = bll2.CarregaAnexos(idpropostas);
                    foreach (DataGridViewRow item in dgAnexos.Rows)
                    {
                        if (item.Cells[4].Value.ToString() == "1")
                        {
                            item.Cells[1].Value = 1;
                        }

                        if (item.Cells[5].Value.ToString() == "1")
                        {
                            item.Cells[2].Value = 1;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Número da proposta já existe!");
                    ts.Rollback();
                    cx2.Close();
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
                ts.Rollback();
                cx2.Close();
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btIncluirServ_Click(object sender, EventArgs e)
        {
            
            try
            {
                ModeloServicos modelo = new ModeloServicos();
                modelo.Servico = txtDescServico.Text;
                modelo.Unidade = txtUnServicos.Text;
                if (txtValorServico.Text != "")
                {
                    modelo.Valor = Convert.ToDouble(Validacao.MaskMoedaFloat(txtValorServico.Text));
                }

                if (txtQuantServicos.Text != "")
                {
                    modelo.Quantidade = Convert.ToDouble(txtQuantServicos.Text);
                }

                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLServicos bll = new BLLServicos(cx);
                if (this.idpropostas > 0)
                {
                    modelo.IdPropostas = this.idpropostas;
                    if ((txtValorServico.Text != "") && (txtQuantServicos.Text != ""))
                    {
                        modelo.Vl_Total = Convert.ToDouble(txtQuantServicos.Text) * Convert.ToDouble(Validacao.MaskMoedaFloat(txtValorServico.Text));
                    }

                    if (id_serv == 0)
                    {
                        bll.Incluir(modelo);
                    }
                    else
                    {
                        modelo.IdServ_Propostas = id_serv;
                        bll.Alterar(modelo);
                    }
                    txtDescServico.Text = "";
                    txtValorServico.Text = "";
                    txtUnServicos.Text = "";
                    txtQuantServicos.Text = "";
                    AtualizaServicos();

                    id_serv = 0;
                }
                else
                {
                    MessageBox.Show("Para incluir um serviço salve primeiro a proposta!");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void AtualizaServicos()
        {
            Double valor = 0;
            DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
            BLLServicos bll2 = new BLLServicos(cx2);
            dgServico.DataSource = bll2.CarregaServicos(idpropostas);

            foreach (DataGridViewRow item in dgServico.Rows)
            {
                valor = valor + Convert.ToDouble(Validacao.MaskMoedaFloat(Validacao.MaskFloatMoeda(item.Cells[6].Value.ToString())));

                item.Cells[5].Value = Validacao.MaskFloatMoeda(item.Cells[5].Value.ToString());
                item.Cells[6].Value = Validacao.MaskFloatMoeda(item.Cells[6].Value.ToString());
            }

            txtTotal_Servico.Text = Validacao.MaskFloatMoeda(valor.ToString());
            txtVl_Servico.Text = txtTotal_Servico.Text;
            txtVl_Total.Text = Validacao.MaskFloatMoeda(Convert.ToString(Convert.ToDouble(Validacao.MaskMoedaFloat(txtVl_Material.Text)) + Convert.ToDouble(Validacao.MaskMoedaFloat(txtVl_Servico.Text))));
        }

        private void AtualizaMateriais()
        {
            Double valor = 0;
            DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
            BLLMateriais bll2 = new BLLMateriais(cx2);
            dgMateriais.DataSource = bll2.CarregaMateriais(idpropostas);

            foreach (DataGridViewRow item in dgMateriais.Rows)
            {
                valor = valor + Convert.ToDouble(Validacao.MaskMoedaFloat(Validacao.MaskFloatMoeda(item.Cells[6].Value.ToString())));

                item.Cells[5].Value = Validacao.MaskFloatMoeda(item.Cells[5].Value.ToString());
                item.Cells[6].Value = Validacao.MaskFloatMoeda(item.Cells[6].Value.ToString());
            }

            txtTotal_Material.Text = Validacao.MaskFloatMoeda(valor.ToString());
            txtVl_Material.Text = txtTotal_Material.Text;
            txtVl_Total.Text = Validacao.MaskFloatMoeda(Convert.ToString(Convert.ToDouble(Validacao.MaskMoedaFloat(txtVl_Material.Text)) + Convert.ToDouble(Validacao.MaskMoedaFloat(txtVl_Servico.Text))));
        }

        private void btExcluirServ_Click(object sender, EventArgs e)
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

                    foreach (DataGridViewRow item in dgServico.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            com = new SqlCommand("delete from serv_propostas where idserv_propostas=" + item.Cells[1].Value, cx);
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

                AtualizaServicos();
            }
        }

        private void btIncluirMat_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloMateriais modelo = new ModeloMateriais();
                modelo.Material = txtDescMaterial.Text;
                modelo.Unidade = txtUnMaterial.Text;

                if (txtValorMaterial.Text != "")
                {
                    modelo.Vl_Unitario = Convert.ToDouble(Validacao.MaskMoedaFloat(txtValorMaterial.Text));
                }
                if (txtQuantMaterial.Text != "")
                {
                    modelo.Quantidade = Convert.ToDouble(txtQuantMaterial.Text);
                }

                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLMateriais bll = new BLLMateriais(cx);
                if (this.idpropostas > 0)
                {
                    modelo.IdPropostas = this.idpropostas;
                    if (id_mat == 0)
                    {
                        if ((txtValorMaterial.Text != "") && (txtQuantMaterial.Text != ""))
                        {
                            modelo.Vl_Total = Convert.ToDouble(txtQuantMaterial.Text) * Convert.ToDouble(Validacao.MaskMoedaFloat(txtValorMaterial.Text));
                        }
                        bll.Incluir(modelo);
                    }
                    else
                    {
                        modelo.IdMat_Propostas = id_mat;
                        if ((txtValorMaterial.Text != "") && (txtQuantMaterial.Text != ""))
                        {
                            modelo.Vl_Total = Convert.ToDouble(txtQuantMaterial.Text) * Convert.ToDouble(Validacao.MaskMoedaFloat(txtValorMaterial.Text));
                        }
                        bll.Alterar(modelo);

                        id_mat = 0;
                    }

                    txtDescMaterial.Text = "";
                    txtUnMaterial.Text = "";
                    txtQuantMaterial.Text = "";
                    txtValorMaterial.Text = "";
                    AtualizaMateriais();
                }
                else
                {
                    MessageBox.Show("Para incluir um material salve primeiro a proposta!");
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btExcluirMat_Click(object sender, EventArgs e)
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

                    foreach (DataGridViewRow item in dgMateriais.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            com = new SqlCommand("delete from mat_propostas where idmat_propostas=" + item.Cells[1].Value, cx);
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
                AtualizaMateriais();
            }
        }

        private void btImprimirTec_Click(object sender, EventArgs e)
        {
            using (var fileStream = new System.IO.FileStream("output.pdf", System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                var rpt = new PropostaReport(fileStream, idempresas, idpropostas);
                rpt.BasePath = Application.StartupPath;

                rpt.PageTitle = "PROPOSTA TÉCNICA";
                rpt.ImprimirCabecalhoPadrao = true;
                rpt.ImprimirRodapePadrao = true;
                rpt.GetOutput(1);
                
            }

            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLAnexos bll = new BLLAnexos(cx);
            bll.MergeAnexos("output.pdf", idpropostas, 1);

            System.Diagnostics.Process.Start("output.pdf");
        }

        private void btIncluirEquip_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloEquipamentos modelo = new ModeloEquipamentos();
                modelo.Equipamento = txtDescEquipamentos.Text;
                modelo.Quantidade = Convert.ToDouble(txtQuantEquipamentos.Text);

                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLEquipamentos bll = new BLLEquipamentos(cx);
                if (this.idpropostas > 0)
                {
                    modelo.IdPropostas = this.idpropostas;
                    if (id_equip == 0)
                    {
                        bll.Incluir(modelo);
                    }
                    else
                    {
                        modelo.IdEquip_Propostas = id_equip;
                        bll.Alterar(modelo);
                        id_equip = 0;
                    }

                    txtDescEquipamentos.Text = "";
                    txtQuantEquipamentos.Text = "";
                    dgEquipamentos.DataSource = bll.CarregaEquipamentos(idpropostas);
                }
                else
                {
                    MessageBox.Show("Para incluir um equipamento / recurso salve primeiro a proposta!");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btExcluirEquip_Click(object sender, EventArgs e)
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

                    foreach (DataGridViewRow item in dgEquipamentos.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            com = new SqlCommand("delete from equip_propostas where idequip_propostas=" + item.Cells[1].Value, cx);
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
                BLLEquipamentos bll2 = new BLLEquipamentos(cx2);
                dgEquipamentos.DataSource = bll2.CarregaEquipamentos(idpropostas);
            }
        }

        private void btIncluirAnexo_Click(object sender, EventArgs e)
        {
            try
            {
                var arquivo = EscolherArquivo();

                if (!string.IsNullOrWhiteSpace(arquivo))
                {
                    ModeloAnexos modelo = new ModeloAnexos();
                    modelo.Descricao = txtDescAnexo.Text;
                    modelo.Anexo = arquivo;
                    modelo.Nome_Arquivo = txtDescAnexo.Text;

                    DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                    BLLAnexos bll = new BLLAnexos(cx);
                    if (this.idpropostas > 0)
                    {
                        modelo.IdPropostas = this.idpropostas;
                        bll.Incluir(modelo);

                        txtDescAnexo.Text = "";
                        dgAnexos.DataSource = bll.CarregaAnexos(idpropostas);
                    }
                    else
                    {
                        MessageBox.Show("Para incluir um anexo salve primeiro a proposta!");
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btExcluirAnexo_Click(object sender, EventArgs e)
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

                    foreach (DataGridViewRow item in dgAnexos.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[0].Value) == 1)
                        {
                            com = new SqlCommand("delete from anexos_propostas where idanexos=" + item.Cells[3].Value, cx);
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
                BLLAnexos bll2 = new BLLAnexos(cx2);
                dgAnexos.DataSource = bll2.CarregaAnexos(idpropostas);
            }
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

        private void dgAnexos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DALConexao cx2 = new DALConexao(DadosConexao.StringConexao);
                BLLAnexos bll2 = new BLLAnexos(cx2);
                bll2.AbrirAnexo(Convert.ToInt32(dgAnexos.Rows[e.RowIndex].Cells[3].Value));
            }
        }

        private void btImprimirCom_Click(object sender, EventArgs e)
        {
            using (var fileStream = new System.IO.FileStream("output2.pdf", System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                var rpt = new PropostaReport(fileStream, idempresas, idpropostas);
                rpt.BasePath = Application.StartupPath;

                rpt.PageTitle = "PROPOSTA COMERCIAL";
                rpt.ImprimirCabecalhoPadrao = true;
                rpt.ImprimirRodapePadrao = true;
                rpt.chkValores = chkValores.Checked;
                rpt.GetOutput(2);
                
            }

            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLAnexos bll = new BLLAnexos(cx);
            bll.MergeAnexos("output2.pdf", idpropostas, 2);

            System.Diagnostics.Process.Start("output2.pdf");
        }

        private void btImpServ_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Para importar, salve uma planilha no formato CSV (separado por ví­rgula)!\n\n Os dados devem iniciar a partir da linha 2 sendo na coluna A a descrição do serviço, na coluna B a unidade,\n na coluna C a quantidade e na coluna D o valor unitário!");

                var arquivo = EscolherArquivo();

                if (!string.IsNullOrWhiteSpace(arquivo))
                {
                    ModeloServicos modelo = new ModeloServicos();
                    //Ler Excel
                    //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                    StreamReader rd = new StreamReader(arquivo, Encoding.Default);
                    //Declaro uma string que serÃ¡ utilizada para receber a linha completa do arquivo 
                    string linha = null;
                    //Declaro um array do tipo string que serÃ¡ utilizado para adicionar o conteudo da linha separado 
                    string[] linhaseparada = null;
                    linha = rd.ReadLine();
                    //realizo o while para ler o conteudo da linha 
                    while ((linha = rd.ReadLine()) != null)
                    {
                        //com o split adiciono a string 'quebrada' dentro do array 
                        linhaseparada = linha.Split(';');
                        //aqui incluo o mÃ©todo necessÃ¡rio para continuar o trabalho 
                        modelo.Servico = linhaseparada[0];
                        if (modelo.Servico != "")
                        {
                            modelo.Unidade = linhaseparada[1];
                            modelo.Quantidade = Convert.ToDouble(linhaseparada[2]);
                            modelo.Valor = Convert.ToDouble(Validacao.MaskMoedaFloat(linhaseparada[3]));

                            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                            BLLServicos bll = new BLLServicos(cx);
                            if (this.idpropostas > 0)
                            {
                                modelo.IdPropostas = this.idpropostas;
                                if ((modelo.Quantidade >= 0) && (modelo.Valor >= 0))
                                {
                                    modelo.Vl_Total = modelo.Quantidade * modelo.Valor;
                                }
                                bll.Incluir(modelo);

                                txtDescServico.Text = "";
                                txtUnServicos.Text = "";
                                txtQuantServicos.Text = "";
                                txtValorServico.Text = "";
                                AtualizaServicos();
                            }
                            else
                            {
                                MessageBox.Show("Para incluir um serviço salve primeiro a proposta!");
                                break;
                            }
                        }
                    }
                    rd.Close();

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btImpMat_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Para importar, salve uma planilha no formato CSV (separado por ví­rgula)!\n\n" +
                    "Os dados devem iniciar a partir da linha 2 sendo na coluna A o material, na coluna B a unidade, na coluna C a quantidade e na coluna D o valor unitário!");

                var arquivo = EscolherArquivo();

                if (!string.IsNullOrWhiteSpace(arquivo))
                {
                    ModeloMateriais modelo = new ModeloMateriais();
                    //Ler Excel
                    //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                    StreamReader rd = new StreamReader(arquivo, Encoding.Default);
                    //Declaro uma string que serÃ¡ utilizada para receber a linha completa do arquivo 
                    string linha = null;
                    //Declaro um array do tipo string que serÃ¡ utilizado para adicionar o conteudo da linha separado 
                    string[] linhaseparada = null;
                    linha = rd.ReadLine();
                    //realizo o while para ler o conteudo da linha 
                    while ((linha = rd.ReadLine()) != null)
                    {
                        //com o split adiciono a string 'quebrada' dentro do array 
                        linhaseparada = linha.Split(';');
                        //aqui incluo o mÃ©todo necessÃ¡rio para continuar o trabalho 
                        modelo.Material = linhaseparada[0];
                        if (modelo.Material != "")
                        {
                            modelo.Unidade = linhaseparada[1];
                            modelo.Quantidade = Convert.ToDouble(linhaseparada[2]);
                            modelo.Vl_Unitario = Convert.ToDouble(Validacao.MaskMoedaFloat(linhaseparada[3]));

                            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                            BLLMateriais bll = new BLLMateriais(cx);
                            if (this.idpropostas > 0)
                            {
                                modelo.IdPropostas = this.idpropostas;
                                if ((modelo.Quantidade >= 0) && (modelo.Vl_Unitario >= 0))
                                {
                                    modelo.Vl_Total = modelo.Quantidade * modelo.Vl_Unitario;
                                }
                                bll.Incluir(modelo);

                                txtDescMaterial.Text = "";
                                txtUnMaterial.Text = "";
                                txtQuantMaterial.Text = "";
                                txtValorMaterial.Text = "";
                                txtVl_Material.Text = "";
                                AtualizaMateriais();
                            }
                            else
                            {
                                MessageBox.Show("Para incluir um material salve primeiro a proposta!");
                                break;
                            }
                        }
                    }
                    rd.Close();

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btImpEquip_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Para importar, salve uma planilha no formato CSV (separado por ví­rgula)!\n\n" +
                    "Os dados devem iniciar a partir da linha 2 sendo na coluna A o equipamento e na coluna B a quantidade!");

                var arquivo = EscolherArquivo();

                if (!string.IsNullOrWhiteSpace(arquivo))
                {
                    ModeloEquipamentos modelo = new ModeloEquipamentos();
                    //Ler Excel
                    //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                    StreamReader rd = new StreamReader(arquivo, Encoding.Default);
                    //Declaro uma string que serÃ¡ utilizada para receber a linha completa do arquivo 
                    string linha = null;
                    //Declaro um array do tipo string que serÃ¡ utilizado para adicionar o conteudo da linha separado 
                    string[] linhaseparada = null;
                    linha = rd.ReadLine();
                    //realizo o while para ler o conteudo da linha 
                    while ((linha = rd.ReadLine()) != null)
                    {
                        //com o split adiciono a string 'quebrada' dentro do array 
                        linhaseparada = linha.Split(';');
                        //aqui incluo o mÃ©todo necessÃ¡rio para continuar o trabalho 
                        modelo.Equipamento = linhaseparada[0];
                        modelo.Quantidade = Convert.ToDouble(linhaseparada[1]);

                        DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                        BLLEquipamentos bll = new BLLEquipamentos(cx);
                        if (this.idpropostas > 0)
                        {
                            modelo.IdPropostas = this.idpropostas;
                            bll.Incluir(modelo);

                            txtDescEquipamentos.Text = "";
                            txtQuantEquipamentos.Text = "";
                            dgEquipamentos.DataSource = bll.CarregaEquipamentos(idpropostas);
                        }
                        else
                        {
                            MessageBox.Show("Para incluir um equipamento / recurso salve primeiro a proposta!");
                        }
                    }
                    rd.Close();

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void txtValorMaterial_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txtValorMaterial.Text.Contains(","))
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

        private void txtValorMaterial_Leave(object sender, EventArgs e)
        {
           txtValorMaterial.Text = Validacao.MaskFloatMoeda(Validacao.MaskMoedaFloat(txtValorMaterial.Text));
        }

        private void txtValorServico_KeyPress(object sender, KeyPressEventArgs e)
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
                        if (!txtValorServico.Text.Contains(","))
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

        private void txtValorServico_Leave(object sender, EventArgs e)
        {
            txtValorServico.Text = Validacao.MaskFloatMoeda(Validacao.MaskMoedaFloat(txtValorServico.Text));
        }

        private void ckMarcar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgUsuarios.Rows)
            {
                if (ckMarcar.Checked == true)
                {
                    Row.Cells[0].Value = 1;
                }
                else
                {
                    Row.Cells[0].Value = 0;
                }
            }
        }

        private void dgServico_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtDescServico.Text = dgServico.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtUnServicos.Text = dgServico.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtQuantServicos.Text = dgServico.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtValorServico.Text = dgServico.Rows[e.RowIndex].Cells[5].Value.ToString();
                this.id_serv = Convert.ToInt32(dgServico.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }

        private void dgEquipamentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtDescEquipamentos.Text = dgEquipamentos.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtQuantEquipamentos.Text = dgEquipamentos.Rows[e.RowIndex].Cells[3].Value.ToString();
                this.id_equip = Convert.ToInt32(dgEquipamentos.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }

        private void ckMarcarMat_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgMateriais.Rows)
            {
                if (Convert.ToInt32(Row.Cells[0].Value) == 0)
                    Row.Cells[0].Value = 1;
                else
                    Row.Cells[0].Value = 0;
            }
        }

        private void ckMarcarServ_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgServico.Rows)
            {
                if (Convert.ToInt32(Row.Cells[0].Value) == 0)
                    Row.Cells[0].Value = 1;
                else
                    Row.Cells[0].Value = 0;
            }
        }

        private void ckMarcarEquip_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgEquipamentos.Rows)
            {
                if (Convert.ToInt32(Row.Cells[0].Value) == 0)
                    Row.Cells[0].Value = 1;
                else
                    Row.Cells[0].Value = 0;
            }
        }

        private void btImprimirBud_Click(object sender, EventArgs e)
        {
            using (var fileStream = new System.IO.FileStream("output2.pdf", System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                var rpt = new PropostaReport(fileStream, idempresas, idpropostas);
                rpt.BasePath = Application.StartupPath;

                rpt.PageTitle = "PROPOSTA BUDGET";
                rpt.ImprimirCabecalhoPadrao = true;
                rpt.ImprimirRodapePadrao = true;
                rpt.chkValores = chkValores.Checked;
                rpt.GetOutput(3);

            }

            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            BLLAnexos bll = new BLLAnexos(cx);
            bll.MergeAnexos("output2.pdf", idpropostas, 2);

            System.Diagnostics.Process.Start("output2.pdf");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabAnexos.Focus() == true)
            {
                foreach (DataGridViewRow item in dgAnexos.Rows)
                {
                    if (item.Cells[4].Value.ToString() == "1")
                    {
                        item.Cells[1].Value = 1;
                    }

                    if (item.Cells[5].Value.ToString() == "1")
                    {
                        item.Cells[2].Value = 1;
                    }
                }
            }
                DALConexao cx = new DALConexao(DadosConexao.StringConexao);
                BLLPropostas bll = new BLLPropostas(cx);
                ModeloPropostas modelo = bll.CarregaPropostas(this.idpropostas);

                foreach (DataGridViewRow item in dgUsuarios.Rows)
                {
                    if (item.Cells[4].Value.ToString() == "1")
                    {
                        item.Cells[0].Value = 1;
                    }

                    if (Convert.ToInt32(item.Cells[1].Value.ToString()) == modelo.IdUsuarios)
                    {
                        item.Cells[0].Value = 1;
                    }
                }
           // }
        }

        private void dgMateriais_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtDescMaterial.Text = dgMateriais.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtUnMaterial.Text = dgMateriais.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtQuantMaterial.Text = dgMateriais.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtValorMaterial.Text = dgMateriais.Rows[e.RowIndex].Cells[5].Value.ToString();
                this.id_mat = Convert.ToInt32(dgMateriais.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }
    }
}
