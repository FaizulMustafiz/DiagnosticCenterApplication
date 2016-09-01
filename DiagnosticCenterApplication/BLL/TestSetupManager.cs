using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.BLL
{
    public class TestSetupManager
    {
        TestSetupGateway testSetupGateway = new TestSetupGateway();

        public int SaveTest(TestSetupEntry test)
        {
            if (IsTestNameExists(test))
            {
                throw new Exception("Test Name already exists");
            }
            return testSetupGateway.SaveTest(test);
        }

        public bool IsTestNameExists(TestSetupEntry test)
        {
            return testSetupGateway.IsTestNameExist(test);
        }

        public List<ViewTest> GetAllTests()
        {
            return testSetupGateway.GetAllTests();
        }

        public List<TestSetupEntry> GetAllTestWithType()
        {
            return testSetupGateway.GetAllTestWithType();
        }

        public TestSetupEntry GetAllTestById(int testId)
        {
            return testSetupGateway.GetTestByTestId(testId);
        }

        //public double GetFee(string id)
        //{
        //    return testSetupGateway.GetFee(id);
        //}
    }
}