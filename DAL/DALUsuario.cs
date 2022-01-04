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
    public class DALUsuario
    {
        private DALConexao conexao;
        public DALUsuario(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloUsuario modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into usuarios (idempresas,usuario,senha,nome) " +
                "values (@idempresas,@usuario,@senha,@nome); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@usuario", modelo.Usuario);
            cmd.Parameters.AddWithValue("@senha", modelo.Senha);
            cmd.Parameters.AddWithValue("@nome", modelo.Nome);
           
            conexao.Conectar();
            modelo.IdUsuarios = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloUsuario modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update usuarios set usuario=@usuario,senha=@senha,nome=@nome " +
                "where idusuarios=@idusuarios;";
            cmd.Parameters.AddWithValue("@idusuarios", modelo.IdUsuarios);
            cmd.Parameters.AddWithValue("@usuario", modelo.Usuario);
            cmd.Parameters.AddWithValue("@senha", modelo.Senha);
            cmd.Parameters.AddWithValue("@nome", modelo.Nome);
            
            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public void Excluir(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "delete from usuarios where idusuarios=@idusuarios;";
            cmd.Parameters.AddWithValue("@idusuarios", codigo);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }
        public DataTable Localizar(int idempresas, int idcentrodecusto)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select u.idusuarios, u.usuario, u.nome, (select 1 from centros_de_custos_usuarios c where c.idusuarios=u.idusuarios and c.idcentros_de_custos="+ idcentrodecusto +") as ch from usuarios u where u.idempresas=" + idempresas + " order by u.usuario", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public DataTable Localizar(String valor, String buscapor, int idempresas)
        {
            String where = "nome";
            if (buscapor == "Usuário")
            {
                where = "usuario";
            }
            else
            {
                where = "nome";
            }
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idusuarios,usuario,nome from usuarios where " + where + " like '%" + valor + "%' and idempresas="+idempresas+ " order by " + where, conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public int LocalizaCentrodeCusto(int idcentrodecusto, int idusuarios)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select idcentros_de_custos from centros_de_custos_usuarios where idusuarios=@idusuarios and idcentros_de_custos=@idcentrodecusto;";
            cmd.Parameters.AddWithValue("@idusuarios", idusuarios);
            cmd.Parameters.AddWithValue("@idcentrodecusto", idcentrodecusto);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                conexao.Desconectar();
                return 1;
            }
            else
            {
                conexao.Desconectar();
                return 0;
            }
        }

        public int VerificaUsuario(String usuario, String senha, int idusuarios)
        {
            int cadastrado = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select idusuarios from usuarios where usuario=@usuario and senha=@senha;";
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@senha", senha);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                while (registro.Read()) {
                    if ((Convert.ToInt32(registro["idusuarios"]) > 0) && (idusuarios == 0))
                    {
                        cadastrado = 1;
                        break;
                    }
                    else if ((Convert.ToInt32(registro["idusuarios"]) > 0) && (idusuarios != (Convert.ToInt32(registro["idusuarios"]))))
                    {
                        cadastrado = 1;
                        break;
                    }
                }
            }
            conexao.Desconectar();
            return cadastrado;
        }

        public ModeloUsuario CarregaUsuario(int codigo)
        {
            ModeloUsuario modelo = new ModeloUsuario();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from usuarios where idusuarios=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdUsuarios = Convert.ToInt32(registro["idusuarios"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.Usuario = Convert.ToString(registro["usuario"]);
                modelo.Senha = Convert.ToString(registro["senha"]);
                modelo.Nome = Convert.ToString(registro["nome"]);
            }
            conexao.Desconectar();
            return modelo;
        }

        public ModeloUsuario CarregaUsuario(String usuario, String senha)
        {
            ModeloUsuario modelo = new ModeloUsuario();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from usuarios where usuario like '" + usuario + "' and senha like '" + senha + "'";
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdUsuarios = Convert.ToInt32(registro["idusuarios"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.Usuario = Convert.ToString(registro["usuario"]);
                modelo.Senha = Convert.ToString(registro["senha"]);
                modelo.Nome = Convert.ToString(registro["nome"]);
            }
            conexao.Desconectar();
            return modelo;
        }
    }
}
