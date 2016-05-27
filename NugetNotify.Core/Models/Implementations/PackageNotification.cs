namespace NugetNotify.Core.Models.Implementations
{
    internal class PackageNotification : IPackageNotification
    {
        public IPackage Package { get; set; }
        
        public IPackageNotificationTarget Target { get; set; }
    }
}