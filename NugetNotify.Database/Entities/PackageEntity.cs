using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NugetNotify.Database.Entities
{
    [Table("Packages")]
    public class PackageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        [Index("IX_PackageName", IsUnique = true)]
        public string Name { get; set; }
    }
}