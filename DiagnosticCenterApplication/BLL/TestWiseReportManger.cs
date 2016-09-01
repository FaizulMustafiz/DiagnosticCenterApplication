using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.BLL
{
    public class TestWiseReportManger
    {
        TestWiseReportGateway testWiseReportGateway = new TestWiseReportGateway();

        internal bool ValidInput(string startDate, string endDate)
        {
            if (startDate == string.Empty)
            {
                throw new Exception("Select a Date");
            }
            else if (endDate == string.Empty)
            {
                throw new Exception("Select a Date");
            }
            else if (Convert.ToDateTime(startDate) > DateTime.Now)
            {
                throw new Exception("search date cannot go beyond Current date!");
            }
            else if (Convert.ToDateTime(endDate)> DateTime.Now)
            {
                throw new Exception("search date cannot go beyond Current date!");
            }
            return true;
        }


        internal List<DateWiseTestReport> GetDateWiseTestReports(string startDate, string endDate)
        {
            try
            {
                return testWiseReportGateway.GetDateWiseTestReport(startDate, endDate);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        internal List<DateWiseTestReport> NotGeDateWiseTestReports()
        {
            return testWiseReportGateway.NotGetDateWiseTestReport();
        }

    }
}