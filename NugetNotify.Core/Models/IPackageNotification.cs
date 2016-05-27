namespace NugetNotify.Core.Models
{
    public interface IPackageNotification
    {
        IPackage Package { get; set; }

        IPackageNotificationTarget Target { get; set; }
    }
}