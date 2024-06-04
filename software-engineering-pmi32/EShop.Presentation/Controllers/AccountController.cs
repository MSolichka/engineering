using System.Security.Claims;
using EShop.Domain.Models;
using EShop.Presentation.Models;
using EShop.Presentation.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace EShop.Presentation.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger _logger;
    private readonly SignInManager<User> _signInManager;

    public AccountController(
        UserManager<User> userManager,
        ILogger logger,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
    }
    public async Task<IActionResult> Index()
    {
        var user = await  _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        var model = await GetAccountIndexViewModel(user);
        _logger.Information("User {0} visited the account page", user.UserName);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Index(AccountIndexViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.Error("Unable to load user with ID '{0}'", _userManager.GetUserId(User));
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        if (!ModelState.IsValid)
        {
            _logger.Error("Model state is invalid");
            return View(await GetAccountIndexViewModel(user));
        }
        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (model.PhoneNumber != phoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                _logger.Error("Unexpected error occurred setting phone number for user with ID '{0}'", _userManager.GetUserId(User));
                var userId = await _userManager.GetUserIdAsync(user);
                throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
            }
        }
        _logger.Information("User {0} updated their profile", user.UserName);
        model.StatusMessage = "Your profile has been updated";
        return View(model);
    }
    public async Task<IActionResult> ChangePassword()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.Error("Unable to load user with ID '{0}'", _userManager.GetUserId(User));
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.Error("Model state is invalid");
            return View();
        }
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.Error("Unable to load user with ID '{0}'", _userManager.GetUserId(User));
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            _logger.Error("Error changing password for user with ID '{0}'", _userManager.GetUserId(User));
            return View();
        }
        await _signInManager.RefreshSignInAsync(user);
        _logger.Information("User {0} changed their password successfully", user.UserName);
        model.StatusMessage = "Your password has been changed";
        return View();
    }
    public async Task<IActionResult> ChangeEmail()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.Error("Unable to load user with ID '{0}'", _userManager.GetUserId(User));
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        var model = await GetChangeEmailViewModel(user);
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.Error("Model state is invalid");
            return View();
        }
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            _logger.Error("Unable to load user with ID '{0}'", _userManager.GetUserId(User));
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        var email = await _userManager.GetEmailAsync(user);
        var role = User.FindFirst(ClaimTypes.Role);
        if (model.NewEmail != email && role?.Value != "Admin")
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, model.NewEmail);
            var setUserNameResult = await _userManager.SetUserNameAsync(user, model.NewEmail);
            if (!setEmailResult.Succeeded || !setUserNameResult.Succeeded)
            {
                _logger.Error("Unexpected error occurred setting email for user with ID '{0}'", _userManager.GetUserId(User));
                var userId = await _userManager.GetUserIdAsync(user);
                throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
            }
            model.Email = setEmailResult.Succeeded ? model.NewEmail : email;
            model.StatusMessage = "Email successfully changed.";
            _logger.Information("User {0} changed their email successfully", user.UserName);
            return View(model);
        }
        model.StatusMessage = "Your email is unchanged.";
        model.Email = email;
        return View(model);
    }
    private async Task<AccountIndexViewModel> GetAccountIndexViewModel(User user)
    {
        var userName = await _userManager.GetUserNameAsync(user);
        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        var model = new AccountIndexViewModel
        {
            Username = userName,
            PhoneNumber = phoneNumber
        };
        return model;
    }
    private async Task<ChangeEmailViewModel> GetChangeEmailViewModel(User user)
    {
        var email = await _userManager.GetEmailAsync(user);
        var model = new ChangeEmailViewModel
        {
            Email = email,
            NewEmail = email
        };
        return model;
    }
}