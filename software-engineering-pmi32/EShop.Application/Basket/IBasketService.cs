namespace EShop.Application.Basket;

public interface IBasketService
{
    Task<Domain.Models.Basket> GetUserBasket(Guid userId);
    Task<Domain.Models.Basket> AddItemToBasket(Guid userId, Guid itemId, decimal price, int quantity = 1);
    Task<Domain.Models.Basket> SetQuantities(Guid basketId, Guid itemId, int quantity);
    Task<Domain.Models.Basket> RemoveItemFromBasket(Guid basketId, Guid itemId);
    Task ClearBasketAsync(Guid basketId);
    Task ProceedBasket(Guid basketId);
}
