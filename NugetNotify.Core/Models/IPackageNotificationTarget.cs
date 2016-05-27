using NugetNotify.Database.Enumerations;

namespace NugetNotify.Core.Models
{
    public interface IPackageNotificationTarget
    {
        int Id { get; set; }

        PackageNotificationType Type { get; set; }

        string Value { get; set; }
    }
}