using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NugetNotify.Core.Helpers;
using NugetNotify.Core.Helpers.Implementations;
using NugetNotify.Core.Services;
using NugetNotify.Core.Services.Implementations;

namespace NugetNotify.Core.Windsor
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient(),
                Component.For<IStringHelper>().ImplementedBy<StringHelper>().LifestyleTransient(),
                Component.For<IPackageNotificationService>().ImplementedBy<PackageNotificationService>().LifestyleTransient(),
                Component.For<IPackageNotificationTargetService>().ImplementedBy<PackageNotificationTargetService>().LifestyleTransient(),
                Component.For<IPackageService>().ImplementedBy<PackageService>().LifestyleTransient()
            );
        }
    }
}