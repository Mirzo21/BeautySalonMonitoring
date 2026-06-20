using BeautySalon.Application.Analytics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

[ApiController]
[Route("api/analytics")]
[Authorize(Roles = "Admin,Analyst")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<AnalyticsSummaryDto>> GetSummary(CancellationToken ct)
    {
        var result = await _analyticsService.GetSummaryAsync(ct);
        return Ok(result);
    }
}