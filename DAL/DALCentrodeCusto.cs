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
    public class DALCentrodeCusto
    {
        private DALConexao conexao;
        public DALCentrodeCusto(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloCentrodeCusto modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into centros_de_custos (idempresas,codigo,descricao) " +
                "values (@idempresas,@codigo,@descricao); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@codigo", modelo.Codigo);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            
            conexao.Conectar();
            modelo.IdCentros_de_Custos = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloCentrodeCusto modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update centros_de_custos set codigo=@codigo,descricao=@descricao " +
                "where idcentros_de_custos=@idcentros_de_custos;";
            cmd.Parameters.AddWithValue("@idcentros_de_custos", modelo.IdCentros_de_Custos);
            cmd.Parameters.AddWithValue("@codigo", modelo.Codigo);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        /*public void Excluir(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "delete from usuarios where idusuarios=@idusuarios;";
            cmd.Parameters.AddWithValue("@idusuarios", codigo);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }*/

        public DataTable Localizar(int idempresas,int idusuarios)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select cc.idcentros_de_custos,cc.codigo,cc.descricao, (select 1 from centros_de_custos_usuarios c where c.idusuarios="+ idusuarios +" and c.idcentros_de_custos=cc.idcentros_de_custos) as ch from centros_de_custos cc where cc.idempresas=" + idempresas + " order by cc.codigo", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public DataTable Localizar(String valor, String buscapor, int idempresas)
        {
            String where = "descricao";
            if (buscapor == "Código")
            {
                where = "codigo";
            }
            else
            {
                where = "descricao";
            }
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idcentros_de_custos,codigo,descricao from centros_de_custos where " + where + " like '%" + valor + "%' and idempresas=" + idempresas + " order by codigo", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public bool LocalizarCentrodeCustoUsuario(int idcentrosdecusto, int idusuarios)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from centros_de_custos_usuarios where idcentros_de_custos=" + idcentrosdecusto.ToString() + " and idusuarios=" + idusuarios.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            bool resultado = false;
            if (registro.HasRows)
            {
                resultado = true;
            }
            conexao.Desconectar();
            return resultado;
        }

        public int VerificaUsuario(int idusuarios)
        {
            int cadastrado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select idusuarios from centros_de_custos_usuarios where idusuarios=@usuarios ";
            cmd.Parameters.AddWithValue("@idusuarios", idusuarios);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
               cadastrado = 1;
            }
            conexao.Desconectar();
            return cadastrado;
        }

        public ModeloCentrodeCusto CarregaCentrodeCusto(int codigo)
        {
            ModeloCentrodeCusto modelo = new ModeloCentrodeCusto();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from centros_de_custos where idcentros_de_custos=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdCentros_de_Custos = Convert.ToInt32(registro["idcentros_de_custos"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.Codigo = Convert.ToString(registro["codigo"]);
                modelo.Descricao = Convert.ToString(registro["descricao"]);
            }
            conexao.Desconectar();
            return modelo;
        }
    }
}
