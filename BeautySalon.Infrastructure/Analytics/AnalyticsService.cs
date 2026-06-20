using BeautySalon.Application.Analytics;
using BeautySalon.Domain.Enums;
using BeautySalon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.Analytics;

public class AnalyticsService : IAnalyticsService
{
    private readonly AppDbContext _db;

    public AnalyticsService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<AnalyticsSummaryDto> GetSummaryAsync(CancellationToken ct)
    {
        var appointments = _db.Appointments.AsQueryable();

        var total = await appointments.CountAsync(ct);
        var completed = await appointments.CountAsync(x => x.Status == AppointmentStatus.Completed, ct);
        var cancelled = await appointments.CountAsync(x => x.Status == AppointmentStatus.Cancelled, ct);
        var uniqueUsers = await appointments.Select(x => x.UserId).Distinct().CountAsync(ct);

        // Группировка с анонимным типом, потом преобразование
        var popularServicesRaw = await appointments
            .GroupBy(x => x.ServiceName)
            .Select(g => new { ServiceName = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToListAsync(ct);

        var popularDtos = popularServicesRaw
            .Select(x => new ServicePopularityDto(x.ServiceName, x.Count))
            .ToList();

        return new AnalyticsSummaryDto(total, uniqueUsers, completed, cancelled, popularDtos);
    }
}