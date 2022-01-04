using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPropostasPermissoes
    {
        private DALConexao conexao;
        public BLLPropostasPermissoes(DALConexao cx)
        {
            this.conexao = cx;
        }
        
        public DataTable Localizar(int idempresas, int idusuarios, int idpropostas)
        {
            DALPropostasPermissoes DALobj = new DALPropostasPermissoes(conexao);
            return DALobj.Localizar(idempresas, idusuarios, idpropostas);
        }
    }
}
