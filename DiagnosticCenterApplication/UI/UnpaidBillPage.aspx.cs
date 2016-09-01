using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterApplication.BLL;
using DiagnosticCenterApplication.Models;
using DiagnosticCenterApplication.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DiagnosticCenterApplication.UI
{
    public partial class UnpaidBillPage : System.Web.UI.Page
    {
        UnpaidBillReportManager unpaidBillReportManager = new UnpaidBillReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                messageLable.Text = "";
            }
            messageLable.Text = "";
            unpaidBillPdfButton.Visible = false;

        }


        private void LoadEmptyUnpaidBillGridView()
        {
            DataTable dataTable = new DataTable();
            unpaidBillGridView.DataSource = dataTable;
            unpaidBillGridView.DataBind();
        }


        private void LoadUnpaidBillInGridView(string toDate, string fromDate)
        {
            List<UnpaidBillReport> unpaidBillReportList = unpaidBillReportManager.GetUnPaidBillList(toDate, fromDate);

            unpaidBillGridView.DataSource = unpaidBillReportList;
            unpaidBillGridView.DataBind();
            unpaidBillPdfButton.Visible = true;
        }

        protected void unpaidBillSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string fromDate = toDateTextBox.Text;
                string toDate = fromDateTextBox.Text;

                if (fromDate == string.Empty || toDate == string.Empty)
                {
                    messageLable.Text = "Please Select Both Date";
                    return;
                }
                LoadUnpaidBillInGridView(toDate, fromDate);

            }
            catch (Exception exception)
            {

                messageLable.Text = exception.Message;
                unpaidBillPdfButton.Visible = false;
                unpaidBillGridView.Visible = false;
            }
        }

        protected void unpaidBillPdfButton_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }

        private int serialNo = 0;
        private decimal total = 0;
        protected void unpaidBillGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "BillNo").ToString();
                e.Row.Cells[2].Text = DataBinder.Eval(e.Row.DataItem, "MobileNo").ToString();
                e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "PatientName").ToString();
                e.Row.Cells[4].Text = DataBinder.Eval(e.Row.DataItem, "TotalAmount").ToString();
                e.Row.Cells[5].Text = DataBinder.Eval(e.Row.DataItem, "PaymentStatus").ToString();
                Control unpaidBill = e.Row.Cells[6].FindControl("DueAmount");
                

                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DueAmount"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";
                e.Row.Cells[4].Text = "Total Due Amount: ";
                e.Row.Cells[5].Text = total.ToString();
            }
        }


        private void GeneratePDF()
        {

            int columnsCount = unpaidBillGridView.HeaderRow.Cells.Count;
            // Create the PDF Table specifying the number of columns


            PdfPTable pdfTable = new PdfPTable(columnsCount);



            // Loop thru each cell in GrdiView header row
            foreach (TableCell gridViewHeaderCell in unpaidBillGridView.HeaderRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                //// Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.HeaderStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(unpaidBillGridView.HeaderStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }

            // Loop thru each datarow in GrdiView
            foreach (GridViewRow gridViewRow in unpaidBillGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        //Font font = new Font();
                        //font.Color = new BaseColor(unpaidGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text));

                        pdfCell.BackgroundColor = new BaseColor(unpaidBillGridView.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }



            foreach (TableCell gridViewHeaderCell in unpaidBillGridView.FooterRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                // Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.FooterStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(unpaidBillGridView.FooterStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }



            // Create the PDF document specifying page size and margins
            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            string centerName = "Diagnostic Center Bill Mangement System";
            string reportName = "Unpaid Bill Report";


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
                "attachment;filename=UnpaidBillReport.pdf");
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