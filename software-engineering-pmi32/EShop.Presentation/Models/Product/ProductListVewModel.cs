using EShop.Application.Product;

namespace EShop.Presentation.Models.Product;

public class ProductListVewModel
{
    public List<GetProductDto> Products { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
}