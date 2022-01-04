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
    public class DALClassificacao
    {
        private DALConexao conexao;
        public DALClassificacao(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloClassificacao modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into classificacao (idempresas,descricao,indice,indicepai,analiticosintetico) " +
                "values (@idempresas,@descricao,@indice,@indicepai,@analiticosintetico); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@indice", modelo.Indice);
            cmd.Parameters.AddWithValue("@indicepai", modelo.IndicePai);
            cmd.Parameters.AddWithValue("@analiticosintetico", modelo.AnaliticoSintetico);

            conexao.Conectar();
            modelo.IdClassificacao = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloClassificacao modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update classificacao set descricao=@descricao,indice=@indice,indicepai=@indicepai,analiticosintetico=@analiticosintetico " +
                "where idclassificacao=@idclassificacao;";
            cmd.Parameters.AddWithValue("@idclassificacao", modelo.IdClassificacao);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@indice", modelo.Indice);
            cmd.Parameters.AddWithValue("@indicepai", modelo.IndicePai);
            cmd.Parameters.AddWithValue("@analiticosintetico", modelo.AnaliticoSintetico);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }
        
        public DataTable Localizar(String valor, String buscapor, int idempresas)
        {
            String where = "descricao";
            if (buscapor == "Índice")
            {
                where = "indice";
            }
            else
            {
                where = "descricao";
            }
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idclassificacao,indice,descricao from classificacao where " + where + " like '%" + valor + "%' and idempresas=" + idempresas + " order by indice", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public DataTable LocalizarPai(int idempresas)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idclassificacao,indice,indicepai,(indice + ' - ' + descricao) as descr from classificacao where analiticosintetico='Sintético' and idempresas=" + idempresas + " order by indice", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public DataTable LocalizarAnalitico(int idempresas)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idclassificacao,indice,indicepai,(indice + ' - ' + descricao) as descr from classificacao where analiticosintetico='Analítico' and idempresas=" + idempresas + " order by indice", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
        public String LocalizarFilho(int idempresas)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select indice from classificacao where analiticosintetico='Sintético' and idempresas=" + idempresas + " order by indice desc";
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            String indice = "";
            if (registro.HasRows)
            {
                registro.Read();
                indice = registro["indice"].ToString();
            }
            conexao.Desconectar();
            return indice;
        }

        public String LocalizarFilho(int idempresas, string pai)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select indice from classificacao where indice like '"+pai+"%' and idempresas=" + idempresas + " order by indice desc";
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            String indice = "";
            if (registro.HasRows)
            {
                registro.Read();
                indice = registro["indice"].ToString();
            }
            conexao.Desconectar();
            return indice;
        }

        public ModeloClassificacao CarregaClassificacao(int codigo)
        {
            ModeloClassificacao modelo = new ModeloClassificacao();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from classificacao where idclassificacao=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdClassificacao = Convert.ToInt32(registro["idclassificacao"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.Descricao = Convert.ToString(registro["descricao"]);
                modelo.Indice = Convert.ToString(registro["indice"]);
                modelo.IndicePai = Convert.ToString(registro["indicepai"]);
                modelo.AnaliticoSintetico = Convert.ToString(registro["analiticosintetico"]);
            }
            conexao.Desconectar();
            return modelo;
        }
    }
}
