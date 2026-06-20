using BeautySalon.Application.Appointments;
using Microsoft.AspNetCore.Authorization;   // ← ДОБАВИТЬ
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/appointments")]
[Authorize]  
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentsController(IAppointmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto> >>Get([FromQuery] AppointmentFilter filter, CancellationToken ct)
    {
        var result = await _service.GetAsync(filter, ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentDto >>Create(CreateAppointmentRequest request, CancellationToken ct)
    {
        var userId = 1;
        var result = await _service.CreateAsync(request, userId, ct);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}