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
    public class DALMateriais
    {
        private DALConexao conexao;
        public DALMateriais(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloMateriais modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into mat_propostas (idpropostas,material,unidade,quantidade,vl_unitario,vl_total) " +
                "values (@idpropostas,@material,@unidade,@quantidade,@vl_unitario,@vl_total); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idpropostas", modelo.IdPropostas);
            cmd.Parameters.AddWithValue("@material", modelo.Material);
            cmd.Parameters.AddWithValue("@unidade", modelo.Unidade);
            cmd.Parameters.AddWithValue("@quantidade", modelo.Quantidade);
            cmd.Parameters.AddWithValue("@vl_unitario", modelo.Vl_Unitario);
            cmd.Parameters.AddWithValue("@vl_total", modelo.Vl_Total);

            conexao.Conectar();
            modelo.IdMat_Propostas = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloMateriais modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update mat_propostas set material=@material,unidade=@unidade,quantidade=@quantidade,vl_unitario=@vl_unitario,vl_total=@vl_total " +
                "where idmat_propostas=@idmat_propostas;";
            cmd.Parameters.AddWithValue("@idmat_propostas", modelo.IdMat_Propostas);
            cmd.Parameters.AddWithValue("@material", modelo.Material);
            cmd.Parameters.AddWithValue("@unidade", modelo.Unidade);
            cmd.Parameters.AddWithValue("@quantidade", modelo.Quantidade);
            cmd.Parameters.AddWithValue("@vl_unitario", modelo.Vl_Unitario);
            cmd.Parameters.AddWithValue("@vl_total", modelo.Vl_Total);

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

        public DataTable CarregaMateriais(int codigo)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idmat_propostas, material, unidade, quantidade, CONVERT(varchar,CONVERT(money,vl_unitario, 2)) as vl_unitario, CONVERT(varchar,CONVERT(money,vl_total, 2)) as vl_total from mat_propostas where idpropostas=" + codigo + " order by idmat_propostas", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
    }
}
