using BeautySalon.Application.Auth;

namespace BeautySalon.Application.Auth;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct);
}