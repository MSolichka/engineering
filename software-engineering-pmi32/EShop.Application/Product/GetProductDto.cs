using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EShop.Application.Product;

public class GetProductDto
{
    public Guid Id { get; set; }
    public decimal Price{ get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = null!;
    public string Img { get; set; } = null!;
    public long LikeCount { get; set; }
    public Domain.Models.ProductType Type { get; set; } = null!;
    public List<ProductLike> Likes { get; set; }
    
}