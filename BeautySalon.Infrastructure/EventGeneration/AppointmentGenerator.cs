using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Enums;
using BeautySalon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EventGeneration;

public class AppointmentGenerator
{
    private readonly AppDbContext _db;
    private static readonly Random _random = new();
    private static readonly string[] _services = { "Стрижка", "Маникюр", "Педикюр", "Окрашивание", "Укладка" };
    private static readonly AppointmentStatus[] _statuses = { AppointmentStatus.Created, AppointmentStatus.Completed, AppointmentStatus.Cancelled };

    public AppointmentGenerator(AppDbContext db)
    {
        _db = db;
    }

    public async Task<int> GenerateAsync(int count, CancellationToken ct)
    {
        var userIds = await _db.Users.Select(x => x.Id).ToListAsync(ct);
        if (userIds.Count == 0) return 0;

        var appointments = new List<Appointment>();
        for (int i = 0; i < count; i++)
        {
            appointments.Add(new Appointment
            {
                UserId = userIds[_random.Next(userIds.Count)],
                ServiceName = _services[_random.Next(_services.Length)],
                Status = _statuses[_random.Next(_statuses.Length)],
                CreatedAt = DateTime.UtcNow.AddMinutes(-_random.Next(0, 10080))
            });
        }

        await _db.Appointments.AddRangeAsync(appointments, ct);
        return await _db.SaveChangesAsync(ct);
    }
}