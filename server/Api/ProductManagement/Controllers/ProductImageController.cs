using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.ProductManagement.Controllers;

[ApiController]
[Route("v1/products/{productId:Guid}/image")]
public class ProductImageController : ControllerBase
{
    private readonly IProductMappingService _productMappingService;
    private readonly IProductImageService _productImageService;

    public ProductImageController(IProductMappingService productMappingService, IProductImageService productImageService)
    {
        _productMappingService = productMappingService;
        _productImageService = productImageService;
    }

    [HttpPut]
    public async Task<IActionResult> SetProductImage([FromRoute] Guid productId, IFormFile image)
    {
        var setProductImageDTO = _productMappingService.MapSetProductImageDTO(productId, image);
        await _productImageService.SetProductImage(setProductImageDTO);
        return NoContent();
    }
}