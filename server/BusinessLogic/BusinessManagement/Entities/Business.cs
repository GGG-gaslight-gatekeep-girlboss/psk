using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Entities;

namespace CoffeeShop.BusinessLogic.BusinessManagement.Entities;

public class Business : BaseEntity
{
    public User BusinessOwner { get; set; } = null!;
    public required string BusinessOwnerId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public ICollection<User> Employees { get; set; } = new List<User>();
}
