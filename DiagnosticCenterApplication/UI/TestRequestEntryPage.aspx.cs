using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterApplication.BLL;
using DiagnosticCenterApplication.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DiagnosticCenterApplication.UI
{
    public partial class TestRequestEntryPage : System.Web.UI.Page
    {
        TestSetupManager testSetupManager = new TestSetupManager();
        TestRequestManager testRequestManager = new TestRequestManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTestTypeDropDown();
            }

            messageLable.Text = "";
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            if (selectTestDropDownList.SelectedIndex != 0)
            {
                int testId = Convert.ToInt32(selectTestDropDownList.SelectedValue);

                if (ViewState["testsId"] == null)
                {

                    List<int> testsId = new List<int>();
                    testsId.Add(testId);
                    ViewState["testsId"] = testsId;
                }
                else
                {
                    List<int> testsId = ViewState["testsId"] as List<int>;
                    testsId.Add(testId);
                    ViewState["testsId"] = testsId;
                }

                LoadDataInGridviwe();
            }
            else
            {
                messageLable.Text = "Please Select at least One test";
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> testsIdList = ViewState["testsId"] as List<int>;

                if (testsIdList == null)
                {
                    messageLable.Text = "please Select at least one test";
                }

                else
                {
                    TestRequestEntry testRequest = new TestRequestEntry();
                    testRequest.PatientName = patientNameTextBox.Text;
                    testRequest.Dob = dobTextBox.Text;
                    testRequest.MobileNo = mobileNoTextBox.Text;
                    testRequest.TotalAmount = (decimal)ViewState["TotalAmount"];
                    testRequest.DueDate = DateTime.Now;
                    testRequest.PaymentStatus = 0;

                    if (testRequestManager.SaveTestRequest(testRequest) > 0)
                    {
                        TestRequestEntry getTestRequest = testRequestManager.GetPatientByMobilelNo(testRequest.MobileNo);
                        int patientId = getTestRequest.PatientId;
                        string requesteDate = DateTime.Now.ToString();
                        foreach (int testId in testsIdList)
                        {
                            testRequestManager.SavepatientTest(patientId, testId, requesteDate);
                        }

                        messageLable.Text = "Saved Successfully";

                        if (FillBillLable(getTestRequest))
                        {
                            messageLable.Text = "Saved Successfully";
                        }

                        patientNameTextBox.Text = "";
                        dobTextBox.Text = "";
                        mobileNoTextBox.Text = "";
                        selectTestDropDownList.SelectedIndex = 0;
                        feeTextBox.Text = "";
                        GeneratePDF();
                    }
                    else
                    {
                        messageLable.Text = "Save Failed";
                    }

                }
            }
            catch (Exception exception)
            {

                messageLable.Text = exception.Message;
            }

            patientNameTextBox.Text = "";
            dobTextBox.Text = "";
            mobileNoTextBox.Text = "";
            selectTestDropDownList.SelectedIndex = 0;
            feeTextBox.Text = "";
            testRequestGridview.DataBind();

        }


        private bool FillBillLable(TestRequestEntry geTestRequest)
        {
            patientNameLable.Text = geTestRequest.PatientName;
            billNoLable.Text = geTestRequest.BillNo;
            dateLable.Text = DateTime.Now.ToShortDateString();
            return true;
        }

        protected void selectTestDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectTestDropDownList.SelectedIndex == 0)
            {
                feeTextBox.Text = string.Empty;
            }
            else
            {
                int testId = Convert.ToInt32(selectTestDropDownList.SelectedValue.ToString());
                ViewState["testId"] = testId;

                TestSetupEntry test = testSetupManager.GetAllTestById(testId);
                feeTextBox.Text = test.TestFee.ToString();
            }
        }

        protected void LoadTestTypeDropDown()
        {
            List<TestSetupEntry> tests = testSetupManager.GetAllTestWithType();
            selectTestDropDownList.DataSource = tests;
            selectTestDropDownList.DataTextField = "TestName";
            selectTestDropDownList.DataValueField = "TestId";
            selectTestDropDownList.DataBind();
            selectTestDropDownList.Items.Insert(0, "----Select----");
        }

        private void LoadDataInGridviwe()
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow = null;
            dataTable.Columns.Add("SerialNo");
            dataTable.Columns.Add("TestName");
            dataTable.Columns.Add("TestFee");


            if (ViewState["tests"] != null)
            {
                for (int i = 0; i < 1; i++)
                {
                    dataTable = (DataTable)ViewState["tests"];
                    if (dataTable.Rows.Count > 0)
                    {
                        dataRow = dataTable.NewRow();
                        dataRow["TestName"] = selectTestDropDownList.SelectedItem;
                        dataRow["TestFee"] = feeTextBox.Text;
                        dataTable.Rows.Add(dataRow);

                        testRequestGridview.DataSource = dataTable;
                        testRequestGridview.DataBind();
                    }
                }
            }
            else
            {
                //ViewState["SerialNo"] = 1;

                dataRow = dataTable.NewRow();
                //dataRow["SerialNo"] = ViewState["SerialNo"];
                dataRow["TestName"] = selectTestDropDownList.SelectedItem;
                dataRow["TestFee"] = feeTextBox.Text;

                dataTable.Rows.Add(dataRow);
                testRequestGridview.DataSource = dataTable;
                testRequestGridview.DataBind();
            }
            //ViewState["SerialNo"] = (int)ViewState["SerialNo"] + 1;
            ViewState["tests"] = dataTable;

        }


        private decimal total = 0;
        private int serialNo = 0;
        protected void testRequestGridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (serialNo += 1).ToString();
                total = total + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TestFee"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "Total : ";
                e.Row.Cells[2].Text = total.ToString();

            }
            ViewState["TotalAmount"] = total;

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    total += (DataBinder.Eval(e.Row.DataItem, "Fee") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TestFee")) : 0;
            //    totalTextBoxs.Text = total.ToString();
            //}
        }

        protected void indexButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }


        private void GeneratePDF()
        {

            int columnsCount = testRequestGridview.HeaderRow.Cells.Count;
            // Create the PDF Table specifying the number of columns


            PdfPTable pdfTable = new PdfPTable(columnsCount);



            // Loop thru each cell in GrdiView header row
            foreach (TableCell gridViewHeaderCell in testRequestGridview.HeaderRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                //// Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.HeaderStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(testRequestGridview.HeaderStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }

            // Loop thru each datarow in GrdiView
            foreach (GridViewRow gridViewRow in testRequestGridview.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {
                        //Font font = new Font();
                        //font.Color = new BaseColor(unpaidGridView.RowStyle.ForeColor);

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text));

                        pdfCell.BackgroundColor = new BaseColor(testRequestGridview.RowStyle.BackColor);

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }



            foreach (TableCell gridViewHeaderCell in testRequestGridview.FooterRow.Cells)
            {
                // Create the Font Object for PDF document
                //Font font = new Font();
                // Set the font color to GridView header row font color
                //font.Color = new BaseColor(unpaidGridView.FooterStyle.ForeColor);

                // Create the PDF cell, specifying the text and font
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));

                // Set the PDF cell backgroundcolor to GridView header row BackgroundColor color
                pdfCell.BackgroundColor = new BaseColor(testRequestGridview.FooterStyle.BackColor);

                // Add the cell to PDF table
                pdfTable.AddCell(pdfCell);
            }



            // Create the PDF document specifying page size and margins
            Document pdfDocument = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

            string centerName = "Diagnostic Center Bill Mangement System";
            string reportName = "Test Request Report";
            string patientName = patientNameLable.Text;
            string billNo = billNoLable.Text;
            string date = dateLable.Text;


            pdfDocument.Open();

            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph(DateTime.Now.ToString()));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph(centerName));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph(reportName + "  For  "+ billNo));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("Patient Name: " + patientName));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("Bill Number: " + billNo));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph("Bill Date: " + date));
            pdfDocument.Add(new Paragraph(" \n"));
            pdfDocument.Add(new Paragraph(" \n"));





            pdfDocument.Add(pdfTable);
            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition",
                "attachment;filename=TestRequestReport.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
    }
}