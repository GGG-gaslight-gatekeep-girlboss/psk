using CoffeeShop.BusinessLogic.BusinessManagement.DTOs;

namespace CoffeeShop.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessService
{
    Task<BusinessDTO> CreateBusiness(CreateBusinessDTO dto);
    Task<BusinessDTO> GetBusiness(Guid id);
}
