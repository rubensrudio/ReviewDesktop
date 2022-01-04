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
    public class BLLServicos
    {
        private DALConexao conexao;
        public BLLServicos(DALConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloServicos modelo)
        {

            if (modelo.Servico.Trim().Length == 0)
            {
                throw new Exception("Serviço é obrigatório!");
            }
            if (modelo.Valor <= 0)
            {
                throw new Exception("Valor é obrigatório!");
            }

            DALServicos DALobj = new DALServicos(conexao);
            DALobj.Incluir(modelo);
        }
        public void Alterar(ModeloServicos modelo)
        {
            if (modelo.IdServ_Propostas <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Servico.Trim().Length == 0)
            {
                throw new Exception("Serviço é obrigatório!");
            }
            if (modelo.Valor <= 0)
            {
                throw new Exception("Valor é obrigatório!");
            }

            DALServicos DALobj = new DALServicos(conexao);
            DALobj.Alterar(modelo);
        }
        public DataTable CarregaServicos(int codigo)
        {
            DALServicos DALobj = new DALServicos(conexao);
            return DALobj.CarregaServicos(codigo);
        }
    }
}
