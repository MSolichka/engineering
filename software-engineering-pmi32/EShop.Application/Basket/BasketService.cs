using EShop.Application.Product;
using EShop.Domain.Specification;
using Microsoft.AspNetCore.Identity;

namespace EShop.Application.Basket;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly UserManager<Domain.Models.User> _userManager;

    public BasketService(IBasketRepository repository, IProductRepository productRepository, UserManager<Domain.Models.User> userManager)
    {
        _repository = repository;
        _productRepository = productRepository;
        _userManager = userManager;
    }

    public async Task<Domain.Models.Basket> GetUserBasket(Guid userId)
    {
        var isUserExists = await _userManager.FindByIdAsync(userId.ToString());
        if (isUserExists is null)
        {
            throw new ArgumentException("User not found");
        }
        return await GetOrCreateBasket(userId);
    }

    public async Task<Domain.Models.Basket> AddItemToBasket(Guid userId, Guid itemId, decimal price, int quantity = 1)
    {
        var basket = await GetOrCreateBasket(userId);

        if (basket.Items.All(i => i.ProductId != itemId))
        {
            var product = await _productRepository.AnyAsync(itemId);
            if (!product)
            {
                throw new Exception("Product not found");
            }
            
            basket.Items.Add(new Domain.Models.BasketItem
            {
                BasketId = basket.Id,
                ProductId = itemId,
                Quantity = quantity
            });
            basket.TotalPrice += price * quantity;
            await _repository.UpdateAsync(basket);
            return basket;
        }
        
        basket.Items.First(i => i.ProductId == itemId).Quantity += quantity;
        basket.TotalPrice += price * quantity;
        await _repository.UpdateAsync(basket);
        return basket;
    }

    public async Task<Domain.Models.Basket> SetQuantities(Guid basketId, Guid itemId, int quantity)
    {
        var basket = await _repository.GetBySpecificationAsync(new GetBasketByIdSpecification(basketId));
        if (basket == null)
        {
            throw new Exception("Basket not found");
        }
        
        var item = basket.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new Exception("Item not found");
        }
        
        item.Quantity = quantity;
        basket.TotalPrice = basket.Items.Sum(i => i.Quantity * i.Product.Price);
        
        await _repository.UpdateAsync(basket);
        return basket;
    }

    public async Task<Domain.Models.Basket> RemoveItemFromBasket(Guid basketId, Guid itemId)
    {
        var basket = await _repository.GetBySpecificationAsync(new GetBasketByIdSpecification(basketId));
        if (basket == null)
        {
            throw new Exception("Basket not found");
        }
        
        var item = basket.Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new Exception("Item not found");
        }
        
        basket.Items.Remove(item);
        basket.TotalPrice = basket.Items.Sum(i => i.Quantity * i.Product.Price);
        await _repository.UpdateAsync(basket);
        return basket;
    }

    public async Task ClearBasketAsync(Guid basketId)
    {
        var basket = await _repository.GetBySpecificationAsync(new GetBasketByIdSpecification(basketId));
        if (basket is null)
        {
            throw new Exception("Basket not found");
        }
        
        basket.Items.Clear();
        basket.TotalPrice = 0;
        
        await _repository.UpdateAsync(basket);
    }

    public async Task ProceedBasket(Guid basketId)
    {
        var basket = await _repository.GetBySpecificationAsync(new GetBasketByIdSpecification(basketId));
        if (basket is null)
        {
            throw new Exception("Basket not found");
        }

        if (basket.Items.Count == 0)
        {
            throw new Exception("Nothing to proceed");
        }
        
        basket.IsProceed = true;
        await _repository.UpdateAsync(basket);
    }
    
    private async Task<Domain.Models.Basket> GetOrCreateBasket(Guid userId)
    {
        var basket = await _repository.GetBySpecificationAsync(new GetBasketByUserSpecification(userId));
        if (basket == null)
        {
            basket = new Domain.Models.Basket();
            basket.UserId = userId;
            await _repository.CreateAsync(basket);
        }

        return basket;
    }
}