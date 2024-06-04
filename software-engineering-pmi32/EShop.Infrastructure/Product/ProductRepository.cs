using EShop.Application.Abstractions;
using EShop.Application.Product;
using EShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Product;

public class ProductRepository : BaseRepository<Guid,Domain.Models.Product, ApplicationDbContext>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}