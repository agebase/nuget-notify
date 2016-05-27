using System.Data.Entity.Migrations;
using NugetNotify.Database.Migrations;

namespace NugetNotify.Database
{
    public class DatabaseConfiguration : DbMigrationsConfiguration<DatabaseContext>
    {
        public DatabaseConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            SetHistoryContextFactory("System.Data.SqlClient", (connection, defaultSchema) => new MigrationContext(connection, defaultSchema));
        }
    }
}