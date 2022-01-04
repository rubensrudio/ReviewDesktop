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
    public class BLLPropostas
    {
        private DALConexao conexao;
        public BLLPropostas(DALConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloPropostas modelo)
        {
            if (modelo.Num_Proposta.Trim().Length == 0)
            {
                throw new Exception("Proposta é obrigatória!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }
            if (modelo.Tipo_Proposta.Trim().Length == 0)
            {
                throw new Exception("Tipo de Proposta é obrigatório!");
            }
            if (modelo.IdClientes <= 0)
            {
                throw new Exception("Cliente é obrigatório!");
            }
            if (modelo.Prazo <= 0)
            {
                throw new Exception("Prazo é obrigatório!");
            }
            if (modelo.Validade <= 0)
            {
                throw new Exception("Validade é obrigatória!");
            }

            DALPropostas DALobj = new DALPropostas(conexao);
            DALobj.Incluir(modelo);
        }
        public void Alterar(ModeloPropostas modelo)
        {
            if (modelo.IdPropostas <= 0)
            {
                throw new Exception("Código é obrigatório!");
            }
            if (modelo.Num_Proposta.Trim().Length == 0)
            {
                throw new Exception("Proposta é obrigatória!");
            }
            if (modelo.Descricao.Trim().Length == 0)
            {
                throw new Exception("Descrição é obrigatória!");
            }
            if (modelo.Tipo_Proposta.Trim().Length == 0)
            {
                throw new Exception("Tipo de Proposta é obrigatório!");
            }
            if (modelo.IdClientes <= 0)
            {
                throw new Exception("Cliente é obrigatório!");
            }
            if (modelo.Prazo <= 0)
            {
                throw new Exception("Prazo é obrigatório!");
            }
            if (modelo.Validade <= 0)
            {
                throw new Exception("Validade é obrigatória!");
            }

            DALPropostas DALobj = new DALPropostas(conexao);
            DALobj.Alterar(modelo);
        }
        public void Excluir(int codigo)
        {
            DALPropostas DALobj = new DALPropostas(conexao);
            DALobj.Excluir(codigo);
        }
        public DataTable Localizar(int idempresas, int idpropostas)
        {
            DALPropostas DALobj = new DALPropostas(conexao);
            return DALobj.Localizar(idempresas, idpropostas);
        }
        public DataTable Localizar(String valor, String buscapor, int idempresas, String situacao, int idusuarios, decimal quantpagina, decimal pagina)
        {
            DALPropostas DALobj = new DALPropostas(conexao);
            return DALobj.Localizar(valor, buscapor, idempresas, situacao, idusuarios, quantpagina, pagina);
        }
        public String LocalizarUsuario(int idempresas, int idpropostas)
        {
            DALPropostas DALobj = new DALPropostas(conexao);
            return DALobj.LocalizarUsuario(idempresas, idpropostas);
        }

        public bool PropostaExiste(int idempresas, String prop)
        {
            DALPropostas DALobj = new DALPropostas(conexao);
            return DALobj.PropostaExiste(idempresas, prop);
        }
        public ModeloPropostas CarregaPropostas(int codigo)
        {
            DALPropostas DALobj = new DALPropostas(conexao);
            return DALobj.CarregaPropostas(codigo);
        }

        public int TotalRegistros(int idempresas)
        {
            DALPropostas DALobj = new DALPropostas(conexao);
            return DALobj.TotalRegistros(idempresas);
        }
    }
}
