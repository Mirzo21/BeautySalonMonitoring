using BeautySalon.Domain.Entities;
using BeautySalon.Application.Appointments;

namespace BeautySalon.Application.Appointments;

public interface IAppointmentRepository
{
    Task<Appointment> AddAsync(Appointment entity, CancellationToken ct);
    Task<IReadOnlyList<Appointment> >GetAsync(AppointmentFilter filter, CancellationToken ct);
    Task<Appointment?> GetByIdAsync(int id, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}