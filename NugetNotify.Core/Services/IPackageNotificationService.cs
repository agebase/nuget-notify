using NugetNotify.Database.Enumerations;

namespace NugetNotify.Core.Services
{
    public interface IPackageNotificationService
    {
        void Create(string package, string email, string mobile, string twitter);

        bool Exists(string package, PackageNotificationType type, string value);
    }
}