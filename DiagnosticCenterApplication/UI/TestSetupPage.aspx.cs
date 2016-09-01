using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticCenterApplication.BLL;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models;

namespace DiagnosticCenterApplication.UI
{
    public partial class TestSetupPage : System.Web.UI.Page
    {
        TestSetupManager testSetupManager = new TestSetupManager();
        TestTypeManager testTypeManager = new TestTypeManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllTestTypes();
            }
            
            LoadAllTests();
            messageLable.Text = "";
        }


        protected void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                TestSetupEntry test = new TestSetupEntry();

                test.TestName = testNameTextBox.Text;
                test.TestFee = Convert.ToDecimal(feeTextBox.Text);
                test.TestTypeId = Convert.ToInt32(testTypeDropDownList.SelectedValue);

                if (testSetupManager.SaveTest(test) > 0)
                {
                    messageLable.Text = "Test Successfully Saved";
                    LoadAllTests();
                    ClearForm();
                }

            }
            catch (Exception exception)
            {

                messageLable.Text = exception.Message;
            }
        }

        private void LoadAllTests()
        {
            List<ViewTest> tests = testSetupManager.GetAllTests();

            testSetupGridView.DataSource = tests;
            testSetupGridView.DataBind();
        }

        private void LoadAllTestTypes()
        {
            List<TestTypeEntry> testTypes = testTypeManager.GetAllTestTypes();

            testTypeDropDownList.DataSource = testTypes;
            testTypeDropDownList.DataTextField = "TestTypeName";
            testTypeDropDownList.DataValueField = "TestTypeId";
            testTypeDropDownList.DataBind();
            testTypeDropDownList.Items.Insert(0, "----Select----");
        }

        protected void ClearForm()
        {
            testNameTextBox.Text = "";
            feeTextBox.Text = "";
            testTypeDropDownList.SelectedIndex = 0;
        }

        protected void indexButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Index.aspx");
        }



    }
}