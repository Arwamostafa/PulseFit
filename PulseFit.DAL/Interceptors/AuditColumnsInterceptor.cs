using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Interceptors;

public class AuditColumnsInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAuditInformation(eventData.Context);
        return base.SavingChanges(eventData, result);

    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ApplyAuditInformation(DbContext? context)
    {

        if (context == null) return;
        var entries = context.ChangeTracker.Entries<BaseEntity>();

        var currentTime = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = currentTime;
                    entry.Entity.UpdatedAt = null;

                    if (entry.Entity.IsDeleted && entry.Entity.DeletedAt is null)
                        entry.Entity.DeletedAt = currentTime;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = currentTime;
                    if (entry.Entity.IsDeleted)
                    {
                        if (entry.Entity.DeletedAt is null)
                            entry.Entity.DeletedAt = currentTime;
                    }
                    else
                    {
                        entry.Entity.DeletedAt = null;
                    }
                    break;
            }

        }
    }







}

