using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barangay_System.Models
{
    public class ResidentViewModel
    {
        public string ResidentID { get; set; }
        public string HouseholdID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<DateTime> Birthdate { get; set; }
        public string Sex { get; set; }
        public string CivilStatus { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }
    }
}