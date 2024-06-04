namespace EShop.Application.Product;

public record UpdateProductDto: CreateProductDto
{
    public Guid Id { get; set; }
}