using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterApplication.BLL;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models;
using DiagnosticCenterApplication.Models.ViewModels;


namespace DiagnosticCenterApplication.UI
{
    [Serializable()]
    public partial class PaymentPage : System.Web.UI.Page
    {
        PaymentManger paymentManger = new PaymentManger();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearForm();
        }

        public void ClearForm()
        {
            //billDateFixedLable.Text = "";
            billDateLable.Text = "";

            //totalFeeFixedLable.Text = "";
            totalFeeLable.Text = "";

            //paidAmountFixedlable.Text = "";
            paidAmountLable.Text = "";

            //dueAmountFixedLable.Text = "";
            dueAmountLable.Text = "";
        }



        protected void searchButton_Click(object sender, EventArgs e)
        {
            SearchView search = new SearchView();
            if (billNoOrMobilenoSearchTextBox.Text == string.Empty)
            {
                messageLable.Text = "Plese Provide Bill Number or Mobile Number ";
            }

            string billNo = billNoOrMobilenoSearchTextBox.Text;
            string mobileNo = billNoOrMobilenoSearchTextBox.Text;
            TestRequestEntry testRequest = paymentManger.SearchByBillorMobile(billNo, mobileNo);
            ViewState["testRequest"] = testRequest;
            if (ViewState["testRequest"] == null)
            {
                messageLable.Text = "Not Data Found ! Please try Again";
                billNoOrMobilenoSearchTextBox.Text = "";
            }
            else
            {
                List<SearchView> searches = paymentManger.GetAllBillInfo(billNo, mobileNo);

                billDateLable.Text = testRequest.DueDate.ToShortDateString();
                totalFeeLable.Text = testRequest.TotalAmount.ToString();
                if (testRequest.PaymentStatus == 0)
                {
                    paidAmountLable.Text = 0.ToString();
                }
                else
                {
                    paidAmountLable.Text = (testRequest.TotalAmount - testRequest.PaymentStatus).ToString();
                }
                if (testRequest.PaymentStatus== 0)
                {
                    dueAmountLable.Text = testRequest.TotalAmount.ToString();
                }
                else
                {
                    dueAmountLable.Text = testRequest.PaymentStatus.ToString();
                }
                

                billInfoGridView.DataSource = searches;
                billInfoGridView.DataBind();

            }

        }

        protected void payButton_Click(object sender, EventArgs e)
        {
            string billNo = billNoOrMobilenoSearchTextBox.Text;
            string mobileNo = billNoOrMobilenoSearchTextBox.Text;
            SearchView search = new SearchView();
            TestRequestEntry testRequest = paymentManger.SearchByBillorMobile(billNo, mobileNo);
            ViewState["testRequest"] = testRequest;
            decimal total = 0;
            decimal amount = Convert.ToDecimal(amountTextBox.Text);
            if (testRequest.PaymentStatus == 0)
            {
                total = testRequest.TotalAmount - amount;
            }
            else
            {
                total = testRequest.PaymentStatus - amount;
            }



            if (amountTextBox.Text == string.Empty)
            {
                messageLable.Text = "Please Provide Bill number or Mobile Number";
            }
            if (paymentManger.UpdatePaymentStatus(testRequest.BillNo, testRequest.MobileNo, total) > 0)
            {
                messageLable.Text = "Payment Successfull";

                billDateLable.Text = testRequest.DueDate.ToShortDateString();
                totalFeeLable.Text = testRequest.TotalAmount.ToString();
                paidAmountLable.Text = amountTextBox.Text;
                dueAmountLable.Text = total.ToString();
            }
        }

        protected void indexButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }



    }
}