using AutoMapper;
using EShop.Application.ProductType;

namespace EShop.UnitTests;

public class ProductTypeTests
{
    private readonly ProductTypeService _productTypeServices;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMapper _mapper;
    public ProductTypeTests()
    {
        _productTypeRepository = Substitute.For<IProductTypeRepository>();
        _mapper = Substitute.For<IMapper>();
        _productTypeServices = new ProductTypeService(_productTypeRepository, _mapper);
    }
    
    [Fact]
    public async Task GetProductTypes_ShouldReturnListOfProductTypes()
    {
        // Arrange
        List<Domain.Models.ProductType> productTypes = new()
        {
            new() { Id = Guid.NewGuid(), Name = "ProductType1" },
            new() { Id = Guid.NewGuid(), Name = "ProductType2" }
        };
        _productTypeRepository.GetAllAsync().Returns(Task.FromResult(productTypes));
        
        // Act
        List<Domain.Models.ProductType> result = await _productTypeServices.GetProductTypes();
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(productTypes);
    }
    
    [Fact]
    public async Task CreateProductType_ShouldReturnProductType()
    {
        // Arrange
        var productTypeDto = new CreateProductTypeDto ("ProductType1");
        var productType = new Domain.Models.ProductType { Id = Guid.NewGuid(), Name = productTypeDto.Name };
        _mapper.Map<Domain.Models.ProductType>(productTypeDto).Returns(productType);
        _productTypeRepository.CreateAsync(productType).Returns(Task.FromResult(productType));
        
        // Act
        var result = await _productTypeServices.CreateProductType(productTypeDto);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(productType);
    }
    
    [Fact]
    public async Task Delete_WhenProductTypeNotExists_ShouldThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productTypeRepository.AnyAsync(Arg.Any<Guid>()).Returns(Task.FromResult(false));
        
        // Act
        Func<Task> act = async () => await _productTypeServices.Delete(id);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Product type not found");
    }
    
    [Fact]
    public async Task Delete_WhenProductTypeExists_ShouldDeleteProductType()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productTypeRepository.AnyAsync(Arg.Any<Guid>()).Returns(Task.FromResult(true));
        
        // Act
        await _productTypeServices.Delete(id);
        
        // Assert
        await _productTypeRepository.Received(1).DeleteAsync(id);
    }
}