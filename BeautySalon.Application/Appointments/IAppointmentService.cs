namespace BeautySalon.Application.Appointments;

public interface IAppointmentService
{
    Task<AppointmentDto> CreateAsync(CreateAppointmentRequest request, int userId, CancellationToken ct);
    Task<IReadOnlyList<AppointmentDto >>GetAsync(AppointmentFilter filter, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}