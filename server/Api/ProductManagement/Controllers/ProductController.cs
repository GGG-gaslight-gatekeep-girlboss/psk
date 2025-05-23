using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.ProductManagement.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)}")]
    public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductDTO request)
    {
        return Ok(await _productService.CreateProduct(request));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
    {
        return Ok(await _productService.GetProduct(id));
    }

    [HttpDelete]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)}")]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProduct(id);
        return NoContent();
    }

    [HttpPatch]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)}")]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDTO request)
    {
        var product = await _productService.UpdateProduct(id, request);
        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
    {
        var products = await _productService.GetAllProducts();
        return Ok(products);
    }
}
