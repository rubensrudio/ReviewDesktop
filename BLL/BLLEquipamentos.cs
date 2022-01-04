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
    public class BLLEquipamentos
    {
        private DALConexao conexao;
        public BLLEquipamentos(DALConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloEquipamentos modelo)
        {

            if (modelo.Equipamento.Trim().Length == 0)
            {
                throw new Exception("Equipamento / Recurso é obrigatório!");
            }
            if (modelo.Quantidade <= 0)
            {
                throw new Exception("Quantidade é obrigatória!");
            }

            DALEquipamentos DALobj = new DALEquipamentos(conexao);
            DALobj.Incluir(modelo);
        }
        public void Alterar(ModeloEquipamentos modelo)
        {
            if (modelo.IdEquip_Propostas <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Equipamento.Trim().Length == 0)
            {
                throw new Exception("Equipamento / Recurso é obrigatório!");
            }
            if (modelo.Quantidade <= 0)
            {
                throw new Exception("Quantidade é obrigatória!");
            }

            DALEquipamentos DALobj = new DALEquipamentos(conexao);
            DALobj.Alterar(modelo);
        }
        public DataTable CarregaEquipamentos(int codigo)
        {
            DALEquipamentos DALobj = new DALEquipamentos(conexao);
            return DALobj.CarregaEquipamentos(codigo);
        }
    }
}
