using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPropostas
    {
        private DALConexao conexao;
        public DALPropostas(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloPropostas modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into propostas (idempresas,idclientes,idorcamentos,idusuarios,num_proposta,localidade,tipo_proposta,contato,telefone," +
                "e_mail,descricao,prazo,validade,info_contratada,info_contratante,cond_pag,garantia,observacoes,vl_servicos,vl_materiais,vl_total,situacao," +
                "data_criacao,data_aprovacao,cod_fiscal,edital,prob_fechamento,intro_empresa,despesas_inclusas,despesas_exclusas,documentacao,premissas,acrescimos_escopo,multa," +
                "responsaveis,leis_sociais,irrf,csll,cps,pis,cofins,iss,mes_base,ano_base,escopo,eventos_pagamento) " +
                "values (@idempresas,@idclientes,@idorcamentos,@idusuarios,@num_proposta,@localidade,@tipo_proposta,@contato,@telefone," +
                "@e_mail,@descricao,@prazo,@validade,@info_contratada,@info_contratante,@cond_pag,@garantia,@observacoes,@vl_servicos," +
                "@vl_materiais,@vl_total,@situacao,@data_criacao,@data_aprovacao,@cod_fiscal,@edital,@prob_fechamento,@intro_empresa," +
                "@despesas_inclusas,@despesas_exclusas,@documentacao,@premissas,@acrescimos_escopo,@multa,@responsaveis,@leis_sociais,@irrf,@csll,@cps,@pis,@cofins,@iss,@mes_base,@ano_base,@escopo,@eventos_pagamento); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@idclientes", modelo.IdClientes);
            cmd.Parameters.AddWithValue("@idorcamentos", modelo.IdOrcamentos);
            cmd.Parameters.AddWithValue("@idusuarios", modelo.IdUsuarios);
            cmd.Parameters.AddWithValue("@num_proposta", modelo.Num_Proposta);
            cmd.Parameters.AddWithValue("@localidade", modelo.Localidade);
            cmd.Parameters.AddWithValue("@tipo_proposta", modelo.Tipo_Proposta);
            cmd.Parameters.AddWithValue("@contato", modelo.Contato);
            cmd.Parameters.AddWithValue("@telefone", modelo.Telefone);
            cmd.Parameters.AddWithValue("@e_mail", modelo.E_Mail);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@prazo", modelo.Prazo);
            cmd.Parameters.AddWithValue("@validade", modelo.Validade);
            cmd.Parameters.AddWithValue("@info_contratada", modelo.Info_Contratada);
            cmd.Parameters.AddWithValue("@info_contratante", modelo.Info_Contratante);
            cmd.Parameters.AddWithValue("@cond_pag", modelo.Cond_Pag);
            cmd.Parameters.AddWithValue("@garantia", modelo.Garantia);
            cmd.Parameters.AddWithValue("@observacoes", modelo.Observacoes);
            cmd.Parameters.AddWithValue("@intro_empresa", modelo.IntroEmpresa);
            cmd.Parameters.AddWithValue("@despesas_inclusas", modelo.DespesasInclusas);
            cmd.Parameters.AddWithValue("@despesas_exclusas", modelo.DespesasExclusas);
            cmd.Parameters.AddWithValue("@documentacao", modelo.Documentacao);
            cmd.Parameters.AddWithValue("@premissas", modelo.Premissas);
            cmd.Parameters.AddWithValue("@acrescimos_escopo", modelo.AcrescimosEscopo);
            cmd.Parameters.AddWithValue("@vl_servicos", modelo.Vl_Servicos);
            cmd.Parameters.AddWithValue("@vl_materiais", modelo.Vl_Materiais);
            cmd.Parameters.AddWithValue("@vl_total", modelo.Vl_Total);
            cmd.Parameters.AddWithValue("@situacao", modelo.Situacao);
            cmd.Parameters.AddWithValue("@cod_fiscal", modelo.Cod_Fiscal);
            cmd.Parameters.AddWithValue("@edital", modelo.Edital);
            cmd.Parameters.AddWithValue("@multa", modelo.Multa);
            cmd.Parameters.AddWithValue("@prob_fechamento", modelo.Prob_Fechamento);
            cmd.Parameters.AddWithValue("@responsaveis", modelo.Responsaveis);
            cmd.Parameters.Add("@data_criacao", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@data_criacao"].Value = modelo.Data_Criacao;
            if (modelo.Data_Aprovacao.ToString() != "01/01/0001 00:00:00")
            {
                cmd.Parameters.Add("@data_aprovacao", System.Data.SqlDbType.DateTime);
                cmd.Parameters["@data_aprovacao"].Value = modelo.Data_Aprovacao;
            }
            else
            {
                cmd.Parameters.Add("@data_aprovacao", System.Data.SqlDbType.DateTime);
                cmd.Parameters["@data_aprovacao"].Value = SqlDateTime.Null;
            }
            cmd.Parameters.AddWithValue("@leis_sociais", modelo.Leis_Sociais);
            cmd.Parameters.AddWithValue("@irrf", modelo.IRRF);
            cmd.Parameters.AddWithValue("@csll", modelo.CSLL);
            cmd.Parameters.AddWithValue("@cps", modelo.CPS);
            cmd.Parameters.AddWithValue("@pis", modelo.PIS);
            cmd.Parameters.AddWithValue("@cofins", modelo.COFINS);
            cmd.Parameters.AddWithValue("@iss", modelo.ISS);
            cmd.Parameters.AddWithValue("@mes_base", modelo.Mes_Base);
            cmd.Parameters.AddWithValue("@ano_base", modelo.Ano_Base);
            cmd.Parameters.AddWithValue("@escopo", modelo.Escopo);
            cmd.Parameters.AddWithValue("@eventos_pagamento", modelo.Eventos_Pagamento);

            conexao.Conectar();
            modelo.IdPropostas = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloPropostas modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update propostas set idclientes=@idclientes,idorcamentos=@idorcamentos,num_proposta=@num_proposta,localidade=@localidade,tipo_proposta=@tipo_proposta,contato=@contato,telefone=@telefone," +
                "e_mail=@e_mail,descricao=@descricao,prazo=@prazo,validade=@validade,info_contratada=@info_contratada,info_contratante=@info_contratante,cond_pag=@cond_pag,garantia=@garantia,observacoes=@observacoes," +
                "vl_servicos=@vl_servicos,vl_materiais=@vl_materiais,vl_total=@vl_total,situacao=@situacao,data_aprovacao=@data_aprovacao,cod_fiscal=@cod_fiscal,edital=@edital,prob_fechamento=@prob_fechamento," +
                "intro_empresa=@intro_empresa,despesas_inclusas=@despesas_inclusas,despesas_exclusas=@despesas_exclusas,documentacao=@documentacao,premissas=@premissas,acrescimos_escopo=@acrescimos_escopo,multa=@multa,responsaveis=@responsaveis," +
                "leis_sociais=@leis_sociais,irrf=@irrf,csll=@csll,cps=@cps,pis=@pis,cofins=@cofins,iss=@iss,mes_base=@mes_base,ano_base=@ano_base,escopo=@escopo,eventos_pagamento=@eventos_pagamento" +
                " where idpropostas=@idpropostas;";
            cmd.Parameters.AddWithValue("@idpropostas", modelo.IdPropostas);
            cmd.Parameters.AddWithValue("@idclientes", modelo.IdClientes);
            cmd.Parameters.AddWithValue("@idorcamentos", modelo.IdOrcamentos);
            cmd.Parameters.AddWithValue("@num_proposta", modelo.Num_Proposta);
            cmd.Parameters.AddWithValue("@localidade", modelo.Localidade);
            cmd.Parameters.AddWithValue("@tipo_proposta", modelo.Tipo_Proposta);
            cmd.Parameters.AddWithValue("@contato", modelo.Contato);
            cmd.Parameters.AddWithValue("@telefone", modelo.Telefone);
            cmd.Parameters.AddWithValue("@e_mail", modelo.E_Mail);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@prazo", modelo.Prazo);
            cmd.Parameters.AddWithValue("@validade", modelo.Validade);
            cmd.Parameters.AddWithValue("@info_contratada", modelo.Info_Contratada);
            cmd.Parameters.AddWithValue("@info_contratante", modelo.Info_Contratante);
            cmd.Parameters.AddWithValue("@cond_pag", modelo.Cond_Pag);
            cmd.Parameters.AddWithValue("@garantia", modelo.Garantia);
            cmd.Parameters.AddWithValue("@observacoes", modelo.Observacoes);
            cmd.Parameters.AddWithValue("@intro_empresa", modelo.IntroEmpresa);
            cmd.Parameters.AddWithValue("@despesas_inclusas", modelo.DespesasInclusas);
            cmd.Parameters.AddWithValue("@despesas_exclusas", modelo.DespesasExclusas);
            cmd.Parameters.AddWithValue("@documentacao", modelo.Documentacao);
            cmd.Parameters.AddWithValue("@premissas", modelo.Premissas);
            cmd.Parameters.AddWithValue("@acrescimos_escopo", modelo.AcrescimosEscopo);
            cmd.Parameters.AddWithValue("@vl_servicos", modelo.Vl_Servicos);
            cmd.Parameters.AddWithValue("@vl_materiais", modelo.Vl_Materiais);
            cmd.Parameters.AddWithValue("@vl_total", modelo.Vl_Total);
            cmd.Parameters.AddWithValue("@situacao", modelo.Situacao);
            cmd.Parameters.AddWithValue("@cod_fiscal", modelo.Cod_Fiscal);
            cmd.Parameters.AddWithValue("@edital", modelo.Edital);
            cmd.Parameters.AddWithValue("@multa", modelo.Multa);
            cmd.Parameters.AddWithValue("@prob_fechamento", modelo.Prob_Fechamento);
            cmd.Parameters.AddWithValue("@responsaveis", modelo.Responsaveis);
            if (modelo.Data_Aprovacao.ToString() != "01/01/0001 00:00:00")
            {
                cmd.Parameters.Add("@data_aprovacao", System.Data.SqlDbType.DateTime);
                cmd.Parameters["@data_aprovacao"].Value = modelo.Data_Aprovacao;
            }
            else
            {
                cmd.Parameters.Add("@data_aprovacao", System.Data.SqlDbType.DateTime);
                cmd.Parameters["@data_aprovacao"].Value = SqlDateTime.Null;
            }
            cmd.Parameters.AddWithValue("@leis_sociais", modelo.Leis_Sociais);
            cmd.Parameters.AddWithValue("@irrf", modelo.IRRF);
            cmd.Parameters.AddWithValue("@csll", modelo.CSLL);
            cmd.Parameters.AddWithValue("@cps", modelo.CPS);
            cmd.Parameters.AddWithValue("@pis", modelo.PIS);
            cmd.Parameters.AddWithValue("@cofins", modelo.COFINS);
            cmd.Parameters.AddWithValue("@iss", modelo.ISS);
            cmd.Parameters.AddWithValue("@mes_base", modelo.Mes_Base);
            cmd.Parameters.AddWithValue("@ano_base", modelo.Ano_Base);
            cmd.Parameters.AddWithValue("@escopo", modelo.Escopo);
            cmd.Parameters.AddWithValue("@eventos_pagamento", modelo.Eventos_Pagamento);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public void Excluir(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "delete from propostas where idpropostas=@idpropostas;";
            cmd.Parameters.AddWithValue("@idpropostas", codigo);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }
        public DataTable Localizar(int idempresas, int idpropostas)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select p.idpropostas,p.num_proposta,p.descricao,c.nome_fantasia as cliente,u.nome from propostas p join clientes c on c.idclientes=p.idclientes join usuarios u on u.idusuarios=p.idusuarios where idempresas=" + idempresas + " order by p.idpropostas desc", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public DataTable Localizar(String valor, String buscapor, int idempresas, String situacao, int idusuarios, decimal quantpagina, decimal pagina)
        {
            String where = "p.descricao";
            String where2 = "";
            if (buscapor == "Número Proposta")
            {
                where = "p.num_proposta";
            }
            else if (buscapor == "Cliente")
            {
                where = "c.nome_fantasia";
            }
            else if (buscapor == "Elaborada por")
            {
                where = "u.nome";
            }
            else
            {
                where = "p.descricao";
            }

            if (situacao != "Todas")
            {
                where2 = "and p.situacao like '%" + situacao + "%'";
            }

            DataTable tabela = new DataTable();
            string sql = "SELECT idpropostas,num_proposta,descricao,cliente,total,nome,ch FROM (" +
                "select ROW_NUMBER() OVER(ORDER BY p.idpropostas desc) AS Numero, p.idpropostas,p.num_proposta,p.descricao,c.nome_fantasia as cliente,CONVERT(varchar,CONVERT(money,p.vl_total,2)) as total,u.nome, (select top 1 1 from usuarios_propostas up where (up.idusuarios=" + idusuarios + " and up.idpropostas=p.idpropostas) or " + idusuarios + "=25) as ch from propostas p join clientes c on c.idclientes=p.idclientes join usuarios u on u.idusuarios=p.idusuarios where " + where + " like '%" + valor + "%' and p.idempresas=" + idempresas + " " + where2 +
                ") AS TBL" +
                " WHERE Numero BETWEEN(("+pagina+ " - 1) * " + quantpagina + " + 1) AND(" + pagina + " * " + quantpagina + ")" +
                " ORDER BY idpropostas desc";
            SqlDataAdapter da = new SqlDataAdapter(sql, conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public String LocalizarUsuario(int idempresas, int idpropostas)
        {
            String usuario = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select u.nome from propostas p join usuarios u on u.idusuarios=p.idusuarios where p.idempresas=" + idempresas + " and idpropostas=" + idpropostas;
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();

                usuario = Convert.ToString(registro["nome"]);
            }
            conexao.Desconectar();
            return usuario;
        }

        public bool PropostaExiste(int idempresas, String prop)
        {
            bool result = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from propostas p where p.idempresas=" + idempresas + " and num_proposta='" + prop + "'";
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();

                result = true;
            }
            conexao.Desconectar();
            return result;
        }

        public ModeloPropostas CarregaPropostas(int codigo)
        {
            ModeloPropostas modelo = new ModeloPropostas();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select p.*,c.nome_fantasia,u.nome from propostas p join clientes c on c.idclientes=p.idclientes join usuarios u on u.idusuarios=p.idusuarios where p.idpropostas=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdPropostas = Convert.ToInt32(registro["idpropostas"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.IdClientes = Convert.ToInt32(registro["idclientes"]);
                //modelo.IdOrcamentos = Convert.ToInt32(registro["idorcamentos"]);
                modelo.IdUsuarios = Convert.ToInt32(registro["idusuarios"]);
                modelo.Num_Proposta = Convert.ToString(registro["num_proposta"]);
                modelo.Localidade = Convert.ToString(registro["localidade"]);
                modelo.Tipo_Proposta = Convert.ToString(registro["tipo_proposta"]);
                modelo.Contato = Convert.ToString(registro["contato"]);
                modelo.Telefone = Convert.ToString(registro["telefone"]);
                modelo.E_Mail = Convert.ToString(registro["e_mail"]);
                modelo.Descricao = Convert.ToString(registro["descricao"]);
                if (registro["prazo"].ToString() != "")
                {
                    modelo.Prazo = Convert.ToInt32(registro["prazo"]);
                }
                if (registro["validade"].ToString() != "")
                {
                    modelo.Validade = Convert.ToInt32(registro["validade"]);
                }
                if (registro["leis_sociais"].ToString() != "")
                {
                    modelo.Leis_Sociais = Convert.ToDouble(registro["leis_sociais"]);
                }
                if (registro["irrf"].ToString() != "")
                {
                    modelo.IRRF = Convert.ToDouble(registro["irrf"]);
                }
                if (registro["csll"].ToString() != "")
                {
                    modelo.CSLL = Convert.ToDouble(registro["csll"]);
                }
                if (registro["cps"].ToString() != "")
                {
                    modelo.CPS = Convert.ToDouble(registro["cps"]);
                }
                if (registro["pis"].ToString() != "")
                {
                    modelo.PIS = Convert.ToDouble(registro["pis"]);
                }
                if (registro["cofins"].ToString() != "")
                {
                    modelo.COFINS = Convert.ToDouble(registro["cofins"]);
                }
                if (registro["iss"].ToString() != "")
                {
                    modelo.ISS = Convert.ToDouble(registro["iss"]);
                }
                modelo.Info_Contratada = Convert.ToString(registro["info_contratada"]);
                modelo.Info_Contratante = Convert.ToString(registro["info_contratante"]);
                modelo.Cond_Pag = Convert.ToString(registro["cond_pag"]);
                modelo.Garantia = Convert.ToString(registro["garantia"]);
                modelo.Observacoes = Convert.ToString(registro["observacoes"]);
                modelo.IntroEmpresa = Convert.ToString(registro["intro_empresa"]);
                modelo.DespesasInclusas = Convert.ToString(registro["despesas_inclusas"]);
                modelo.DespesasExclusas = Convert.ToString(registro["despesas_exclusas"]);
                modelo.Documentacao = Convert.ToString(registro["documentacao"]);
                modelo.Premissas = Convert.ToString(registro["premissas"]);
                modelo.AcrescimosEscopo = Convert.ToString(registro["acrescimos_escopo"]);
                modelo.Cod_Fiscal = Convert.ToString(registro["cod_fiscal"]);
                modelo.Edital = Convert.ToString(registro["edital"]);
                modelo.Multa = Convert.ToString(registro["multa"]);
                modelo.Prob_Fechamento = Convert.ToString(registro["prob_fechamento"]);
                modelo.Responsaveis = Convert.ToString(registro["responsaveis"]);
                modelo.Escopo = Convert.ToString(registro["escopo"]);
                modelo.Eventos_Pagamento = Convert.ToString(registro["eventos_pagamento"]);
                if (registro["vl_servicos"].ToString() != "")
                {
                    modelo.Vl_Servicos = Convert.ToDouble(registro["vl_servicos"]);
                }
                if (registro["vl_materiais"].ToString() != "")
                {
                    modelo.Vl_Materiais = Convert.ToDouble(registro["vl_materiais"]);
                }
                if (registro["vl_total"].ToString() != "")
                {
                    modelo.Vl_Total = Convert.ToDouble(registro["vl_total"]);
                }
                if (registro["situacao"].ToString() != "")
                {
                    modelo.Situacao = Convert.ToString(registro["situacao"]);
                }
                if (registro["data_criacao"].ToString() != "")
                {
                    modelo.Data_Criacao = Convert.ToDateTime(registro["data_criacao"]);
                }
                if (registro["data_aprovacao"].ToString() != "")
                {
                    modelo.Data_Aprovacao = Convert.ToDateTime(registro["data_aprovacao"]);
                }
                modelo.Mes_Base = Convert.ToString(registro["mes_base"]);
                if (registro["ano_base"].ToString() != "")
                {
                    modelo.Ano_Base = Convert.ToInt32(registro["ano_base"]);
                }
            }
            conexao.Desconectar();
            return modelo;
        }

        public int TotalRegistros(int idempresas)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select count(*) from propostas p where p.idempresas=" + idempresas;
            conexao.Conectar();
            int quant = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
            return quant;
        }
    }
}
