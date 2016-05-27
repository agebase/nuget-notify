using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NugetNotify.Core.Config;
using NugetNotify.Core.Windsor;
using NugetNotify.Database;

namespace NugetNotify.Core.Global
{
    public class GlobalApplication : HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            var migrator = new DatabaseMigrator();
            migrator.Update();

            _container = new WindsorContainer().Install(FromAssembly.This());
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(_container.Kernel));
        }

        protected void Application_End()
        {
            _container.Dispose();
        }
    }
}