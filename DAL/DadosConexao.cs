using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DadosConexao
    {
        public static String servidor;
        public static String banco;
        public static String usuario;
        public static String senha;
        public static String StringConexao
        {
            get
            {
                //return "Data Source=NOT-RUBENS\\SQLEXPRESS;Initial Catalog=Neoland;User ID=sa;Password=545786ru";
                return "Data Source="+servidor+";Initial Catalog="+banco+";User ID="+usuario+";Password="+senha + ";MultiSubnetFailover=True";
            }
        }
    }
}
