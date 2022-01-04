using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace DAL
{
    public class DALEmpresa
    {
        private DALConexao conexao;
        public DALEmpresa(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloEmpresa modelo,ModeloEndereco modeloEnd)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into empresas (idenderecos,razao_social,nome_fantasia,cnpj,insc_est,email,responsavel,ramo_atividade,telefone1,telefone2,logo,nome_arquivo) " +
                "values (@idenderecos,@razao_social,@nome_fantasia,@cnpj,@insc_est,@email,@responsavel,@ramo_atividade,@telefone1,@telefone2,@logo,@nome_arquivo); select @@IDENTITY;";
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
            cmd.Parameters.AddWithValue("@logo", File.ReadAllBytes(modelo.Nome_Arquivo));
            cmd.Parameters.AddWithValue("@nome_arquivo", Path.GetFileName(modelo.Nome_Arquivo));

            conexao.Conectar();
            modelo.IdEmpresas = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloEmpresa modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update empresas set razao_social=@razao_social,nome_fantasia=@nome_fantasia,cnpj=@cnpj,insc_est=@insc_est,email=@email,responsavel=@responsavel,ramo_atividade=@ramo_atividade,telefone1=@telefone1,telefone2=@telefone2,logo=@logo,nome_arquivo=@nome_arquivo " +
                "where idempresas=@idempresas;";
            cmd.Parameters.AddWithValue("@idempresas", modelo.IdEmpresas);
            cmd.Parameters.AddWithValue("@razao_social", modelo.Razao_Social);
            cmd.Parameters.AddWithValue("@nome_fantasia", modelo.Nome_Fantasia);
            cmd.Parameters.AddWithValue("@cnpj", modelo.CNPJ);
            cmd.Parameters.AddWithValue("@insc_est", modelo.Insc_Est);
            cmd.Parameters.AddWithValue("@email", modelo.Email);
            cmd.Parameters.AddWithValue("@responsavel", modelo.Responsavel);
            cmd.Parameters.AddWithValue("@ramo_atividade", modelo.Ramo_Atividade);
            cmd.Parameters.AddWithValue("@telefone1", modelo.Telefone1);
            cmd.Parameters.AddWithValue("@telefone2", modelo.Telefone2);
            cmd.Parameters.AddWithValue("@logo", modelo.Logo);
            cmd.Parameters.AddWithValue("@nome_arquivo", modelo.Nome_Arquivo);

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
            cmd.CommandText = "delete from empresas where idempresas=@idempresas;";
            cmd.Parameters.AddWithValue("@idempresas", codigo);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();

            //Excluir Endereço
            DALEndereco DALObj = new DALEndereco(this.conexao);
            DALObj.Excluir(idenderecos);
        }

        public DataTable Localizar(String valor, String buscapor)
        {
            String where = "nome_fantasia";
            if(buscapor == "Razão Social")
            {
                where = "razao_social";
            }
            else if(buscapor == "Nome Fantasia")
            {
                where = "nome_fantasia";
            }
            else
            {
                where = "CNPJ";
            }
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idempresas,razao_social,nome_fantasia,cnpj from empresas where "+where+" like '%" + valor + "%' order by " + where, conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public int LocalizarEnd(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select idenderecos from empresas where idempresas=" + codigo.ToString();
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

        public int VerificaCNPJ(String valor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select cnpj from empresas where cnpj like '" + valor.ToString() + "'";
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

        public ModeloEmpresa CarregaEmpresa(int codigo)
        {
            ModeloEmpresa modelo = new ModeloEmpresa();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select * from empresas where idempresas=" + codigo.ToString();
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            
            if (registro.HasRows)
            {
                registro.Read();
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
                if (registro["logo"].ToString() != "")
                {
                    modelo.Logo = (Byte[])registro["logo"];
                }
                modelo.Nome_Arquivo = Convert.ToString(registro["nome_arquivo"]);

            }
            conexao.Desconectar();

            return modelo;
        }

        
    }
}
