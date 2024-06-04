using System.Text;
using AutoMapper;
using EShop.Application.Product;
using EShop.Domain.Specification;
using Microsoft.AspNetCore.Http;

namespace EShop.UnitTests;

public class ProductTests
{
    private readonly ProductService _productServices;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IProductLikeRepository _productLikeRepository;
    public ProductTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _productLikeRepository = Substitute.For<IProductLikeRepository>();
        _productServices = new ProductService(_productRepository, _mapper,_productLikeRepository);
    }
    
    [Fact]
    public async Task GetProducts_ShouldReturnListOfProducts()
    {
        // Arrange
        List<Domain.Models.Product> products = new()
        {
            new() { Id = Guid.NewGuid(), Description = "Product1" },
            new() { Id = Guid.NewGuid(), Description = "Product2" }
        };
        int page = 1;
        int pageSize = 10;
        _productRepository.GetAllBySpecificationAsync(Arg.Any<GetPagedProductsSpecification>()).Returns(Task.FromResult(products));
        
        // Act
        List<GetProductDto> result = await _productServices.GetProducts(page, pageSize);
        
        // Assert
        result.Should().NotBeNull();
        result.Count.Should().BeLessThan(pageSize);
        result.Count.Should().Be(products.Count);
    }
    
    [Fact]
    public async Task CreateProduct_ShouldReturnProduct()
    {
        // Arrange
        var productDto = new CreateProductDto
        {
            Description = "Product1",
            ImgFile = new FormFile(new MemoryStream("This is a dummy file"u8.ToArray()), 0, 0, "ImgFile", "dummy.jpg"),
            Price = 100,
            ProductTypeId = Guid.NewGuid(),
            Quantity = 10
        };
        var product = new Domain.Models.Product { Id = Guid.NewGuid(), Description = productDto.Description };
        _mapper.Map<Domain.Models.Product>(productDto).Returns(product);
        _productRepository.CreateAsync(product).Returns(Task.FromResult(product));
        
        // Act
        var result = await _productServices.CreateProduct(productDto);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(product);
    }
    
    [Fact]
    public async Task UpdateProduct_ShouldReturnProduct()
    {
        // Arrange
        var id = Guid.NewGuid();
        var productDto = new UpdateProductDto
        {
            Description = "Product1",
            ImgFile = new FormFile(new MemoryStream("This is a dummy file"u8.ToArray()), 0, 0, "ImgFile", "dummy.jpg"),
            Price = 100,
            ProductTypeId = Guid.NewGuid(),
            Quantity = 10
        };
        var product = new Domain.Models.Product { Id = id, Description = productDto.Description };
        _productRepository.GetByIdAsync(id)!.Returns(Task.FromResult(product));
        _mapper.Map(productDto, product).Returns(product);
        _productRepository.UpdateAsync(product).Returns(Task.FromResult(product));
        
        // Act
        var result = await _productServices.UpdateProduct(id, productDto);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(product);
    }
    
    [Fact]
    public async Task UpdateProduct_WhenProductNotExists_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var productDto = new UpdateProductDto
        {
            Description = "Product1",
            ImgFile = new FormFile(new MemoryStream("This is a dummy file"u8.ToArray()), 0, 0, "ImgFile", "dummy.jpg"),
            Price = 100,
            ProductTypeId = Guid.NewGuid(),
            Quantity = 10
        };
        _productRepository.GetByIdAsync(Arg.Any<Guid>())!.Returns(Task.FromResult((Domain.Models.Product)null!));
        
        // Act
        Func<Task> act = async () => await _productServices.UpdateProduct(id, productDto);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Product not found");
    }
    
    [Fact]
    public async Task Delete_WhenProductNotExists_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productRepository.AnyAsync(Arg.Any<Guid>()).Returns(Task.FromResult(false));
        
        // Act
        Func<Task> act = async () => await _productServices.DeleteProduct(id);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Product not found");
    }
    
    [Fact]
    public async Task Delete_WhenProductExists_ShouldDeleteProduct()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productRepository.GetByIdAsync(Arg.Any<Guid>())!.Returns(Task.FromResult( new Domain.Models.Product { Id = id, Description = "Product1", Img = "test"}));
        
        // Act
        Func<Task> act = async () => await _productServices.DeleteProduct(id);
        
        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }
    
    [Fact]
    public async Task GetProductById_ShouldReturnProduct()
    {
        // Arrange
        var id = Guid.NewGuid();
        var product = new Domain.Models.Product { Id = id, Description = "Product1", ProductTypeId = Guid.NewGuid(), Baskets = []};
        var resultProduct = new GetProductDto { Id = id, Description = "Product1" };
        _productRepository.GetBySpecificationAsync(Arg.Any<GetProductDetailsSpecification>())!.Returns(Task.FromResult(product));
        _mapper.Map<GetProductDto>(product).Returns(resultProduct);
        // Act
        var result = await _productServices.GetProductById(id);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(resultProduct);
    }
    
    [Fact]
    public async Task GetProductById_WhenProductNotExists_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productRepository.GetByIdAsync(Arg.Any<Guid>())!.Returns(Task.FromResult((Domain.Models.Product)null!));
        
        // Act
        Func<Task> act = async () => await _productServices.GetProductById(id);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Product not found");
    }
    
    [Fact]
    public async Task GetTotalProducts_ShouldReturnTotalProducts()
    {
        // Arrange
        _productRepository.CountAsync().Returns(Task.FromResult(10));
        
        // Act
        var result = await _productServices.GetTotalProducts();
        
        // Assert
        result.Should().Be(10);
    }
    
    [Fact]
    public async Task LikeProduct_ShouldLikeProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var product = new Domain.Models.Product { Id = productId, Description = "Product1" };
        _productRepository.GetByIdAsync(Arg.Any<Guid>())!.Returns(Task.FromResult(product));
        
        // Act
        await _productServices.AddLikeToProduct(productId, userId);
        
        // Assert
        await _productLikeRepository.Received().CreateAsync(Arg.Any<Domain.Models.ProductLike>());
    }
    
    [Fact]
    public async Task LikeProduct_WhenProductNotExists_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        _productRepository.GetByIdAsync(Arg.Any<Guid>())!.Returns(Task.FromResult((Domain.Models.Product)null!));
        
        // Act
        Func<Task> act = async () => await _productServices.AddLikeToProduct(productId, userId);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Product not found");
    }
    
    [Fact]
    public async Task UnlikeProduct_ShouldUnlikeProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var product = new Domain.Models.Product { Id = productId, Description = "Product1" };
        var productLike = new Domain.Models.ProductLike { Id = Guid.NewGuid(), ProductId = productId, UserId = userId };
        _productLikeRepository.GetBySpecificationAsync(Arg.Any<GetProductLikeSpecification>())!.Returns(Task.FromResult(productLike));
        
        // Act
        await _productServices.RemoveLikeFromProduct(productId, userId);
        
        // Assert
        await _productLikeRepository.Received().DeleteAsync(productLike.Id);
    }
    
    [Fact]
    public async Task UnlikeProduct_WhenLikeNotExists_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        _productLikeRepository.GetBySpecificationAsync(Arg.Any<GetProductLikeSpecification>())!.Returns(Task.FromResult((Domain.Models.ProductLike)null!));
        
        // Act
        Func<Task> act = async () => await _productServices.RemoveLikeFromProduct(productId, userId);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Like not found");
    }
}