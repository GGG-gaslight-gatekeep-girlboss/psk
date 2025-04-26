using CoffeeShop.BusinessLogic.BusinessManagement.DTOs;
using CoffeeShop.BusinessLogic.BusinessManagement.Entities;

namespace CoffeeShop.BusinessLogic.BusinessManagement.Interfaces;

public interface IBusinessMappingService
{
    BusinessDTO MapBusinessToBusinessDTO(Business business);
    Business MapCreateBusinessDTOToBusiness(CreateBusinessDTO dto);
}
