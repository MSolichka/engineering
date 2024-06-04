using EShop.Application.Basket;
using EShop.Application.Order;
using EShop.Application.Product;
using EShop.Application.ProductType;
using EShop.Infrastructure.Basket;
using EShop.Infrastructure.Database;
using EShop.Infrastructure.Order;
using EShop.Infrastructure.Product;
using EShop.Infrastructure.ProductType;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IProductLikeRepository, ProductLikeRepository>();
        return services;
    }
}