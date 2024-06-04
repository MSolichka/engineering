using System.Security.Claims;
using EShop.Application.Basket;
using EShop.Application.Order;
using EShop.Domain.Enums;
using EShop.Presentation.Models;
using EShop.Presentation.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Presentation.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IBasketService _basketService;

    public OrderController(IOrderService orderService, IBasketService basketService)
    {
        _orderService = orderService;
        _basketService = basketService;
    }
    public async Task<IActionResult> Index()
    {
        var roleOfUser = User.FindFirst(ClaimTypes.Role)?.Value;
        List<GetAllOrdersDto> orders;
        if (roleOfUser == "Admin")
        {
            orders = await _orderService.GetAllOrdersAsync();
        }
        else
        {
            var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            orders = await _orderService.GetOrdersByUserIdAsync(Guid.Parse(userId));
        }
        var orderViewModels = orders.ConvertAll(order => new OrderListViewModel
        {
            Id = order.Id,
            OrderTime = order.OrderTime,
            Address = order.Address,
            TotalPrice = order.TotalPrice,
            TotalItemsCount = order.TotalItemCount,
            OrderStatus = order.Status
        });
        return View(orderViewModels);
    }
    public async Task<IActionResult> ProceedToCheckout(Guid basketId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return RedirectToAction("Login", "Auth");
        }
        var basket = await _basketService.GetUserBasket(Guid.Parse(userId));
        var orderViewModel = new ProceedOrderViewModel
        {
            BasketId = basket.Id,
            UserId = Guid.Parse(userId),
            TotalPrice = basket.TotalPrice,
            Address = ""
        };
        return View(orderViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Guid basketId, Guid userId, decimal totalPrice, string address)
    {
        var createOrderDto = new CreateOrderDto
        {
            BasketId = basketId,
            UserId = userId,
            Address = address
        };
        await _orderService.CreateOrderAsync(createOrderDto);
        await _basketService.ProceedBasket(basketId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Confirm(Guid orderId)
    {
        await _orderService.ChangeOrderStatus(orderId, Status.Confirmed);
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Cancel(Guid orderId)
    {
        await _orderService.ChangeOrderStatus(orderId, Status.Canceled);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ChangeAddress(Guid orderid)
    {
        var order = await _orderService.GetOrderByIdAsync(orderid);
        var orderViewModel = new ChangeAddressViewModel
        {
            OrderId = order.Id,
            Address = order.Address
        };
        return View(orderViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> ChangeAddress(ChangeAddressViewModel model)
    {
        await _orderService.ChangeAddress(model.OrderId, model.Address);
        return RedirectToAction(nameof(Index));
    }
}