using Microsoft.EntityFrameworkCore;
using Serilog;
using Telligent.Core.Domain.Loggers;
using Telligent.Core.Domain.Notifications;
using Telligent.Core.Infrastructure.Loggers;

namespace Telligent.Core.Infrastructure.Database
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<NotificationLog> NotificationLogs { get; set; } = null!;
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; } = null!;
        public DbSet<NotificationSchedule> NotificationSchedules { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationTemplate>().Ignore(n => n.TenantId);
            modelBuilder.Entity<NotificationSchedule>().Ignore(n => n.TenantId);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditLogEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog ||
                    entry.State is EntityState.Detached or EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditLogEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = ""
                };

                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        if (property.CurrentValue != null)
                            auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.QueryType = QueryType.Create;

                            if (property.CurrentValue != null)
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.QueryType = QueryType.Delete;

                            if (property.OriginalValue != null)
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified when !property.IsModified:
                            continue;
                        case EntityState.Modified:
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.QueryType = QueryType.Update;

                            if (property.OriginalValue != null)
                                auditEntry.OldValues[propertyName] = property.OriginalValue;

                            if (property.CurrentValue != null)
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                    }
                }
            }

            try
            {
                foreach (var auditEntry in auditEntries) AuditLogs?.Add(auditEntry.ToAudit());
                return await base.SaveChangesAsync(cancellationToken) - auditEntries.Count;
            }
            catch (DbUpdateException ex)
            {
                Log.Fatal(ex, ex.Message);
                throw;
            }
        }
    }
}