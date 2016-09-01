using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models;

namespace DiagnosticCenterApplication.BLL
{
    public class TestTypeManager
    {
        TestTypeGateway testTypeGateway = new TestTypeGateway();


        public int SaveTestType(TestTypeEntry testType)
        {
            if (IsTypeNameExists(testType))
            {
                throw new Exception("Type Name Already Exists.");
            }
            return testTypeGateway.SaveTestType(testType);
        }

        public bool IsTypeNameExists(TestTypeEntry testType)
        {
            return testTypeGateway.IsTypeNameExist(testType);
            //TestTypeEntry existingTestType = testTypeGateway.IsTypeNameExist(testType);
            //if (existingTestType != null)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public List<TestTypeEntry> GetAllTestTypes()
        {
            return testTypeGateway.GetAllTestTypes();
        }
    }
}