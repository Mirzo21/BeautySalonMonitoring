using BeautySalon.Infrastructure.EventGeneration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/generate")]
[Authorize(Roles = "Admin")]
public class GenerateController : ControllerBase
{
    private readonly AppointmentGenerator _generator;

    public GenerateController(AppointmentGenerator generator)
    {
        _generator = generator;
    }

    [HttpPost("events")]
    public async Task<IActionResult> GenerateEvents(int count = 500, CancellationToken ct = default)
    {
        var safeCount = Math.Clamp(count, 1, 10000);
        var created = await _generator.GenerateAsync(safeCount, ct);
        return Ok(new { generated = created });
    }
}