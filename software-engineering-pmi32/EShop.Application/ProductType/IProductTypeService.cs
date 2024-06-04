namespace EShop.Application.ProductType;

public interface IProductTypeService
{
    Task<List<Domain.Models.ProductType>> GetProductTypes();
    Task<Domain.Models.ProductType> CreateProductType(CreateProductTypeDto productType);
    Task Delete(Guid id);
}