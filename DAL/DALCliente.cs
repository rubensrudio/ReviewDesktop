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
    public class DALCliente
    {
        private DALConexao conexao;
        public DALCliente(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloCliente modelo, ModeloEndereco modeloEnd)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into clientes (idempresas,idenderecos,razao_social,nome_fantasia,cnpj,insc_est,email,responsavel,ramo_atividade,telefone1,telefone2,clientefornecedor) " +
                "values (@idempresas,@idenderecos,@razao_social,@nome_fantasia,@cnpj,@insc_est,@email,@responsavel,@ramo_atividade,@telefone1,@telefone2,@clientefornecedor); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@idenderecos", modeloEnd.IdEnderecos);
            cmd.Parameters.AddWithValue("@razao_social", modelo.Razao_Social);
            cmd.Parameters.AddWithValue("@nome_fantasia", modelo.Nome_Fantasia);
            cmd.Parameters.AddWithValue("@cnpj", modelo.CNPJ);
            cmd.Parameters.AddWithValue("@insc_est", modelo.Insc_Est);
            cmd.Parameters.AddWithValue("@email", modelo.Email);
            cmd.Parameters.AddWithValue("@responsavel", modelo.Responsavel);
            cmd.Parameters.AddWithValue("@ramo_atividade", modelo.Ramo_Atividade);
            cmd.Parameters.AddWithValue("@telefone1", modelo.Telefone1);
            cmd.Parameters.AddWithValue("@telefone2", modelo.Telefone2);
            cmd.Parameters.AddWithValue("@clientefornecedor", modelo.ClienteFornecedor);

            conexao.Conectar();
            modelo.IdClientes = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloCliente modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update clientes set razao_social=@razao_social,nome_fantasia=@nome_fantasia,cnpj=@cnpj,insc_est=@insc_est,email=@email,responsavel=@responsavel,ramo_atividade=@ramo_atividade,telefone1=@telefone1,telefone2=@telefone2,clientefornecedor=@clientefornecedor " +
                "where idclientes=@idclientes;";
            cmd.Parameters.AddWithValue("@idclientes", modelo.IdClientes);
            cmd.Parameters.AddWithValue("@razao_social", modelo.Razao_Social);
            cmd.Parameters.AddWithValue("@nome_fantasia", modelo.Nome_Fantasia);
            cmd.Parameters.AddWithValue("@cnpj", modelo.CNPJ);
            cmd.Parameters.AddWithValue("@insc_est", modelo.Insc_Est);
            cmd.Parameters.AddWithValue("@email", modelo.Email);
            cmd.Parameters.AddWithValue("@responsavel", modelo.Responsavel);
            cmd.Parameters.AddWithValue("@ramo_atividade", modelo.Ramo_Atividade);
            cmd.Parameters.AddWithValue("@telefone1", modelo.Telefone1);
            cmd.Parameters.AddWithValue("@telefone2", modelo.Telefone2);
            cmd.Parameters.AddWithValue("@clientefornecedor", modelo.ClienteFornecedor);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public void Excluir(int codigo)
        {
            int idenderecos = LocalizarEnd(codigo);

            //Excluir Empresa
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "delete from clientes where idclientes=@idclientes;";
            cmd.Parameters.AddWithValue("@idclientes", codigo);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();

            //Excluir Endereço
            DALEndereco DALObj = new DALEndereco(this.conexao);
            DALObj.Excluir(idenderecos);
        }

        public DataTable Localizar(String valor, String buscapor, int idempresas, String clifor)
        {
            String where = "nome_fantasia";
            if (buscapor == "Razão Social")
            {
                where = "razao_social";
            }
            else if (buscapor == "Nome Fantasia")
            {
                where = "nome_fantasia";
            }
            else
            {
                where = "CNPJ";
            }

            String where1;
            if (clifor == "cl") {
                where1 = " and (clientefornecedor='cl' or clientefornecedor='cf') and ";
            }
            else
            {
                where1 = " and (clientefornecedor='fo' or clientefornecedor='cf') and ";
            }
            DataTable tabela = new DataTable();
            String sql = "select idclientes,idenderecos,razao_social,nome_fantasia,cnpj from clientes where idempresas=" + idempresas.ToString() + where1 + where + " like '%" + valor + "%' order by " + where;
            SqlDataAdapter da = new SqlDataAdapter(sql, conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public int LocalizarEnd(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select idenderecos from clientes where idclientes=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            int idenderecos = 0;
            if (registro.HasRows)
            {
                registro.Read();
                idenderecos = Convert.ToInt32(registro["idenderecos"]);
            }
            conexao.Desconectar();
            return idenderecos;
        }

        public int VerificaCNPJ(String valor, int idempresas)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select cnpj from clientes where cnpj like '" + valor.ToString() + "' and idempresas="+idempresas;
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            int cnpj = 0;
            if (registro.HasRows)
            {
                registro.Read();
                cnpj = 1;
            }
            conexao.Desconectar();
            return cnpj;
        }

        public ModeloCliente CarregaCliente(int codigo)
        {
            ModeloCliente modelo = new ModeloCliente();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from clientes where idclientes=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.IdClientes = Convert.ToInt32(registro["idclientes"]);
                modelo.IdEmpresas = Convert.ToInt32(registro["idempresas"]);
                modelo.IdEnderecos = Convert.ToInt32(registro["idenderecos"]);
                modelo.Razao_Social = Convert.ToString(registro["razao_social"]);
                modelo.Nome_Fantasia = Convert.ToString(registro["nome_fantasia"]);
                modelo.CNPJ = Convert.ToString(registro["cnpj"]);
                modelo.Insc_Est = Convert.ToString(registro["insc_est"]);
                modelo.Email = Convert.ToString(registro["email"]);
                modelo.Responsavel = Convert.ToString(registro["responsavel"]);
                modelo.Ramo_Atividade = Convert.ToString(registro["ramo_atividade"]);
                modelo.Telefone1 = Convert.ToString(registro["telefone1"]);
                modelo.Telefone2 = Convert.ToString(registro["telefone2"]);
                modelo.ClienteFornecedor = Convert.ToString(registro["clientefornecedor"]);
            }
            conexao.Desconectar();
            return modelo;
        }
    }
}
