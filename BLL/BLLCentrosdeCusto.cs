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
    public class BLLCentrosdeCusto
    {
        private DALConexao conexao;
        public BLLCentrosdeCusto(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloCentrodeCusto modelo)
        {
            if (modelo.Codigo.Trim().Length == 0)
            {
                throw new Exception("Centro de Custo é obrigatório!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }

            DALCentrodeCusto DALobj = new DALCentrodeCusto(conexao);
            DALobj.Incluir(modelo);
        }

        public void Alterar(ModeloCentrodeCusto modelo)
        {
            if (modelo.IdCentros_de_Custos <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Codigo.Trim().Length == 0)
            {
                throw new Exception("Centro de Custo é obrigatório!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }

            DALCentrodeCusto DALobj = new DALCentrodeCusto(conexao);
            DALobj.Alterar(modelo);
        }

        public DataTable Localizar(int idempresas, int idusuarios)
        {
            DALCentrodeCusto DALobj = new DALCentrodeCusto(conexao);
            return DALobj.Localizar(idempresas, idusuarios);
        }

        public DataTable Localizar(String valor, String busca, int idempresas)
        {
            DALCentrodeCusto DALobj = new DALCentrodeCusto(conexao);
            return DALobj.Localizar(valor, busca, idempresas);
        }

        public bool LocalizarCentrodeCustoUsuario(int idcentrosdecusto, int idusuarios)
        {
            DALCentrodeCusto DALobj = new DALCentrodeCusto(conexao);
            return DALobj.LocalizarCentrodeCustoUsuario(idcentrosdecusto, idusuarios);
        }

        public ModeloCentrodeCusto CarregaCentrosdeCusto(int codigo)
        {
            DALCentrodeCusto DALobj = new DALCentrodeCusto(conexao);
            return DALobj.CarregaCentrodeCusto(codigo);
        }
    }
}
