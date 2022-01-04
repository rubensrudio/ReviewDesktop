using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DALModulo
    {
        private DALConexao conexao;
        public DALModulo(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloModulo modelo)
        {
            
        }

        public void Alterar(ModeloModulo modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update modulos set modulo=@modulo,valor=@valor " +
                "where idmodulos=@idmodulos;";
            cmd.Parameters.AddWithValue("@idmodulos", modelo.IdModulos);
            cmd.Parameters.AddWithValue("@modulo", modelo.Modulo);
            cmd.Parameters.AddWithValue("@valor", modelo.Valor);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public void Excluir(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "delete from modulos where idmodulos=@idmodulos;";
            cmd.Parameters.AddWithValue("@idmodulos", codigo);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }
        public DataTable Localizar(int idempresas, int idusuarios)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select m.idmodulos, m.modulo, (select 1 from modulos_usuarios mu where mu.idusuarios="+idusuarios+" and mu.idmodulos=m.idmodulos) as ch from modulos m order by m.modulo", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public bool LocalizarModuloUsuario(int idmodulos, int idusuarios)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from modulos_usuarios where idmodulos=" + idmodulos.ToString() + " and idusuarios=" + idusuarios.ToString();
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

    }
}
