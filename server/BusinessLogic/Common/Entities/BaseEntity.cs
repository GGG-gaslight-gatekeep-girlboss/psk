using CoffeeShop.BusinessLogic.UserManagement.Entities;

namespace CoffeeShop.BusinessLogic.Common.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; set; }
    public User CreatedBy { get; set; } = null!;
    public string? CreatedById { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public User ModifiedBy { get; set; } = null!;
    public string? ModifiedById { get; set; }
}
