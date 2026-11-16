using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barangay_System.Models
{
    public class PaymentViewModel
    {
        public string PaymentID { get; set; }
        public string RequestID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string ORNumber { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string Status { get; set; }
    }
}