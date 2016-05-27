using System;
using System.Linq;
using NugetNotify.Core.Helpers;
using NugetNotify.Core.Models;
using NugetNotify.Core.Models.Implementations;
using NugetNotify.Database;
using NugetNotify.Database.Entities;
using NugetNotify.Database.Enumerations;

namespace NugetNotify.Core.Services.Implementations
{
    internal class PackageNotificationTargetService : IPackageNotificationTargetService
    {
        private readonly IStringHelper _stringHelper;

        public PackageNotificationTargetService(IStringHelper stringHelper)
        {
            _stringHelper = stringHelper;
        }

        public IPackageNotificationTarget Create(PackageNotificationType type, string value)
        {
            var cleaned = _stringHelper.Clean(value);

            if (string.IsNullOrWhiteSpace(cleaned))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            if (Exists(type, value))
                return null;

            var retval = new PackageNotificationTarget();

            using (var db = new DatabaseContext())
            {
                var entity = new PackageNotificationTargetEntity
                {
                    Type = type,
                    Value = cleaned
                };

                db.PackageNotificationTargets.Add(entity);
                db.SaveChanges();

                retval.Id = entity.Id;
                retval.Type = entity.Type;
                retval.Value = entity.Value;
            }

            return retval;
        }

        public bool Exists(PackageNotificationType type, string value)
        {
            var cleaned = _stringHelper.Clean(value);

            if (string.IsNullOrWhiteSpace(cleaned))
                return false;

            bool retval;

            using (var db = new DatabaseContext())
                retval = db.PackageNotificationTargets.Any(pnt => pnt.Type == type && pnt.Value.Equals(cleaned));

            return retval;
        }

        public IPackageNotificationTarget Get(PackageNotificationType type, string value)
        {
            var cleaned = _stringHelper.Clean(value);

            if (string.IsNullOrWhiteSpace(cleaned))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            if (!Exists(type, value))
                return null;

            var retval = new PackageNotificationTarget();

            using (var db = new DatabaseContext())
            {
                var entity = db.PackageNotificationTargets.Single(pnt => pnt.Type == type && pnt.Value.Equals(cleaned));

                retval.Id = entity.Id;
                retval.Type = entity.Type;
                retval.Value = entity.Value;
            }

            return retval;
        }
    }
}