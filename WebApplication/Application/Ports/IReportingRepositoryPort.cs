using MilkCoPOS.Application.Models;

namespace MilkCoPOS.Application.Ports;

public interface IReportingRepositoryPort
{
    Task<OperationsReport> GetOperationsSummaryAsync(DateTime utcNow);
}
