using MilkCoPOS.Domain.Entities;
using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public interface IOrderUseCaseService
{
    Task<List<Order>> GetOrdersAsync();
    Task<Order?> GetOrderAsync(int orderId);
    Task<(bool Success, string? Error, Order? Order)> CreateOrderAsync(CreateOrderRequest request);
    Task<(bool Success, string? Error)> AddItemAsync(int orderId, AddOrderItemRequest request);
    Task<(bool Success, string? Error)> RemoveItemAsync(int orderId, int orderItemId);
    Task<(bool Success, string? Error)> SendToKitchenAsync(int orderId);
    Task<(bool Success, string? Error)> CloseOrderAsync(int orderId);
}
