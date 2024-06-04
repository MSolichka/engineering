using EShop.Domain.Abstractions;
using EShop.Domain.Models;

namespace EShop.Domain.Specification;

public class GetPagedProductsSpecification : Specification<Product>
{
    public GetPagedProductsSpecification(int page, int pageSize) : base(criteria: null)
    {
        AddOrderBy(p => p.Quantity);
        AddPagination(page, pageSize);
        AddInclude(p => p.Type);
        AddInclude(p => p.Likes);
        IsSplitQuery = true;
    }
}