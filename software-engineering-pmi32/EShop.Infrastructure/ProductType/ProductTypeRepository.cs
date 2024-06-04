using EShop.Application.ProductType;
using EShop.Infrastructure.Database;

namespace EShop.Infrastructure.ProductType;

public class ProductTypeRepository : BaseRepository<Guid,Domain.Models.ProductType, ApplicationDbContext>, IProductTypeRepository
{
    public ProductTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}