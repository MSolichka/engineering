using EShop.Application.Product;
using EShop.Domain.Models;
using EShop.Infrastructure.Database;

namespace EShop.Infrastructure.Product;

public class ProductLikeRepository : BaseRepository<Guid, ProductLike, ApplicationDbContext>, IProductLikeRepository
{
    public ProductLikeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}