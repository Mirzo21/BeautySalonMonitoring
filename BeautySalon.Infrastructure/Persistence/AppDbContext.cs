using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BeautySalon.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Индексы
        modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<AppUser>().HasIndex(x => x.UserName).IsUnique();

        // Ограничения
        modelBuilder.Entity<Appointment>()
            .Property(x => x.ServiceName)
            .HasMaxLength(100);

        // Связи
        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.User)
            .WithMany(x => x.Appointments)
            .HasForeignKey(x => x.UserId);

        // Начальные данные (роли)
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Analyst" },
            new Role { Id = 3, Name = "Operator" }
        );

        // Начальные данные (пользователи)
        modelBuilder.Entity<AppUser>().HasData(
            new AppUser { Id = 1, UserName = "admin", PasswordHash = "Admin123!", RoleId = 1, IsActive = true },
            new AppUser { Id = 2, UserName = "analyst", PasswordHash = "Analyst123!", RoleId = 2, IsActive = true },
            new AppUser { Id = 3, UserName = "operator", PasswordHash = "Operator123!", RoleId = 3, IsActive = true }
        );
    }
}