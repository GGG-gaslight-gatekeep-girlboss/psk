using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CoffeeShop.DataAccess.Common.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public AuditingInterceptor(ICurrentUserAccessor currentUserAccessor)
    {
        _currentUserAccessor = currentUserAccessor;
    }

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
        var addedEntityEntries = dbContext
            .ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .ToList();

        foreach (var entityEntry in addedEntityEntries)
        {
            var currentUserId = _currentUserAccessor.GetCurrentUserId();
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                entityEntry.Entity.CreatedById = currentUserId;
            }

            entityEntry.Entity.ModifiedAt = DateTimeOffset.UtcNow;
            entityEntry.Entity.ModifiedById = currentUserId;
        }
    }
}
