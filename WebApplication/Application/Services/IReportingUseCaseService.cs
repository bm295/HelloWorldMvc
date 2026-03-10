using MilkCoPOS.Application.Models;

namespace MilkCoPOS.Application.Services;

public interface IReportingUseCaseService
{
    Task<OperationsReport> GetOperationsSummaryAsync();
}
