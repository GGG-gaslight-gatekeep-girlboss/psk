using CoffeeShop.BusinessLogic.BusinessManagement.DTOs;
using CoffeeShop.BusinessLogic.BusinessManagement.Interfaces;
using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.UserManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.BusinessLogic.BusinessManagement.Services;

public class BusinessService : IBusinessService
{
    private readonly IBusinessMappingService _businessMappingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBusinessRepository _businessRepository;
    private readonly UserManager<User> _userManager;

    public BusinessService(
        IBusinessMappingService businessMappingService,
        IUnitOfWork unitOfWork,
        IBusinessRepository businessRepository,
        UserManager<User> userManager
    )
    {
        _businessMappingService = businessMappingService;
        _unitOfWork = unitOfWork;
        _businessRepository = businessRepository;
        _userManager = userManager;
    }

    public async Task<BusinessDTO> CreateBusiness(CreateBusinessDTO dto)
    {
        var business = _businessMappingService.MapCreateBusinessDTOToBusiness(dto);
        _businessRepository.Add(business);

        await _unitOfWork.SaveChanges();

        var businessOwner = await _userManager.FindByIdAsync(business.BusinessOwnerId.ToString());
        businessOwner!.OwnedBusiness = business;
        await _userManager.UpdateAsync(businessOwner);

        return _businessMappingService.MapBusinessToBusinessDTO(business);
    }

    public async Task<BusinessDTO> GetBusiness(Guid id)
    {
        return _businessMappingService.MapBusinessToBusinessDTO(await _businessRepository.Get(id));
    }
}
