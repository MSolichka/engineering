using EShop.Application.Abstractions;

namespace EShop.Application.Basket;

public interface IBasketRepository : IRepository<Guid,Domain.Models.Basket>
{
    
}