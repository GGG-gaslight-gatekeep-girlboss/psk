using CoffeeShop.BusinessLogic.Common.Entities;

namespace CoffeeShop.BusinessLogic.Common.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    void Add(TEntity entity);
    Task<TEntity> Get(Guid id);
    Task<List<TEntity>> GetMany(IEnumerable<Guid> ids);
    Task Delete(Guid id);
    void DeleteMany(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    string GetEntityNotFoundErrorMessage(Guid id);
}
