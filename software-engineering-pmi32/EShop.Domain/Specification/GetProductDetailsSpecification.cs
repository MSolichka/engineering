using System.Linq.Expressions;
using EShop.Domain.Abstractions;
using EShop.Domain.Models;

namespace EShop.Domain.Specification;

public class GetProductDetailsSpecification : Specification<Product>
{
    public GetProductDetailsSpecification(Expression<Func<Product, bool>>? criteria) : base(criteria)
    {
    }

    public GetProductDetailsSpecification(Specification<Product> specification) : base(specification)
    {
    }

    public GetProductDetailsSpecification(Guid id) : base(p => p.Id == id)
    {
        AddInclude(p => p.Type);
        AddInclude(p => p.Likes);
        IsSplitQuery = true;
    }
}