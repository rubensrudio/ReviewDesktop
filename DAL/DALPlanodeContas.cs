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
    public class DALPlanodeContas
    {
        private DALConexao conexao;
        public DALPlanodeContas(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloPlanodeContas modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into planosdecontas (idempresas,descricao,indice,planopai,analiticosintetico,debitocredito,liberado,gerarpago,ativo) " +
                "values (@idempresas,@descricao,@indice,@planopai,@analiticosintetico,@debitocredito,@liberado,@gerarpago,@ativo); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@indice", modelo.Indice);
            cmd.Parameters.AddWithValue("@planopai", modelo.PlanoPai);
            cmd.Parameters.AddWithValue("@analiticosintetico", modelo.AnaliticoSintetico);
            cmd.Parameters.AddWithValue("@debitocredito", modelo.DebitoCredito);
            cmd.Parameters.AddWithValue("@liberado", modelo.Liberado);
            cmd.Parameters.AddWithValue("@gerarpago", modelo.GerarPago);
            cmd.Parameters.AddWithValue("@ativo", modelo.Ativo);
            
            conexao.Conectar();
            modelo.IdPlanodeContas = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloPlanodeContas modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update planosdecontas set descricao=@descricao,indice=@indice,planopai=@planopai,analiticosintetico=@analiticosintetico, " +
                "debitocredito=@debitocredito,liberado=@liberado,gerarpago=@gerarpago,ativo=@ativo" +
                " where idplanosdecontas=@idplanosdecontas;";
            cmd.Parameters.AddWithValue("@idplanosdecontas", modelo.IdPlanodeContas);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@indice", modelo.Indice);
            cmd.Parameters.AddWithValue("@planopai", modelo.PlanoPai);
            cmd.Parameters.AddWithValue("@analiticosintetico", modelo.AnaliticoSintetico);
            cmd.Parameters.AddWithValue("@debitocredito", modelo.DebitoCredito);
            cmd.Parameters.AddWithValue("@liberado", modelo.Liberado);
            cmd.Parameters.AddWithValue("@gerarpago", modelo.GerarPago);
            cmd.Parameters.AddWithValue("@ativo", modelo.Ativo);

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
            SqlDataAdapter da = new SqlDataAdapter("select idplanosdecontas,indice,descricao,debitocredito,(case ativo when 1 then 'Sim' else 'Não' end) as at from planosdecontas where " + where + " like '%" + valor + "%' and idempresas=" + idempresas + " order by indice", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public DataTable LocalizarPai(int idempresas)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idplanosdecontas,indice,planopai,(indice + ' - ' + descricao) as descr from planosdecontas where analiticosintetico='Sintético' and idempresas=" + idempresas + " order by indice", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public DataTable LocalizarAnalitico(int idempresas)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idplanosdecontas,indice,planopai,(indice + ' - ' + descricao) as descr from planosdecontas where analiticosintetico='Analítico' and ativo=1 and debitocredito='Débito' and idempresas=" + idempresas + " order by indice", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public String LocalizarFilho(int idempresas)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select indice from planosdecontas where analiticosintetico='Sintético' and idempresas=" + idempresas + " order by indice desc";
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
            cmd.CommandText = "select indice from planosdecontas where indice like '" + pai + "%' and idempresas=" + idempresas + " order by indice desc";
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

        public ModeloPlanodeContas CarregaPlanodeContas(int codigo)
        {
            ModeloPlanodeContas modelo = new ModeloPlanodeContas();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from planosdecontas where idplanosdecontas=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdPlanodeContas = Convert.ToInt32(registro["idplanosdecontas"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.Descricao = Convert.ToString(registro["descricao"]);
                modelo.Indice = Convert.ToString(registro["indice"]);
                modelo.PlanoPai = Convert.ToString(registro["planopai"]);
                modelo.AnaliticoSintetico = Convert.ToString(registro["analiticosintetico"]);
                modelo.DebitoCredito = Convert.ToString(registro["debitocredito"]);
                modelo.Liberado = Convert.ToInt32(registro["liberado"]);
                modelo.GerarPago = Convert.ToInt32(registro["gerarpago"]);
                modelo.Ativo = Convert.ToInt32(registro["ativo"]);
            }
            conexao.Desconectar();
            return modelo;
        }
    }
}
