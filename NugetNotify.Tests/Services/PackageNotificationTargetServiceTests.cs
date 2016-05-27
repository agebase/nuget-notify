using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NugetNotify.Core.Helpers.Implementations;
using NugetNotify.Core.Services.Implementations;
using NugetNotify.Database.Entities;
using NugetNotify.Database.Enumerations;
using NugetNotify.Database.Interfaces;

namespace NugetNotify.Tests.Services
{
    [TestClass]
    public class PackageNotificationTargetServiceTests
    {
        private Mock<IDatabaseContext> _databaseContext;
        private Mock<PackageNotificationTargetService> _packageNotificationTargetService;

        [TestInitialize]
        public void Setup()
        {
            _databaseContext = new Mock<IDatabaseContext>();
            _packageNotificationTargetService = new Mock<PackageNotificationTargetService>(
                _databaseContext.Object, 
                new StringHelper());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_TestExceptionIfPassedEmptyString()
        {
            // Act
            _packageNotificationTargetService.Object.Create(PackageNotificationType.Email, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_TestExceptionIfAlreadyExists()
        {
            // Arrange
            var packageNotificationTargets = new TestDbSet<PackageNotificationTargetEntity>
            {
                new PackageNotificationTargetEntity
                {
                    Id = 1,
                    Type = PackageNotificationType.Twitter,
                    Value = "target"
                }
            };
            _databaseContext.SetupGet(dc => dc.PackageNotificationTargets).Returns(packageNotificationTargets);

            // Act
            _packageNotificationTargetService.Object.Create(PackageNotificationType.Twitter, "target");
        }

        [TestMethod]
        public void Create_TestNotNullIfCreated()
        {
            // Arrange
            var packageNotificationTargets = new TestDbSet<PackageNotificationTargetEntity>();
            _databaseContext.SetupGet(dc => dc.PackageNotificationTargets).Returns(packageNotificationTargets);

            // Act
            var retval = _packageNotificationTargetService.Object.Create(PackageNotificationType.Mobile, "target");

            // Assert
            Assert.IsNotNull(retval);
            Assert.AreEqual(PackageNotificationType.Mobile, retval.Type);
            Assert.AreEqual("target", retval.Value);
        }

        [TestMethod]
        public void Exists_TestFalseIfNotExists()
        {
            // Arrange
            _databaseContext.SetupGet(dc => dc.PackageNotificationTargets)
                .Returns(new TestDbSet<PackageNotificationTargetEntity>());

            // Act
            var retval = _packageNotificationTargetService.Object.Exists(PackageNotificationType.Mobile, "target");

            // Assert
            Assert.IsFalse(retval);
        }

        [TestMethod]
        public void Exists_TestTrueIfExists()
        {
            // Arrange
            var packageNotificationTargets = new TestDbSet<PackageNotificationTargetEntity>
            {
                new PackageNotificationTargetEntity
                {
                    Id = 1,
                    Type = PackageNotificationType.Twitter,
                    Value = "target"
                }
            };
            _databaseContext.SetupGet(dc => dc.PackageNotificationTargets).Returns(packageNotificationTargets);

            // Act
            var retval = _packageNotificationTargetService.Object.Exists(PackageNotificationType.Twitter, "target");

            // Assert
            Assert.IsTrue(retval);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_TestExceptionIfPassedEmptyString()
        {
            // Act
            _packageNotificationTargetService.Object.Get(PackageNotificationType.Email, string.Empty);
        }

        [TestMethod]
        public void Get_TestNullIfNotExists()
        {
            // Arrange
            _databaseContext.SetupGet(dc => dc.PackageNotificationTargets)
                .Returns(new TestDbSet<PackageNotificationTargetEntity>());

            // Act
            var retval = _packageNotificationTargetService.Object.Get(PackageNotificationType.Email, "target");

            // Assert
            Assert.IsNull(retval);
        }

        [TestMethod]
        public void Get_TestNotNullIfExists()
        {
            // Arrange
            var packageNotificationTargets = new TestDbSet<PackageNotificationTargetEntity>
            {
                new PackageNotificationTargetEntity
                {
                    Id = 1,
                    Type = PackageNotificationType.Email,
                    Value = "target"
                }
            };
            _databaseContext.SetupGet(dc => dc.PackageNotificationTargets).Returns(packageNotificationTargets);

            // Act
            var retval = _packageNotificationTargetService.Object.Get(PackageNotificationType.Email, "target");

            // Assert
            Assert.IsNotNull(retval);
            Assert.AreEqual(PackageNotificationType.Email, retval.Type);
            Assert.AreEqual("target", retval.Value);
        }
    }
}