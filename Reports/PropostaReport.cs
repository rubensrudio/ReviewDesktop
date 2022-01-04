using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Tools;
using Modelo;
using DAL;
using BLL;
using System.Globalization;
using System.Data;

namespace Reports
{
    public class PropostaReport : TNEReport
    {
        private int idempresas;
        private int idpropostas;
        DALConexao cx = new DALConexao(DadosConexao.StringConexao);
        public PropostaReport(System.IO.FileStream fileStream, int idemp, int idprop)
        {
            Paisagem = false;
            output = fileStream;
            this.idempresas = idemp;
            this.idpropostas = idprop;
        }

        public void CriaCell(PdfPTable table, string texto, int alinhamento, int borda, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, font));
            cell.Border = borda;
            cell.SetLeading(3, 2);
            cell.HorizontalAlignment = alinhamento;
            table.AddCell(cell);
        }

        public void CriaParagrafo(string texto, int alinhamento, Font font)
        {
            Paragraph paragrafo = new Paragraph("", font);
            paragrafo.Alignment = alinhamento;
            paragrafo.SetLeading(3, 2);
            paragrafo.Font = font;
            paragrafo.Add(texto);
            doc.Add(paragrafo);
        }

        private void PropostaTecnica(ModeloPropostas modeloprop, int i)
        {
            PdfPTable table = new PdfPTable(1);
            BaseColor preto = new BaseColor(0, 0, 0);
            Font font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
            int item1 = 1;

            CriaParagrafo("1. PROPOSTA TÉCNICA", Element.ALIGN_LEFT, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            CriaParagrafo("1." + item1 + ". Introdução", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item1++;
            CriaParagrafo(modeloprop.IntroEmpresa, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            if (modeloprop.Edital != "")
            {
                CriaParagrafo("1." + item1 + ". Edital", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item1++;
                CriaParagrafo(modeloprop.Edital, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            CriaParagrafo("1." + item1 + ". Objetivo", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item1++;
            CriaParagrafo(modeloprop.Descricao, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            if (modeloprop.Escopo != "")
            {
                CriaParagrafo("1." + item1 + ". Escopo", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item1++;
                CriaParagrafo(modeloprop.Escopo, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
            }

            CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            CriaParagrafo("1." + item1 + ". Tabela de Anexos", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item1++;
            CriaParagrafo(" ", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            table = new PdfPTable(2);
            float[] sizes = new float[] { 20, 100 };
            //table.TotalWidth = doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin);
            table.SetWidths(sizes);

            font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
            BaseColor fundo = new BaseColor(200, 200, 200);
            table.AddCell(getNewCell("Anexo", font, Element.ALIGN_LEFT, 10, 15, preto, fundo));
            table.AddCell(getNewCell("Descrição", font, Element.ALIGN_LEFT, 10, 15, preto, fundo));

            BLLAnexos bll = new BLLAnexos(cx);
            DataTable dt = bll.CarregaAnexos(this.idpropostas);
            DataRow[] row = dt.Select();
            font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);

            int j = 1;
            foreach (DataRow dr in row)
            {
                table.AddCell(getNewCell(j.ToString(), font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(dr["descricao"].ToString(), font, Element.ALIGN_LEFT, 5, 15, preto));
                j++;
            }

            doc.Add(table);
                      

            CriaParagrafo("1." + item1 + ". Prazo", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item1++;
            CriaParagrafo(modeloprop.Prazo.ToString() + " dias", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            if (modeloprop.DespesasInclusas != "")
            {
                CriaParagrafo("1." + item1 + ". Despesas Inclusas", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item1++;
                CriaParagrafo(modeloprop.DespesasInclusas, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            if (modeloprop.DespesasExclusas != "")
            {
                CriaParagrafo("1." + item1 + ". Despesas Exclusas", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item1++;
                CriaParagrafo(modeloprop.DespesasExclusas, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            if (modeloprop.AcrescimosEscopo != "")
            {
                CriaParagrafo("1." + item1 + ". Acréscimos ao Escopo", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item1++;
                CriaParagrafo(modeloprop.AcrescimosEscopo, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            if (modeloprop.Documentacao != "")
            {
                CriaParagrafo("1." + item1 + ". Documentação Envolvida e Necessária", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item1++;
                CriaParagrafo(modeloprop.Documentacao, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            if (modeloprop.Premissas != "")
            {
                CriaParagrafo("1." + item1 + ". Premissas Técnicas Adotadas", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item1++;
                CriaParagrafo(modeloprop.Premissas, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            CriaParagrafo("1." + item1 + ". Equipamentos / Recursos", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item1++;
            CriaParagrafo(" ", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            table = new PdfPTable(2);
            float[] sizes2 = new float[] { 100, 20 };
            //table.TotalWidth = doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin);
            table.SetWidths(sizes2);

            font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
            BaseColor fundo3 = new BaseColor(200, 200, 200);
            table.AddCell(getNewCell("Equipamento / Recurso", font, Element.ALIGN_LEFT, 10, 15, preto, fundo3));
            table.AddCell(getNewCell("Quantidade", font, Element.ALIGN_LEFT, 10, 15, preto, fundo3));

            BLLEquipamentos bll2 = new BLLEquipamentos(cx);
            DataTable dt2 = bll2.CarregaEquipamentos(this.idpropostas);
            DataRow[] row2 = dt2.Select();
            font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);

            foreach (DataRow dr in row2)
            {
                table.AddCell(getNewCell(dr["equipamento"].ToString(), font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(dr["quantidade"].ToString(), font, Element.ALIGN_CENTER, 5, 15, preto));
            }

            doc.Add(table);
        }

        private void PropostaComercial(ModeloPropostas modeloprop, int i)
        {
            PdfPTable table = new PdfPTable(1);
            BaseColor preto = new BaseColor(0, 0, 0);
            Font font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
            int item2 = 1;

            if (i == 3)
            {
                CriaParagrafo("2. PROPOSTA BUDGET", Element.ALIGN_LEFT, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }
            else
            {
                CriaParagrafo("2. PROPOSTA COMERCIAL", Element.ALIGN_LEFT, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }
            CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            CriaParagrafo("2." + item2 + ". Condição de Pagamento", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item2++;
            CriaParagrafo(modeloprop.Cond_Pag, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            if (modeloprop.Eventos_Pagamento != "")
            {
                CriaParagrafo("2." + item2 + ". Eventos de Pagamento", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item2++;
                CriaParagrafo(modeloprop.Eventos_Pagamento, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            CriaParagrafo("2." + item2 + ". Validade da Proposta", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item2++;
            CriaParagrafo("Essa proposta tem a validade de "+ modeloprop.Validade.ToString() + " dias a partir da data de sua emissão.", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));

            if (modeloprop.Observacoes != "")
            {
                CriaParagrafo("2." + item2 + ". Informações Adicionais", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item2++;
                CriaParagrafo("Código Fiscal: " + modeloprop.Cod_Fiscal, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
                CriaParagrafo("Data Base: " + modeloprop.Mes_Base + "/" + modeloprop.Ano_Base, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
                CriaParagrafo(modeloprop.Observacoes, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            if (modeloprop.Garantia != "")
            {
                CriaParagrafo("2." + item2 + ". Garantia", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item2++;
                CriaParagrafo(modeloprop.Garantia, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            if (modeloprop.Info_Contratada != "")
            {
                CriaParagrafo("2." + item2 + ". Responsabilidades da Contratada", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item2++;
                CriaParagrafo(modeloprop.Info_Contratada, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            if (modeloprop.Info_Contratante != "")
            {
                CriaParagrafo("2." + item2 + ". Responsabilidades da Contratante", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item2++;
                CriaParagrafo(modeloprop.Info_Contratante, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            if (modeloprop.Multa != "")
            {
                CriaParagrafo("2." + item2 + ". Multa", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                item2++;
                CriaParagrafo(modeloprop.Multa, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

                CriaParagrafo(" ", Element.ALIGN_CENTER, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            }

            CriaParagrafo("2." + item2 + ". Serviços", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item2++;
            CriaParagrafo(" ", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            //Tabela de Serviços
            float[] sizes2;
            if (((i == 2) || (i == 3)) && chkValores)
            {
                table = new PdfPTable(5);
                sizes2 = new float[] { 96, 17, 17, 35, 35 };
            }
            else
            {
                table = new PdfPTable(3);
                sizes2 = new float[] { 100, 20, 20 };
            }

            table.SetWidths(sizes2);

            font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
            BaseColor fundo2 = new BaseColor(200, 200, 200);
            table.AddCell(getNewCell("Serviço", font, Element.ALIGN_LEFT, 10, 15, preto, fundo2));
            table.AddCell(getNewCell("Un.", font, Element.ALIGN_CENTER, 10, 15, preto, fundo2));
            table.AddCell(getNewCell("Qtd.", font, Element.ALIGN_CENTER, 10, 15, preto, fundo2));

            if (((i == 2) || (i == 3)) && chkValores)
            {
                table.AddCell(getNewCell("Valor Un.", font, Element.ALIGN_CENTER, 10, 15, preto, fundo2));
                table.AddCell(getNewCell("Valor Total", font, Element.ALIGN_CENTER, 10, 15, preto, fundo2));
            }

            BLLServicos bll2 = new BLLServicos(cx);
            DataTable dt2 = bll2.CarregaServicos(this.idpropostas);
            DataRow[] row2 = dt2.Select();
            font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
            double total_servicos = 0;

            foreach (DataRow dr in row2)
            {
                table.AddCell(getNewCell(dr["servico"].ToString(), font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(dr["unidade"].ToString(), font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(dr["quantidade"].ToString(), font, Element.ALIGN_CENTER, 5, 15, preto));
                if (((i == 2) || (i == 3)) && chkValores)
                {
                    table.AddCell(getNewCell(Validacao.MaskFloatMoeda(dr["valor"].ToString()), font, Element.ALIGN_RIGHT, 5, 15, preto));
                    table.AddCell(getNewCell(Validacao.MaskFloatMoeda(dr["vl_total"].ToString()), font, Element.ALIGN_RIGHT, 5, 15, preto));
                }
                total_servicos = total_servicos + Convert.ToDouble(Validacao.MaskMoedaFloat(Validacao.MaskFloatMoeda(dr["vl_total"].ToString())));
            }
            if ((i == 2) || (i == 3))
            {
                font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
                CriaCell(table, " ", 0, 0, font);
                CriaCell(table, " ", 0, 0, font);
                CriaCell(table, " ", 0, 0, font);
                CriaCell(table, "TOTAL", 2, 0, font);
                table.AddCell(getNewCell(Validacao.MaskFloatMoeda(total_servicos.ToString()), font, Element.ALIGN_RIGHT, 5, 15, preto));
            }
            doc.Add(table);

            CriaParagrafo("2." + item2 + ". Materiais", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item2++;
            CriaParagrafo(" ", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            //Tabela de Materiais
            float[] sizes3;
            if (((i == 2) || (i == 3)) && chkValores)
            {
                table = new PdfPTable(5);
                sizes3 = new float[] { 96, 17, 17, 35, 35 };
            }
            else
            {
                table = new PdfPTable(3);
                sizes3 = new float[] { 100, 20, 20 };
            }
            
            table.SetWidths(sizes3);

            font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
            BaseColor fundo3 = new BaseColor(200, 200, 200);
            table.AddCell(getNewCell("Material", font, Element.ALIGN_LEFT, 10, 15, preto, fundo3));
            table.AddCell(getNewCell("Un.", font, Element.ALIGN_CENTER, 10, 15, preto, fundo3));
            table.AddCell(getNewCell("Qtd.", font, Element.ALIGN_CENTER, 10, 15, preto, fundo3));
            if (((i == 2) || (i == 3)) && chkValores)
            {
                table.AddCell(getNewCell("Valor Un.", font, Element.ALIGN_CENTER, 10, 15, preto, fundo3));
                table.AddCell(getNewCell("Valor Total", font, Element.ALIGN_CENTER, 10, 15, preto, fundo3));
            }

            BLLMateriais bll3 = new BLLMateriais(cx);
            DataTable dt3 = bll3.CarregaMateriais(this.idpropostas);
            DataRow[] row3 = dt3.Select();
            font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
            double total_materiais = 0;

            foreach (DataRow dr in row3)
            {
                table.AddCell(getNewCell(dr["material"].ToString(), font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(dr["unidade"].ToString(), font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(dr["quantidade"].ToString(), font, Element.ALIGN_CENTER, 5, 15, preto));
                if (((i == 2) || (i == 3)) && chkValores)
                {
                    table.AddCell(getNewCell(Validacao.MaskFloatMoeda(dr["vl_unitario"].ToString()), font, Element.ALIGN_RIGHT, 5, 15, preto));
                    table.AddCell(getNewCell(Validacao.MaskFloatMoeda(dr["vl_total"].ToString()), font, Element.ALIGN_RIGHT, 5, 15, preto));
                }
                total_materiais = total_materiais + Convert.ToDouble(Validacao.MaskMoedaFloat(Validacao.MaskFloatMoeda(dr["vl_total"].ToString())));
            }
            if ((i == 2) || (i == 3))
            {
                font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
                CriaCell(table, " ", 0, 0, font);
                CriaCell(table, " ", 0, 0, font);
                CriaCell(table, " ", 0, 0, font);
                CriaCell(table, "TOTAL", 2, 0, font);
                table.AddCell(getNewCell(Validacao.MaskFloatMoeda(total_materiais.ToString()), font, Element.ALIGN_RIGHT, 5, 15, preto));
            }
            doc.Add(table);

            if ((i == 2) || (i == 3))
            {
                CriaParagrafo(" Total Serviços: " + Validacao.MaskFloatMoeda(total_servicos.ToString()), Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
                CriaParagrafo(" Total Materiais: " + Validacao.MaskFloatMoeda(total_materiais.ToString()), Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
                double total_geral = total_materiais + total_servicos;
                CriaParagrafo(" Total da Proposta: " + Validacao.MaskFloatMoeda(total_geral.ToString()), Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
                CriaParagrafo(" ", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
            }

            CriaParagrafo("2." + item2 + ". Impostos e Encargos", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item2++;
            CriaParagrafo(" ", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            //Somente para SPEED
            if (idempresas == 5)
            {
                table = new PdfPTable(3);
                float[] sizes5 = new float[] { 100, 20, 20 };
                table.SetWidths(sizes5);
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
                CriaCell(table, "Leis Sociais", 3, 0, font);
                CriaCell(table, " ", 0, 0, font);
                CriaCell(table, " ", 0, 0, font);
                table.AddCell(getNewCell("Descrição", font, Element.ALIGN_LEFT, 10, 15, preto, fundo2));
                table.AddCell(getNewCell("% de Incidência Parcial", font, Element.ALIGN_CENTER, 10, 15, preto, fundo2));
                table.AddCell(getNewCell("% de Incidência Subtotal", font, Element.ALIGN_CENTER, 10, 15, preto, fundo2));
                table.AddCell(getNewCell("GRUPO I", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("36,60", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
                table.AddCell(getNewCell("01 - INSS", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("20,00", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("02 - SESI", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("1,70", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("03 - SENAI", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("1,20", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("04 - INCRA", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("0,20", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("05 - SALÁRIO EDUCAÇÃO", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("2,50", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("06 - SEGUROS DE ACIDENTE DE TRABALHO", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("3,00", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("07 - FGTS", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("8,00", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
                table.AddCell(getNewCell("GRUPO II", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("61,17", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
                table.AddCell(getNewCell("08 - REPOUSO SEMANAL REMUNERADO", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("18,22", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("09 - FÉRIAS", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("14,87", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("10 - FERIADOS E DIAS SANTIFICADOS", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("4,46", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("11 - AVISO-PRÉVIO", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("10,00", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("12 - AUXÍLIO-ENFERMIDADE", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("1,12", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("13 - ACIDENTES NO TRABALHO", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("0,42", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("14 - ENCARGOS PATERNIDADE", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("0,93", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto)); ;
                table.AddCell(getNewCell("15 - 13° SALÁRIO", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("11,15", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
                table.AddCell(getNewCell("GRUPO III", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("6,00", font, Element.ALIGN_CENTER, 5, 15, preto));
                font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
                table.AddCell(getNewCell("16 - INCIDÊNCIA DO FGTS SOBRE O 13º SALÁRIO", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("0,89", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("17 - PAGAMENTO DE 40% POR RESCISÃO S/JUSTA", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("5,11", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
                table.AddCell(getNewCell("INCIDÊNCIAS CUMULATIVAS:", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
                table.AddCell(getNewCell("GRUPO I SOBRE GRUPO II", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("22,39", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell("22,39", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
                table.AddCell(getNewCell("TOTAL:", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("126,16", font, Element.ALIGN_CENTER, 5, 15, preto));
                table.AddCell(getNewCell("ADOTADO:", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell(" ", font, Element.ALIGN_LEFT, 5, 15, preto));
                table.AddCell(getNewCell("126,00%", font, Element.ALIGN_CENTER, 5, 15, preto));
                doc.Add(table);
            }

            table = new PdfPTable(1);
            CriaCell(table, " ", 0, 0, font);
            doc.Add(table);

            table = new PdfPTable(3);
            float[] sizes4 = new float[] { 33, 33, 33 };
            table.SetWidths(sizes4);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);
            CriaCell(table, "IRRF: " + Validacao.MaskFloatMoeda2(modeloprop.IRRF.ToString()) +" %", 5, 15, font);
            CriaCell(table, "CSLL: " + Validacao.MaskFloatMoeda2(modeloprop.CSLL.ToString()) + " %", 5, 15, font);
            CriaCell(table, "CPS: " + Validacao.MaskFloatMoeda2(modeloprop.CPS.ToString()) + " %", 5, 15, font);
            CriaCell(table, "PIS: " + Validacao.MaskFloatMoeda2(modeloprop.PIS.ToString()) + " %", 5, 15, font);
            CriaCell(table, "COFINS: " + Validacao.MaskFloatMoeda2(modeloprop.COFINS.ToString()) + " %", 5, 15, font);
            CriaCell(table, "ISS: " + Validacao.MaskFloatMoeda2(modeloprop.ISS.ToString()) + " %", 5, 15, font);
            doc.Add(table);

            CriaParagrafo(" ", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            BLLEmpresas bll5 = new BLLEmpresas(cx);
            ModeloEmpresa modeloemp = bll5.CarregaEmpresa(this.idempresas);
            ModeloEndereco modeloend = bll5.CarregaEndereco(modeloemp.IdEnderecos);
            CriaParagrafo("2." + item2 + ". Dados da Empresa", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item2++;
            CriaParagrafo(modeloemp.Razao_Social, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
            CriaParagrafo(modeloend.Logradouro, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
            CriaParagrafo("Bairro: " + modeloend.Bairro + " " + modeloend.Cidade + "-" + modeloend.UF, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
            CriaParagrafo("CEP: " + modeloend.CEP, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
            CriaParagrafo("CNPJ: " + modeloemp.CNPJ, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
            CriaParagrafo("Insc. Est.: " + modeloemp.Insc_Est, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));

            CriaParagrafo(" ", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
            CriaParagrafo("2." + item2 + ". Responsáveis", Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.BOLD, preto));
            item2++;
            CriaParagrafo(modeloprop.Responsaveis, Element.ALIGN_JUSTIFIED, FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto));
        }

        public override void MontaCorpoDados(int i)
        {
            //Empresa no Rodapé
            BLLEmpresas bll = new BLLEmpresas(cx);
            ModeloEmpresa modeloemp = bll.CarregaEmpresa(this.idempresas);
            empresa = modeloemp.Nome_Fantasia;
            if (modeloemp.Logo != null)
            {
                System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                System.Drawing.Image img = (System.Drawing.Image)converter.ConvertFrom(modeloemp.Logo);
                img = Validacao.ResizeImage(img, 120, 60);
                img.Save(modeloemp.Nome_Arquivo);
                nome_arquivo = modeloemp.Nome_Arquivo;
            }
            ModeloEndereco modeloend = bll.CarregaEndereco(modeloemp.IdEnderecos);
            endereco1 = modeloend.Logradouro + ", " + modeloend.Bairro;
            endereco2 = modeloend.Cidade + "-" + modeloend.UF + " " + modeloend.CEP + " " + modeloemp.Telefone1;
            BLLPropostas bll2 = new BLLPropostas(cx);
            ModeloPropostas modeloprop = bll2.CarregaPropostas(this.idpropostas);
            num_proposta = modeloprop.Num_Proposta;
            BLLCliente bll3 = new BLLCliente(cx);
            ModeloCliente modelocliente = bll3.CarregaCliente(modeloprop.IdClientes);
            BLLUsuarios bll4 = new BLLUsuarios(cx);
            ModeloUsuario modelousuario = bll4.CarregaUsuario(modeloprop.IdUsuarios);           

            base.MontaCorpoDados();

            BaseColor preto = new BaseColor(0, 0, 0);
            Font font = FontFactory.GetFont("Verdana", 8, Font.NORMAL, preto);

            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            doc.SetMargins(30f, 30f, 65f, 70f);

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 90f;
            table.HorizontalAlignment = Element.ALIGN_CENTER;

            CriaCell(table, "Aracruz, " + DateTime.Today.ToString("dd") + " de " + culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month)) + " de " + DateTime.Today.ToString("yyyy"), 2, 0, font);

            CriaCell(table, "A " + modelocliente.Nome_Fantasia, 0, 0, font);
            CriaCell(table, modeloprop.Localidade, 0, 0, font);
            CriaCell(table, "Att, " + modeloprop.Contato, 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, "Prezado Cliente,", 0, 0, font);
            if (i == 3)
            {
                CriaCell(table, "Conforme vossa solicitação, estamos apresentando nossa proposta técnica/budget referente a " + modeloprop.Descricao, 0, 0, font);
            }
            else
            {
                CriaCell(table, "Conforme vossa solicitação, estamos apresentando nossa proposta técnica/comercial referente a " + modeloprop.Descricao, 0, 0, font);
            }
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, "A " + modeloemp.Nome_Fantasia + " encontra-se a seu dispor para quaisquer esclarecimentos que se fizerem necessários.", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, "Atenciosamente,", 0, 0, font);
            
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            doc.Add(table);

            table = new PdfPTable(3);
            font = FontFactory.GetFont("Verdana", 8, Font.BOLD, preto);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, modelousuario.Nome, 1, 1, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, " ", 0, 0, font);
            CriaCell(table, modeloemp.Nome_Fantasia, 1, 0, font);
            CriaCell(table, " ", 0, 0, font);
            doc.Add(table);

            doc.Add(Chunk.NEXTPAGE);

            PropostaTecnica(modeloprop, i);

            doc.Add(Chunk.NEXTPAGE);

            PropostaComercial(modeloprop, i);

            
            
        }
    }
}