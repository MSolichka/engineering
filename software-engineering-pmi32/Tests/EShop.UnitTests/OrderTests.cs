using AutoMapper;
using EShop.Application.Basket;
using EShop.Application.Order;
using EShop.Domain.Enums;
using EShop.Domain.Models;
using EShop.Domain.Specification;
using Microsoft.AspNetCore.Identity;

namespace EShop.UnitTests;

public class OrderTests
{
    private readonly OrderService _orderServices;
    private readonly IOrderRepository _orderRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public OrderTests()
    {
        _basketRepository = Substitute.For<IBasketRepository>();
        _orderRepository = Substitute.For<IOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        var userStore = Substitute.For<IUserStore<User>>();
        _userManager = Substitute.For<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);
        _orderServices = new OrderService(_orderRepository, _userManager, _basketRepository, _mapper);
    }
    
    [Fact]
    public async Task GetOrders_ShouldReturnListOfOrders()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        List<Order> orders =
        [
            new()
            {
                Id = Guid.NewGuid(), 
                OrderTime = DateTimeOffset.Now,
                Address = "Address",
                UserId = Guid.NewGuid(),
                User = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Name",
                    Email = "Email",
                    PasswordHash = "Password",
                },
                Status = Status.Confirmed,
                BasketId = basketId,
                Basket = new Basket
                {
                    Id = basketId,
                    Items = []
                }
            },
        ];
        var ordersDto = orders.ConvertAll(o => new GetAllOrdersDto
        {
            OrderTime = o.OrderTime,
            Address = o.Address,
            Id = o.Id,
            Status = o.Status,
            TotalItemCount = o.Basket.Items.Sum(i => i.Quantity),
            TotalPrice = o.Basket.TotalPrice
        });
        _orderRepository.GetAllBySpecificationAsync(Arg.Any<GetAllOrdersSpecification>()).Returns(Task.FromResult(orders));
        
        // Act
        List<GetAllOrdersDto> result = await _orderServices.GetAllOrdersAsync();
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(ordersDto);
    }
    
    [Fact]
    public async Task GetOrdersByUserId_ShouldReturnListOfOrders()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var basketId = Guid.NewGuid();
        List<Order> orders =
        [
            new()
            {
                Id = Guid.NewGuid(),
                BasketId = basketId, 
                OrderTime = DateTimeOffset.Now, Basket = new Basket
                {
                    Id = basketId,
                    Items = []
                }
            },
        ];
        _orderRepository.GetAllBySpecificationAsync(Arg.Any<GetUserOrdersSpecification>()).Returns(Task.FromResult(orders));
        
        // Act
        List<GetAllOrdersDto> result = await _orderServices.GetOrdersByUserIdAsync(userId);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(orders.Count);
    }
    
    
    [Fact]
    public async Task CreateOrder_ShouldReturnOrder()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var orderDto = new CreateOrderDto
        {
            BasketId = Guid.NewGuid(),
            UserId =userId
        };
        var user = new User { Id = userId, UserName = "Name", Email = "Email", PasswordHash = "Password" };
        var order = new Order { Id = Guid.NewGuid(), OrderTime = DateTimeOffset.Now };
        _mapper.Map<Order>(orderDto).Returns(order);
        _orderRepository.CreateAsync(order).Returns(Task.FromResult(order));
        _basketRepository.AnyAsync(Arg.Any<Guid>()).Returns(Task.FromResult(true));
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        
        // Act
        Func<Task> act = async () =>  await _orderServices.CreateOrderAsync(orderDto);
        
        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }
    
    
    [Fact]
    public async Task Delete_WhenOrderNotExists_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _orderRepository.AnyAsync(Arg.Any<Guid>()).Returns(Task.FromResult(false));
        
        // Act
        Func<Task> act = async () => await _orderServices.DeleteOrder(id);
        
        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Delete_WhenOrderExists_ShouldNotThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _orderRepository.AnyAsync(Arg.Any<Guid>()).Returns(Task.FromResult(true));

        // Act
        Func<Task> act = async () => await _orderServices.DeleteOrder(id);

        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }
    
    [Fact]
    public async Task ChangeOrderStatus_WhenOrderNotExists_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _orderRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult((Order)null));
        
        // Act
        Func<Task> act = async () => await _orderServices.ChangeOrderStatus(id, Status.Confirmed);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Order not found");
    }
    
    [Fact]
    public async Task ChangeOrderStatus_WhenOrderExists_ShouldNotThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var order = new Order { Id = id };
        _orderRepository.GetByIdAsync(Arg.Any<Guid>())!.Returns(Task.FromResult(order));
        
        // Act
        Func<Task> act = async () => await _orderServices.ChangeOrderStatus(id, Status.Confirmed);
        
        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }
    
    [Fact]
    public async Task ChangeAddress_WhenOrderNotExists_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _orderRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult((Order)null));
        
        // Act
        Func<Task> act = async () => await _orderServices.ChangeAddress(id, "Address");
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Order not found");
    }
    
    [Fact]
    public async Task ChangeAddress_WhenOrderExists_ShouldNotThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var order = new Order { Id = id };
        _orderRepository.GetByIdAsync(Arg.Any<Guid>())!.Returns(Task.FromResult(order));
        
        // Act
        Func<Task> act = async () => await _orderServices.ChangeAddress(id, "Address");
        
        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }
    
    [Fact]
    public async Task GetOrderByIdAsync_WhenOrderNotExists_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _orderRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult((Order)null));
        
        // Act
        Func<Task> act = async () => await _orderServices.GetOrderByIdAsync(id);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Order not found");
    }
    
    [Fact]
    public async Task GetOrderByIdAsync_WhenOrderExists_ShouldNotThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var order = new Order { Id = id };
        var orderDto = new GetOrderDto { Id = id };
        _orderRepository.GetByIdAsync(Arg.Any<Guid>())!.Returns(Task.FromResult(order));
        _mapper.Map<GetOrderDto>(order).Returns(orderDto);
        
        // Act
        var result = await _orderServices.GetOrderByIdAsync(id);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(orderDto);
    }
    
}