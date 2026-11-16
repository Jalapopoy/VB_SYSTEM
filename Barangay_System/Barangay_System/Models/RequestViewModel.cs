using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barangay_System.Models
{
    public class RequestViewModel
    {
        public string RequestID { get; set; }
        public string ResidentID { get; set; }
        public string DocTypeID { get; set; }
        public string Purpose { get; set; }
        public Nullable<System.DateTime> DateRequested { get; set; }
        public string Status { get; set; }
        public string ProcessedBy { get; set; }
        public Nullable<System.DateTime> DateReleased { get; set; }
        public string Remarks { get; set; }
    }
}