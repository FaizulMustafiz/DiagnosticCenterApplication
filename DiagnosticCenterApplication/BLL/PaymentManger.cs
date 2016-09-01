using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.BLL
{
    public class PaymentManger
    {
        TestRequestGateway testRequestGateway = new TestRequestGateway();

        public TestRequestEntry SearchByBillorMobile(string billNo, string mobileNo)
        {
            return testRequestGateway.SearchByBillorMobileNo(billNo, mobileNo);
        }

        public List<SearchView> GetAllBillInfo(string billNo, string mobileNo)
        {
            return testRequestGateway.GetAllBillInfo(billNo, mobileNo);
        }


        public int UpdatePaymentStatus(string billNo, string mobileNo, decimal amount)
        {
            return testRequestGateway.UpdatePaymentStatus(billNo, mobileNo, amount);
        }

    }
}