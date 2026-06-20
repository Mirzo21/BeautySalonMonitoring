namespace BeautySalon.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<AppUser> Users { get; set; } = new();
}