using EShop.Application.Abstractions;

namespace EShop.Application.Order;

public interface IOrderRepository : IRepository<Guid,Domain.Models.Order>
{
    
}