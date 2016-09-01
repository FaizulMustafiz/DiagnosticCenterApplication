using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterApplication.Models
{
    [Serializable()]
    public class UnpaidBillReport
    {
        public string BillNo { get; set; }
        public string MobileNo { get; set; }
        public string PatientName { get; set; }
        public decimal PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal DueAmount
        {
            get
            {
                if (PaymentStatus==0)
                {
                    return TotalAmount;
                }
                else
                {
                    return PaymentStatus;
                }
            }
        }




    }
}