using NugetNotify.Core.Models;
using NugetNotify.Database.Enumerations;

namespace NugetNotify.Core.Services
{
    public interface IPackageNotificationTargetService
    {
        bool Exists(PackageNotificationType type, string value);

        IPackageNotificationTarget Create(PackageNotificationType type, string value);

        IPackageNotificationTarget Get(PackageNotificationType type, string value);
    }
}