using CoffeeShop.BusinessLogic.BusinessManagement.DTOs;
using CoffeeShop.BusinessLogic.BusinessManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.BusinessManagement.Controllers;

[ApiController]
[Route("v1/businesses")]
public class BusinessesController : ControllerBase
{
    private readonly IBusinessService _businessService;

    public BusinessesController(
        IBusinessService businessService,
        IBusinessMappingService businessMappingService
    )
    {
        _businessService = businessService;
    }

    [HttpPost]
    public async Task<ActionResult<BusinessDTO>> CreateBusiness(
        [FromBody] CreateBusinessDTO request
    )
    {
        return Ok(await _businessService.CreateBusiness(request));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<ActionResult<BusinessDTO>> GetBusiness(Guid id)
    {
        return Ok(await _businessService.GetBusiness(id));
    }
}
