using System.Security.Claims;
using EShop.Application.Product;
using EShop.Application.ProductType;
using EShop.Presentation.Models;
using EShop.Presentation.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Presentation.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductTypeService _productTypeService;

    public ProductController(IProductService productService, IProductTypeService productTypeService)
    {
        _productService = productService;
        _productTypeService = productTypeService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var products = await _productService.GetProducts(page,pageSize);
        var productCount = await _productService.GetTotalProducts();
        var viewModel = new ProductListVewModel
        {
            Products = products,
            PageNumber = page,
            PageSize = pageSize,
            TotalItems = productCount
        };
        return View(viewModel);
    }

    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _productService.GetProductById(id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var productVm = new ProductViewModel
        {
            Id = product.Id,
            Price = product.Price,
            Quantity = product.Quantity,
            Description = product.Description,
            Img = product.Img,
            ProductType = product.Type.Name,
            LikeCount = product.LikeCount,
            IsLikedByCurrentUser = product.Likes.Any(pl => pl.UserId == new Guid(userId))
        };
        return View(productVm);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var model = new CreateProductViewModel();
        var productTypes = await _productTypeService.GetProductTypes();
        model.ProductTypes = productTypes.ConvertAll(pt => new SelectListItem
        {
            Value = pt.Id.ToString(),
            Text = pt.Name
        });

        return View(model);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Create));
        }

        var img = HttpContext.Request.Form.Files[0];
        var productDto = new CreateProductDto
        {
            Price = model.Price,
            Quantity = model.Quantity,
            Description = model.Description,
            ProductTypeId = model.ProductTypeId,
            ImgFile = img
        };
        await _productService.CreateProduct(productDto);
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id)
    {
        var product = await _productService.GetProductById(id);
        var model = new UpdateProductViewModel
        {
            Id = product.Id,
            Price = product.Price,
            Quantity = product.Quantity,
            Description = product.Description,
            ProductTypeId = product.Type.Id
        };
        var productTypes = await _productTypeService.GetProductTypes();
        model.ProductTypes = productTypes.ConvertAll(pt => new SelectListItem
        {
            Value = pt.Id.ToString(),
            Text = pt.Name
        });

        return View(model);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(UpdateProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Update));
        }
        var productDto = new UpdateProductDto
        {
            Id = model.Id,
            Price = model.Price,
            Quantity = model.Quantity,
            Description = model.Description,
            ProductTypeId = model.ProductTypeId
        };
        var img = HttpContext.Request.Form.Files.FirstOrDefault();
        if (img is not null)
        {
            productDto.ImgFile = img;
        }
        await _productService.UpdateProduct(model.Id, productDto);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteProduct(id);
        return RedirectToAction("Index");
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddLike(Guid productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        // Call a method in the ProductService to add the like
        await _productService.AddLikeToProduct(productId, new Guid(userId));
        // Redirect back to the product details page
        return RedirectToAction("GetProduct", new { id = productId });
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Unlike(Guid productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        await _productService.RemoveLikeFromProduct(productId, new Guid(userId));

        return RedirectToAction("GetProduct", new { id = productId });
    }
}