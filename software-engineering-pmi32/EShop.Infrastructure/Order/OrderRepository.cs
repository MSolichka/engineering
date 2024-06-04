using EShop.Application.Order;
using EShop.Infrastructure.Database;

namespace EShop.Infrastructure.Order;

public class OrderRepository : BaseRepository<Guid,Domain.Models.Order,ApplicationDbContext>,IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}