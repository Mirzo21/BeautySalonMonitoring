using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace BeautySalon.Application.Appointments;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(IAppointmentRepository repository, ILogger<AppointmentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<AppointmentDto> CreateAsync(CreateAppointmentRequest request, int userId, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.ServiceName))
            throw new ArgumentException("Название услуги обязательно.");

        var entity = new Appointment
        {
            UserId = userId,
            ServiceName = request.ServiceName.Trim(),
            Status = AppointmentStatus.Created,   // ← было CreatedAt, исправил
            CreatedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Создана запись на услугу {ServiceName} для пользователя {UserId}", entity.ServiceName, userId);

        var saved = await _repository.AddAsync(entity, ct);
        return ToDto(saved);
    }

    public async Task<IReadOnlyList<AppointmentDto >>GetAsync(AppointmentFilter filter, CancellationToken ct)
    {
        var appointments = await _repository.GetAsync(filter, ct);
        return appointments.Select(ToDto).ToList();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        return await _repository.DeleteAsync(id, ct);
    }

    private static AppointmentDto ToDto(Appointment entity)
    {
        return new AppointmentDto(
            entity.Id,
            entity.UserId,
            entity.User?.UserName ?? "unknown",
            entity.ServiceName,
            entity.Status.ToString(),
            entity.CreatedAt
        );
    }
}