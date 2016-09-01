using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterApplication.Models.ViewModels
{
    [Serializable()]
    public class DateWiseTestReport
    {
        public string TestName { get; set; }
        public int TotalTest { get; set; }
        public int TotalFee { get; set; }

        public DateWiseTestReport() { }
    }
}