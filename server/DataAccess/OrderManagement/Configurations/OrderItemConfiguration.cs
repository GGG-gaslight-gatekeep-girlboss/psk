using CoffeeShop.DataAccess.Common.Configurations;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.OrderManagement.Configurations;

public class OrderItemConfiguration : BaseEntityConfiguration<OrderItem>{
    
    private const string TableName = "OrderItem";

    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.Property(oi => oi.ProductName)
            .IsRequired();

        builder.Property(oi => oi.ProductPrice)
            .IsRequired();

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired();

        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable(TableName);
    }
}