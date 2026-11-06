using System.Diagnostics;
using IntelligentNewsAnalyzer.Data;
using IntelligentNewsAnalyzer.Models;
using IntelligentNewsAnalyzer.Services;
using IntelligentNewsAnalyzer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelligentNewsAnalyzer.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly INewsApiService _newsApi;
    private readonly IAiLanguageService _ai;

    public HomeController(AppDbContext db, INewsApiService newsApi, IAiLanguageService ai)
    {
        _db = db;
        _newsApi = newsApi;
        _ai = ai;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = new HomeDashboardViewModel
        {
            ArticleCount = await _db.Articles.CountAsync(cancellationToken),
            AnalysisCount = await _db.Analyses.CountAsync(cancellationToken),
            SourceCount = await _db.Articles.Select(a => a.SourceName).Distinct().CountAsync(cancellationToken),
            NewsApiConfigured = _newsApi.IsConfigured,
            AiConfigured = _ai.IsConfigured,
            LatestArticles = await _db.Articles.AsNoTracking()
                .OrderByDescending(a => a.PublishedAt ?? a.FetchedAt)
                .Take(6)
                .ToListAsync(cancellationToken),
            LatestAnalyses = await _db.Analyses.AsNoTracking()
                .OrderByDescending(a => a.CreatedAt)
                .Take(3)
                .ToListAsync(cancellationToken)
        };

        return View(model);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
