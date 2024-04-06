using System.Collections.Generic;

namespace BlazorAdmin.ViewModels;

public class OrderDetailViewModel : OrderViewModel
{
    public List<OrderItemViewModel> OrderItems { get; set; } = new();
}
