using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

namespace DAL
{
    public class DALAnexos
    {
        private DALConexao conexao;
        public DALAnexos(DALConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloAnexos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "insert into anexos_propostas (idpropostas,descricao,anexo,nome_arquivo,comercial,tecnica) " +
                "values (@idpropostas,@descricao,@anexo,@nome_arquivo,1,1); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@idpropostas", modelo.IdPropostas);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@anexo", File.ReadAllBytes(modelo.Anexo));
            cmd.Parameters.AddWithValue("@nome_arquivo", Path.GetFileName(modelo.Nome_Arquivo));

            conexao.Conectar();
            modelo.IdAnexos = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloAnexos modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "update anexos_propostas set descricao=@descricao,anexo=@anexo,nome_arquivo=@nome_arquivo " +
                "where idanexos=@idanexos;";
            cmd.Parameters.AddWithValue("@idanexos", modelo.IdAnexos);
            cmd.Parameters.AddWithValue("@descricao", modelo.Descricao);
            cmd.Parameters.AddWithValue("@anexo", File.ReadAllBytes(modelo.Anexo));
            cmd.Parameters.AddWithValue("@nome_arquivo", Path.GetFileName(modelo.Nome_Arquivo));

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public void AbrirAnexo(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select anexo,nome_arquivo from anexos_propostas where idanexos=@idanexos;";
            cmd.Parameters.AddWithValue("@idanexos", codigo);

            conexao.Conectar();
            var bytes = cmd.ExecuteScalar() as byte[];
            if (bytes != null)
            {
                SqlDataReader registro = cmd.ExecuteReader();
                registro.Read();
                string nome_arquivo = registro["nome_arquivo"].ToString();
                var arquivotemp = Path.GetTempFileName();
                arquivotemp = Path.ChangeExtension(arquivotemp, Path.GetExtension(nome_arquivo));
                File.WriteAllBytes(arquivotemp, bytes);
                Process.Start(arquivotemp);
            }
            conexao.Desconectar();
        }

        public DataTable CarregaAnexos(int codigo)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select idanexos, tecnica, comercial, descricao, anexo from anexos_propostas where idpropostas=" + codigo + " order by idanexos", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        public void MergeAnexos(string nome, int idpropostas, int tipo)
        {
            string [] idanexos = new string[50];
            int i = 0;
            string where = "";
            if(tipo == 1)
            {
                where = " and tecnica=1";
            }
            else if(tipo == 2)
            {
                where = " and comercial=1";
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.ObjetoConexao;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conexao.ObjetoConexao;
            cmd.CommandText = "select idanexos,anexo,nome_arquivo from anexos_propostas where idpropostas=@idpropostas"+where+";";
            cmd.Parameters.AddWithValue("@idpropostas", idpropostas);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            
            if (registro.HasRows)
            {
                while (registro.Read())
                {
                    idanexos[i] = registro["idanexos"].ToString();

                    i++;
                }
            }
            
            conexao.Desconectar();

            for (int j = 0; j < i; j++)
            {
                cmd2.CommandText = "select anexo,nome_arquivo,descricao from anexos_propostas where idanexos=@idanexos"+j+where+";";
                cmd2.Parameters.AddWithValue("@idanexos"+j, idanexos[j]);

                conexao.Conectar();
                var bytes = cmd2.ExecuteScalar() as byte[];
                if (bytes != null)
                {
                    // Create a new PDF document
                    PdfDocument document = new PdfDocument();
                    document.Info.Title = "PDF";

                    // Create an empty page
                    PdfPage page = document.AddPage();

                    // Get an XGraphics object for drawing
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Create a font
                    XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                    int jx = j + 1;
                    // Draw the text
                    gfx.DrawString("ANEXO " + jx , font, XBrushes.Black,
                      new XRect(0, 0, page.Width, page.Height),
                      XStringFormats.Center);

                    // Save the document...
                    const string filename = "titleanexo.pdf";
                    document.Save(filename);

                    insertBlanksAndMerge(nome, "titleanexo.pdf");

                    File.WriteAllBytes("anexo.pdf", bytes);

                    insertBlanksAndMerge(nome, "anexo.pdf");
                }
                conexao.Desconectar();
            }
        }

        public static void insertBlanksAndMerge(string file1, string file2)
        {
            // Get some file names
            string[] files = { file1, file2};

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();
            
            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }

            // Save the document...
            string filename = file1;
            outputDocument.Save(filename);
        }
    }
}
