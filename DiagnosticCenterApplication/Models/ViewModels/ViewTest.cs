using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterApplication.Models
{
    [Serializable()]
    public class ViewTest
    {
        public string TestName { get; set; }
        public decimal TestFee { get; set; }
        public string TestTypeName { get; set; }

    }
}