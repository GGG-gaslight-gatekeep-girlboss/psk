using CoffeeShop.BusinessLogic.BusinessManagement.Entities;
using CoffeeShop.BusinessLogic.Common.Interfaces;

namespace CoffeeShop.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessRepository : IBaseRepository<Business>
{
    Task<Business> GetWithEmployees(Guid id);
}
