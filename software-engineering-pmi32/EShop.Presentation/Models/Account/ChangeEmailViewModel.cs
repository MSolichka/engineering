using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Models.Account;

public record ChangeEmailViewModel
{
    public string Email { get; set; }
    [TempData]
    public string StatusMessage { get; set; }
    [Microsoft.Build.Framework.Required]
    [EmailAddress]
    [Display(Name = "New email")]
    public string NewEmail { get; set; }
}