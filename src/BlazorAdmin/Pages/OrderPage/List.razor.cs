using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorAdmin.Interfaces;
using BlazorAdmin.ViewModels;

namespace BlazorAdmin.Pages.OrderPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderViewModelService _orderViewModelService { get; set; }

    private List<OrderViewModel> orders = new List<OrderViewModel>();

    private Details DetailsComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            orders = await _orderViewModelService.List();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async void DetailsClick(int id)
    {
        await DetailsComponent.Open(id);
    }

    private async Task ReloadOrders()
    {
        orders = await _orderViewModelService.List();
        StateHasChanged();
    }
}
