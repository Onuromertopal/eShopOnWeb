using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.Admin.Orders;

public class GetOrdersHandler : IRequestHandler<GetOrders, IEnumerable<OrderViewModel>>
{
    private readonly IReadRepository<Order> _orderRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetOrdersHandler(IReadRepository<Order> orderRepository, UserManager<ApplicationUser> userManager)
    {
        _orderRepository = orderRepository;
        _userManager = userManager;
    }

    public async Task<IEnumerable<OrderViewModel>> Handle(GetOrders request, CancellationToken cancellationToken)
    {
        var specification = new OrdersWithItemsSpecification();
        var orders = await _orderRepository.ListAsync(specification, cancellationToken);

        var orderViewModels = new List<OrderViewModel>();

        foreach (var o in orders)
        {
            var buyer = await _userManager.FindByEmailAsync(o.BuyerId);
            var orderViewModel = new OrderViewModel
            {
                OrderDate = o.OrderDate,
                OrderNumber = o.Id,
                Total = o.Total(),
                Buyer = buyer.UserName,
                Status = o.OrderStatus
            };
            orderViewModels.Add(orderViewModel);
        }

        return orderViewModels;
    }
}
