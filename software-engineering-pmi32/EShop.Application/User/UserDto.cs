using EShop.Domain.Models;

namespace EShop.Application.User;

public record UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserType { get; set; } = null!;
    public bool IsConfirmed { get; set; }
};