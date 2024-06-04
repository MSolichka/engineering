using EShop.Application.User;
using EShop.Domain.Models;using EShop.Presentation.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;

    public UserController(IUserService userService, UserManager<User> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsersAsync();

        var usersVm = users.ConvertAll(u => new UserListViewModel
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            UserType = u.UserType,
            IsConfirmed = u.IsConfirmed
        });
        return View(usersVm);
    }
    public async Task<IActionResult> Edit(Guid Id)
    {
        var user = await _userService.GetUserById(Id);
        var userVm = new UserUpdateViewModel
        {
            Id = user.Id,
            Email = user.Email,
        };
        return View(userVm);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(UserUpdateViewModel userUpdateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userUpdateViewModel);
        }
        var userDto = new UserUpdateDto
        {
            Id = userUpdateViewModel.Id,
            Email = userUpdateViewModel.Email,
            Password = userUpdateViewModel.Password
        };
        try
        {
            await _userService.UpdateUser(userDto);
        } catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(userUpdateViewModel);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteUser(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ConfirmUser(Guid id)
    {
        await _userService.ConfirmUserAsync(id);
        return RedirectToAction("Index");
    }
}