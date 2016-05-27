using System.Data.Entity.Migrations;

namespace NugetNotify.Database
{
    public class DatabaseMigrator : DbMigrator
    {
        public DatabaseMigrator() : base(new DatabaseConfiguration())
        {
            
        }
    }
}