namespace BeautySalon.Application.Analytics;

public record AnalyticsSummaryDto(
    int TotalAppointments,
    int UniqueUsers,
    int Completed,
    int Cancelled,
    IReadOnlyList<ServicePopularityDto> PopularServices
);

public record ServicePopularityDto(string ServiceName, int Count);