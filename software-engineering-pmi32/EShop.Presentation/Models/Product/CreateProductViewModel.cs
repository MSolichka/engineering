using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Presentation.Models.Product;

public record CreateProductViewModel
{
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    public string Description { get; set; }
    public Guid ProductTypeId { get; set; }
    [ValidateNever]
    public List<SelectListItem> ProductTypes { get; set; }
}