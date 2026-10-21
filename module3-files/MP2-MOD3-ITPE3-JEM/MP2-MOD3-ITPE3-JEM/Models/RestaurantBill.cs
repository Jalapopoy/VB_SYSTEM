using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MP2_MOD3_ITPE3_JEM.Models
{
    public class RestaurantBill
    {
        [Display(Name = "Customer Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Number Required")]
        public string CNumJEM { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        public string LNameJEM { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        public string FNameJEM { get; set; }

        [Display(Name = "Customer Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Type Required")]
        public string CTypeJEM { get; set; }

        [Display(Name = "Total Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Total Amount Required")]
        public Nullable<double> TAmountJEM { get; set; }

        [Display(Name = "Tax Charges")]
        public Nullable<double> TChargesJEM { get; set; }

        [Display(Name = "Service Charge")]
        public Nullable<double> SChargesJEM { get; set; }

        [Display(Name = "Total Charges")]
        public Nullable<double> TotChargesJEM { get; set; }

        [Display(Name = "Total Discount")]
        public Nullable<double> TDiscountJEM { get; set; }

        [Display(Name = "Amount to Pay")]
        public Nullable<double> APayJEM { get; set; }
    }
}