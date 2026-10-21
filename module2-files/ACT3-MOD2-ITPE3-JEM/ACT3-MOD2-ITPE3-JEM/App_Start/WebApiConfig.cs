using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ACT3_MOD2_ITPE3_JEM
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
