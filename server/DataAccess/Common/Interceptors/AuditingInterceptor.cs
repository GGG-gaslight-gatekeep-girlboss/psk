using CoffeeShop.BusinessLogic.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CoffeeShop.DataAccess.Common.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is not null)
        {
            AuditChangedEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AuditChangedEntities(DbContext dbContext)
    {
        // TODO add createdBy and modifiedBy when user login is completed
        var addedEntityEntries = dbContext
            .ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .ToList();

        foreach (var entityEntry in addedEntityEntries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.CreatedAt = DateTimeOffset.UtcNow;
            }

            entityEntry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
        }
    }
}
