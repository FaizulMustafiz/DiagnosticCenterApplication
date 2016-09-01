using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterApplication.BLL;
using DiagnosticCenterApplication.Models;

namespace DiagnosticCenterApplication.UI
{
    public partial class TestTypeSetupPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearLable();
            LoadAllTestType();
        }
        string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticCenterConnectionString"].ConnectionString;

        TestTypeManager testTypeManager = new TestTypeManager();

        private void ClearLable()
        {
            messageLable.Text = "";
        }
        protected void saveTestTypeButton_Click(object sender, EventArgs e)
        {
            try
            {
                TestTypeEntry testType = new TestTypeEntry();

                testType.TestTypeName = testTypeTextbox.Text;

                if (testTypeManager.SaveTestType(testType) > 0)
                {
                    messageLable.Text = "Test Type Successfully Saved";
                    LoadAllTestType();
                    testTypeTextbox.Text = "";
                }
                else
                {
                    messageLable.Text = "Save Failed";
                }
            }
            catch (Exception exception)
            {

                messageLable.Text = exception.Message;
            }
        }

        private void LoadAllTestType()
        {
            List<TestTypeEntry> testTypes = testTypeManager.GetAllTestTypes();

            if (testTypes.Count > 0)
            {
                testTypeGridView.DataSource = testTypes;
                testTypeGridView.DataBind();
            }
        }

        protected void indexButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }

    }
}