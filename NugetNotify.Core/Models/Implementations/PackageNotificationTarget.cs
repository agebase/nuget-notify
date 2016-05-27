using NugetNotify.Database.Enumerations;

namespace NugetNotify.Core.Models.Implementations
{
    public class PackageNotificationTarget : IPackageNotificationTarget
    {
        public int Id { get; set; }
        
        public PackageNotificationType Type { get; set; }
        
        public string Value { get; set; }
    }
}