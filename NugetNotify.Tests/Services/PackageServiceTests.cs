using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NugetNotify.Core.Helpers.Implementations;
using NugetNotify.Core.Services.Implementations;
using NugetNotify.Database.Entities;
using NugetNotify.Database.Interfaces;

namespace NugetNotify.Tests.Services
{
    [TestClass]
    public class PackageServiceTests
    {
        private Mock<IDatabaseContext> _databaseContext;
        private Mock<PackageService> _packageService;

        [TestInitialize]
        public void Setup()
        {
            _databaseContext = new Mock<IDatabaseContext>();
            _packageService = new Mock<PackageService>(_databaseContext.Object, new StringHelper());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_TestExceptionIfPassedEmptyString()
        {
            // Act
            _packageService.Object.Create(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_TestExceptionIfAlreadyExists()
        {
            // Arrange
            var packages = new TestDbSet<PackageEntity>
            {
                new PackageEntity
                {
                    Id = 1,
                    Name = "packagename"
                }
            };
            _databaseContext.SetupGet(dc => dc.Packages).Returns(packages);

            // Act
            _packageService.Object.Create("packagename");
        }

        [TestMethod]
        public void Create_TestNotNullIfCreated()
        {
            // Arrange
            var packages = new TestDbSet<PackageEntity>();
            _databaseContext.SetupGet(dc => dc.Packages).Returns(packages);

            // Act
            var retval = _packageService.Object.Create("packagename");

            // Assert
            Assert.IsNotNull(retval);
            Assert.AreEqual("packagename", retval.Name);
        }

        [TestMethod]
        public void Exists_TestFalseIfNotExists()
        {
            // Arrange
            _databaseContext.SetupGet(dc => dc.Packages).Returns(new TestDbSet<PackageEntity>());

            // Act
            var retval = _packageService.Object.Exists("packagename");

            // Assert
            Assert.IsFalse(retval);
        }

        [TestMethod]
        public void Exists_TestTrueIfExists()
        {
            // Arrange
            var packages = new TestDbSet<PackageEntity>
            {
                new PackageEntity
                {
                    Id = 1,
                    Name = "packagename"
                }
            };
            _databaseContext.SetupGet(dc => dc.Packages).Returns(packages);

            // Act
            var retval = _packageService.Object.Exists("packagename");

            // Assert
            Assert.IsTrue(retval);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_TestExceptionIfPassedEmptyString()
        {
            // Act
            _packageService.Object.Get(string.Empty);
        }

        [TestMethod]
        public void Get_TestNullIfNotExists()
        {
            // Arrange
            _databaseContext.SetupGet(dc => dc.Packages).Returns(new TestDbSet<PackageEntity>());

            // Act
            var retval = _packageService.Object.Get("packagename");

            // Assert
            Assert.IsNull(retval);
        }

        [TestMethod]
        public void Get_TestNotNullIfExists()
        {
            // Arrange
            var packages = new TestDbSet<PackageEntity>
            {
                new PackageEntity
                {
                    Id = 1,
                    Name = "packagename"
                }
            };
            _databaseContext.SetupGet(dc => dc.Packages).Returns(packages);

            // Act
            var retval = _packageService.Object.Get("packagename");

            // Assert
            Assert.IsNotNull(retval);
            Assert.AreEqual("packagename", retval.Name);
        }
    }
}