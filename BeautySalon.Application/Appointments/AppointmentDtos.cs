namespace BeautySalon.Application.Appointments;

public record CreateAppointmentRequest(
    string ServiceName
);

public record AppointmentDto(
    int Id,
    int UserId,
    string UserName,
    string ServiceName,
    string Status,
    DateTime CreatedAt
);

public class AppointmentFilter
{
    public int? UserId { get; set; }
    public string? ServiceName { get; set; }
    public string? Status { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}