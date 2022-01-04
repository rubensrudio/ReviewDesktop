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
    public class BLLClassificacao
    {
        private DALConexao conexao;
        public BLLClassificacao(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloClassificacao modelo)
        {
            if (modelo.Indice.Trim().Length == 0)
            {
                throw new Exception("Índice é obrigatório!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }

            DALClassificacao DALobj = new DALClassificacao(conexao);
            DALobj.Incluir(modelo);
        }

        public void Alterar(ModeloClassificacao modelo)
        {
            if (modelo.IdClassificacao <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Indice.Trim().Length == 0)
            {
                throw new Exception("Índice é obrigatório!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }

            DALClassificacao DALobj = new DALClassificacao(conexao);
            DALobj.Alterar(modelo);
        }

        public DataTable Localizar(String valor, String busca, int idempresas)
        {
            DALClassificacao DALobj = new DALClassificacao(conexao);
            return DALobj.Localizar(valor, busca, idempresas);
        }

        public DataTable LocalizarPai(int idempresas)
        {
            DALClassificacao DALobj = new DALClassificacao(conexao);
            return DALobj.LocalizarPai(idempresas);
        }

        public DataTable LocalizarAnalitico(int idempresas)
        {
            DALClassificacao DALobj = new DALClassificacao(conexao);
            return DALobj.LocalizarAnalitico(idempresas);
        }

        public String LocalizarFilho(int idempresas)
        {
            DALClassificacao DALobj = new DALClassificacao(conexao);
            return DALobj.LocalizarFilho(idempresas);
        }

        public String LocalizarFilho(int idempresas, string pai)
        {
            DALClassificacao DALobj = new DALClassificacao(conexao);
            return DALobj.LocalizarFilho(idempresas,pai);
        }

        public ModeloClassificacao CarregaClassificacao(int codigo)
        {
            DALClassificacao DALobj = new DALClassificacao(conexao);
            return DALobj.CarregaClassificacao(codigo);
        }
    }
}
