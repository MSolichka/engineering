using AutoMapper;
using EShop.Application.User;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.UnitTests;

public class UserTests
{
    private readonly UserService _userServices;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public UserTests()
    {
        _mapper = Substitute.For<IMapper>();
        var userStore = Substitute.For<IUserStore<User>>();
        _userManager = Substitute.For<UserManager<User>>(userStore, null, null, null, null, null, null, null, null);
        _userServices = new UserService(_mapper, _userManager);
    }
    
    [Fact]
    public async Task GetUserById_ShouldReturnUser()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), UserName = "User1" };
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        _mapper.Map<UserDto>(user).Returns(new UserDto { Id = user.Id, UserName = user.UserName });
        
        // Act
        var result = await _userServices.GetUserById(user.Id);
        
        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
        result.UserName.Should().Be(user.UserName);
    }
    
    [Fact]
    public async Task GetUserById_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult<User>(null));
        
        // Act
        Func<Task> act = async () => await _userServices.GetUserById(Guid.NewGuid());
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("User not found");
    }
    
    // update
    [Fact]
    public async Task UpdateUser_ShouldUpdateUser()
    {
        // Arrange
        var userDto = new UserUpdateDto { Id = Guid.NewGuid(), Email = "User1" };
        var user = new User { Id = userDto.Id, UserName = "User1" };
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        
        // Act
        await _userServices.UpdateUser(userDto);
        
        // Assert
        await _userManager.Received().SetEmailAsync(user, userDto.Email);
        await _userManager.Received().SetUserNameAsync(user, userDto.Email);
    }
    
    [Fact]
    public async Task UpdateUser_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult<User>(null));
        
        // Act
        Func<Task> act = async () => await _userServices.UpdateUser(new UserUpdateDto());
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("User not found");
    }
    
    [Fact]
    public async Task UpdateUser_ShouldThrowException_WhenPasswordIsNotValid()
    {
        // Arrange
        var userDto = new UserUpdateDto { Id = Guid.NewGuid(), Password = "User1" };
        var user = new User { Id = userDto.Id, UserName = "User1" };
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        _userManager.GeneratePasswordResetTokenAsync(user).Returns(Task.FromResult("token"));
        _userManager.ResetPasswordAsync(user, "token", userDto.Password).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Error" })));
        
        // Act
        Func<Task> act = async () => await _userServices.UpdateUser(userDto);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Failed to change password. Error");
    }
    
    [Fact]
    public async Task UpdateUser_ShouldNotUpdatePassword_WhenPasswordIsEmpty()
    {
        // Arrange
        var userDto = new UserUpdateDto { Id = Guid.NewGuid() };
        var user = new User { Id = userDto.Id, UserName = "User1" };
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        
        // Act
        await _userServices.UpdateUser(userDto);
        
        // Assert
        await _userManager.DidNotReceive().GeneratePasswordResetTokenAsync(user);
        await _userManager.DidNotReceive().ResetPasswordAsync(user, Arg.Any<string>(), Arg.Any<string>());
    }
    
    //delete
    [Fact]
    public async Task DeleteUser_ShouldDeleteUser()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), UserName = "User1" };
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        _userManager.DeleteAsync(user).Returns(Task.FromResult(IdentityResult.Success));
        
        // Act
        await _userServices.DeleteUser(user.Id);
        
        // Assert
        await _userManager.Received().DeleteAsync(user);
    }
    
    [Fact]
    public async Task DeleteUser_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult<User>(null));
        
        // Act
        Func<Task> act = async () => await _userServices.DeleteUser(Guid.NewGuid());
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("User not found");
    }
    
    [Fact]
    public async Task DeleteUser_ShouldThrowException_WhenFailedToDeleteUser()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), UserName = "User1" };
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        _userManager.DeleteAsync(user).Returns(Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Error" })));
        
        // Act
        Func<Task> act = async () => await _userServices.DeleteUser(user.Id);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Failed to delete user");
    }
    
    [Fact]
    public async Task ConfirmUserAsync_ShouldConfirmUser()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), UserName = "User1" };
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        _userManager.GenerateEmailConfirmationTokenAsync(user).Returns(Task.FromResult("token"));
        _userManager.ConfirmEmailAsync(user, "token").Returns(Task.FromResult(IdentityResult.Success));
        
        // Act
        await _userServices.ConfirmUserAsync(user.Id);
        
        // Assert
        await _userManager.Received().ConfirmEmailAsync(user, "token");
    }
    
    [Fact]
    public async Task ConfirmUserAsync_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult<User>(null));
        
        // Act
        Func<Task> act = async () => await _userServices.ConfirmUserAsync(Guid.NewGuid());
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("User not found");
    }
    
    [Fact]
    public async Task ConfirmUserAsync_ShouldThrowException_WhenFailedToConfirmUser()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), UserName = "User1" };
        _userManager.FindByIdAsync(Arg.Any<string>())!.Returns(Task.FromResult(user));
        _userManager.GenerateEmailConfirmationTokenAsync(user).Returns(Task.FromResult("token"));
        _userManager.ConfirmEmailAsync(user, "token").Returns(Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Error" })));
        
        // Act
        Func<Task> act = async () => await _userServices.ConfirmUserAsync(user.Id);
        
        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Failed to confirm user");
    }
}