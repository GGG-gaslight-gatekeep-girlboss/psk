using CoffeeShop.BusinessLogic.BusinessManagement.Entities;
using CoffeeShop.DataAccess.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PointOfSale.DataAccess.BusinessManagement.Configurations;

public class BusinessConfiguration : BaseEntityConfiguration<Business>
{
    private const string TableName = "Business";

    public override void Configure(EntityTypeBuilder<Business> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(b => b.BusinessOwner)
            .WithOne(u => u.OwnedBusiness)
            .HasForeignKey<Business>(b => b.BusinessOwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(b => b.Employees)
            .WithOne(u => u.EmployerBusiness)
            .HasForeignKey(u => u.EmployerBusinessId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TableName);
    }
}
