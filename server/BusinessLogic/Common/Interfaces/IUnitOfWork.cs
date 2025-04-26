namespace CoffeeShop.BusinessLogic.Common.Interfaces;

public interface IUnitOfWork
{
    Task ExecuteInTransaction(Func<Task> action);
    Task<T> ExecuteInTransaction<T>(Func<Task<T>> action);
    Task SaveChanges();
}
