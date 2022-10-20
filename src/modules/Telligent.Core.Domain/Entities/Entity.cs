using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telligent.Core.Domain.Entities
{
    public class Entity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("tenant_id")]
        public Guid TenantId { get; set; }

        [Column("entity_status")]
        public bool EntityStatus { get; set; }

        [Column("creation_time")]
        public DateTime? CreationTime { get; set; }

        [Column("creator_id")]
        public Guid? CreatorId { get; set; }

        [Column("modification_time")]
        public DateTime? ModificationTime { get; set; }

        [Column("modifier_id")]
        public Guid? ModifierId { get; set; }

        [Column("deletion_time")]
        public DateTime? DeletionTime { get; set; }

        [Column("deleter_id")]
        public Guid? DeleterId { get; set; }
    }
}
