using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterApplication.BLL;
using DiagnosticCenterApplication.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DiagnosticCenterApplication.UI
{
    public partial class TypeWiseReportPage : System.Web.UI.Page
    {
        TypeWiseReportManger typeManger = new TypeWiseReportManger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                messageLable.Text = "";
            }
            messageLable.Text = "";
            typeWisePdfButton.Visible = false;
        }


        private void LoadEmptyTypeGridView()
        {
            DataTable dataTable = new DataTable();
            typeWiseGridView.DataSource = dataTable;
            typeWiseGridView.DataBind();
        }

        private void LoadTestInGridiew(string fromDate, string toDate)
        {
            List<DateWiseTypeReport> typeReportlList = typeManger.GetDateWiseTypeReports(fromDate, toDate);
            List<DateWiseTypeReport> notTypeReportList = typeManger.NotGetDateWiseTypeReports();

            foreach (DateWiseTypeReport notDETyR in notTypeReportList)
            {
                typeReportlList.Add(notDETyR);
            }
            if (typeReportlList.Count != 0)
            {
                typeWiseGridView.DataSource = typeReportlList;
                typeWiseGridView.DataBind();
                typeWisePdfButton.Visible = true;
            }
            else
            {
                typeWiseGridView.DataSource = null;
                typeWiseGridView.DataBind();
                typeWisePdfButton.Visible = false;
            }


        }

        protected void typeWiseSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string fromDate = fromDateTextBox.Text;
                string toDate = toDateTextBox.Text;

                if (fromDate == string.Empty || toDate == string.Empty)
                {
                    messageLable.Text = "Please Select Both Date";
                    return;
                }
                LoadTestInGridiew(fromDate, toDate);

            }
            catch (Exception exception)
            {
                messageLable.Text = exception.Message;
                typeWisePdfButton.Visible = false;
                typeWiseGridView.Visible = false;
            }
        }

        protected void typeWisePdfButton_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }

        private int serialNo = 0;
        private decimal total = 0;
        protected void typeWiseGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "TestTypeName").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "TotalTest").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "TotalFee").ToString();

                total = total + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalFee"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "Total Amount: ";
                e.Row.Cells[3].Text = total.ToString();
            }
        }



        private void GeneratePDF()
        {

            int columnsCount = typeWiseGridView.HeaderRow.Cells.Count;
            // Create the PDF Table specifying the number of columns


            PdfPTable pdfTable = new PdfPTable(columnsCount);



            // Loop thru each cell in GrdiView header row
            foreach (TableCell gridViewHeaderCell in typeWiseGridView.HeaderRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                //// Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.HeaderStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(typeWiseGridView.HeaderStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }

            // Loop thru each datarow in GrdiView
            foreach (GridViewRow gridViewRow in typeWiseGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        //Font font = new Font();
                        //font.Color = new BaseColor(unpaidGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text));

                        pdfCell.BackgroundColor = new BaseColor(typeWiseGridView.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }



            foreach (TableCell gridViewHeaderCell in typeWiseGridView.FooterRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                // Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.FooterStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(typeWiseGridView.FooterStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }



            // Create the PDF document specifying page size and margins
            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            string centerName = "Diagnostic Center Bill Mangement System";
            string reportName = "Type Wise Report";


            pdfDocument.Open();

            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph(DateTime.Now.ToString()));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph(centerName));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph(reportName));
            pdfDocument.Add(new Paragraph(" \n\n"));



            pdfDocument.Add(pdfTable);
            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition",
                "attachment;filename=TypeWiseReport.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void indexButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }



    }
}