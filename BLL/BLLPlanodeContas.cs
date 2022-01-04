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
    public class BLLPlanodeContas
    {
        private DALConexao conexao;
        public BLLPlanodeContas(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloPlanodeContas modelo)
        {
            if (modelo.Indice.Trim().Length == 0)
            {
                throw new Exception("Índice é obrigatório!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }
            if (modelo.DebitoCredito.Trim().Length == 0)
            {
                throw new Exception("Débito/Crédito é obrigatório!");
            }
            if (modelo.AnaliticoSintetico.Trim().Length == 0)
            {
                throw new Exception("Tipo é obrigatório!");
            }

            DALPlanodeContas DALobj = new DALPlanodeContas(conexao);
            DALobj.Incluir(modelo);
        }

        public void Alterar(ModeloPlanodeContas modelo)
        {
            if (modelo.IdPlanodeContas <= 0)
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
            if (modelo.DebitoCredito.Trim().Length == 0)
            {
                throw new Exception("Débito/Crédito é obrigatório!");
            }
            if (modelo.AnaliticoSintetico.Trim().Length == 0)
            {
                throw new Exception("Tipo é obrigatório!");
            }

            DALPlanodeContas DALobj = new DALPlanodeContas(conexao);
            DALobj.Alterar(modelo);
        }

        public DataTable Localizar(String valor, String busca, int idempresas)
        {
            DALPlanodeContas DALobj = new DALPlanodeContas(conexao);
            return DALobj.Localizar(valor, busca, idempresas);
        }

        public DataTable LocalizarPai(int idempresas)
        {
            DALPlanodeContas DALobj = new DALPlanodeContas(conexao);
            return DALobj.LocalizarPai(idempresas);
        }

        public DataTable LocalizarAnalitico(int idempresas)
        {
            DALPlanodeContas DALobj = new DALPlanodeContas(conexao);
            return DALobj.LocalizarAnalitico(idempresas);
        }

        public String LocalizarFilho(int idempresas)
        {
            DALPlanodeContas DALobj = new DALPlanodeContas(conexao);
            return DALobj.LocalizarFilho(idempresas);
        }

        public String LocalizarFilho(int idempresas, string pai)
        {
            DALPlanodeContas DALobj = new DALPlanodeContas(conexao);
            return DALobj.LocalizarFilho(idempresas, pai);
        }

        public ModeloPlanodeContas CarregaPlanodeContas(int codigo)
        {
            DALPlanodeContas DALobj = new DALPlanodeContas(conexao);
            return DALobj.CarregaPlanodeContas(codigo);
        }
    }
}
