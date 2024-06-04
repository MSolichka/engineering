using System.Diagnostics;
using EShop.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HandleErrorCode(int statusCode)
    {
        var viewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, StatusCode = statusCode };

        switch (statusCode)
        {
            case 404:
                ViewBag.ErrorMessage = "Sorry, the page you requested could not be found.";
                break;
            case 500:
                ViewBag.ErrorMessage = "Sorry, something went wrong on our end. Please try again later.";
                break;
            default:
                ViewBag.ErrorMessage = "Sorry, an error occurred while processing your request.";
                break;
        }

        return View("Error", viewModel);
    }
}