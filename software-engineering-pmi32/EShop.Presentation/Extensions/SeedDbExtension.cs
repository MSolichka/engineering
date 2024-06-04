using EShop.Domain.Models;
using EShop.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace EShop.Presentation.Extensions;

public static class SeedDbExtension
{
    public static async Task SeedDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        await DbSeed.SeedUsers(userManager);
    }
}