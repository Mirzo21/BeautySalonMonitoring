using BeautySalon.Domain.Enums;

namespace BeautySalon.Domain.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public List<Appointment> Appointments { get; set; } = new();
}