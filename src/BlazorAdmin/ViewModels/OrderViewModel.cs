using System;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate.Enums;

namespace BlazorAdmin.ViewModels;

public class OrderViewModel
{
    public int OrderNumber { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
    public string Buyer { get; set; }
}
