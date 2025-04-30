using CoffeeShop.BusinessLogic.Common.Entities;

namespace CoffeeShop.BusinessLogic.ProductManagement.Entities;

public class ProductImage : BaseEntity
{
    public Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public string BlobKey { get; private set; }

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