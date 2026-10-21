using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EXM_MID_ITPE3_JEM.Models
{
    public partial class tblemployee
    {
        [Display(Name = "Employee Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Number required")]
        public string EmployeeNumber { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Middle Name required")]
        public string MiddleName { get; set; }

        [Display(Name = "No. of Hours Work")]
        [Required(ErrorMessage = "Hours Work required")]
        public int HoursWork { get; set; }

        [Display(Name = "No. of Hours Overtime")]
        [Required(ErrorMessage = "Hours Overtime required")]
        public int HoursOvertime { get; set; }

        [Display(Name = "No. of Hours Late")]
        [Required(ErrorMessage = "Hours Late required")]
        public int HoursLate { get; set; }

        [Display(Name = "Rate Per Hour")]
        [Required(ErrorMessage = "Rate Per Hour required")]
        public int RatePerHour { get; set; }

        public int GrossPay { get; set; }
        public int Tax { get; set; }
        public int SSS { get; set; }
        public int PagIbig { get; set; }
        public int PhilHealth { get; set; }
        public int TotalDeduction { get; set; }
        public int NetPay { get; set; }
    }
}