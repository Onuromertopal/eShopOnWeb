using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BlazorAdmin.Interfaces;
using BlazorAdmin.ViewModels;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class OrderViewModelService : IOrderViewModelService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OrderViewModelService> _logger;
    public OrderViewModelService(HttpClient httpClient, ILogger<OrderViewModelService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    
    public async Task<bool> ApproveOrderAsync(int orderId)
    {
        try
        {
            _logger.LogInformation("Approve order from web api.");
            var result = await _httpClient.PostAsJsonAsync($"Sell/approve/{orderId}", false);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "Error Approve order from web api.");
            return false;
        }
    }

    public async Task<OrderDetailViewModel> GetDetails(int orderId)
    {
        try
        {
            _logger.LogInformation("Fetching order details from web api.");
            var orders = await _httpClient.GetFromJsonAsync<OrderDetailViewModel>($"Sell/{orderId}");
            return orders;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "Error fetching order details from web api.");
            return new OrderDetailViewModel();
        }
    }

    public async Task<List<OrderViewModel>> List()
    {
        try
        {
            _logger.LogInformation("Fetching orders from web api.");
            var orders = await _httpClient.GetFromJsonAsync<List<OrderViewModel>>("Sell");
            return orders;
        }
        catch (Exception exc)
        {
            _logger.LogError(exc, "Error fetching orders from web api.");
            return new List<OrderViewModel>();
        }
        
    }
}
