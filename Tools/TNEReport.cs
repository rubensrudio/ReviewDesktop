using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace Tools
{
    public abstract class TNEReport
    {
        protected Document doc;
        PdfWriter writer;
        public FileStream output { get; set; }
        public string empresa { get; set; }
        public string endereco1 { get; set; }
        public string endereco2 { get; set; }
        public string num_proposta { get; set; }
        public string nome_arquivo { get; set; }
        public string PageTitle { get; set; }
        public string PageSubTitle { get; set; }
        public string PageSubLogo { get; set; }
        public string BasePath { get; set; }
        public bool ImprimirCabecalhoPadrao { get; set; }
        public bool ImprimirRodapePadrao { get; set; }
        public bool Paisagem { get; set; }
        public bool chkValores { get; set; }

        public TNEReport()
        {
            InicializaVariaveis();
        }

        private void InicializaVariaveis()
        {
            ImprimirCabecalhoPadrao = true;
            ImprimirRodapePadrao = true;
            empresa = string.Empty;
            endereco1 = string.Empty;
            endereco2 = string.Empty;
            num_proposta = string.Empty;
            nome_arquivo = string.Empty;
            PageTitle = string.Empty;
            PageSubTitle = string.Empty;
            BasePath = string.Empty;
            Paisagem = false;
            chkValores = true;
        }

        public FileStream GetOutput()
        {
            MontaCorpoDados();

            if (output == null || output.Length == 0)
            {
                throw new Exception("Sem dados para exibir.");
            }

            try
            {
                writer.Flush();

                if (writer.PageEmpty)
                {
                    doc.Add(new Paragraph("Nenhum registro para listar."));
                }

                doc.Close();
            }
            catch { }
            finally
            {
                doc = null;
                writer = null;
            }

            return output;
        }

        public FileStream GetOutput(int i)
        {
            MontaCorpoDados(i);

            if (output == null || output.Length == 0)
            {
                throw new Exception("Sem dados para exibir.");
            }

            try
            {
                writer.Flush();

                if (writer.PageEmpty)
                {
                    doc.Add(new Paragraph("Nenhum registro para listar."));
                }

                doc.Close();
            }
            catch { }
            finally
            {
                doc = null;
                writer = null;
            }

            return output;
        }

        public virtual void MontaCorpoDados()
        {
            if (!Paisagem)
            {
                doc = new Document(PageSize.A4, 20, 10, 70, 75);
            }
            else
            {
                doc = new Document(PageSize.A4.Rotate(), 20, 10, 80, 80);
            }
            //output = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, output);

            doc.AddAuthor("Rubens Rudio");
            doc.AddTitle(PageTitle);
            doc.AddSubject(PageTitle);

            var footer = new MSPDFFooter();
            footer.PageTitle = PageTitle;
            footer.PageSubTitle = PageSubTitle;
            footer.BasePath = BasePath;
            footer.ImprimirCabecalhoPadrao = ImprimirCabecalhoPadrao;
            footer.ImprimirRodapePadrao = ImprimirRodapePadrao;
            footer.empresa = empresa;
            footer.endereco1 = endereco1;
            footer.endereco2 = endereco2;
            footer.num_proposta = num_proposta;
            footer.nome_arquivo = nome_arquivo;
            footer.chkValores = chkValores;

            writer.PageEvent = footer;

            doc.Open();

            return;
        }

        public virtual void MontaCorpoDados(int i)
        {
            if (!Paisagem)
            {
                doc = new Document(PageSize.A4, 20, 10, 70, 75);
            }
            else
            {
                doc = new Document(PageSize.A4.Rotate(), 20, 10, 80, 80);
            }
            //output = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, output);

            doc.AddAuthor("Rubens Rudio");
            doc.AddTitle(PageTitle);
            doc.AddSubject(PageTitle);

            var footer = new MSPDFFooter();
            footer.PageTitle = PageTitle;
            footer.PageSubTitle = PageSubTitle;
            footer.BasePath = BasePath;
            footer.ImprimirCabecalhoPadrao = ImprimirCabecalhoPadrao;
            footer.ImprimirRodapePadrao = ImprimirRodapePadrao;
            footer.empresa = empresa;
            footer.endereco1 = endereco1;
            footer.endereco2 = endereco2;
            footer.num_proposta = num_proposta;
            footer.nome_arquivo = nome_arquivo;
            footer.chkValores = chkValores;

            writer.PageEvent = footer;

            doc.Open();

            return;
        }

        protected PdfPCell getNewCell(string Texto, Font Fonte, int Alinhamento, float Espacamento, int Borda, BaseColor CorBorda, BaseColor CorFundo)
        {
            var cell = new PdfPCell(new Phrase(Texto, Fonte));
            cell.HorizontalAlignment = Alinhamento;
            cell.Padding = Espacamento;
            cell.Border = Borda;
            cell.BorderColor = CorBorda;
            cell.BackgroundColor = CorFundo;

            return cell;
        }

        protected PdfPCell getNewCell(string Texto, Font Fonte, int Alinhamento, float Espacamento, int Borda, BaseColor CorBorda)
        {
            return getNewCell(Texto, Fonte, Alinhamento, Espacamento, Borda, CorBorda, new BaseColor(255,255,255));
        }
        protected PdfPCell getNewCell(string Texto, Font Fonte, int Alinhamento = 0, float Espacamento = 5, int Borda = 0)
        {
            return getNewCell(Texto, Fonte, Alinhamento, Espacamento, Borda, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255));
        }
    }

    public class MSPDFFooter : PdfPageEventHelper
    {
        public string empresa { get; set; }
        public string endereco1 { get; set; }
        public string endereco2 { get; set; }
        public string num_proposta { get; set; }
        public string nome_arquivo { get; set; }
        public string PageTitle { get; set; }
        public string PageSubTitle { get; set; }
        public string PageSubLogo { get; set; }
        public string BasePath { get; set; }
        public bool ImprimirCabecalhoPadrao { get; set; }
        public bool ImprimirRodapePadrao { get; set; }
        public bool chkValores { get; set; }

        public override void OnOpenDocument(PdfWriter writer, Document doc)
        {
            base.OnOpenDocument(writer, doc);
        }

        public override void OnStartPage(PdfWriter writer, Document doc)
        {
            base.OnStartPage(writer, doc);

            ImprimeCabecalho(writer, doc);
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            base.OnEndPage(writer, doc);

            ImprimeRodape(writer, doc);
        }

        private void ImprimeRodape(PdfWriter writer, Document doc)
        {
            #region Dados do Rodapé
            if (ImprimirRodapePadrao)
            {
                BaseColor preto = new BaseColor(0, 0, 0);
                Font font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
                Font negrito = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
                float[] sizes = new float[] { 1.0f, 3.5f, 1f };

                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin);
                table.SetWidths(sizes);

                PdfPCell cell = new PdfPCell();
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                cell.BorderWidthTop = 0.5f;
                cell.PaddingLeft = 10f;
                cell.PaddingTop = 10f;
                table.AddCell(cell);

                PdfPTable micros = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(empresa, negrito));
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                micros.AddCell(cell);
                cell = new PdfPCell(new Phrase(endereco1, font));
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                micros.AddCell(cell);
                cell = new PdfPCell(new Phrase(endereco2, font));
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                micros.AddCell(cell);

                cell = new PdfPCell(micros);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                cell.BorderWidthTop = 0.5f;
                cell.PaddingTop = 10f;
                table.AddCell(cell);
                
                #region Página
                micros = new PdfPTable(1);
                cell = new PdfPCell();
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                micros.AddCell(cell);
                cell = new PdfPCell();
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                micros.AddCell(cell);
                cell = new PdfPCell(new Phrase((doc.PageNumber).ToString(), font));
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                micros.AddCell(cell);

                cell = new PdfPCell(micros);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                cell.BorderWidthTop = 0.5f;
                cell.PaddingTop = 10f;
                table.AddCell(cell);
                #endregion

                table.WriteSelectedRows(0, -1, doc.LeftMargin, 70, writer.DirectContent);
            }
            #endregion 
        }

        private void ImprimeCabecalho(PdfWriter writer, Document doc)
        {
            #region Dados do Cabeçalho
            if (ImprimirCabecalhoPadrao)
            {
                BaseColor preto = new BaseColor(0, 0, 0);
                Font font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
                Font titulo = FontFactory.GetFont("Verdana", 12, Font.BOLD, preto);
                float[] sizes = new float[] { 1f, 3f, 1f };

                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin);
                table.SetWidths(sizes);

                #region Logo Empresa
                PdfPCell cell;
                if (nome_arquivo != "")
                {
                    Image logo;
                    if (File.Exists(BasePath + @"\PublicResources\" + PageSubLogo))
                    {
                        logo = Image.GetInstance(BasePath + @"\PublicResources\" + PageSubLogo);
                    }
                    else
                    {
                        logo = Image.GetInstance(BasePath + @"\" + nome_arquivo);
                    }

                    cell = new PdfPCell(logo);
                }
                else
                {
                    cell = new PdfPCell(new Phrase("LOGO", font));
                }
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = 0;
                //cell.BorderWidthTop = 1f;
                cell.BorderWidthBottom = 0.5f;
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                PdfPTable micros = new PdfPTable(1);
                cell = new PdfPCell(new Phrase(PageSubTitle, font));
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                micros.AddCell(cell);
                cell = new PdfPCell(new Phrase(PageTitle, titulo));
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                micros.AddCell(cell);

                cell = new PdfPCell(micros);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                //cell.BorderWidthTop = 1.5f;
                cell.BorderWidthBottom = 0.5f;
                cell.PaddingTop = 10f;
                table.AddCell(cell);
                #endregion

                #region Página
                micros = new PdfPTable(1);
                cell = new PdfPCell(new Phrase("N°: " + num_proposta, font));
                cell.Border = 0;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                micros.AddCell(cell);

                cell = new PdfPCell(micros);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                //cell.BorderWidthTop = 1.5f;
                cell.BorderWidthBottom = 0.5f;
                cell.PaddingTop = 10f;              
                table.AddCell(cell);
                #endregion

                table.WriteSelectedRows(0, -1, doc.LeftMargin, (doc.PageSize.Height - 10), writer.DirectContent);
            }
            #endregion
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}