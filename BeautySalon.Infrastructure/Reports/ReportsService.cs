using BeautySalon.Application.Reports;
using BeautySalon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.Reports;

public class ReportsService
{
    private readonly AppDbContext _db;
    private readonly CsvReportGenerator _csv;

    public ReportsService(AppDbContext db, CsvReportGenerator csv)
    {
        _db = db;
        _csv = csv;
    }

    public async Task<byte[]> BuildEventsCsvAsync(CancellationToken ct)
    {
        var rows = await _db.Appointments
            .AsNoTracking()
            .Include(x => x.User)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new AppointmentReportRow(
                x.Id,
                x.User.UserName,
                x.ServiceName,
                x.Status.ToString(),
                x.CreatedAt
            ))
            .ToListAsync(ct);

        return _csv.Generate(rows);
    }
}