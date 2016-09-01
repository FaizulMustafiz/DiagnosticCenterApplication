using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiagnosticCenterApplication
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void testTypeSetupPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/TestTypeSetupPage.aspx");
        }

        protected void testSetupPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/TestSetupPage.aspx");
        }

        protected void testRequestEntryPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/TestRequestEntryPage.aspx");
        }

        protected void paymentPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/PaymentPage.aspx");
        }

        protected void testWiseReportPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/TestWiseReportPage.aspx");
        }

        protected void typeWiseReportPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/TypeWiseReportPage.aspx");
        }

        protected void unpaidBillPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/UnpaidBillPage.aspx");
        }
    }
}