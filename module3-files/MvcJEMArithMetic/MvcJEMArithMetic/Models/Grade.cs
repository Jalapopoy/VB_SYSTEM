using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcJEMArithMetic.Models
{
    public class Grade
    {
        [Display(Name = "Prelim Grade")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Prelim Grade Required")]
        public Nullable<double> PGradeJEM { get; set; }

        [Display(Name = "Midterm Grade")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Midterm Grade Required")]
        public Nullable<double> MGradeJEM { get; set; }

        [Display(Name = "Final Grade")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Final Grade Required")]
        public Nullable<double> FGradeJEM { get; set; }

        [Display(Name = "Subject Grade")]
        public Nullable<double> SGradeJEM { get; set; }
    }
}