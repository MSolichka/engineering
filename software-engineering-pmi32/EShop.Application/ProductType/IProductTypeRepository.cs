using EShop.Application.Abstractions;

namespace EShop.Application.ProductType;

public interface IProductTypeRepository : IRepository<Guid,Domain.Models.ProductType>
{
    
}