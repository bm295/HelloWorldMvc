using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Ports;

public interface IPaymentRepositoryPort
{
    Task<List<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(int id);
    Task<Payment> AddAsync(Payment payment);
}
