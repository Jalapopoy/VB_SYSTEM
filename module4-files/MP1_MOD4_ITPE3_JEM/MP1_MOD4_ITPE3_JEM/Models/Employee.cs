using System;
using System.Collections.Generic;
//imports
using System.ComponentModel.DataAnnotations;


namespace MP1_MOD4_ITPE3_JEM.Models
{
    public partial class Employee
    {
        //Validations for String
        [Display(Name = "Employee ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee ID required")]
        public string EmployeeID { get; set; }

        //Validations for String
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required")]
        public string LastName { get; set; }

        //Validations for String
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]
        public string FirstName { get; set; }

        //Validations for String
        [Display(Name = "Middle Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Middle Name required")]
        public string MiddleName { get; set; }

        //Validations for Email
        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address required")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        //Validations for Department
        [Display(Name = "Department")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Department required")]
        public string Department { get; set; }
    }
}