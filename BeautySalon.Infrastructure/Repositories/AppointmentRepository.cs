using BeautySalon.Application.Appointments;
using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _db;

    public AppointmentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Appointment> AddAsync(Appointment entity, CancellationToken ct)
    {
        _db.Appointments.Add(entity);
        await _db.SaveChangesAsync(ct);
        await _db.Entry(entity).Reference(x => x.User).LoadAsync(ct);
        return entity;
    }

    public async Task<IReadOnlyList<Appointment >>GetAsync(AppointmentFilter filter, CancellationToken ct)
    {
        var query = _db.Appointments
            .AsNoTracking()
            .Include(x => x.User)
            .AsQueryable();

        if (filter.UserId.HasValue)
            query = query.Where(x => x.UserId == filter.UserId.Value);
        if (!string.IsNullOrWhiteSpace(filter.ServiceName))
            query = query.Where(x => x.ServiceName == filter.ServiceName);
        if (!string.IsNullOrWhiteSpace(filter.Status))
            query = query.Where(x => x.Status.ToString() == filter.Status);
        if (filter.From.HasValue)
            query = query.Where(x => x.CreatedAt >= filter.From.Value);
        if (filter.To.HasValue)
            query = query.Where(x => x.CreatedAt <= filter.To.Value);

        var page = Math.Max(filter.Page, 1);
        var pageSize = Math.Clamp(filter.PageSize, 1, 100);

        return await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public async Task<Appointment?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _db.Appointments
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Appointments.FindAsync([id], ct);
        if (entity is null) return false;
        _db.Appointments.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}