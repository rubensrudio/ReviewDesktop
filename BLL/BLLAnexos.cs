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
    public class BLLAnexos
    {
        private DALConexao conexao;
        public BLLAnexos(DALConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloAnexos modelo)
        {

            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição do Anexo é obrigatória!");
            }

            DALAnexos DALobj = new DALAnexos(conexao);
            DALobj.Incluir(modelo);
        }
        public void Alterar(ModeloAnexos modelo)
        {
            if (modelo.IdAnexos <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição do Anexo é obrigatória!");
            }

            DALAnexos DALobj = new DALAnexos(conexao);
            DALobj.Alterar(modelo);
        }

        public void AbrirAnexo(int codigo)
        {
            DALAnexos DALobj = new DALAnexos(conexao);
            DALobj.AbrirAnexo(codigo);
        }
        public DataTable CarregaAnexos(int codigo)
        {
            DALAnexos DALobj = new DALAnexos(conexao);
            return DALobj.CarregaAnexos(codigo);
        }

        public void MergeAnexos(string nome, int idpropostas, int tipo)
        {
            DALAnexos DALobj = new DALAnexos(conexao);
            DALobj.MergeAnexos(nome, idpropostas, tipo);
        }
    }
}
