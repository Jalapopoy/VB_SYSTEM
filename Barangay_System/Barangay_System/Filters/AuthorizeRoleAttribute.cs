using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barangay_System.Filters
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] roles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            this.roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string role = httpContext.Session["Role"] as string;
            return role != null && roles.Contains(role, StringComparer.OrdinalIgnoreCase);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["Role"] == null)
                filterContext.Result = new RedirectResult("~/Home/Index");        // not logged in
            else
                filterContext.Result = new RedirectResult("~/Home/AccessDenied"); // wrong role
        }
    }
}