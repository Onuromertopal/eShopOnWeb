using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.ViewModels;

namespace BlazorAdmin.Interfaces;

public interface IOrderViewModelService
{
    Task<bool> ApproveOrderAsync(int orderId);
    Task<List<OrderViewModel>> List();
    Task<OrderDetailViewModel> GetDetails(int orderId);
}
