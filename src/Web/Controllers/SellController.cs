using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Features.Admin.Orders;
using Microsoft.eShopWeb.Web.Features.OrderDetails;

namespace Microsoft.eShopWeb.Web.Controllers;
[Route("[controller]")]
//[Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS)]
[ApiController]
public class SellController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderService _orderService;

    public SellController(IMediator mediator, IOrderService orderService)
    {
        _mediator = mediator;
        _orderService = orderService;
    }

    [HttpGet]
    [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _mediator.Send(new GetOrders());

        return Ok(orders);
    }

    [HttpPost("approve/{orderId}")]
    [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> ApproveOrder(int orderId)
    {
        var result = await _orderService.ApproveOrderAsync(orderId);

        return Ok(result);
    }


    [HttpGet("{orderId}")]
    public async Task<IActionResult> Detail(int orderId)
    {
        Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
        var orderDetails = await _mediator.Send(new GetOrderDetails(User.Identity.Name, orderId));

        if (orderDetails == null)
        {
            return BadRequest("No such order detail found.");
        }

        return Ok(orderDetails);
    }
}
