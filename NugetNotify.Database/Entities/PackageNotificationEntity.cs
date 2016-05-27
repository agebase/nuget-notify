using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NugetNotify.Database.Entities
{
    [Table("PackageNotifications")]
    public class PackageNotificationEntity
    {
        [Key]
        [Column(Order = 0)]
        [Index("IX_PackageNotificationPackageId")]
        public int PackageId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PackageNotificationTargetId { get; set; }

        [ForeignKey("PackageId")]
        public virtual PackageEntity Package { get; set; }

        [ForeignKey("PackageNotificationTargetId")]
        public virtual PackageNotificationTargetEntity Target { get; set; }
    }
}