using System.Linq.Expressions;
using EShop.Domain.Abstractions;
using EShop.Domain.Models;

namespace EShop.Domain.Specification;

public class GetAllOrdersSpecification : Specification<Order>
{
    public GetAllOrdersSpecification(Expression<Func<Order, bool>>? criteria) : base(criteria)
    {
    }

    public GetAllOrdersSpecification(Specification<Order> specification) : base(specification)
    {
    }

    public GetAllOrdersSpecification() : base(criteria:null)
    {
        AddOrderBy(o => o.OrderTime);
        AddInclude($"{nameof(Order.Basket)}.{nameof(Basket.Items)}");
        IsSplitQuery = true;
    }
}