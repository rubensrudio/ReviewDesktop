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
    public class BLLInsumos
    {
        private DALConexao conexao;
        public BLLInsumos(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloInsumos modelo)
        {
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }
            if (modelo.Unidade.Trim().Length == 0)
            {
                throw new Exception("Unidade é obrigatória!");
            }
            if (modelo.Preco <= 0)
            {
                throw new Exception("Preço é obrigatório!");
            }
            if (modelo.IdClassificacao <= 0)
            {
                throw new Exception("Classificação é obrigatória!");
            }
            if (modelo.IdPlanosdeContas <= 0)
            {
                throw new Exception("Plano de contas é obrigatório!");
            }

            DALInsumos DALobj = new DALInsumos(conexao);
            DALobj.Incluir(modelo);
        }

        public void Alterar(ModeloInsumos modelo)
        {
            if (modelo.IdInsumos <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }
            if (modelo.Unidade.Trim().Length == 0)
            {
                throw new Exception("Unidade é obrigatória!");
            }
            if (modelo.Preco <= 0)
            {
                throw new Exception("Preço é obrigatório!");
            }
            if (modelo.IdClassificacao <= 0)
            {
                throw new Exception("Classificação é obrigatória!");
            }
            if (modelo.IdPlanosdeContas <= 0)
            {
                throw new Exception("Plano de contas é obrigatório!");
            }

            DALInsumos DALobj = new DALInsumos(conexao);
            DALobj.Alterar(modelo);
        }

        public DataTable Localizar(String valor, String busca, int idempresas)
        {
            DALInsumos DALobj = new DALInsumos(conexao);
            return DALobj.Localizar(valor, busca, idempresas);
        }
               

        public ModeloInsumos CarregaInsumos(int codigo)
        {
            DALInsumos DALobj = new DALInsumos(conexao);
            return DALobj.CarregaInsumos(codigo);
        }
    }
}
