using System.Linq.Expressions;
using EShop.Domain.Models;
using EShop.Domain.Abstractions;

namespace EShop.Domain.Specification;

public class GetProductLikeSpecification : Specification<ProductLike>
{
    public GetProductLikeSpecification(Expression<Func<ProductLike, bool>>? criteria) : base(criteria)
    {
    }

    public GetProductLikeSpecification(Specification<ProductLike> specification) : base(specification)
    {
    }
    
    public GetProductLikeSpecification(Guid productId, Guid userId) : base(pl => pl.ProductId == productId && pl.UserId == userId)
    {
    }
}