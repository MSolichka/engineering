using System.Linq.Expressions;
using EShop.Domain.Abstractions;
using EShop.Domain.Models;

namespace EShop.Domain.Specification;

public class GetBasketByIdSpecification : Specification<Basket>
{
    public GetBasketByIdSpecification(Expression<Func<Basket, bool>>? criteria) : base(criteria)
    {
    }

    public GetBasketByIdSpecification(Specification<Basket> specification) : base(specification)
    {
    }
    public GetBasketByIdSpecification(Guid basketId) : base(b => b.Id == basketId && !b.IsProceed)
    {
        AddInclude($"{nameof(Basket.Items)}.{nameof(BasketItem.Product)}");
    }
}