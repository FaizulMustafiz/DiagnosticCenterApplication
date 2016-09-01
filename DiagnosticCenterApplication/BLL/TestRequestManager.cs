using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models;

namespace DiagnosticCenterApplication.BLL
{
    public class TestRequestManager
    {
        TestRequestGateway testRequestGateway = new TestRequestGateway();


        public int SaveTestRequest(TestRequestEntry testRequest)
        {
            if (testRequestGateway.IsMobileNoxists(testRequest.MobileNo))
            {
                throw new Exception("Mobile number already exists");
            }
            else
            {
                return testRequestGateway.SaveTestRequest(testRequest);
            }

        }

        public int SavepatientTest(int patienId, int testId, string requestDate)
        {
            if (testRequestGateway.IsPatientTestExists(patienId, testId))
            {
                throw new Exception("You are adding duplicate Test, please select single test.");
            }
            else
            {
                return testRequestGateway.SavepatientTests(patienId, testId, requestDate);
            }
        }

        public TestRequestEntry GetPatientByMobilelNo(string mobileNo)
        {
            return testRequestGateway.GetPatientByMobileNo(mobileNo);
        }


    }
}