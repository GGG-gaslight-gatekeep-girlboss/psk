using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.DataAccess.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.DataAccess.ProductManagement.Configurations;

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    private const string TableName = "Product";

    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Version)
            .IsRowVersion()
            .HasColumnName("xmin");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(p => p.Image)
            .WithOne(i => i.Product)
            .HasForeignKey<ProductImage>(p => p.ProductId)
            .IsRequired();
        
        builder.Property(p => p.Stock)
            .IsRequired();

        builder.ToTable(TableName);
    }
}