using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;

namespace NugetNotify.Database.Migrations
{
    public class MigrationContext : HistoryContext
    {
        public MigrationContext(DbConnection existingConnection, string defaultSchema) 
            : base(existingConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable("Migrations", "dbo");
        }
    }
}