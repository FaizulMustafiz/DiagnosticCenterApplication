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
    public partial class TestWiseReportPage : System.Web.UI.Page
    {
        TestWiseReportManger testManger = new TestWiseReportManger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                messageLable.Text = "";
            }
            messageLable.Text = "";
            testWisePdfButton.Visible = false;
        }


        private void LoadEmptyTestGridView()
        {
            DataTable dataTable = new DataTable();
            testWiseGridView.DataSource = dataTable;
            testWiseGridView.DataBind();
        }


        protected void testWiseSearchButton_Click(object sender, EventArgs e)
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

                LoadTestInGridView(fromDate, toDate);

            }
            catch (Exception exception)
            {
                messageLable.Text = exception.Message;
                testWisePdfButton.Visible = false;
                testWiseGridView.Visible = false;
            }

        }


        protected void testWisePdfButton_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }


        private void LoadTestInGridView(string fromDate, string toDate)
        {
            List<DateWiseTestReport> testReportList = testManger.GetDateWiseTestReports(fromDate, toDate);
            List<DateWiseTestReport> notTestReportList = testManger.NotGeDateWiseTestReports();

            foreach (DateWiseTestReport notDWTR in notTestReportList)
            {
                testReportList.Add(notDWTR);
            }

            if (testReportList.Count != 0)
            {
                testWiseGridView.DataSource = testReportList;
                testWiseGridView.DataBind();
                testWisePdfButton.Visible = true;
            }
            else
            {
                testWiseGridView.DataSource = null;
                testWiseGridView.DataBind();
                testWisePdfButton.Visible = false;
            }
        }



        private int serialNo = 0;
        private decimal total = 0;
        protected void testWiseGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "TestName").ToString();
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

            int columnsCount = testWiseGridView.HeaderRow.Cells.Count;
            // Create the PDF Table specifying the number of columns


            PdfPTable pdfTable = new PdfPTable(columnsCount);



            // Loop thru each cell in GrdiView header row
            foreach (TableCell gridViewHeaderCell in testWiseGridView.HeaderRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                //// Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.HeaderStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(testWiseGridView.HeaderStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }

            // Loop thru each datarow in GrdiView
            foreach (GridViewRow gridViewRow in testWiseGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        //Font font = new Font();
                        //font.Color = new BaseColor(unpaidGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text));

                        pdfCell.BackgroundColor = new BaseColor(testWiseGridView.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }



            foreach (TableCell gridViewHeaderCell in testWiseGridView.FooterRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                // Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.FooterStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(testWiseGridView.FooterStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }



            // Create the PDF document specifying page size and margins
            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            string centerName = "Diagnostic Center Bill Management System";
            string reportName = "Test Wise Report";


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
                "attachment;filename=TestWiseReport.pdf");
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