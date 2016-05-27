namespace NugetNotify.Core.Models.Implementations
{
    public class PackageNotification : IPackageNotification
    {
        public IPackage Package { get; set; }
        
        public IPackageNotificationTarget Target { get; set; }
    }
}