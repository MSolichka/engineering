using System.Text;
using System.Text.Encodings.Web;
using EShop.Domain.Models;
using EShop.Presentation.Models;
using EShop.Presentation.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using ILogger = Serilog.ILogger;

namespace EShop.Presentation.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger _logger;
    private readonly IUserStore<User> _userStore;
    private readonly IEmailSender _emailSender;
    private readonly IUserEmailStore<User> _emailStore;

    public AuthController(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        ILogger logger,
        IUserStore<User> userStore,
        IEmailSender emailSender
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _userStore = userStore;
        _emailSender = emailSender;
        _emailStore = (IUserEmailStore<User>)userStore;
    }
    public IActionResult Login()
    {
        _logger.Information("User is on the login page.");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.Error("User entered invalid data.");
            return View();
        }
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            _logger.Information("User logged in.");
            return RedirectToAction("Index", "Product");
        }
        _logger.Error("User entered invalid credentials.");
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View();
    }
    public IActionResult Register()
    {
        _logger.Information("User is on the register page.");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.Error("User entered invalid data.");
            return View();
        }

        var user = new User();
        await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            _logger.Information("User registered.");
            await _userManager.AddToRoleAsync(user, "User");
            _logger.Information("User added to User role.");
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Product");
        }
        _logger.Error("User entered invalid data.");
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.Information("User logged out.");
        return RedirectToAction("Index", "Product");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        // if (!ModelState.IsValid)
        // {
        //     return View();
        // }
        // var user = await _userManager.FindByEmailAsync(model.Email);
        // if (user is null)
        // {
        //     ModelState.AddModelError(string.Empty, "User with email not found.");
        //     return View();
        // }
        // var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        // var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
        // if (result.Succeeded)
        // {
        //     return RedirectToAction("Login");
        // }
        // foreach (var error in result.Errors)
        // {
        //     ModelState.AddModelError(string.Empty, error.Description);
        // }
        // return View();
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Action(action: "ResetPassword", controller: "Auth", values: new {code = code}, protocol: Request.Scheme,
                host: Request.Host.ToString());

            await _emailSender.SendEmailAsync(
                model.Email,
                "Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl ?? throw new InvalidOperationException())}'>clicking here</a>.");

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        return View();
    }
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }
    public IActionResult ResetPassword(string code = null)
    {
        if (code == null)
        {
            return BadRequest("A code must be supplied for password reset.");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
        var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View();
    }

    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
}