using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.Common.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        DbSet = dbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public async Task<TEntity> Get(Guid id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity is not null)
        {
            return entity;
        }

        throw new EntityNotFoundException(typeof(TEntity), id);
    }

    public async Task<List<TEntity>> GetMany(IEnumerable<Guid> ids)
    {
        var distinctIds = ids.Distinct().ToList();
        if (distinctIds.Count == 0)
        {
            return [];
        }

        return await DbSet.Join(distinctIds, e => e.Id, id => id, (e, _) => e).ToListAsync();
    }

    public async Task Delete(Guid id)
    {
        await DbSet.Where(e => e.Id.Equals(id)).ExecuteDeleteAsync();
    }

    public void DeleteMany(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
}
