using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barangay_System.Models
{
    public class HouseholdViewModel
    {
        public string HouseholdID { get; set; }
        public string HouseholdNo { get; set; }
        public string Purok { get; set; }
        public string Address { get; set; }
        public string HeadResidentID { get; set; }
    }
}