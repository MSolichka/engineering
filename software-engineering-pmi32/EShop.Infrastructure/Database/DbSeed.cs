using Microsoft.AspNetCore.Identity;

namespace EShop.Infrastructure.Database;

public class DbSeed
{
    public static async Task SeedUsers(UserManager<Domain.Models.User> userManager)
    {
        if (await userManager.FindByEmailAsync("admin@mail.com") is not null)
        {
            return;
        }

        var user = new Domain.Models.User
        {
            Email = "admin@mail.com",
            UserName = "admin@mail.com",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "ADMIN@mail.com",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(user, "Admin123_");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}