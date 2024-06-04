namespace EShop.Application.User;

public interface IUserService
{
    Task<UserDto> GetUserById(Guid id);
    Task<List<UserDto>> GetAllUsersAsync();
    Task UpdateUser(UserUpdateDto userDto);
    Task ConfirmUserAsync(Guid id);
    Task DeleteUser(Guid id);
}