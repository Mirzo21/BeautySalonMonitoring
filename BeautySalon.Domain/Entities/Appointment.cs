using BeautySalon.Domain.Enums;

namespace BeautySalon.Domain.Entities;

public class Appointment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public string ServiceName { get; set; } = string.Empty;  // Название услуги
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Created;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}