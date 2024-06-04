namespace EShop.Application.User;

public record UserUpdateDto
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
};