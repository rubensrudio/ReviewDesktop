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
    public class DALServicos
    {
        private DALConexao conexao;
        public DALServicos(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloServicos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into serv_propostas (idpropostas,servico,valor,unidade,quantidade,vl_total) " +
                "values (@idpropostas,@servico,@valor,@unidade,@quantidade,@vl_total); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idpropostas", modelo.IdPropostas);
            cmd.Parameters.AddWithValue("@servico", modelo.Servico);
            cmd.Parameters.AddWithValue("@valor", modelo.Valor);
            cmd.Parameters.AddWithValue("@unidade", modelo.Unidade);
            cmd.Parameters.AddWithValue("@quantidade", modelo.Quantidade);
            cmd.Parameters.AddWithValue("@vl_total", modelo.Vl_Total);

            conexao.Conectar();
            modelo.IdServ_Propostas = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloServicos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update serv_propostas set servico=@servico,valor=@valor,unidade=@unidade,quantidade=@quantidade,vl_total=@vl_total " +
                "where idserv_propostas=@idserv_propostas;";
            cmd.Parameters.AddWithValue("@idserv_propostas", modelo.IdServ_Propostas);
            cmd.Parameters.AddWithValue("@servico", modelo.Servico);
            cmd.Parameters.AddWithValue("@valor", modelo.Valor);
            cmd.Parameters.AddWithValue("@unidade", modelo.Unidade);
            cmd.Parameters.AddWithValue("@quantidade", modelo.Quantidade);
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

        public DataTable CarregaServicos(int codigo)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idserv_propostas, servico,unidade,quantidade, CONVERT(varchar,CONVERT(money,valor, 2)) as valor, CONVERT(varchar,CONVERT(money,vl_total, 2)) as vl_total from serv_propostas where idpropostas=" + codigo + " order by idserv_propostas", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
    }
}
