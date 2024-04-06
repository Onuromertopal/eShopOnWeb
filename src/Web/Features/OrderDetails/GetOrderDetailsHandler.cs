using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.OrderDetails;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetails, OrderDetailViewModel?>
{
    private readonly IReadRepository<Order> _orderRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    public GetOrderDetailsHandler(IReadRepository<Order> orderRepository, UserManager<ApplicationUser> userManager)
    {
        _orderRepository = orderRepository;
        _userManager = userManager;
    }

    public async Task<OrderDetailViewModel?> Handle(GetOrderDetails request,
        CancellationToken cancellationToken)
    {
        var spec = new OrderWithItemsByIdSpec(request.OrderId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec, cancellationToken);

        if (order == null)
        {
            return null;
        }
        var buyer = await _userManager.FindByEmailAsync(order.BuyerId);
        return new OrderDetailViewModel
        {
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
            {
                PictureUrl = oi.ItemOrdered.PictureUri,
                ProductId = oi.ItemOrdered.CatalogItemId,
                ProductName = oi.ItemOrdered.ProductName,
                UnitPrice = oi.UnitPrice,
                Units = oi.Units
            }).ToList(),
            OrderNumber = order.Id,
            ShippingAddress = order.ShipToAddress,
            Status = order.OrderStatus,
            Buyer = buyer.UserName,
            Total = order.Total()
        };
    }
}
