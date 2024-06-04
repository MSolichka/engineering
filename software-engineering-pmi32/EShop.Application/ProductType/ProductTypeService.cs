using AutoMapper;

namespace EShop.Application.ProductType;

public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductTypeService(IProductTypeRepository productTypeRepository, IMapper mapper)
    {
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
    }
    public async Task<List<Domain.Models.ProductType>> GetProductTypes()
    {
        return await _productTypeRepository.GetAllAsync();
    }

    public async Task<Domain.Models.ProductType> CreateProductType(CreateProductTypeDto productType)
    {
        var newProductType = _mapper.Map<Domain.Models.ProductType>(productType);
        return await _productTypeRepository.CreateAsync(newProductType);
    }

    public async Task Delete(Guid id)
    {
        if (!await _productTypeRepository.AnyAsync(id))
        {
            throw new Exception("Product type not found");
        }
        await _productTypeRepository.DeleteAsync(id);
    }
}