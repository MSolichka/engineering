using EShop.Domain.Models;

namespace EShop.Presentation.Models.Basket;

public record BasketViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public IEnumerable<BasketItem> Items { get; set; }
};