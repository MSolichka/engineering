using EShop.Domain.Enums;

namespace EShop.Presentation.Models.Order;

public class OrderListViewModel
{
    public Guid Id { get; set; }
    public DateTimeOffset OrderTime { get; set; }
    public string Address { get; set; }
    public decimal TotalPrice { get; set; }
    public int TotalItemsCount { get; set; }
    public Status OrderStatus { get; set; }
}