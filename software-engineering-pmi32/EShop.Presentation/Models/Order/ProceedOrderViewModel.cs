namespace EShop.Presentation.Models.Order;

public record ProceedOrderViewModel
{
    public Guid BasketId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public string Address { get; set; }
};