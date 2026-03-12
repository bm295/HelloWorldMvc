using MilkCoPOS.Application.Ports;
using MilkCoPOS.Domain.Entities;
using MilkCoPOS.Domain.Enums;
using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public class PaymentUseCaseService(
    IPaymentRepositoryPort paymentRepository,
    IOrderRepositoryPort orderRepository) : IPaymentUseCaseService
{
    public Task<List<Payment>> GetPaymentsAsync() => paymentRepository.GetAllAsync();

    public Task<Payment?> GetPaymentAsync(int id) => paymentRepository.GetByIdAsync(id);

    public async Task<(bool Success, string? Error, Payment? Payment)> ProcessPaymentAsync(int orderId, ProcessPaymentRequest request)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return (false, "Order not found.", null);
        }

        if (order.Status == OrderStatus.Closed)
        {
            return (false, "Closed order cannot receive payment.", null);
        }

        var payment = await paymentRepository.AddAsync(new Payment
        {
            OrderId = orderId,
            Amount = request.Amount,
            Method = request.Method,
            Status = "Captured",
            ProcessedAtUtc = DateTime.UtcNow
        });

        order.Status = OrderStatus.Paid;
        await orderRepository.SaveChangesAsync();

        return (true, null, payment);
    }
}
