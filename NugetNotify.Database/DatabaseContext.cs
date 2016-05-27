using System.Data.Entity;
using System.Data.Entity.SqlServer;
using NugetNotify.Database.Entities;
using NugetNotify.Database.Interfaces;

namespace NugetNotify.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext() : base("NugetNotifyDatabaseContext")
        {
            // Hack to ensure assembly is copied
            var copyAssembly = SqlProviderServices.Instance;
        }

        public DbSet<PackageEntity> Packages { get; set; }

        public DbSet<PackageNotificationTargetEntity> PackageNotificationTargets { get; set; }

        public DbSet<PackageNotificationEntity> PackageNotifications { get; set; }
    }
}