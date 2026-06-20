using BeautySalon.Application.Analytics;

namespace BeautySalon.Application.Analytics;

public interface IAnalyticsService
{
    Task<AnalyticsSummaryDto> GetSummaryAsync(CancellationToken ct);
}