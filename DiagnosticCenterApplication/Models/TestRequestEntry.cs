using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterApplication.Models
{
    [Serializable()]
    public class TestRequestEntry
    {

        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Dob { get; set; }
        public string MobileNo { get; set; }
        public int TestId { get; set; }
        public string BillNo { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PaymentStatus { get; set; }


    }
}