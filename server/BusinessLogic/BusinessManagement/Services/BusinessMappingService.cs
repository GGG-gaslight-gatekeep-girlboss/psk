using CoffeeShop.BusinessLogic.BusinessManagement.DTOs;
using CoffeeShop.BusinessLogic.BusinessManagement.Entities;
using CoffeeShop.BusinessLogic.BusinessManagement.Interfaces;

namespace CoffeeShop.Api.BusinessManagement.Services;

public class BusinessMappingService : IBusinessMappingService
{
    public BusinessDTO MapBusinessToBusinessDTO(Business business)
    {
        return new BusinessDTO
        {
            Id = business.Id,
            Name = business.Name,
            Address = business.Address,
            BusinessOwnerId = business.BusinessOwnerId,
            Email = business.Email,
            PhoneNumber = business.PhoneNumber,
        };
    }

    public Business MapCreateBusinessDTOToBusiness(CreateBusinessDTO dto)
    {
        return new Business
        {
            BusinessOwnerId = dto.BusinessOwnerId,
            Name = dto.Name,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
        };
    }
}
