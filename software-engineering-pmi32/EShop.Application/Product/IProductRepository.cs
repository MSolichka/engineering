using EShop.Application.Abstractions;

namespace EShop.Application.Product;

public interface IProductRepository : IRepository<Guid,Domain.Models.Product>
{
}