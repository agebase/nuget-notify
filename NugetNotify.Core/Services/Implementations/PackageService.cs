using System;
using System.Linq;
using NugetNotify.Core.Helpers;
using NugetNotify.Core.Models;
using NugetNotify.Core.Models.Implementations;
using NugetNotify.Database.Entities;
using NugetNotify.Database.Interfaces;

namespace NugetNotify.Core.Services.Implementations
{
    internal class PackageService : IPackageService
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IStringHelper _stringHelper;

        public PackageService(
            IDatabaseContext databaseContext,
            IStringHelper stringHelper)
        {
            _databaseContext = databaseContext;
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

            var entity = new PackageEntity
            {
                Name = cleaned
            };

            _databaseContext.Packages.Add(entity);
            _databaseContext.SaveChanges();

            retval.Id = entity.Id;
            retval.Name = entity.Name;

            return retval;
        }

        public bool Exists(string name)
        {
            var cleaned = _stringHelper.Clean(name);

            return !string.IsNullOrWhiteSpace(cleaned) && _databaseContext.Packages.Any(p => p.Name.Equals(cleaned));
        } 

        public IPackage Get(string name)
        {
            var cleaned = _stringHelper.Clean(name);

            if (string.IsNullOrWhiteSpace(cleaned))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            if (!Exists(name))
                return null;

            var entity = _databaseContext.Packages.Single(p => p.Name.Equals(cleaned));

            return new Package
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}