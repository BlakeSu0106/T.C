using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telligent.Core.Domain.Loggers
{
    [Table("sys_audit_log")]
    public class AuditLog
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("table_name")]
        public string TableName { get; set; }

        [Column("query_type")]
        public QueryType QueryType { get; set; }

        [Column("primary_key")]
        public string PrimaryKey { get; set; }

        [Column("old_values")]
        public string OldValues { get; set; }

        [Column("new_values")]
        public string NewValues { get; set; }

        [Column("affected_columns")]
        public string AffectedColumns { get; set; }

        [Column("audit_time")]
        public DateTime AuditTime { get; set; }
    }
}
