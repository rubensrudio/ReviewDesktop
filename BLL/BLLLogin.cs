using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLLogin
    {
        private DALConexao conexao;
        public BLLLogin(DALConexao cx)
        {
            this.conexao = cx;
        }
       
        public ModeloUsuario Login(String usuario, String senha)
        {
            DALUsuario DALobj = new DALUsuario(conexao);
            return DALobj.CarregaUsuario(usuario,senha);
        }
    }
}
