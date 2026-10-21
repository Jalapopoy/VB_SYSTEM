using System.Web;
using System.Web.Mvc;

namespace MP1_MOD1_ITPE3_JEM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}