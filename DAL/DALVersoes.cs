using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALVersoes
    {
        private DALConexao conexao;
        public DALVersoes(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloVersoes modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into versao (maior,menor,build,arquivo,nome_arquivo,data_atualizacao) " +
                "values (@maior,@menor,@build,@arquivo,@nome_arquivo,@data_atualizacao); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@maior", modelo.Maior);
            cmd.Parameters.AddWithValue("@menor", modelo.Menor);
            cmd.Parameters.AddWithValue("@build", modelo.Build);
            cmd.Parameters.AddWithValue("@arquivo", File.ReadAllBytes(modelo.Arquivo));
            cmd.Parameters.AddWithValue("@nome_arquivo", Path.GetFileName(modelo.Nome_Arquivo));
            cmd.Parameters.Add("@data_atualizacao", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@data_atualizacao"].Value = modelo.Data_Atualizacao;

            conexao.Conectar();
            modelo.IdVersao = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloVersoes modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update versao set maior=@maior,menor=@menor,build=@build,arquivo=@arquivo,nome_arquivo=@nome_arquivo,data_atualizacao=@data_atualizacao " +
                "where idversao=@idversao;";
            cmd.Parameters.AddWithValue("@idversao", modelo.IdVersao);
            cmd.Parameters.AddWithValue("@maior", modelo.Maior);
            cmd.Parameters.AddWithValue("@menor", modelo.Menor);
            cmd.Parameters.AddWithValue("@build", modelo.Build);
            cmd.Parameters.AddWithValue("@arquivo", File.ReadAllBytes(modelo.Arquivo));
            cmd.Parameters.AddWithValue("@nome_arquivo", Path.GetFileName(modelo.Nome_Arquivo));
            cmd.Parameters.Add("@data_atualizacao", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@data_atualizacao"].Value = modelo.Data_Atualizacao;

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public void AbrirVersao(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select arquivo,nome_arquivo from versao where idversao=@idversao;";
            cmd.Parameters.AddWithValue("@idversao", codigo);

            conexao.Conectar();
            var bytes = cmd.ExecuteScalar() as byte[];
            if (bytes != null)
            {
                SqlDataReader registro = cmd.ExecuteReader();
                registro.Read();
                string nome_arquivo = registro["nome_arquivo"].ToString();
                var arquivotemp = Path.GetTempFileName();
                arquivotemp = Path.ChangeExtension(arquivotemp, Path.GetExtension(nome_arquivo));
                File.WriteAllBytes(arquivotemp, bytes);
                Process.Start(arquivotemp);
            }
            conexao.Desconectar();
        }

        public DataTable CarregaVersao()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idversao, (CONVERT(varchar,maior) + '.' + CONVERT(varchar,menor) + '.' + CONVERT(varchar,build)) as versao, data_atualizacao from versao order by idversao", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
    }
}
