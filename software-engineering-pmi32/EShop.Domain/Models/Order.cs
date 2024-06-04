using EShop.Domain.Enums;

namespace EShop.Domain.Models;

public class Order : IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public DateTimeOffset OrderTime { get; set; }
    public Status Status { get; set; }
    public string Address { get; set; } = null!;
    public Guid BasketId { get; set; }
    public Guid UserId { get; set; }
    public Basket Basket { get; set; } = null!;
    public User User { get; set; } = null!;
}