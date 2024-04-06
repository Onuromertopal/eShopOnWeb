using MediatR;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.Admin.Orders;

public class GetOrders : IRequest<IEnumerable<OrderViewModel>>
{
}
