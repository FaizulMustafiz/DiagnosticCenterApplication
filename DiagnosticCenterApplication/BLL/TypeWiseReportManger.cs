using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.BLL
{
    public class TypeWiseReportManger
    {
        TypeWiseReportGateway typeWiseReportGateway = new TypeWiseReportGateway();

        internal bool ValidateInput(string startDate, string endDate)
        {
            if (startDate == String.Empty)
            {
                throw new Exception("Select a Date");
            }

            else if (endDate == String.Empty)
            {
                throw new Exception("Select a Date");
            }

            else if (Convert.ToDateTime(startDate) > DateTime.Now)
            {
                throw new Exception("Search Date Cannot Go Beyond Current Date!");
            }

            else if (Convert.ToDateTime(endDate) > DateTime.Now)
            {
                throw new Exception("Search Date Cannot Go Beyond Current Date!");
            }

            return true;
        }

        internal List<DateWiseTypeReport> GetDateWiseTypeReports(string startDate, string endDate)
        {
            try
            {
                return typeWiseReportGateway.GetDateWiseTestReport(startDate, endDate);
            }
            catch (Exception exception)
            {
                
                throw new Exception(exception.Message);
            }
        }

        internal List<DateWiseTypeReport> NotGetDateWiseTypeReports()
        {
            return typeWiseReportGateway.NotGetDateWiseTestreport();
        }


    }
}