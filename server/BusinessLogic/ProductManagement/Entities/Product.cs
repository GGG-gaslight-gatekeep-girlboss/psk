using CoffeeShop.BusinessLogic.Common.Entities;

namespace CoffeeShop.BusinessLogic.ProductManagement.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required string ImageUrl { get; set; }
    public int Stock { get; set; }
}