using EShop.Application.Basket;
using EShop.Infrastructure.Database;

namespace EShop.Infrastructure.Basket;

public class BasketRepository : BaseRepository<Guid, Domain.Models.Basket, ApplicationDbContext>, IBasketRepository
{
    public BasketRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}