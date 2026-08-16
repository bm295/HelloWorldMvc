using MilkCoPOS.Application.Models;
using MilkCoPOS.Application.Ports;

namespace MilkCoPOS.Application.Services;

public class ReportingUseCaseService(IReportingRepositoryPort reportingRepository) : IReportingUseCaseService
{
    public Task<OperationsReport> GetOperationsSummaryAsync() =>
        reportingRepository.GetOperationsSummaryAsync(DateTime.UtcNow);
}
