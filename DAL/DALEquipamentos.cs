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
    public class DALEquipamentos
    {
        private DALConexao conexao;
        public DALEquipamentos(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloEquipamentos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into equip_propostas (idpropostas,equipamento,quantidade) " +
                "values (@idpropostas,@equipamento,@quantidade); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idpropostas", modelo.IdPropostas);
            cmd.Parameters.AddWithValue("@equipamento", modelo.Equipamento);
            cmd.Parameters.AddWithValue("@quantidade", modelo.Quantidade);

            conexao.Conectar();
            modelo.IdEquip_Propostas = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloEquipamentos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update equip_propostas set equipamento=@equipamento,quantidade=@quantidade " +
                "where idequip_propostas=@idequip_propostas;";
            cmd.Parameters.AddWithValue("@idequip_propostas", modelo.IdEquip_Propostas);
            cmd.Parameters.AddWithValue("@equipamento", modelo.Equipamento);
            cmd.Parameters.AddWithValue("@quantidade", modelo.Quantidade);

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

        public DataTable CarregaEquipamentos(int codigo)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idequip_propostas, equipamento, quantidade from equip_propostas where idpropostas=" + codigo + " order by idequip_propostas", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
    }
}
