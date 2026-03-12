using MilkCoPOS.Domain.Entities;
using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public interface IPaymentUseCaseService
{
    Task<List<Payment>> GetPaymentsAsync();
    Task<Payment?> GetPaymentAsync(int id);
    Task<(bool Success, string? Error, Payment? Payment)> ProcessPaymentAsync(int orderId, ProcessPaymentRequest request);
}
