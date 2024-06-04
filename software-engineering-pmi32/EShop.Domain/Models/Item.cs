using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class Item
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public Guid TypeId { get; set; }
    
    [JsonIgnore]
    public ProductType Type { get; set; }
    [JsonIgnore]
    public ICollection<Basket> Baskets { get; set; }
}