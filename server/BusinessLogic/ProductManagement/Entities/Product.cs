using CoffeeShop.BusinessLogic.Common.Entities;

namespace CoffeeShop.BusinessLogic.ProductManagement.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }
    public ProductImage? Image { get; private set; }

    public void SetImage(ProductImage image) => Image = image;
}