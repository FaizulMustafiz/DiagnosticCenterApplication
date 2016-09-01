using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiagnosticCenterApplication.Models
{
    [Serializable()]
    public class TestTypeEntry
    {
        public int TestTypeId { get; set; }
        public string TestTypeName { get; set; }
    }
}