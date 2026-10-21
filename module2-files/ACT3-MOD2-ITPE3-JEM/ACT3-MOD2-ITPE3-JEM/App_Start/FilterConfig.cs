using System.Web;
using System.Web.Mvc;

namespace ACT3_MOD2_ITPE3_JEM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}