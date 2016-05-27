using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NugetNotify.Database.Enumerations;

namespace NugetNotify.Database.Entities
{
    [Table("PackageNotificationTargets")]
    public class PackageNotificationTargetEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PackageNotificationType Type { get; set; }

        [Required]
        [MaxLength(256)]
        public string Value { get; set; }
    }
}