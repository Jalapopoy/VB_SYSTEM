using System.Web;
using System.Web.Mvc;

namespace EXM_MID_ITPE3_JEM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}