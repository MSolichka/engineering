using EShop.Domain.Enums;

namespace EShop.Application.Order;

public interface IOrderService
{
    Task<List<GetAllOrdersDto>> GetAllOrdersAsync();
    Task<List<GetAllOrdersDto>> GetOrdersByUserIdAsync(Guid userId);
    Task<GetOrderDto> GetOrderByIdAsync(Guid orderId);
    Task CreateOrderAsync(CreateOrderDto createOrderDto);
    public Task ChangeOrderStatus(Guid orderId, Status status);
    public Task ChangeAddress(Guid orderId, string address);
    Task DeleteOrder (Guid orderId);
}