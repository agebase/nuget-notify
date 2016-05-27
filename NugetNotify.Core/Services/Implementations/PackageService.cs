using System;
using System.Linq;
using NugetNotify.Core.Helpers;
using NugetNotify.Core.Models;
using NugetNotify.Core.Models.Implementations;
using NugetNotify.Database;
using NugetNotify.Database.Entities;

namespace NugetNotify.Core.Services.Implementations
{
    internal class PackageService : IPackageService
    {
        private readonly IStringHelper _stringHelper;

        public PackageService(IStringHelper stringHelper)
        {
            _stringHelper = stringHelper;
        }

        public IPackage Create(string name)
        {
            var cleaned = _stringHelper.Clean(name);

            if (string.IsNullOrWhiteSpace(cleaned))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            if (Exists(name))
                return null;

            var retval = new Package();

            using (var db = new DatabaseContext())
            {
                var entity = new PackageEntity
                {
                    Name = cleaned
                };

                db.Packages.Add(entity);
                db.SaveChanges();

                retval.Id = entity.Id;
                retval.Name = entity.Name;
            }

            return retval;
        }

        public bool Exists(string name)
        {
            var cleaned = _stringHelper.Clean(name);

            if (string.IsNullOrWhiteSpace(cleaned))
                return false;

            bool retval;

            using (var db = new DatabaseContext())
                retval = db.Packages.Any(p => p.Name.Equals(cleaned));

            return retval;
        } 

        public IPackage Get(string name)
        {
            var cleaned = _stringHelper.Clean(name);

            if (string.IsNullOrWhiteSpace(cleaned))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            if (!Exists(name))
                return null;

            var retval = new Package();

            using (var db = new DatabaseContext())
            {
                var entity = db.Packages.Single(p => p.Name.Equals(cleaned));

                retval.Id = entity.Id;
                retval.Name = entity.Name;
            }

            return retval;
        }
    }
}