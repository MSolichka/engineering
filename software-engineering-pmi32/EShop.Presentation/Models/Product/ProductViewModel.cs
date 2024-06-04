namespace EShop.Presentation.Models.Product;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public string ProductType { get; set; }
    public long LikeCount { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
}