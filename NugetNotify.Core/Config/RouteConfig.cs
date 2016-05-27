using System.Web.Mvc;
using System.Web.Routing;

namespace NugetNotify.Core.Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.LowercaseUrls = true;
            RouteTable.Routes.AppendTrailingSlash = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute("Home", "{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Shared", action = "Index", id = UrlParameter.Optional });
        }
    }
}