using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Models.Account;

public record AccountIndexViewModel
{
    [TempData]
    public string StatusMessage { get; set; }
    public string Username { get; set; }
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }
}