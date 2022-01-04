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
    public class BLLEmpresas
    {
        private DALConexao conexao;
        public BLLEmpresas(DALConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloEmpresa modelo, ModeloEndereco modeloEnd)
        {

            if (modelo.Razao_Social.Trim().Length == 0)
            {
                throw new Exception("Razão Social é obrigatório!");
            }
            if (modelo.Nome_Fantasia.Trim().Length == 0)
            {
                throw new Exception("Nome Fantasia é obrigatório!");
            }
            if (modelo.CNPJ.Trim().Length == 0)
            {
                throw new Exception("CNPJ é obrigatório!");
            }
            DALEmpresa DALobj1 = new DALEmpresa(conexao);
            if (DALobj1.VerificaCNPJ(modelo.CNPJ) == 1)
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
            if (modelo.Logo == null)
            {
                throw new Exception("Logo é obrigatória!");
            }

            DALEndereco DALobj = new DALEndereco(conexao);
            DALobj.Incluir(modeloEnd);

            DALobj1.Incluir(modelo, modeloEnd);
        }
        public void Alterar(ModeloEmpresa modelo, ModeloEndereco modeloEnd)
        {
            if (modelo.IdEmpresas <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Razao_Social.Trim().Length == 0)
            {
                throw new Exception("Razão Social é obrigatório!");
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
            if (modelo.Logo == null)
            {
                throw new Exception("Logo é obrigatória!");
            }

            DALEndereco DALobj = new DALEndereco(conexao);
            DALobj.Alterar(modeloEnd);
            DALEmpresa DALobj1 = new DALEmpresa(conexao);
            DALobj1.Alterar(modelo);
        }
        public void Excluir(int codigo)
        {
            DALEmpresa DALobj = new DALEmpresa(conexao);
            DALobj.Excluir(codigo);
        }
        public DataTable Localizar(String valor, String buscapor)
        {
            DALEmpresa DALobj = new DALEmpresa(conexao);
            return DALobj.Localizar(valor, buscapor);
        }
        public int LocalizarEnd(int codigo)
        {
            DALEmpresa DALobj = new DALEmpresa(conexao);
            return DALobj.LocalizarEnd(codigo);
        }
        public ModeloEmpresa CarregaEmpresa(int codigo)
        {
            DALEmpresa DALobj = new DALEmpresa(conexao);
            return DALobj.CarregaEmpresa(codigo);
        }
        public ModeloEndereco CarregaEndereco(int codigo)
        {
            DALEndereco DALobj = new DALEndereco(conexao);
            return DALobj.CarregaEndereco(codigo);
        }
    }
}
