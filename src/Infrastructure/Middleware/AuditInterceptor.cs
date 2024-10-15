using Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Application.Repositories;

namespace Infrastructure.Middleware
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUser _currentUser;

        public AuditInterceptor(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
            {
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var userId = _currentUser.GetUserId();
            var actionName = RequestContext.ActionName;
            var userName = _currentUser.GetUserNameAndChandaNo();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity &&
                    entry.State != EntityState.Detached &&
                    entry.State != EntityState.Unchanged)
                {
                    var auditEntry = new AuditEntry(entry)
                    {
                        Action = entry.State.ToString(),
                        ActionName = actionName,
                        UserId = userId,
                        UserName = userName,
                    };
                    auditEntries.Add(auditEntry);
                }
            }
            context.Set<AuditEntry>().AddRange(auditEntries);
            var baseSaveResult = await base.SavingChangesAsync(eventData, result, cancellationToken);
            return baseSaveResult;
        }
    }

    public class AuditEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EntityName { get; set; } = "Unknown";
        public string Action { get; set; } = "Unknown";
        public string ActionName { get; set; } = "Unknown";
        public string UserId { get; set; } = "Anonymous";
        public string UserName { get; set; } = "Anonymous";
        public DateTime Timestamp { get; set; }
        public Dictionary<string, object?> OldValues { get; } = new();
        public Dictionary<string, object?> NewValues { get; } = new();

        public AuditEntry() { }

        public AuditEntry(EntityEntry entry)
        {
            EntityName = entry.Entity.GetType().Name;
            Timestamp = DateTime.UtcNow;

            foreach (var property in entry.Properties)
            {
                if (property.IsModified)
                {
                    OldValues[property.Metadata.Name] = property.OriginalValue;
                    NewValues[property.Metadata.Name] = property.CurrentValue;
                }
            }
        }
    }

}
