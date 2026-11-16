using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barangay_System.Models
{
    public class DocumentTypeViewModel
    {
        public string DocTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public int ProcessingDays { get; set; }
    }
}