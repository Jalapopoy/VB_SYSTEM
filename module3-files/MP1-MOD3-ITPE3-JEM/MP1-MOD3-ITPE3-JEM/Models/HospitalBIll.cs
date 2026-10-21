using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MP1_MOD3_ITPE3_JEM.Models
{
    public class HospitalBill
    {
        [Display(Name = "Patient Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Patient Number Required")]
        public string PNumJEM { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        public string LNameJEM { get; set; }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        public string FNameJEM { get; set; }

        [Display(Name = "Age")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Age Required")]
        public Nullable<double> AgeJEM { get; set; }

        [Display(Name = "Room Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Room Type Required")]
        public string RTypeJEM { get; set; }

        [Display(Name = "Rate Per Day")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Rate Per Day Required")]
        public Nullable<double> RPDayJEM { get; set; }

        [Display(Name = "No. of Days")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "No. of Days Required")]
        public Nullable<double> NDaysJEM { get; set; }

        [Display(Name = "Laboratory Charges")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Laboratory Charges")]
        public Nullable<double> LChargesJEM { get; set; }

        [Display(Name = "Extra Charges")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Extra Charges Required")]
        public Nullable<double> EChargesJEM { get; set; }

        [Display(Name = "Room Charge")]
        public Nullable<double> RChargeJEM { get; set; }

        [Display(Name = "Total Amount")]
        public Nullable<double> TAmountJEM { get; set; }

        [Display(Name = "Health Insurance")]
        public Nullable<double> HInsuranceJEM { get; set; }

        [Display(Name = "PhilHealth")]
        public Nullable<double> PHealthJEM { get; set; }

        [Display(Name = "SSS")]
        public Nullable<double> SSSJEM { get; set; }

        [Display(Name = "Total Discount")]
        public Nullable<double> TDiscountJEM { get; set; }

        [Display(Name = "Amount To Pay")]
        public Nullable<double> APayJEM { get; set; }

    }
}