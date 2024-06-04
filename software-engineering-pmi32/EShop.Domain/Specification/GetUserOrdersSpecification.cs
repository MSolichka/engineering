using System.Linq.Expressions;
using EShop.Domain.Abstractions;
using EShop.Domain.Models;

namespace EShop.Domain.Specification;

public class GetUserOrdersSpecification : Specification<Order>
{
    public GetUserOrdersSpecification(Expression<Func<Order, bool>>? criteria) : base(criteria)
    {
    }

    public GetUserOrdersSpecification(Specification<Order> specification) : base(specification)
    {
    }

    public GetUserOrdersSpecification(Guid userId) : base(o => o.UserId == userId)
    {
        AddOrderBy(o => o.OrderTime);
        AddInclude($"{nameof(Order.Basket)}.{nameof(Basket.Items)}");
        IsSplitQuery = true;
    }
}