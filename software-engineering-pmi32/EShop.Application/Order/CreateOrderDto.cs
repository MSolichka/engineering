namespace EShop.Application.Order;

public record CreateOrderDto
{
    public Guid BasketId { get; set; }
    public string Address { get; set; } = null!;
    public Guid UserId { get; set; }
};