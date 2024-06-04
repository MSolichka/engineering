using AutoMapper;
using EShop.Domain.Models;
using EShop.Domain.Specification;

namespace EShop.Application.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IProductLikeRepository _likeRepository;
    private readonly string _uploadPath = Path.Combine("wwwroot","images", "products");
    public ProductService(IProductRepository repository, IMapper mapper, IProductLikeRepository likeRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _likeRepository = likeRepository;
    }
    
    public async Task<GetProductDto> GetProductById(Guid id)
    {
        var product = await _repository.GetBySpecificationAsync(new GetProductDetailsSpecification(id));
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        
        var productDto = _mapper.Map<GetProductDto>(product);
        return productDto;
    }

    public async Task<List<GetProductDto>> GetProducts(int page, int pageSize)
    {
        var products =  await _repository.GetAllBySpecificationAsync(new GetPagedProductsSpecification(page, pageSize));
        var productsDto = products.ConvertAll(p => new GetProductDto
        {
            Id = p.Id,
            Price = p.Price,
            Quantity = p.Quantity,
            Description = p.Description,
            Img = p.Img,
            LikeCount = p.Likes.Count,
            Type = p.Type
        });
        
        return productsDto;
    }
    
    public async Task<Domain.Models.Product> CreateProduct(CreateProductDto product)
    {
        var newProduct = _mapper.Map<Domain.Models.Product>(product);
        //file upload
        await UploadImage(product, newProduct);
        
        return await _repository.CreateAsync(newProduct);
    }

    public async Task<Domain.Models.Product> UpdateProduct(Guid id, UpdateProductDto product)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            throw new Exception("Product not found");
        }
        var updatedProduct = _mapper.Map(product, existingProduct);
        if(product.ImgFile is not null)
        {
            await UploadImage(product, updatedProduct);
        }
        await _repository.UpdateAsync(updatedProduct);
        return updatedProduct;
    }

    public Task<int> GetTotalProducts()
    {
        return _repository.CountAsync();
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        await _repository.DeleteAsync(id);
        DeleteProductImage(product);
        return true;
    }

    public async Task AddLikeToProduct(Guid productId, Guid userId)
    {
        var product = await _repository.GetByIdAsync(productId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        await _likeRepository.CreateAsync(new ProductLike
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            UserId = userId
        });
    }

    public async Task RemoveLikeFromProduct(Guid productId, Guid userId)
    {
        var like = await _likeRepository.GetBySpecificationAsync(new GetProductLikeSpecification(productId, userId));
        if (like == null)
        {
            throw new Exception("Like not found");
        }
        await _likeRepository.DeleteAsync(like.Id);
    }

    private async Task UploadImage(CreateProductDto product, Domain.Models.Product newProduct)
    {
        if (product.ImgFile is not null)
        {
            var fileName = $"{Guid.NewGuid()}_{product.ImgFile?.FileName}";
            var filePath = Path.Combine(_uploadPath, fileName);
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
            await using (var fileStream = new FileStream(Path.Combine(filePath), FileMode.Create, FileAccess.Write))
            {
                await product.ImgFile?.CopyToAsync(fileStream)!;
            }
            newProduct.Img = fileName;
        }
    }
    
    private void DeleteProductImage(Domain.Models.Product product)
    {
        var imgPath = Path.Combine(_uploadPath, product.Img);
        if (File.Exists(imgPath))
        {
            File.Delete(imgPath);
        }
    }
}