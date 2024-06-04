using EShop.Domain.Enums;

namespace EShop.Application.Order;

public record GetAllOrdersDto
{
    public Guid Id { get; set; }
    public DateTimeOffset OrderTime { get; set; }
    public string Address { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public int TotalItemCount { get; set; }
    public Status Status { get; set; }
};