using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.BLL
{
    public class UnpaidBillReportManager
    {
        UnpaidBillReportGateway unpaidBillReportGateway = new UnpaidBillReportGateway();

        internal bool ValidateInput(string endDate, string startDate)
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
        internal List<UnpaidBillReport> GetUnPaidBillList(string endDate, string startDate)
        {
            try
            {
                return unpaidBillReportGateway.GetUnPaidBillList(endDate, startDate);
            }
            catch (Exception exception)
            {
                
                throw new Exception(exception.Message);
            }
            
        }


        //internal List<DueAmountView> GetDueAmmount()
        //{
        //    return unpaidBillReportGateway.GetDueAmmount();
        //}



    }
}