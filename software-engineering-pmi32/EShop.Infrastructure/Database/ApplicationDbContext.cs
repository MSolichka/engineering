using EShop.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Database;

public class ApplicationDbContext : IdentityDbContext<Domain.Models.User, UserType, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var productTypes = new List<Domain.Models.ProductType>
        {
            new() {Id = Guid.NewGuid(), Name = "Electronics"},
            new() {Id = Guid.NewGuid(), Name = "Clothing"},
            new() {Id = Guid.NewGuid(), Name = "Books"}
        };
        modelBuilder.Entity<Domain.Models.ProductType>().HasData(productTypes);
        modelBuilder.Entity<UserType>().HasData(new List<UserType>
        {
            new()
            {
                Name = "Admin",
                Id = Guid.NewGuid(),
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "ADMIN"
            },
            new()
            {
                Name = "User",
                Id = Guid.NewGuid(),
                NormalizedName = "USER",
                ConcurrencyStamp = "USER"
            }
        });
        modelBuilder.Entity<ProductLike>()
            .HasKey(like => new {like.UserId, like.ProductId});
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Domain.Models.Product> Items { get; set; }
    public DbSet<Domain.Models.ProductType> ProductTypes { get; set; }
    public DbSet<Domain.Models.Order> Orders { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Domain.Models.Basket> Baskets { get; set; }
    public DbSet<ProductLike> ProductLikes { get; set; }
}