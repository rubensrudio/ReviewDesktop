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
    public partial class frmUsuariosCadastro : Review.frmModeloIncluir
    {
        private int idempresas;
        private int idusuarios;
        public frmUsuariosCadastro(int idemp)
        {
            InitializeComponent();
            this.idempresas = idemp;
        }
        public frmUsuariosCadastro(int idemp, int idusu)
        {
            InitializeComponent();
            this.idempresas = idemp;
            this.idusuarios = idusu;
        }

        private void frmUsuariosCadastro_Load(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            if (this.idusuarios > 0)
            {
                txtCodigo.Text = this.idusuarios.ToString();

                BLLUsuarios bll = new BLLUsuarios(cx);
                ModeloUsuario modelo = bll.CarregaUsuario(this.idusuarios);
                txtUsuario.Text = modelo.Usuario;
                txtSenha.Text = modelo.Senha;
                txtNome.Text = modelo.Nome;
            }
            else
            {
                txtCodigo.Text = "";
            }

            //Grid dos Módulos
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgModulos.Columns.Add(checkColumn);

            BLLModulos bll2 = new BLLModulos(cx);
            dgModulos.DataSource = bll2.Localizar(idempresas, idusuarios);

            dgModulos.Columns[0].Visible = true;
            dgModulos.Columns[1].Visible = false;
            dgModulos.Columns[2].HeaderText = "Módulo";
            dgModulos.Columns[2].ReadOnly = true;
            dgModulos.Columns[2].Width = 200;
            dgModulos.Columns[3].Visible = false;


            foreach (DataGridViewRow item in dgModulos.Rows)
            {
                if (item.Cells[3].Value.ToString() == "1")
                {
                    item.Cells[0].Value = 1;
                }
            }


            //Grid dos Centros de Custo
            DataGridViewCheckBoxColumn checkColumn2 = new DataGridViewCheckBoxColumn();
            checkColumn2.Name = "X";
            checkColumn2.HeaderText = "";
            checkColumn2.Width = 50;
            checkColumn2.ReadOnly = false;
            checkColumn2.TrueValue = 1;
            checkColumn2.FalseValue = 0;
            dgCentrosdeCusto.Columns.Add(checkColumn2);

            BLLCentrosdeCusto bll3 = new BLLCentrosdeCusto(cx);
            dgCentrosdeCusto.DataSource = bll3.Localizar(idempresas, idusuarios);

            dgCentrosdeCusto.Columns[0].Visible = true;
            dgCentrosdeCusto.Columns[1].Visible = false;
            dgCentrosdeCusto.Columns[2].HeaderText = "Código";
            dgCentrosdeCusto.Columns[2].ReadOnly = true;
            dgCentrosdeCusto.Columns[2].Width = 100;
            dgCentrosdeCusto.Columns[3].HeaderText = "Descrição";
            dgCentrosdeCusto.Columns[3].ReadOnly = true;
            dgCentrosdeCusto.Columns[3].Width = 200;
            dgCentrosdeCusto.Columns[4].Visible = false;


            foreach (DataGridViewRow item in dgCentrosdeCusto.Rows)
            {
                if (item.Cells[4].Value.ToString() == "1")
                {
                    item.Cells[0].Value = 1;
                }
            }

            //Grid dos Itens Módulos
            DataGridViewCheckBoxColumn checkColumn3 = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "X";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.ReadOnly = false;
            checkColumn.TrueValue = 1;
            checkColumn.FalseValue = 0;
            dgItensModulos.Columns.Add(checkColumn3);

            BLLItemModulo bll4 = new BLLItemModulo(cx);
            dgItensModulos.DataSource = bll4.Localizar(0, idusuarios);

            dgItensModulos.Columns[0].Visible = true;
            dgItensModulos.Columns[0].Width = 50;
            dgItensModulos.Columns[1].Visible = false;
            dgItensModulos.Columns[2].HeaderText = "Item";
            dgItensModulos.Columns[2].ReadOnly = true;
            dgItensModulos.Columns[2].Width = 200;
            dgItensModulos.Columns[3].Visible = false;
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            SqlTransaction ts = null;
            SqlConnection cx = new SqlConnection(DadosConexao.StringConexao);
            SqlCommand com = null;
            SqlCommand com2 = null;
            SqlCommand com3 = null;

            try
            {
                ModeloUsuario modelo = new ModeloUsuario();
                modelo.Usuario = txtUsuario.Text;
                modelo.Senha = txtSenha.Text;
                modelo.Nome = txtNome.Text;
                modelo.IdEmpresas = this.idempresas;

                cx.Open();
                ts = cx.BeginTransaction();

                DALConexao cx1 = new DALConexao(DadosConexao.StringConexao);
                BLLUsuarios bll = new BLLUsuarios(cx1);
                if (this.idusuarios > 0)
                {
                    //Altera dados do Usuário
                    modelo.IdUsuarios = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                }
                else
                {
                    //Inclui um novo usuário
                    bll.Incluir(modelo);
                }

                BLLCentrosdeCusto bll1 = new BLLCentrosdeCusto(cx1);
                foreach (DataGridViewRow item in dgCentrosdeCusto.Rows)
                {
                    if (Convert.ToInt32(item.Cells[0].Value) == 1)
                    {
                        //bool resultado = bll1.LocalizarCentrodeCustoUsuario(Convert.ToInt32(item.Cells[1].Value), idusuarios);
                        com3 = new SqlCommand("select * from centros_de_custos_usuarios where idcentros_de_custos=" + item.Cells[1].Value.ToString() + " and idusuarios=" + idusuarios.ToString(), cx);
                        com3.Transaction = ts;
                        SqlDataReader registro = com3.ExecuteReader();
                        if (registro.HasRows == false)
                        {
                            registro.Close();
                            //Inclui permissões de centros de custos
                            com = new SqlCommand("insert into centros_de_custos_usuarios (idusuarios,idcentros_de_custos) values (" + idusuarios.ToString() + "," + Convert.ToInt32(item.Cells[1].Value) + ")", cx);
                            com.Transaction = ts;
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            registro.Close();
                        }
                    }
                    else
                    {
                        //Exclui permissões em centros de custos
                        com = new SqlCommand("delete from centros_de_custos_usuarios where idcentros_de_custos="+ Convert.ToInt32(item.Cells[1].Value)  + " and idusuarios=" + idusuarios.ToString(), cx);
                        com.Transaction = ts;
                        com.ExecuteNonQuery();
                    }
                }

                BLLModulos bll2 = new BLLModulos(cx1);
                foreach (DataGridViewRow item in dgModulos.Rows)
                {
                    if (Convert.ToInt32(item.Cells[0].Value) == 1)
                    {
                        com3 = new SqlCommand("select * from modulos_usuarios where idmodulos=" + item.Cells[1].Value.ToString() + " and idusuarios=" + idusuarios.ToString(), cx);
                        com3.Transaction = ts;
                        SqlDataReader registro = com3.ExecuteReader();
                        if (registro.HasRows == false)
                        {
                            registro.Close();
                            //Inclui permissões em módulos
                            com2 = new SqlCommand("insert into modulos_usuarios (idusuarios,idmodulos) values (" + idusuarios.ToString() + "," + Convert.ToInt32(item.Cells[1].Value) + ")", cx);
                            com2.Transaction = ts;
                            com2.ExecuteNonQuery();
                        }
                        else
                        {
                            registro.Close();
                        }
                    }
                    else
                    {
                        //Exclui permissões em módulos
                        com = new SqlCommand("delete from modulos_usuarios where idmodulos="+ Convert.ToInt32(item.Cells[1].Value)  + " and idusuarios=" + idusuarios.ToString(), cx);
                        com.Transaction = ts;
                        com.ExecuteNonQuery();
                    }
                }

                int check = 0;
                int idmod = 0;
                BLLItemModulo bll3 = new BLLItemModulo(cx1);
                foreach (DataGridViewRow item in dgItensModulos.Rows)
                {
                    idmod = bll3.LocalizarModulo(Convert.ToInt32(item.Cells[1].Value));

                    if (Convert.ToInt32(item.Cells[0].Value) == 1)
                    {
                        check++;
                        //bool resultado = bll3.LocalizarItemModuloUsuario(Convert.ToInt32(item.Cells[1].Value), idusuarios);
                        com3 = new SqlCommand("select * from itens_modulos_usuarios where iditens_modulos=" + item.Cells[1].Value.ToString() + " and idusuarios=" + idusuarios.ToString(), cx);
                        com3.Transaction = ts;
                        SqlDataReader registro = com3.ExecuteReader();
                        if (registro.HasRows == false)
                        {
                            registro.Close();
                            //Inclui permissões em módulos
                            com2 = new SqlCommand("insert into itens_modulos_usuarios (idusuarios,iditens_modulos) values (" + idusuarios.ToString() + "," + Convert.ToInt32(item.Cells[1].Value) + ")", cx);
                            com2.Transaction = ts;
                            com2.ExecuteNonQuery();

                            //bool resultado2 = bll2.LocalizarModuloUsuario(idmod, idusuarios);
                            com3 = new SqlCommand("select * from itens_modulos_usuarios where iditens_modulos=" + item.Cells[1].Value.ToString() + " and idusuarios=" + idusuarios.ToString(), cx);
                            com3.Transaction = ts;
                            SqlDataReader registro2 = com3.ExecuteReader();
                            if (registro2.HasRows == false)
                            {
                                registro2.Close();
                                //Inclui permissões em módulos
                                com2 = new SqlCommand("insert into modulos_usuarios (idusuarios,idmodulos) values (" + idusuarios.ToString() + "," + idmod + ")", cx);
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
                            registro.Close();
                        }
                    }
                    else
                    {
                        //Exclui permissões em itens dos módulos
                        com = new SqlCommand("delete from itens_modulos_usuarios where idusuarios=" + idusuarios.ToString() + " and iditens_modulos=" + Convert.ToInt32(item.Cells[1].Value), cx);
                        com.Transaction = ts;
                        com.ExecuteNonQuery();

                    }
                }
                
                if (check == 0)
                {
                    //Exclui permissões em módulos se não tem itens
                    com = new SqlCommand("delete from modulos_usuarios where idmodulos=" + idmod + " and idusuarios=" + idusuarios.ToString(), cx);
                    com.Transaction = ts;
                    com.ExecuteNonQuery();
                }

                ts.Commit();
                cx.Close();
                MessageBox.Show("Salvo com sucesso!");
                this.Close();

            }
            catch (Exception erro)
            {
                ts.Rollback();
                cx.Close();
                MessageBox.Show(erro.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgCentrosdeCusto.Rows)
            {
                if (checkBox1.Checked == true)
                {
                    Row.Cells[0].Value = 1;
                }
                else
                {
                    Row.Cells[0].Value = 0;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgModulos.Rows)
            {
                if (checkBox2.Checked == true)
                {
                    Row.Cells[0].Value = 1;
                }
                else
                {
                    Row.Cells[0].Value = 0;
                }
            }
        }

        private void dgModulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DALConexao cx = new DALConexao(DadosConexao.StringConexao);
            
            BLLItemModulo bll2 = new BLLItemModulo(cx);
            dgItensModulos.DataSource = bll2.Localizar(Convert.ToInt32(dgModulos.Rows[e.RowIndex].Cells[1].Value), idusuarios);
            
            foreach (DataGridViewRow item in dgItensModulos.Rows)
            {
                if (item.Cells[3].Value.ToString() == "1")
                {
                    item.Cells[0].Value = 1;
                }
            }
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in dgItensModulos.Rows)
            {
                if (checkBox3.Checked == true)
                {
                    Row.Cells[0].Value = 1;
                }
                else
                {
                    Row.Cells[0].Value = 0;
                }
            }
        }
    }
}
