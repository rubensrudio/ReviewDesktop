using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALOrcamentos
    {
        private DALConexao conexao;
        public DALOrcamentos(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloOrcamentos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into orcamentos (idempresas,idclientes,idusuarios,codigo,descricao,local,revisao,edital,dt_criacao,prev_inicio,prev_fim,leis_sociais,bdi,memorial) " +
                "values (@idempresas,@idclientes,@idusuarios,@codigo,@descricao,@local,@revisao,@edital,@dt_criacao,@prev_inicio,@prev_fim,@leis_sociais,@bdi,@memorial); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@idclientes", modelo.IdClientes);
            cmd.Parameters.AddWithValue("@idusuarios", modelo.IdUsuarios);
            cmd.Parameters.AddWithValue("@codigo", modelo.Codigo);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@local", modelo.Local);
            cmd.Parameters.AddWithValue("@revisao", modelo.Revisao);
            cmd.Parameters.AddWithValue("@edital", modelo.Edital);
            cmd.Parameters.Add("@dt_criacao", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@dt_criacao"].Value = modelo.Dt_Criacao;
            cmd.Parameters.Add("@prev_inicio", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@prev_inicio"].Value = modelo.Prev_Inicio;
            cmd.Parameters.Add("@prev_fim", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@prev_fim"].Value = modelo.Prev_Fim;
            cmd.Parameters.AddWithValue("@leis_sociais", modelo.Leis_Sociais);
            cmd.Parameters.AddWithValue("@bdi", modelo.BDI);
            cmd.Parameters.AddWithValue("@memorial", modelo.Memorial);

            conexao.Conectar();
            modelo.IdOrcamentos = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloOrcamentos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update orcamentos set idempresas=@idempresas,idclientes=@idclientes,idusuarios=@idusuarios,codigo=@codigo,descricao=@descricao,local=@local,revisao=@revisao, " +
                "edital=@edital,dt_cricao=@dt_criacao,prev_inicio=@prev_inicio,prev_fim=@prev_fim,leis_sociais=@leis_sociais,bdi=@bdi,memorial=@memorial " + 
                "where idorcamentos=@idorcamentos;";
            cmd.Parameters.AddWithValue("@idorcamentos", modelo.IdOrcamentos);
            cmd.Parameters.AddWithValue("@idclientes", modelo.IdClientes);
            cmd.Parameters.AddWithValue("@idusuarios", modelo.IdUsuarios);
            cmd.Parameters.AddWithValue("@codigo", modelo.Codigo);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@local", modelo.Local);
            cmd.Parameters.AddWithValue("@revisao", modelo.Revisao);
            cmd.Parameters.AddWithValue("@edital", modelo.Edital);
            cmd.Parameters.Add("@dt_criacao", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@dt_criacao"].Value = modelo.Dt_Criacao;
            cmd.Parameters.Add("@prev_inicio", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@prev_inicio"].Value = modelo.Prev_Inicio;
            cmd.Parameters.Add("@prev_fim", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@prev_fim"].Value = modelo.Prev_Fim;
            cmd.Parameters.AddWithValue("@leis_sociais", modelo.Leis_Sociais);
            cmd.Parameters.AddWithValue("@bdi", modelo.BDI);
            cmd.Parameters.AddWithValue("@memorial", modelo.Memorial);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public DataTable Localizar(String valor, String buscapor, int idempresas)
        {
            String where = "descricao";
            if (buscapor == "Código")
            {
                where = "o.codigo";
            }
            else if (buscapor == "Cliente")
            {
                where = "c.nome_fantasia";
            }
            else
            {
                where = "o.descricao";
            }
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select o.idorcamentos,o.codigo,o.descricao,c.nome_fantasia as cliente from orcamentos o join clientes c on c.idclientes=o.idclientes where " + where + " like '%" + valor + "%' and o.idempresas=" + idempresas + " order by o.dt_criacao desc", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        
        public ModeloOrcamentos CarregaOrcamento(int codigo)
        {
            ModeloOrcamentos modelo = new ModeloOrcamentos();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from orcamentos where idorcamentos=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdOrcamentos = Convert.ToInt32(registro["idorcamentos"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.IdClientes = Convert.ToInt32(registro["idclientes"]);
                modelo.IdUsuarios = Convert.ToInt32(registro["idusuarios"]);
                modelo.Codigo = Convert.ToString(registro["codigo"]);
                modelo.Descricao = Convert.ToString(registro["descricao"]);
                modelo.Local = Convert.ToString(registro["local"]);
                modelo.Revisao = Convert.ToInt32(registro["revisao"]);
                modelo.Edital = Convert.ToString(registro["edital"]);
                modelo.Local = Convert.ToString(registro["local"]);
                modelo.Dt_Criacao = Convert.ToDateTime(registro["dt_criacao"]);
                modelo.Prev_Inicio = Convert.ToDateTime(registro["prev_inicio"]);
                modelo.Prev_Fim = Convert.ToDateTime(registro["prev_fim"]);
                modelo.Leis_Sociais = Convert.ToDouble(registro["leis_sociais"]);
                modelo.BDI = Convert.ToDouble(registro["bdi"]);
                modelo.Memorial = Convert.ToString(registro["memorial"]);
            }
            conexao.Desconectar();
            return modelo;
        }
    }
}
