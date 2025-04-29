using System.Reflection;
using CoffeeShop.BusinessLogic.UserManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using CoffeeShop.DataAccess.Common.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess;

public class ApplicationDbContext : IdentityDbContext<User>
{

    public DbSet<Product> Products { get; set; }


    private readonly ICurrentUserAccessor _currentUserAccessor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserAccessor currentUserAccessor
    )
        : base(options)
    {
        _currentUserAccessor = currentUserAccessor;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditingInterceptor(_currentUserAccessor));
    }
}
