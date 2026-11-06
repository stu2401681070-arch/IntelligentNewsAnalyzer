using IntelligentNewsAnalyzer.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentNewsAnalyzer.Controllers;

public class StatisticsController : Controller
{
    private readonly StatisticsService _statistics;

    public StatisticsController(StatisticsService statistics)
    {
        _statistics = statistics;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await _statistics.BuildAsync(cancellationToken));
    }
}
