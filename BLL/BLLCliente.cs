using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCliente
    {
        private DALConexao conexao;
        public BLLCliente(DALConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloCliente modelo, ModeloEndereco modeloEnd, int idempresas)
        {
            if (modelo.Razao_Social.Trim().Length == 0)
            {
                throw new Exception("Razão Social é obrigatória!");
            }
            if (modelo.Nome_Fantasia.Trim().Length == 0)
            {
                throw new Exception("Nome Fantasia é obrigatório!");
            }
            if (modelo.CNPJ.Trim().Length == 0)
            {
                throw new Exception("CNPJ é obrigatório!");
            }
            DALCliente DALobj1 = new DALCliente(conexao);
            if (DALobj1.VerificaCNPJ(modelo.CNPJ, idempresas) == 1)
            {
                throw new Exception("CNPJ já existe!");
            }
            if (modeloEnd.Logradouro.Trim().Length == 0)
            {
                throw new Exception("Logradouro é obrigatório!");
            }
            if (modeloEnd.Bairro.Trim().Length == 0)
            {
                throw new Exception("Bairro é obrigatório!");
            }
            if (modeloEnd.Cidade.Trim().Length == 0)
            {
                throw new Exception("Cidade é obrigatória!");
            }
            if (modeloEnd.UF.Trim().Length == 0)
            {
                throw new Exception("UF é obrigatório!");
            }

            DALEndereco DALobj = new DALEndereco(conexao);
            DALobj.Incluir(modeloEnd);

            DALobj1.Incluir(modelo, modeloEnd);
        }
        public void Alterar(ModeloCliente modelo, ModeloEndereco modeloEnd)
        {
            if (modelo.IdClientes <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Razao_Social.Trim().Length == 0)
            {
                throw new Exception("Razão Social é obrigatória!");
            }
            if (modelo.Nome_Fantasia.Trim().Length == 0)
            {
                throw new Exception("Nome Fantasia é obrigatório!");
            }
            if (modelo.CNPJ.Trim().Length == 0)
            {
                throw new Exception("CNPJ é obrigatório!");
            }
            if (modeloEnd.IdEnderecos <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modeloEnd.Logradouro.Trim().Length == 0)
            {
                throw new Exception("Logradouro é obrigatório!");
            }
            if (modeloEnd.Bairro.Trim().Length == 0)
            {
                throw new Exception("Bairro é obrigatório!");
            }
            if (modeloEnd.Cidade.Trim().Length == 0)
            {
                throw new Exception("Cidade é obrigatória!");
            }
            if (modeloEnd.UF.Trim().Length == 0)
            {
                throw new Exception("UF é obrigatório!");
            }

            DALEndereco DALobj = new DALEndereco(conexao);
            DALobj.Alterar(modeloEnd);
            DALCliente DALobj1 = new DALCliente(conexao);
            DALobj1.Alterar(modelo);
        }
        public void Excluir(int codigo)
        {
            DALCliente DALobj = new DALCliente(conexao);
            DALobj.Excluir(codigo);
        }
        public DataTable Localizar(String valor, String buscapor, int idempresas, String clifor)
        {
            DALCliente DALobj = new DALCliente(conexao);
            return DALobj.Localizar(valor, buscapor, idempresas, clifor);
        }
        public int LocalizarEnd(int codigo)
        {
            DALCliente DALobj = new DALCliente(conexao);
            return DALobj.LocalizarEnd(codigo);
        }
        public ModeloCliente CarregaCliente(int codigo)
        {
            DALCliente DALobj = new DALCliente(conexao);
            return DALobj.CarregaCliente(codigo);
        }
    }
}
