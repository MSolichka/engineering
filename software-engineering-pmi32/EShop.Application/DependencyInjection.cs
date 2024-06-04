using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using EShop.Application.Basket;
using EShop.Application.Order;
using EShop.Application.Product;
using EShop.Application.ProductType;
using EShop.Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(DependencyInjection)));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductTypeService, ProductTypeService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBasketService, BasketService>();
        return services;
    }  
}