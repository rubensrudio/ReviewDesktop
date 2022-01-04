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
    public class BLLMateriais
    {
        private DALConexao conexao;
        public BLLMateriais(DALConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloMateriais modelo)
        {

            if (modelo.Material.Trim().Length == 0)
            {
                throw new Exception("Material é obrigatório!");
            }
            if (modelo.Unidade.Trim().Length == 0)
            {
                throw new Exception("Unidade é obrigatória!");
            }
            if (modelo.Quantidade <= 0)
            {
                throw new Exception("Quantidade é obrigatória!");
            }
            if (modelo.Vl_Unitario <= 0)
            {
                throw new Exception("Valor unitário é obrigatório!");
            }

            DALMateriais DALobj = new DALMateriais(conexao);
            DALobj.Incluir(modelo);
        }
        public void Alterar(ModeloMateriais modelo)
        {
            if (modelo.IdMat_Propostas <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Material.Trim().Length == 0)
            {
                throw new Exception("Material é obrigatório!");
            }
            if (modelo.Unidade.Trim().Length == 0)
            {
                throw new Exception("Unidade é obrigatória!");
            }
            if (modelo.Quantidade <= 0)
            {
                throw new Exception("Quantidade é obrigatória!");
            }
            if (modelo.Vl_Unitario <= 0)
            {
                throw new Exception("Valor unitário é obrigatório!");
            }

            DALMateriais DALobj = new DALMateriais(conexao);
            DALobj.Alterar(modelo);
        }
        public DataTable CarregaMateriais(int codigo)
        {
            DALMateriais DALobj = new DALMateriais(conexao);
            return DALobj.CarregaMateriais(codigo);
        }
    }
}
