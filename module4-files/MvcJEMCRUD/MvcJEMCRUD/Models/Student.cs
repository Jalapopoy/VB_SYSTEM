using System;
using System.Collections.Generic;
//imports
using System.ComponentModel.DataAnnotations;

namespace MvcJEMCRUD.Models
{
    public partial class Student
    {
        //Validations for String
        [Display(Name = "Student ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Student ID required")]
        public string StudentID { get; set; }

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
        public string Address { get; set; }

        //Validations for Password
        [Display(Name = "Course")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Course required")]
        public string Course { get; set; }
    }
}