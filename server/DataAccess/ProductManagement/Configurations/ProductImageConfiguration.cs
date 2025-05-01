using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.DataAccess.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.DataAccess.ProductManagement.Configurations;

public class ProductImageConfiguration : BaseEntityConfiguration<ProductImage>
{
    private const string TableName = "ProductImage";

    public override void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.BlobKey)
            .HasMaxLength(128);

        builder.ToTable(TableName);
    }
}