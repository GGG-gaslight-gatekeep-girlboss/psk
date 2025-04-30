using CoffeeShop.BusinessLogic.Common.Entities;

namespace CoffeeShop.BusinessLogic.ProductManagement.Entities;

public class ProductImage : BaseEntity
{
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public required string BlobKey { get; set; }

    private ProductImage()
    {
    }

    public static ProductImage Create(Product product, string imageExtension)
    {
        return new ProductImage
        {
            Product = product,
            BlobKey = $"product-images/{product.Id}{imageExtension}"
        };
    }
}