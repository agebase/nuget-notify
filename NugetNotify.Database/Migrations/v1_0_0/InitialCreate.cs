using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;

namespace NugetNotify.Database.Migrations.v1_0_0
{
    public class InitialCreate : DbMigration, IMigrationMetadata
    {
        string IMigrationMetadata.Id => "v1.0.0_InitialCreate";

        string IMigrationMetadata.Source => null;

        string IMigrationMetadata.Target => null; 

        public override void Up()
        {
            CreateTable(
                "dbo.PackageNotifications",
                c => new
                {
                    PackageId = c.Int(nullable: false),
                    PackageNotificationTargetId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.PackageId, t.PackageNotificationTargetId })
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .ForeignKey("dbo.PackageNotificationTargets", t => t.PackageNotificationTargetId, cascadeDelete: true)
                .Index(t => t.PackageId, name: "IX_PackageNotificationPackageId")
                .Index(t => t.PackageNotificationTargetId);

            CreateTable(
                "dbo.Packages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_PackageName");

            CreateTable(
                "dbo.PackageNotificationTargets",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.Int(nullable: false),
                    Value = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PackageNotifications", "PackageNotificationTargetId", "dbo.PackageNotificationTargets");
            DropForeignKey("dbo.PackageNotifications", "PackageId", "dbo.Packages");
            DropIndex("dbo.Packages", "IX_PackageName");
            DropIndex("dbo.PackageNotifications", new[] { "PackageNotificationTargetId" });
            DropIndex("dbo.PackageNotifications", "IX_PackageNotificationPackageId");
            DropTable("dbo.PackageNotificationTargets");
            DropTable("dbo.Packages");
            DropTable("dbo.PackageNotifications");
        }
    }
}