using EShop.Application.Abstractions;

namespace EShop.Application.Product;

public interface IProductLikeRepository : IRepository<Guid,Domain.Models.ProductLike>
{
    
}