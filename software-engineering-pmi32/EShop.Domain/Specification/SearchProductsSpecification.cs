using EShop.Domain.Models;
using EShop.Domain.Abstractions;

namespace EShop.Domain.Specification;

public class SearchProductsSpecification : Specification<Product>
{
    public SearchProductsSpecification(string query, int page, int pageSize)
        : base(p => p.Description.Contains(query))
    {
        AddOrderBy(p => p.Quantity);
        AddPagination(page, pageSize);
        AddInclude(p => p.Type);
        AddInclude(p => p.Likes);
        IsSplitQuery = true;
    }
}