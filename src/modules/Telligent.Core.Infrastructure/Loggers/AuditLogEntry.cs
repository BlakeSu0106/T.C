using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Telligent.Core.Domain.Loggers;
using Telligent.Core.Infrastructure.Extensions;

namespace Telligent.Core.Infrastructure.Loggers
{
    internal class AuditLogEntry
    {
        public AuditLogEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new();
        public Dictionary<string, object> OldValues { get; } = new();
        public Dictionary<string, object> NewValues { get; } = new();
        public QueryType QueryType { get; set; }
        public List<string> ChangedColumns { get; } = new();

        public AuditLog ToAudit()
        {
            var audit = new AuditLog
            {
                UserId = "",
                QueryType = QueryType,
                TableName = TableName,
                AuditTime = DateTime.UtcNow.ToUtc8DateTime(),
                PrimaryKey = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
                AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns)
            };

            return audit;
        }
    }
}
