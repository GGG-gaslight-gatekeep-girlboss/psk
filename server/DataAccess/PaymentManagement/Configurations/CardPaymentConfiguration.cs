using CoffeeShop.BusinessLogic.PaymentManagement.Entities;
using CoffeeShop.DataAccess.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.DataAccess.PaymentManagement.Configurations;

public class CardPaymentConfiguration : BaseEntityConfiguration<CardPayment>
{
    private const string TableName = "CardPayment";

    public override void Configure(EntityTypeBuilder<CardPayment> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.OrderId)
            .IsRequired();
        
        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(p => p.Status)
            .IsRequired();
        
        builder.Property(p => p.IntentId)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.ToTable(TableName);
    }
}