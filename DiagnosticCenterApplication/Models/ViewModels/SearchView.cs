using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterApplication.Models.ViewModels
{
    [Serializable()]
    public class SearchView
    {
        public int PatientId { get; set; }

        public string BillNo { get; set; }

        public string MobileNo { get; set; }

        public string TestName { get; set; }

        public decimal TestFee { get; set; }

        public decimal Amount { get; set; }

    }
}