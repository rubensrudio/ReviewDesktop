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
    public class BLLVersoes
    {
        private DALConexao conexao;
        public BLLVersoes(DALConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloVersoes modelo)
        {

            if (modelo.Build <= 0)
            {
                throw new Exception("Versão é obrigatória!");
            }

            DALVersoes DALobj = new DALVersoes(conexao);
            DALobj.Incluir(modelo);
        }
        public void Alterar(ModeloVersoes modelo)
        {
            if (modelo.IdVersao <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Build <= 0)
            {
                throw new Exception("Versão é obrigatória!");
            }

            DALVersoes DALobj = new DALVersoes(conexao);
            DALobj.Alterar(modelo);
        }

        public void AbrirVersao(int codigo)
        {
            DALVersoes DALobj = new DALVersoes(conexao);
            DALobj.AbrirVersao(codigo);
        }
        public DataTable CarregaVersoes()
        {
            DALVersoes DALobj = new DALVersoes(conexao);
            return DALobj.CarregaVersao();
        }
    }
}
