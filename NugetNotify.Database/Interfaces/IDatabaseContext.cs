using System.Data.Entity;
using NugetNotify.Database.Entities;

namespace NugetNotify.Database.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<PackageEntity> Packages { get; set; }

        DbSet<PackageNotificationTargetEntity> PackageNotificationTargets { get; set; }

        DbSet<PackageNotificationEntity> PackageNotifications { get; set; }

        int SaveChanges();
    }
}