using System;
using System.ComponentModel;
using System.Linq;
using NugetNotify.Core.Helpers;
using NugetNotify.Core.Models;
using NugetNotify.Database.Entities;
using NugetNotify.Database.Enumerations;
using NugetNotify.Database.Interfaces;

namespace NugetNotify.Core.Services.Implementations
{
    public class PackageNotificationService : IPackageNotificationService
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IPackageNotificationTargetService _packageNotificationTargetService;
        private readonly IPackageService _packageService;
        private readonly IStringHelper _stringHelper;

        public PackageNotificationService(
            IDatabaseContext databaseContext,
            IPackageNotificationTargetService packageNotificationTargetService, 
            IPackageService packageService, 
            IStringHelper stringHelper)
        {
            _databaseContext = databaseContext;
            _packageNotificationTargetService = packageNotificationTargetService;
            _packageService = packageService;
            _stringHelper = stringHelper;
        }

        public void Create(string package, string email, string mobile, string twitter)
        {
            if (string.IsNullOrWhiteSpace(package))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(package));

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(mobile) ||
                string.IsNullOrWhiteSpace(twitter))
            {
                throw new ArgumentException("At least one notification target must be specified.");
            }
            
            var corePackage = _packageService.Exists(package) 
                ? _packageService.Get(package) 
                : _packageService.Create(package);

            if (corePackage == null)
                throw new NullReferenceException("A package must exist to create a package notification.");

            Create(corePackage, PackageNotificationType.Email, email);
            Create(corePackage, PackageNotificationType.Mobile, mobile);
            Create(corePackage, PackageNotificationType.Twitter, twitter);
        }

        private void Create(IPackage package, PackageNotificationType type, string value)
        {
            if (package == null)
                throw new ArgumentNullException(nameof(package));

            if (!Enum.IsDefined(typeof(PackageNotificationType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(PackageNotificationType));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            if (Exists(package.Name, type, value))
                return;

            var corePackageNotificationTarget = _packageNotificationTargetService.Exists(type, value)
                ? _packageNotificationTargetService.Get(type, value)
                : _packageNotificationTargetService.Create(type, value);

            if (corePackageNotificationTarget == null)
                throw new NullReferenceException("A package must exist to create a package notification.");
            
            var notification = new PackageNotificationEntity
            {
                PackageId = package.Id,
                PackageNotificationTargetId = corePackageNotificationTarget.Id
            };

            _databaseContext.PackageNotifications.Add(notification);
            _databaseContext.SaveChanges();
        }

        public bool Exists(string package, PackageNotificationType type, string value)
        {
            var cleaned = _stringHelper.Clean(value);
            var cleanedPackage = _stringHelper.Clean(package);

            if (string.IsNullOrWhiteSpace(cleaned) || string.IsNullOrWhiteSpace(cleanedPackage))
                return false;

            return _databaseContext.PackageNotifications.Any(pn =>
                pn.Package.Name.Equals(cleanedPackage) &&
                pn.Target.Type == type &&
                pn.Target.Value.Equals(cleaned));
        }
    }
}