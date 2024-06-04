using Microsoft.AspNetCore.Http;

namespace EShop.Application.Product;

public record CreateProductDto
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = null!;
    public IFormFile? ImgFile { get; set; }
    public Guid ProductTypeId { get; set; }
};