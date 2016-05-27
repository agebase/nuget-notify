using System;
using System.Linq;
using NugetNotify.Core.Helpers;
using NugetNotify.Core.Models;
using NugetNotify.Core.Models.Implementations;
using NugetNotify.Database.Entities;
using NugetNotify.Database.Enumerations;
using NugetNotify.Database.Interfaces;

namespace NugetNotify.Core.Services.Implementations
{
    public class PackageNotificationTargetService : IPackageNotificationTargetService
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IStringHelper _stringHelper;

        public PackageNotificationTargetService(
            IDatabaseContext databaseContext,
            IStringHelper stringHelper)
        {
            _databaseContext = databaseContext;
            _stringHelper = stringHelper;
        }

        public IPackageNotificationTarget Create(PackageNotificationType type, string value)
        {
            var cleaned = _stringHelper.Clean(value);

            if (string.IsNullOrWhiteSpace(cleaned))
                throw new ArgumentNullException(nameof(value));

            if (Exists(type, value))
                throw new ArgumentException("Package Notification Target already exists");

            var entity = new PackageNotificationTargetEntity
            {
                Type = type,
                Value = cleaned
            };

            _databaseContext.PackageNotificationTargets.Add(entity);
            _databaseContext.SaveChanges();

            return new PackageNotificationTarget
            {
                Id = entity.Id,
                Type = entity.Type,
                Value = entity.Value
            };
        }

        public bool Exists(PackageNotificationType type, string value)
        {
            var cleaned = _stringHelper.Clean(value);

            return !string.IsNullOrWhiteSpace(cleaned) && 
                _databaseContext.PackageNotificationTargets.Any(pnt => pnt.Type == type && pnt.Value.Equals(cleaned));
        }

        public IPackageNotificationTarget Get(PackageNotificationType type, string value)
        {
            var cleaned = _stringHelper.Clean(value);

            if (string.IsNullOrWhiteSpace(cleaned))
                throw new ArgumentNullException(nameof(value));

            if (!Exists(type, value))
                return null;

            var entity = _databaseContext.PackageNotificationTargets.Single(pnt => pnt.Type == type && pnt.Value.Equals(cleaned));

            return new PackageNotificationTarget
            {
                Id = entity.Id,
                Type = entity.Type,
                Value = entity.Value
            };
        }
    }
}