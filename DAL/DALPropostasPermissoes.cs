using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPropostasPermissoes
    {
        private DALConexao conexao;
        public DALPropostasPermissoes(DALConexao cx)
        {
            this.conexao = cx;
        }

        public DataTable Localizar(int idempresas, int idusuarios, int idpropostas)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select u.idusuarios, u.usuario, u.nome, (select 1 from usuarios_propostas up where up.idpropostas=" + idpropostas + " and up.idusuarios=u.idusuarios) as ch from usuarios u where u.idempresas="+idempresas+" order by u.nome", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
    }
}
