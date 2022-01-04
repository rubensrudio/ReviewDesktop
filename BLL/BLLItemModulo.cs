using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLItemModulo
    {
        private DALConexao conexao;
        public BLLItemModulo(DALConexao cx)
        {
            this.conexao = cx;
        }
        public DataTable Localizar(int idmodulo,int idusuario)
        {
            DALItemModulo DALobj = new DALItemModulo(conexao);
            return DALobj.Localizar(idmodulo, idusuario);
        }

        public bool LocalizarItemModuloUsuario(int iditensmodulos, int idusuarios)
        {
            DALItemModulo DALobj = new DALItemModulo(conexao);
            return DALobj.LocalizarItemModuloUsuario(iditensmodulos, idusuarios);
        }

        public bool LocalizarModuloUsuario(int idmodulos, int idusuarios)
        {
            DALItemModulo DALobj = new DALItemModulo(conexao);
            return DALobj.LocalizarModuloUsuario(idmodulos, idusuarios);
        }

        public int LocalizarModulo(int iditensmodulos)
        {
            DALItemModulo DALobj = new DALItemModulo(conexao);
            return DALobj.LocalizarModulo(iditensmodulos);
        }
    }
}
