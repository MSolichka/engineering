using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using EShop.Application.Order;
using EShop.Application.Product;
using EShop.Application.ProductType;
using EShop.Application.User;

namespace EShop.Application;

[ExcludeFromCodeCoverage]
public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        CreateMap<CreateProductDto, Domain.Models.Product>()
            .ReverseMap();
        CreateMap<UpdateProductDto, Domain.Models.Product>()
            .ReverseMap();
        CreateMap<CreateProductTypeDto, Domain.Models.ProductType>()
            .ReverseMap();
        CreateMap<CreateOrderDto, Domain.Models.Order>()
            .ReverseMap();
        CreateMap<UserDto, Domain.Models.User>()
            .ReverseMap();
        CreateMap<Domain.Models.Product, GetProductDto>()
            .ReverseMap();
        CreateMap<GetOrderDto, Domain.Models.Order>()
            .ReverseMap();
        CreateMap<Domain.Models.Product, GetProductDto>()
            .AfterMap((product, dto) =>
            {
                dto.LikeCount = product.Likes.Count;
            })
            .ReverseMap();
    }
}