using System.Web;
using System.Web.Mvc;

namespace MP2_MOD4_ITPE3_JEM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}