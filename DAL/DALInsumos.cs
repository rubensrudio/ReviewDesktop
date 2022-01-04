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
    public class DALInsumos
    {
        private DALConexao conexao;
        public DALInsumos(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloInsumos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into insumos (idempresas,idplanosdecontas,idclassificacao,descricao,unidade,tipo,dt_atualizacao,preco) " +
                "values (@idempresas,@idplanosdecontas,@idclassificacao,@descricao,@unidade,@tipo,@dt_atualizacao,@preco); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@idplanosdecontas", modelo.IdPlanosdeContas);
            cmd.Parameters.AddWithValue("@idclassificacao", modelo.IdClassificacao);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@unidade", modelo.Unidade);
            cmd.Parameters.AddWithValue("@tipo", modelo.Tipo);
            cmd.Parameters.Add("@dt_atualizacao", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@dt_atualizacao"].Value = modelo.Dt_Atualizacao;
            cmd.Parameters.AddWithValue("@preco", modelo.Preco);

            conexao.Conectar();
            modelo.IdInsumos = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloInsumos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update insumos set idplanosdecontas=@idplanosdecontas,idclassificacao=@idclassificacao,descricao=@descricao, " +
                "unidade=@unidade,tipo=@tipo,dt_atualizacao=@dt_atualizacao,preco=@preco " +
                "where idinsumos=@idinsumos;";
            cmd.Parameters.AddWithValue("@idinsumos", modelo.IdInsumos);
            cmd.Parameters.AddWithValue("@idplanosdecontas", modelo.IdPlanosdeContas);
            cmd.Parameters.AddWithValue("@idclassificacao", modelo.IdClassificacao);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@unidade", modelo.Unidade);
            cmd.Parameters.AddWithValue("@tipo", modelo.Tipo);
            cmd.Parameters.Add("@dt_atualizacao", System.Data.SqlDbType.DateTime);
            cmd.Parameters["@dt_atualizacao"].Value = modelo.Dt_Atualizacao;
            cmd.Parameters.AddWithValue("@preco", modelo.Preco);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public DataTable Localizar(String valor, String buscapor, int idempresas)
        {
            String where = "i.descricao";
            if (buscapor == "tipo")
            {
                where = "i.tipo";
            }
            else
            {
                where = "i.descricao";
            }
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select i.idinsumos,i.descricao,i.unidade,FORMAT(i.preco, 'C', 'pt-br') as valor,i.dt_atualizacao,c.descricao as class,p.descricao as plano from insumos i join classificacao c on c.idclassificacao=i.idclassificacao join planosdecontas p on p.idplanosdecontas=i.idplanosdecontas where " + where + " like '%" + valor + "%' and i.idempresas=" + idempresas + " order by i.descricao", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public ModeloInsumos CarregaInsumos(int codigo)
        {
            ModeloInsumos modelo = new ModeloInsumos();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from insumos where idinsumos=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdInsumos = Convert.ToInt32(registro["idinsumos"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.IdPlanosdeContas = Convert.ToInt32(registro["idplanosdecontas"]);
                modelo.IdClassificacao = Convert.ToInt32(registro["idclassificacao"]);
                modelo.Descricao = Convert.ToString(registro["descricao"]);
                modelo.Unidade = Convert.ToString(registro["unidade"]);
                modelo.Tipo = Convert.ToString(registro["tipo"]);
                if (registro["dt_atualizacao"].ToString() != "")
                {
                    modelo.Dt_Atualizacao = Convert.ToDateTime(registro["dt_atualizacao"]);
                }
                if (registro["preco"].ToString() != "")
                {
                    modelo.Preco = Convert.ToDouble(registro["preco"]);
                }
            }
            conexao.Desconectar();
            return modelo;
        }
    }
}
