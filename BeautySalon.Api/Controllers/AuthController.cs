using BeautySalon.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse >>Login(LoginRequest request, CancellationToken ct)
    {
        var response = await _authService.LoginAsync(request, ct);
        return response is null ? Unauthorized() : Ok(response);
    }
}