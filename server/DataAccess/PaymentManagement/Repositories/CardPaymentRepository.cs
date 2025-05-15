using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.PaymentManagement.Entities;
using CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;
using CoffeeShop.DataAccess.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.PaymentManagement.Repositories;

public class CardPaymentRepository : BaseRepository<CardPayment>, ICardPaymentRepository
{
    public CardPaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<CardPayment> GetPaymentByIntentId(string intentId)
    {
        var payment = await DbSet
            .Where(p => p.IntentId == intentId)
            .FirstOrDefaultAsync();

        return payment ?? throw new EntityNotFoundException($"Payment intent with id {intentId} not found");
    }
    
    public override string GetEntityNotFoundErrorMessage(Guid id)
    {
        return $"Payment with id {id} not found";
    }
}