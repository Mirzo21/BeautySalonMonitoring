using BeautySalon.Infrastructure.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize(Roles = "Admin,Analyst")]
public class ReportsController : ControllerBase
{
    private readonly ReportsService _reportsService;

    public ReportsController(ReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    [HttpGet("events.csv")]
    public async Task<IActionResult> GetEventsCsv(CancellationToken ct)
    {
        var bytes = await _reportsService.BuildEventsCsvAsync(ct);
        return File(bytes, "text/csv; charset=utf-8", "appointments.csv");
    }
}