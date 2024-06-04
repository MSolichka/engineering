using AutoMapper;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.User;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Models.User> _userManager;

    public UserService( IMapper mapper, UserManager<Domain.Models.User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userRolesDictionary = new Dictionary<Guid, string>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRolesDictionary.Add(user.Id, roles.First() );
        }
        var result = users.ConvertAll(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName ?? throw new InvalidOperationException(),
            Email = u.Email ?? throw new InvalidOperationException(),
            UserType = userRolesDictionary[u.Id],
            IsConfirmed = u.EmailConfirmed
        });
        return result;
    }

    public async Task UpdateUser(UserUpdateDto userDto)
    {
        var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!string.IsNullOrEmpty(userDto.Email))
        {
            await _userManager.SetEmailAsync(user, userDto.Email);
            await _userManager.SetUserNameAsync(user, userDto.Email);
        }

        if (!string.IsNullOrEmpty(userDto.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, userDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to change password. " + String.Join('\n',result.Errors.Select(e => e.Description)));
            }
        }
    }

    public async Task ConfirmUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            throw new Exception("User not found");
        }
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var result = await _userManager.ConfirmEmailAsync(user, token) ;
        if (!result.Succeeded)
        {
            throw new Exception("Failed to confirm user");
        }
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to delete user");
        }
    }
}