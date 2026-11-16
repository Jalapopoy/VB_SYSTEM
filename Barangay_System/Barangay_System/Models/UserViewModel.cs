using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barangay_System.Models
{
    public class UserViewModel
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ResidentID{ get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    }
}