using BeautySalon.Application.Auth;
using BeautySalon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly JwtTokenFactory _tokenFactory;

    public AuthService(AppDbContext db, JwtTokenFactory tokenFactory)
    {
        _db = db;
        _tokenFactory = tokenFactory;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        var user = await _db.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.UserName == request.UserName && x.IsActive, ct);

        if (user is null) return null;
        if (user.PasswordHash != request.Password) return null;

        var token = _tokenFactory.Create(user);
        return new LoginResponse(token, user.UserName, user.Role.Name);
    }
}