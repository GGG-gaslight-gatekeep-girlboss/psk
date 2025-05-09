using CoffeeShop.DataAccess.Common.Configurations;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.OrderManagement.Configurations;

public class OrderConfiguration : BaseEntityConfiguration<Order>{

    private const string TableName = "Order";

    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.PickupTime)
            .IsRequired();

        builder.Property(o => o.OrderStatus)
               .HasConversion<string>()
               .IsRequired();

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId");

        builder.Property(o => o.IsDeleted)
            .HasDefaultValue(false);

        builder.ToTable(TableName);
    }
}