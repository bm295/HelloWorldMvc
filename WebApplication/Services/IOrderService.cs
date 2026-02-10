using System.Threading.Tasks;
using MilkCoPOS.Models;

namespace MilkCoPOS.Services
{
    public interface IOrderService
    {
        Task<(bool Success, string Error, Order Order)> CreateOrderAsync(CreateOrderRequest request);
    }
}
