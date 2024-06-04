namespace EShop.Presentation.Models.Product;

public record UpdateProductViewModel : CreateProductViewModel
{
    public Guid Id { get; set; }
};