using System;
using System.Collections.Generic;
//imports
using System.ComponentModel.DataAnnotations;

namespace MvcJEMCRUD.Models
{
    public partial class User
    {
        //Validations for String
        [Display(Name = "User ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User ID required")]
        public string UserID { get; set; }

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
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Address required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        //Validations for Password
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be 8 char long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //Validations for Confirm Password
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password and password do not match")]
        public string ConfirmPassword { get; set; }
    }
}