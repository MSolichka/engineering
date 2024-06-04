using System.Runtime.InteropServices.JavaScript;
using EShop.Application.Basket;
using EShop.Application.Product;
using EShop.Domain.Models;
using EShop.Domain.Specification;
using Microsoft.AspNetCore.Identity;

namespace EShop.UnitTests;

public class BasketTests
{
    private readonly BasketService _basketServices;
    private readonly IProductRepository _productRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly UserManager<User> _userManager;
    public BasketTests()
    {
        _basketRepository = Substitute.For<IBasketRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        var userStore = Substitute.For<IUserStore<User>>();
        _userManager = Substitute.For<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);
        _basketServices = new BasketService(_basketRepository, _productRepository,_userManager);
    }
    
    [Fact]
    public async Task GetUserBasket_ShouldReturnBasket()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId };
        var basket = new Basket { UserId = userId };  
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByUserSpecification>())!.Returns(Task.FromResult(basket));
        
        // Act
        var result = await _basketServices.GetUserBasket(userId);
        
        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
    }
    
    [Fact]
    public async Task GetUserBasket_WhenUserNotFound_ShouldThrowException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult<User>(null));
        
        // Act
        Func<Task> act = async () => await _basketServices.GetUserBasket(userId);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("User not found");
    }
    
    [Fact]
    public async Task AddItemToBasket_ShouldReturnBasket()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var price = 100;
        var basket = new Basket { UserId = userId, Items = new List<BasketItem>() };
        _productRepository.AnyAsync(itemId).Returns(Task.FromResult(true));
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByUserSpecification>())!.Returns(Task.FromResult(basket));
        _basketRepository.UpdateAsync(Arg.Any<Basket>())!.Returns(Task.FromResult(basket));
        
        // Act
        var result = await _basketServices.AddItemToBasket(userId, itemId, price);
        
        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.Items.Count.Should().Be(1);
        result.Items.First().ProductId.Should().Be(itemId);
        result.Items.First().Quantity.Should().Be(1);
        result.TotalPrice.Should().Be(price);
    }
    
    [Fact]
    public async Task AddItemToBasket_WhenProductNotExists_ShouldThrowException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var price = 100;
        var basket = new Basket { UserId = userId, Items = new List<BasketItem>() };
        _productRepository.AnyAsync(itemId).Returns(Task.FromResult(false));
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByUserSpecification>())!.Returns(Task.FromResult(basket));
        
        // Act
        Func<Task> act = async () => await _basketServices.AddItemToBasket(userId, itemId, price);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Product not found");
    }
    
    [Fact]
    public async Task AddItemToBasket_WhenItemExists_ShouldIncreaseQuantity()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var price = 100;
        var basket = new Basket { UserId = userId, TotalPrice = price, Items = new List<BasketItem> { new BasketItem { ProductId = itemId, Quantity = 1 } } };
        _productRepository.AnyAsync(itemId).Returns(Task.FromResult(true));
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByUserSpecification>())!.Returns(Task.FromResult(basket));
        _basketRepository.UpdateAsync(Arg.Any<Basket>())!.Returns(Task.FromResult(basket));
        
        // Act
        var result = await _basketServices.AddItemToBasket(userId, itemId, price);
        
        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        result.Items.Count.Should().Be(1);
        result.Items.First().ProductId.Should().Be(itemId);
        result.Items.First().Quantity.Should().Be(2);
        result.TotalPrice.Should().Be(200);
    }
    
    //delete
    [Fact]
    public async Task DeleteBasket_ShouldDeleteBasket()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var basket = new Basket { Id = basketId, Items = [], TotalPrice = 0};
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByIdSpecification>())!.Returns(Task.FromResult(basket));
        
        // Act
        Func<Task> act = async () => await _basketServices.ClearBasketAsync(basketId);
        
        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }
    
    [Fact]
    public async Task DeleteBasket_WhenBasketNotExists_ShouldThrowException()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        _basketRepository.AnyAsync(basketId).Returns(Task.FromResult(false));
        
        // Act
        Func<Task> act = async () => await _basketServices.ClearBasketAsync(basketId);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Basket not found");
    }
    
    [Fact]
    public async Task ProceedBasket_ShouldProceedBasket()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var basket = new Basket { Id = basketId, Items = new List<BasketItem> { new BasketItem { Product = new Product { Price = 100 }, Quantity = 2 } } };
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByIdSpecification>())!.Returns(Task.FromResult(basket));
        _basketRepository.UpdateAsync(Arg.Any<Basket>())!.Returns(Task.FromResult(basket));
        
        // Act
        Func<Task> act = async () =>  await _basketServices.ProceedBasket(basketId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task ProceedBasket_WhenBasketNotExists_ShouldThrowException()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        _basketRepository.AnyAsync(basketId).Returns(Task.FromResult(false));
        
        // Act
        Func<Task> act = async () => await _basketServices.ProceedBasket(basketId);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Basket not found");
    }
    
    [Fact]
    public async Task ProceedBasket_WhenBasketIsEmpty_ShouldThrowException()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var basket = new Basket { Id = basketId, Items = new List<BasketItem>() };
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByIdSpecification>())!.Returns(Task.FromResult(basket));
        
        // Act
        Func<Task> act = async () => await _basketServices.ProceedBasket(basketId);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Nothing to proceed");
    }
    
    [Fact]
    public async Task RemoveItemFromBasket_ShouldRemoveItem()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var basket = new Basket { Id = basketId, Items = new List<BasketItem> { new BasketItem { Id = itemId } } };
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByIdSpecification>())!.Returns(Task.FromResult(basket));
        
        // Act
        Func<Task> act = async () => await _basketServices.RemoveItemFromBasket(basketId, itemId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task RemoveItemFromBasket_WhenBasketNotExists_ShouldThrowException()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        _basketRepository.AnyAsync(basketId).Returns(Task.FromResult(false));
        
        // Act
        Func<Task> act = async () => await _basketServices.RemoveItemFromBasket(basketId, itemId);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Basket not found");
    }
    
    [Fact]
    public async Task RemoveItemFromBasket_WhenItemNotExists_ShouldThrowException()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var basket = new Basket { Id = basketId, Items = new List<BasketItem>() };
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByIdSpecification>())!.Returns(Task.FromResult(basket));
        
        // Act
        Func<Task> act = async () => await _basketServices.RemoveItemFromBasket(basketId, itemId);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Item not found");
    }
    
    [Fact]
    public async Task SetQuantities_ShouldSetQuantities()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        // var quantities = new Dictionary<Guid, int> { { Guid.NewGuid(), 2 } };
        var product = new Product { Id = Guid.NewGuid(), Price = 100 };
        var basket = new Basket { Id = basketId, Items = new List<BasketItem> { new BasketItem { Id = itemId, Quantity = 1, ProductId = product.Id, Product = product} } };
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByIdSpecification>())!.Returns(Task.FromResult(basket));
        
        // Act
        var result =  await _basketServices.SetQuantities(basketId, itemId, 5);
        // Assert
        result.Should().NotBeNull();
        result.Items.First().Quantity.Should().Be(5);
    }
    
    [Fact]
    public async Task SetQuantities_WhenBasketNotExists_ShouldThrowException()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        _basketRepository.AnyAsync(basketId).Returns(Task.FromResult(false));
        
        // Act
        Func<Task> act = async () => await _basketServices.SetQuantities(basketId, itemId, 5);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Basket not found");
    }
    
    [Fact]
    public async Task SetQuantities_WhenItemNotExists_ShouldThrowException()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var basket = new Basket { Id = basketId, Items = new List<BasketItem>() };
        _basketRepository.GetBySpecificationAsync(Arg.Any<GetBasketByIdSpecification>())!.Returns(Task.FromResult(basket));
        
        // Act
        Func<Task> act = async () => await _basketServices.SetQuantities(basketId, itemId, 5);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Item not found");
    }
}